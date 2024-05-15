using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using BearsEngine.OpenGL;

namespace BearsEngine.Graphics;

/// <summary>
/// Valid Samples values for <see cref="OpenGL32.glTexImage2DMultisample"/>
/// </summary>
public enum MSAA_SAMPLES
{
    Disabled = 0,
    X2 = 2,
    X4 = 4,
    X8 = 8,
    X16 = 16
}

public static class OpenGLHelper
{
    private static int s_lastBoundTexture;

    internal static int LastBoundFrameBuffer { get; set; }

    internal static int LastBoundShader { get; set; }

    internal static int LastBoundVertexBuffer { get; set; }

    internal static Matrix3 OrthoMatrix { get; set; }

    internal static Dictionary<string, Texture> TextureDictionary { get; set; } = new();

    private static int CompileShader(int programID, SHADER_TYPE shaderType, string shaderSource)
    {
        int shaderID = OpenGL32.glCreateShader(shaderType);
        ShaderSource(shaderID, shaderSource);
        OpenGL32.glCompileShader(shaderID);
        OpenGL32.glAttachShader(programID, shaderID);

        //Check for errors in compiling shader
        var log = GetShaderInfoLog(shaderID);

        if (log.Length > 0)
            Log.Error("Shader compilation error: " + log);

        return shaderID;
    }

    private static ISpriteTexture GenPaddedTexture(string path, int spriteRows, int spriteColumns, int padding, TEXPARAMETER_VALUE minFilter = TEXPARAMETER_VALUE.GL_NEAREST, TEXPARAMETER_VALUE magFilter = TEXPARAMETER_VALUE.GL_NEAREST, TEXPARAMETER_VALUE wrapMode = TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentException(path);
        if (!File.Exists(path))
            throw new FileNotFoundException(path);

        var textureID = GenTexture();

        if (s_lastBoundTexture != textureID)
        {
            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, textureID);
            s_lastBoundTexture = textureID;
        }

        var BMP = new System.Drawing.Bitmap(path);

        //Snip individual bmps out for each sprite cell
        int w = BMP.Width / spriteColumns;
        int h = BMP.Height / spriteRows;
        int newW = BMP.Width + (spriteColumns + 1) * padding;
        int newH = BMP.Height + (spriteRows + 1) * padding;

        var paddedBMP = new System.Drawing.Bitmap(newW, newH);
        using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(paddedBMP))
        {
            //set background color
            g.Clear(System.Drawing.Color.Transparent);
            for (int i = 0; i < spriteColumns; i++)
                for (int j = 0; j < spriteRows; j++)
                {
                    g.DrawImage(BMP.Clone(new System.Drawing.Rectangle(i * w, j * h, w, h), System.Drawing.Imaging.PixelFormat.Format32bppArgb), new System.Drawing.Rectangle(padding + i * (w + padding), padding + j * (h + padding), w, h));
                }
        }

        paddedBMP = BitmapTools.PremultiplyAlpha(paddedBMP);

        var bmpData = paddedBMP.LockBits(new System.Drawing.Rectangle(0, 0, paddedBMP.Width, paddedBMP.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        OpenGL32.glTexImage2D(TEXTURE_TARGET.GL_TEXTURE_2D, 0, TEXTURE_INTERNALFORMAT.GL_RGBA, bmpData.Width, bmpData.Height, 0, PIXEL_FORMAT.GL_BGRA, PIXEL_TYPE.GL_UNSIGNED_BYTE, bmpData.Scan0);

        paddedBMP.UnlockBits(bmpData);

        //Apply interpolation filters for scaled images - normally linear for smoothing, nearest for pixel graphics
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, minFilter);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, magFilter);

        //I had the 3rd parameter in these set to repeat, but it gave the artefact of the top of textures being drawn across the bottom eg line across Haighman's feet. Will need to do %1f modulus on uv coords now..
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_S, wrapMode);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_T, wrapMode);

        BMP.Dispose();
        paddedBMP.Dispose();

        return new SpriteTexture(textureID, newW, newH, spriteColumns, spriteRows, padding);
    }

    private static Texture GenTexture(string path, TEXPARAMETER_VALUE minMagFilter = TEXPARAMETER_VALUE.GL_NEAREST) => GenTexture(new System.Drawing.Bitmap(path), minMagFilter);

    public static void BindShader(int id)
    {
        OpenGL32.glUseProgram(id);
        LastBoundShader = id;
    }

    public static void UnbindShader()
    {
        OpenGL32.glUseProgram(0);
        LastBoundShader = 0;
    }

    public static int CreateShader(string vertexSource, string? geometrySource, string? fragmentSource)
    {
        int programID = OpenGL32.glCreateProgram();

        int vs, gs = 0, fs = 0;
        vs = CompileShader(programID, SHADER_TYPE.GL_VERTEX_SHADER, vertexSource);
        if (geometrySource != null)
            gs = CompileShader(programID, SHADER_TYPE.GL_GEOMETRY_SHADER, geometrySource);
        if (fragmentSource != null)
            fs = CompileShader(programID, SHADER_TYPE.GL_FRAGMENT_SHADER, fragmentSource);

        OpenGL32.glLinkProgram(programID);

        //Check for errors in compiling shader
        var log = GetProgramInfoLog(programID);
        if (log.Length > 0)
        {
            Log.Error("Shader compilation error: " + log);
        }

        //Cleanup
        OpenGL32.glDetachShader(programID, vs);
        OpenGL32.glDeleteShader(vs);
        if (geometrySource != null)
        {
            OpenGL32.glDetachShader(programID, gs);
            OpenGL32.glDeleteShader(gs);
        }
        if (fragmentSource != null)
        {
            OpenGL32.glDetachShader(programID, fs);
            OpenGL32.glDeleteShader(fs);
        }

        //Assign this shader to be the selected one in OpenGL
        OpenGL32.glUseProgram(programID);

        return programID;
    }

    public static int CreateShader(byte[] vertexSource, byte[] fragmentSource) => CreateShader(Encoding.UTF8.GetString(vertexSource), Encoding.UTF8.GetString(fragmentSource));

    public static int CreateShader(string vertexSource, string fragmentSource) => CreateShader(vertexSource, null, fragmentSource);

    public static int CreateShader(byte[] vertexSource, byte[] geometrySource, byte[] fragmentSource)
    {
        return CreateShader(
                Encoding.UTF8.GetString(vertexSource),
                Encoding.UTF8.GetString(geometrySource),
                Encoding.UTF8.GetString(fragmentSource));
    }

    public static void DeleteShader(int id)
    {
        OpenGL32.glUseProgram(0);
        OpenGL32.glDeleteProgram(id);
    }

    public static void CreateFramebuffer(int width, int height, out int framebufferID, out Texture framebufferTexture)
    {
        framebufferID = OpenGLHelper.GenFramebuffer();
        framebufferTexture = new Texture(OpenGLHelper.GenTexture(), width, height);

        OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, framebufferTexture.ID);
        OpenGL32.glTexStorage2D(TEXTURE_TARGET.GL_TEXTURE_2D, 1, TEXTURE_INTERNALFORMAT.GL_RGBA8, width, height);

        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_S, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_T, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
    }

    public static void ResizeFramebuffer(ref Texture framebufferTexture, int newW, int newH)
    {
        OpenGL32.glDeleteTextures(1, new int[1] { framebufferTexture.ID });
        framebufferTexture = new Texture(OpenGLHelper.GenTexture(), newW, newH);

        OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, framebufferTexture.ID);
        OpenGL32.glTexStorage2D(TEXTURE_TARGET.GL_TEXTURE_2D, 1, TEXTURE_INTERNALFORMAT.GL_RGBA8, newW, newH);

        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_S, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_T, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
    }

    public static void CreateMSAAFramebuffer(int width, int height, MSAA_SAMPLES samples, out int framebufferID, out Texture framebufferTexture)
    {
        //Generate FBO and texture to use with the MSAA antialising pass
        framebufferTexture = new Texture(OpenGLHelper.GenTexture(), width, height);

        OpenGL32.glBindTexture(TEXTURE_TARGET.GL_PROXY_TEXTURE_2D_MULTISAMPLE, framebufferTexture.ID);
        OpenGL32.glTexImage2DMultisample(TEXTURE_TARGET.GL_TEXTURE_2D_MULTISAMPLE, (int)samples, TEXTURE_INTERNALFORMAT.GL_RGB8, width, height, false);

        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_S, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_T, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);

        framebufferID = OpenGLHelper.GenFramebuffer();
    }

    public static void ResizeMSAAFramebuffer(ref Texture framebufferTexture, int newW, int newH, MSAA_SAMPLES newSamples)
    {
        OpenGL32.glDeleteTextures(1, new int[1] { framebufferTexture.ID });
        framebufferTexture = new Texture(GenTexture(), newW, newH);

        OpenGL32.glBindTexture(TEXTURE_TARGET.GL_PROXY_TEXTURE_2D_MULTISAMPLE, framebufferTexture.ID);
        OpenGL32.glTexImage2DMultisample(TEXTURE_TARGET.GL_TEXTURE_2D_MULTISAMPLE, (int)newSamples, TEXTURE_INTERNALFORMAT.GL_RGB8, newW, newH, false);

        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_S, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_T, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
    }

    public static Texture GenTexture(System.Drawing.Bitmap bmp, TEXPARAMETER_VALUE minMagFilter = TEXPARAMETER_VALUE.GL_NEAREST)
    {
        bmp = BitmapTools.PremultiplyAlpha(bmp);

        Texture t = new()
        {
            Width = bmp.Width,
            Height = bmp.Height,
            ID = GenTexture()
        };

        OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, t.ID);
        s_lastBoundTexture = t.ID;

        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, minMagFilter);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, minMagFilter);

        System.Drawing.Imaging.BitmapData bmpd = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        OpenGL32.glTexImage2D(TEXTURE_TARGET.GL_TEXTURE_2D, 0, TEXTURE_INTERNALFORMAT.GL_RGBA, bmpd.Width, bmpd.Height, 0, PIXEL_FORMAT.GL_BGRA, PIXEL_TYPE.GL_UNSIGNED_BYTE, bmpd.Scan0);
        bmp.UnlockBits(bmpd);

        return t;
    }

    public static Texture GenTexture(Colour[,] pixels, TEXPARAMETER_VALUE minMagFilter = TEXPARAMETER_VALUE.GL_NEAREST)
    {
        pixels = pixels.Transpose();
        Texture t = new(GenTexture(), pixels.GetLength(1), pixels.GetLength(0));

        OpenGL32.glPixelStorei(PIXEL_STORE_MODE.GL_UNPACK_ALIGNMENT, 1);

        if (s_lastBoundTexture != t.ID)
        {
            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, t.ID);
            s_lastBoundTexture = t.ID;
        }

        GCHandle pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
        IntPtr pointer = pinned.AddrOfPinnedObject();

        OpenGL32.glTexImage2D(TEXTURE_TARGET.GL_TEXTURE_2D, 0, TEXTURE_INTERNALFORMAT.GL_RGBA, t.Width, t.Height, 0, PIXEL_FORMAT.GL_RGBA, PIXEL_TYPE.GL_UNSIGNED_BYTE, pointer);

        pinned.Free();

        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, minMagFilter);
        OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, minMagFilter);

        return t;
    }

    public static Texture GenRectangle(int width, int height, int borderWidth, Colour outside, Colour inside)
    {
        Colour[,] pixels = new Colour[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                if (i - borderWidth < 0 || i + borderWidth >= width || j - borderWidth < 0 || j + borderWidth >= height)
                    pixels[i, j] = outside;
                else
                    pixels[i, j] = inside;
            }

        return GenTexture(pixels);
    }

    public static Polygon GenTrianglePolygon(Rect boundingRect, int border, Direction direction, Colour colour)
    {
        Rect r = boundingRect;

        return direction switch
        {
            Direction.Up => new Polygon(colour, new Point(r.W / 2, border), new Point(r.W - border, r.W - border), new Point(border, r.W - border)),
            Direction.Right => new Polygon(colour, new Point(r.H - border, r.H / 2), new Point(border, r.H - border), new Point(border, border)),
            Direction.Down => new Polygon(colour, new Point(border, border), new Point(r.W - border, border), new Point(r.W / 2, r.W - border)),
            Direction.Left => new Polygon(colour, new Point(border, r.H / 2), new Point(r.H - border, r.H - border), new Point(r.H - border, border)),
            _ => throw new ArgumentException($"Unexpected Direction {direction} passed to HF.Graphics.GenTrianglePolygon"),
        };
    }

    /// <summary>
    /// If TextureDictionary contains the image, get it, otherwise create it and add it to the dictionary, then get it
    /// </summary>
    public static Texture LoadTexture(string path, TEXPARAMETER_VALUE minMagFilter = TEXPARAMETER_VALUE.GL_NEAREST)
    {
        if (TextureDictionary.ContainsKey(path))
            return TextureDictionary[path];

        Ensure.FileExists(path);

        Texture t = GenTexture(path, minMagFilter);
        TextureDictionary.Add(path, t);

        return t;
    }

    /// <summary>
    /// Provide a bitmap and its string name manually
    /// </summary>
    internal static Texture LoadTexture(System.Drawing.Bitmap bufferedImage, string textureName, TEXPARAMETER_VALUE minMagFilter = TEXPARAMETER_VALUE.GL_NEAREST)
    {
        if (OpenGLHelper.TextureDictionary.ContainsKey(textureName))
            return OpenGLHelper.TextureDictionary[textureName];

        Texture t = GenTexture(bufferedImage, minMagFilter);
        OpenGLHelper.TextureDictionary.Add(textureName, t);

        return t;
    }

    /// <summary>
    /// Creates a Texture2D by loading it from file (32bit png type), or retrieves it from the texture dictionary if it has already been loaded. Pads transparent border around the cells of a spritesheet to prevent black lines appearing at top and bottom of images etc
    /// </summary>
    internal static ISpriteTexture LoadSpriteTexture(string path, int spriteRows, int spriteColumns, int padding, TEXPARAMETER_VALUE minFilter, TEXPARAMETER_VALUE maxFilter, TEXPARAMETER_VALUE wrapMode)
    {
        //if (TextureDictionary.ContainsKey(path))
        //    return TextureDictionary[path];

        var texture = GenPaddedTexture(path, spriteRows, spriteColumns, padding, minFilter, maxFilter, wrapMode);
        //TextureDictionary.Add(path, t);

        return texture;
    }

    /// <summary>
    /// Creates a Texture2D by loading it from file (32bit png type), or retrieves it from the texture dictionary if it has already been loaded. Pads transparent border around the cells of a spritesheet to prevent black lines appearing at top and bottom of images etc
    /// </summary>
    public static ISpriteTexture LoadSpriteTexture(string path, int spriteRows, int spriteColumns, int padding, TEXPARAMETER_VALUE filter)
    {
        return LoadSpriteTexture(path, spriteRows, spriteColumns, padding, filter, filter, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
    }

    public static Rect NonZeroAlphaRegion(System.Drawing.Bitmap b)
    {
        Rect r = new();
        int firstPixelX = b.Width;
        int lastPixelX = 0;
        int firstPixelY = b.Height;
        int lastPixelY = 0;

        for (int i = 0; i < b.Width; i++)
            for (int j = 0; j < b.Height; j++)
            {
                if (b.GetPixel(i, j).A > 0)
                {
                    if (i < firstPixelX)
                        firstPixelX = i;
                    if (i > lastPixelX)
                        lastPixelX = i;
                    if (j < firstPixelY)
                        firstPixelY = j;
                    if (j > lastPixelY)
                        lastPixelY = j;
                }
            }
        if (firstPixelX <= lastPixelX && firstPixelY <= lastPixelY)
        {
            r = new Rect(firstPixelX, firstPixelY, lastPixelX - firstPixelX + 1, lastPixelY - firstPixelY + 1);
        }
        return r;
    }

    /// <summary>
    /// Save an OpenGL texture to a PNG file, with optional metadata attached
    /// </summary>
    /// <param name="t"></param>
    /// <param name="filePath"></param>
    /// <param name="metadata"></param>
    public static void SaveTextureToFile(Texture t, string filePath, Dictionary<string, string> metadata = null)
    {
        System.Drawing.Bitmap b = TextureToBitmap(t);
        SaveBitmapToPNGFile(b, filePath, metadata);
    }

    public static System.Drawing.Bitmap TextureToBitmap(Texture t)
    {
        System.Drawing.Bitmap bmp = new(t.Width, t.Height);
        OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, t.ID);
        System.Drawing.Imaging.BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        OpenGL32.glGetTexImage(TEXTURE_TARGET.GL_TEXTURE_2D, 0, PIXEL_FORMAT.GL_BGRA, PIXEL_TYPE.GL_UNSIGNED_BYTE, data.Scan0);
        bmp.UnlockBits(data);
        return bmp;
    }

    public static void WriteBitmapToFile(System.Drawing.Bitmap b, string targetPath)
    {
        //string directory = Path.GetDirectoryName(targetPath);
        //string fileName = Path.GetFileName(targetPath);
        //if (!Directory.Exists(directory))
        //{
        //    Directory.CreateDirectory(directory);
        //}
        b.Save(targetPath);
    }

    /// <summary>
    /// Save a bitmap to a PNG file with the option to specify key-value metadata string pairs to store strings in the png.
    /// </summary>
    /// <param name="bmp"></param>
    /// <param name="filePath"></param>
    /// <param name="metadata"></param>
    public static void SaveBitmapToPNGFile(System.Drawing.Bitmap bmp, string filePath, Dictionary<string, string> metadata = null)
    {
        //Make sure the filepath has exactly one png extension
        if (Path.GetExtension(filePath) == null)
            filePath += ".png";

        //Save the bitmap
        bmp.Save(filePath);

        ////Encode optional metadata into the bitmap
        //if (metadata != null)
        //{
        //    //Somewhat annoyingly, the only clear way to do this is to open the bitmap I jsut saved then remake it and save it again..
        //    // see http://stackoverflow.com/questions/29037442/write-metadata-to-both-jpg-and-png and https://code.google.com/archive/p/pngcs/
        //    string destFilename = "tmp.png";

        //    //Open the file with Pngcs.dll
        //    Hjg.Pngcs.PngReader pngr = Hjg.Pngcs.FileHelper.CreatePngReader(filePath);
        //    Hjg.Pngcs.PngWriter pngw = Hjg.Pngcs.FileHelper.CreatePngWriter(destFilename, pngr.ImgInfo, true);

        //    int chunkBehav = Hjg.Pngcs.Chunks.ChunkCopyBehaviour.COPY_ALL_SAFE; // tell to copy all 'safe' chunks
        //    pngw.CopyChunksFirst(pngr, chunkBehav);          // copy some metadata from reader 

        //    //Write the metadata
        //    foreach (string key in metadata.Keys)
        //    {
        //        Hjg.Pngcs.Chunks.PngChunk chunk = pngw.GetMetadata().SetText(key, metadata[key]);
        //        chunk.Priority = true;
        //    }

        //    int channels = pngr.ImgInfo.Channels;
        //    if (channels < 3)
        //        throw new Exception("This example works only with RGB/RGBA images");
        //    for (int row = 0; row < pngr.ImgInfo.Rows; row++)
        //    {
        //        Hjg.Pngcs.ImageLine l1 = pngr.ReadRowInt(row); // format: RGBRGB... or RGBARGBA...
        //        pngw.WriteRow(l1, row);
        //    }
        //    pngw.CopyChunksLast(pngr, chunkBehav); // metadata after the image pixels? can happen
        //    pngw.End(); // dont forget this
        //    pngr.End();

        //    //Replace original with temporary file
        //    File.Delete(filePath);
        //    File.Move(destFilename, filePath);
        //}
    }

    /// <summary>
    /// Check for any OpenGL errors thrown since last check was called
    /// </summary>
    public static bool CheckOpenGLError(string callerID)
    {
        var err = OpenGL32.glGetError();
        if (err != GL_ERROR.GL_NO_ERROR)
        {
            Log.Warning($"OpenGL error: {err} - thrown in {callerID}.");
            return true;
        }

        return false;
    }

    public static int GenTexture()
    {
        int[] textures = new int[1];

        OpenGL32.glGenTextures(1, textures);

        return textures[0];
    }

    public static Rect GetViewport()
    {
        int[] ints = new int[4];
        OpenGL32.glGetIntegerv(GLCAP.GL_VIEWPORT, ints);
        return new Rect(ints[0], ints[1], ints[2], ints[3]);
    }

    public static string GetString(GETSTRING_NAME name)
    {
        unsafe
        {
            return new string(OpenGL32.glGetString(name));
        }
    }

    public static void BufferData<T>(BUFFER_TARGET target, int size, T[] data, USAGE_PATTERN usage) where T : struct
    {
        GCHandle pinnedArray = GCHandle.Alloc(data, GCHandleType.Pinned);

        IntPtr pointer = pinnedArray.AddrOfPinnedObject();

        OpenGL32.glBufferData(target, (IntPtr)size, pointer, usage);

        pinnedArray.Free();
    }

    public static void DeleteBuffer(int buffer)
    {
        int[] buffers = { buffer };

        OpenGL32.glDeleteBuffers(1, buffers);
    }

    public static int GenBuffer()
    {
        int[] buffers = new int[1];

        OpenGL32.glGenBuffers(1, buffers);

        return buffers[0];
    }

    public static int GenFramebuffer()
    {
        int[] buffers = new int[1];

        OpenGL32.glGenFramebuffers(1, buffers);

        return buffers[0];
    }

    public static string GetProgramInfoLog(int program)
    {
        StringBuilder stringPtr = new(255);

        OpenGL32.glGetProgramInfoLog(program, 255, out _, stringPtr);

        return stringPtr.ToString();
    }

    public static string GetShaderInfoLog(int shader)
    {
        StringBuilder stringPtr = new(255);
        int count;
        OpenGL32.glGetShaderInfoLog(shader, 255, out count, stringPtr);

        return stringPtr.ToString();
    }

    public static void ShaderSource(int shader, string source)
    {
        string[] strings = new string[1] { source };

        OpenGL32.glShaderSource(shader, 1, strings, null);
    }

    public static void UniformMatrix3(int location, Matrix3 matrix)
    {
        unsafe
        {
            fixed (float* valuePointer = matrix.Values)
            {
                OpenGL32.glUniformMatrix3fv(location, 1, false, valuePointer);
            }
        }
    }

    public static void UniformMatrix4(int location, Matrix4 matrix)
    {
        unsafe
        {
            fixed (float* valuePointer = matrix.Values)
            {
                OpenGL32.glUniformMatrix4fv(location, 1, false, valuePointer);
            }
        }
    }

    public static void Viewport(Rect newViewport)
    {
        OpenGL32.glViewport((int)newViewport.X, (int)newViewport.Y, (int)newViewport.W, (int)newViewport.H);
    }

    public static void BindVertexBuffer(int vertexBufferID)
    {
        if (LastBoundVertexBuffer != vertexBufferID)
        {
            OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, vertexBufferID);
            LastBoundVertexBuffer = vertexBufferID;
        }
    }

    public static void UnbindVertexBuffer()
    {
        OpenGL32.glBindBuffer(BUFFER_TARGET.GL_ARRAY_BUFFER, 0);
        LastBoundVertexBuffer = 0;
    }

    public static void BindTexture(ITexture texture)
    {
        if (s_lastBoundTexture != texture.ID)
        {
            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, texture.ID);
            OpenGLHelper.s_lastBoundTexture = texture.ID;
        }
    }

    public static void UnbindTexture()
    {
        OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, 0);
        s_lastBoundTexture = 0;
    }


}
