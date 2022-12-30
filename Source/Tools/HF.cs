using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;
using System.IO;

using Encoding = System.Text.Encoding;
using HaighFramework.OpenGL;

namespace BearsEngine;
public static class HF
{
    public static class Arrays
    {
        public static T[] FillArray<T>(int size, T value)
        {
            var ret = new T[size];
            Array.Fill(ret, value);
            return ret;
        }

        public static T[,] FillArray<T>(int arrayWidth, int arrayHeight, T value)
        {
            var ret = new T[arrayWidth, arrayHeight];

            for (int i = 0; i < arrayWidth; i++)
                for (int j = 0; j < arrayHeight; j++)
                    ret[i, j] = value;

            return ret;
        }
    }
    
    public static class Geom
    {
        public static List<Vertex> QuadToTris(Vertex topLeft, Vertex topRight, Vertex bottomLeft, Vertex bottomRight)
        {
            return new List<Vertex>()
            {
                bottomLeft, topRight, topLeft,
                bottomLeft, topRight, bottomRight
            };
        }

        /// <summary>
        /// returns the unit Point that points in the angle requested clockwise from up
        /// </summary>
        /// <param name="angleInDegrees"></param>
        /// <returns></returns>
        public static Point AngleToPoint(float angleInDegrees) => new((float)Math.Sin(angleInDegrees * Math.PI / 180), (float)Math.Cos(angleInDegrees * Math.PI / 180));
    }

    public static class Graphics
    {
        /// <summary>
        /// How many pixels of padding to add when using LoadPaddedTexture for spritesheeets.
        /// </summary>
        public const int TEXTURE_SPRITE_PADDING = 2;

        public static void BindShader(int id)
        {
            OpenGL32.glUseProgram(id);
            OpenGL.LastBoundShader = id;
        }

        public static void UnbindShader()
        {
            OpenGL32.glUseProgram(0);
            OpenGL.LastBoundShader = 0;
        }

        public static int CreateShader(byte[] vertexSource, byte[] fragmentSource)
            => CreateShader(
                Encoding.UTF8.GetString(vertexSource),
                Encoding.UTF8.GetString(fragmentSource));

        public static int CreateShader(string vertexSource, string fragmentSource) => CreateShader(vertexSource, null, fragmentSource);

        public static int CreateShader(byte[] vertexSource, byte[] geometrySource, byte[] fragmentSource)
            => CreateShader(
                Encoding.UTF8.GetString(vertexSource),
                Encoding.UTF8.GetString(geometrySource),
                Encoding.UTF8.GetString(fragmentSource));

        public static int CreateShader(string vertexSource, string geometrySource, string fragmentSource)
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
            var log = OpenGL.GetProgramInfoLog(programID);
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

        private static int CompileShader(int programID, SHADER_TYPE shaderType, string shaderSrc)
        {
            int shaderID = OpenGL32.glCreateShader(shaderType);
            OpenGL.ShaderSource(shaderID, shaderSrc);
            OpenGL32.glCompileShader(shaderID);
            OpenGL32.glAttachShader(programID, shaderID);

            //Check for errors in compiling shader
            var log = OpenGL.GetShaderInfoLog(shaderID);

            if (log.Length > 0)
                Log.Error("Shader compilation error: " + log);

            return shaderID;
        }

        public static void DeleteShader(int id)
        {
            OpenGL32.glUseProgram(0);
            OpenGL32.glDeleteProgram(id);
        }

        public static void CreateFramebuffer(int width, int height, out int framebufferID, out Texture framebufferTexture)
        {
            framebufferID = OpenGL.GenFramebuffer();
            framebufferTexture = new Texture(OpenGL.GenTexture(), width, height);

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
            framebufferTexture = new Texture(OpenGL.GenTexture(), newW, newH);

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
            framebufferTexture = new Texture(OpenGL.GenTexture(), width, height);

            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_PROXY_TEXTURE_2D_MULTISAMPLE, framebufferTexture.ID);
            OpenGL32.glTexImage2DMultisample(TEXTURE_TARGET.GL_TEXTURE_2D_MULTISAMPLE, (int)samples, TEXTURE_INTERNALFORMAT.GL_RGB8, width, height, false);

            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_S, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_T, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);

            framebufferID = OpenGL.GenFramebuffer();
        }
        
        public static void ResizeMSAAFramebuffer(ref Texture framebufferTexture, int newW, int newH, MSAA_SAMPLES newSamples)
        {
            OpenGL32.glDeleteTextures(1, new int[1] { framebufferTexture.ID });
            framebufferTexture = new Texture(OpenGL.GenTexture(), newW, newH);

            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_PROXY_TEXTURE_2D_MULTISAMPLE, framebufferTexture.ID);
            OpenGL32.glTexImage2DMultisample(TEXTURE_TARGET.GL_TEXTURE_2D_MULTISAMPLE, (int)newSamples, TEXTURE_INTERNALFORMAT.GL_RGB8, newW, newH, false);

            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, TEXPARAMETER_VALUE.GL_LINEAR);
            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_S, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_WRAP_T, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
        }
        
        /// <summary>
        /// Create a Texture from an image file
        /// </summary>
        private static Texture GenTexture(string path, TEXPARAMETER_VALUE minMagFilter = TEXPARAMETER_VALUE.GL_NEAREST) => GenTexture(new System.Drawing.Bitmap(path), minMagFilter);

        /// <summary>
        /// Creates a Texture from a Bitmap
        /// </summary>
        public static Texture GenTexture(System.Drawing.Bitmap bmp, TEXPARAMETER_VALUE minMagFilter = TEXPARAMETER_VALUE.GL_NEAREST)
        {
            bmp = BitmapTools.PremultiplyAlpha(bmp);

            Texture t = new()
            {
                Width = bmp.Width,
                Height = bmp.Height,

                ID = OpenGL.GenTexture()
            };

            OpenGL32.glBindTexture(TEXTURE_TARGET.GL_TEXTURE_2D, t.ID);
            OpenGL.LastBoundTexture = t.ID;

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
            Texture t = new(OpenGL.GenTexture(), pixels.GetLength(1), pixels.GetLength(0));

            OpenGL32.glPixelStorei(PIXEL_STORE_MODE.GL_UNPACK_ALIGNMENT, 1);

            t.Bind();

            GCHandle pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            IntPtr pointer = pinned.AddrOfPinnedObject();

            OpenGL32.glTexImage2D(TEXTURE_TARGET.GL_TEXTURE_2D, 0, TEXTURE_INTERNALFORMAT.GL_RGBA, t.Width, t.Height, 0, PIXEL_FORMAT.GL_RGBA, PIXEL_TYPE.GL_UNSIGNED_BYTE, pointer);

            pinned.Free();

            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MIN_FILTER, minMagFilter);
            OpenGL32.glTexParameteri(TEXTURE_TARGET.GL_TEXTURE_2D, TEXPARAMETER_NAME.GL_TEXTURE_MAG_FILTER, minMagFilter);

            return t;
        }
        

        /// <summary>
        /// Create a Texture from an image file, and insert transparent pixel padding borers around each cell of the spritesheet to prevent artefacts
        /// </summary>
        private static Texture GenPaddedTexture(string path, int spriteRows, int spriteColumns, TEXPARAMETER_VALUE minFilter = TEXPARAMETER_VALUE.GL_NEAREST, TEXPARAMETER_VALUE magFilter = TEXPARAMETER_VALUE.GL_NEAREST, TEXPARAMETER_VALUE wrapMode = TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE)
        {
            if (string.IsNullOrEmpty(path)) 
                throw new ArgumentException(path);
            if (!File.Exists(path)) 
                throw new FileNotFoundException(path);

            var t = new Texture()
            {
                ID = OpenGL.GenTexture()
            };
            t.Bind();

            var BMP = new System.Drawing.Bitmap(path);

            //Snip individual bmps out for each sprite cell
            int pad = TEXTURE_SPRITE_PADDING;
            int w = BMP.Width / spriteColumns;
            int h = BMP.Height / spriteRows;
            int newW = BMP.Width + (spriteColumns + 1) * pad;
            int newH = BMP.Height + (spriteRows + 1) * pad;

            var paddedBMP = new System.Drawing.Bitmap(newW, newH);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(paddedBMP))
            {
                //set background color
                g.Clear(System.Drawing.Color.Transparent);
                for (int i = 0; i < spriteColumns; i++)
                    for (int j = 0; j < spriteRows; j++)
                    {
                        g.DrawImage(BMP.Clone(new System.Drawing.Rectangle(i * w, j * h, w, h), System.Drawing.Imaging.PixelFormat.Format32bppArgb), new System.Drawing.Rectangle(pad + i * (w + pad), pad + j * (h + pad), w, h));
                    }
            }

            t.Width = newW;
            t.Height = newH;
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
            if (OpenGL.TextureDictionary.ContainsKey(path))
                return OpenGL.TextureDictionary[path];

            Texture t = GenTexture(path, minMagFilter);
            OpenGL.TextureDictionary.Add(path, t);

            return t;
        }
        /// <summary>
        /// Provide a bitmap and its string name manually
        /// </summary>
        internal static Texture LoadTexture(System.Drawing.Bitmap bufferedImage, string textureName, TEXPARAMETER_VALUE minMagFilter = TEXPARAMETER_VALUE.GL_NEAREST)
        {
            if (OpenGL.TextureDictionary.ContainsKey(textureName))
                return OpenGL.TextureDictionary[textureName];

            Texture t = GenTexture(bufferedImage, minMagFilter);
            OpenGL.TextureDictionary.Add(textureName, t);

            return t;
        }
        

        /// <summary>
        /// Creates a Texture2D by loading it from file (32bit png type), or retrieves it from the texture dictionary if it has already been loaded. Pads transparent border around the cells of a spritesheet to prevent black lines appearing at top and bottom of images etc
        /// </summary>
        internal static Texture LoadSpriteTexture(string path, int spriteRows, int spriteColumns, TEXPARAMETER_VALUE minFilter, TEXPARAMETER_VALUE maxFilter, TEXPARAMETER_VALUE wrapMode)
        {
            if (OpenGL.TextureDictionary.ContainsKey(path))
                return OpenGL.TextureDictionary[path];

            Texture t = GenPaddedTexture(path, spriteRows, spriteColumns, minFilter, maxFilter, wrapMode);
            OpenGL.TextureDictionary.Add(path, t);

            return t;
        }

        /// <summary>
        /// Creates a Texture2D by loading it from file (32bit png type), or retrieves it from the texture dictionary if it has already been loaded. Pads transparent border around the cells of a spritesheet to prevent black lines appearing at top and bottom of images etc
        /// </summary>
        public static Texture LoadSpriteTexture(string path, int spriteRows, int spriteColumns, TEXPARAMETER_VALUE filter)
        {
            return LoadSpriteTexture(path, spriteRows, spriteColumns, filter, filter, TEXPARAMETER_VALUE.GL_CLAMP_TO_EDGE);
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
            string directory = Path.GetDirectoryName(targetPath);
            string fileName = Path.GetFileName(targetPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
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
        
    }
    
    public static class Randomisation
    {
        private static readonly Random _random = new();

        /// <summary>
        /// returns true with probability (chanceOutOfAHundred)%
        /// </summary>
        public static bool Chance(float chanceOutOfAHundred) => Rand(0, 100) < chanceOutOfAHundred;
        

        /// <summary>
        /// Returns one of a list of things, at random
        /// </summary>
        public static T Choose<T>(params T[] things) => things[Rand(things.Length)];
        /// <summary>
        /// Returns one of a list of things, at random
        /// </summary>
        public static T Choose<T>(List<T> things) => things[Rand(things.Count)];
        

        /// <summary>
        /// Returns a double [0,max)
        /// </summary>
        public static double RandD(double max) => _random.NextDouble() * max;

        /// <summary>
        /// returns an int [min,max)
        /// </summary>
        public static int Rand(int min, int max)
        {
            if (max < min)
                throw new Exception("cannot have max less than min");

            return min + _random.Next(max - min);
        }
        /// <summary>
        /// returns an int [0,max)
        /// </summary>
        public static int Rand(int max)
        {
            if (max <= 0)
                return 0;

            return _random.Next(max);
        }

        public static T RandEnum<T>()
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException($"Generic parameter T must be an enum. Provided was {typeof(T).Name}.");

            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(_random.Next(values.Length));
        }
        

        public static Colour RandColour() => new((byte)Rand(255), (byte)Rand(255), (byte)Rand(255), 255);

        public static Point RandPoint() => new(RandF(2) - 1, RandF(2) - 1);

        public static Direction RandDirection()
        {
            return Choose(Direction.Up, Direction.Right, Direction.Down, Direction.Left);
        }
        

        public static float RandF(int max) => (float)(_random.NextDouble() * max);

        public static float RandF(float max) => (float)(_random.NextDouble() * max);

        /// <summary>
        /// returns a float in range [min,max]
        /// </summary>
        public static float RandF(int min, int max)
        {
            if (max <= min)
                return max;
            return
                min + (float)_random.NextDouble() * (max - min);
        }
        /// <summary>
        /// returns a float in range [min,max]
        /// </summary>
        public static float RandF(float min, float max)
        {
            if (max <= min)
                return max;
            return min + (float)_random.NextDouble() * (max - min);
        }
        

        public static float RandGaussianApprox(float min, float max)
        {
            if (max <= min) return max;

            //guassian = approx (1 + cos x)/2PI [SD = 1] 
            //therefore (x+sinx)/2PI is integral, x from 0 to 2 PI gives 0-1 with normal distribution ish results

            double zeroTo2PI = 2 * Math.PI * _random.NextDouble();
            double zeroToOne = (zeroTo2PI + Math.Sin(zeroTo2PI)) / (2 * Math.PI);
            return min + (float)(zeroToOne * (max - min));
        }
        

        public static string RandUpperCaseString(int chars)
        {
            string def = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder ret = new();
            for (int i = 0; i < chars; i++)
                ret.Append(def.AsSpan(_random.Next(def.Length), 1));
            return ret.ToString();
        }
        

        public static Colour RandSystemColour()
        {
            List<Colour> colours = new();
            foreach (PropertyInfo i in typeof(Colour).GetProperties(
                BindingFlags.Static | BindingFlags.Public))
            {
                var v = i.GetValue(null);
                if (v is Colour colour)
                    colours.Add(colour);
            }
            return Choose(colours);
        }
        

        /// <summary>
        /// Shuffle an array containing Ts
        /// </summary>
        public static T[] Shuffle<T>(T[] array)
        {
            for (int i = array.Length; i > 1; i--)
            {
                // pick random element 0 <= j < i
                int j = Rand(i);
                // swap i and j
                (array[i - 1], array[j]) = (array[j], array[i - 1]);
            }
            return array;
        }

        public static List<T> Shuffle<T>(List<T> list)
        {
            for (int i = list.Count; i > 1; i--)
            {
                // pick random element 0 <= j < i
                int j = Rand(i);
                // swap i and j
                (list[i - 1], list[j]) = (list[j], list[i - 1]);
            }
            return list;
        }
        
    }

    public static class Types
    {
        public static int GetSizeOf(Type t) => Marshal.SizeOf(t);

        public static IEnumerable<Enum> GetUniqueFlags(Enum flags)
        {
            ulong flag = 1;
            foreach (var value in Enum.GetValues(flags.GetType()).Cast<Enum>())
            {
                ulong bits = Convert.ToUInt64(value);

                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits && flags.HasFlag(value))
                {
                    yield return value;
                }
            }
        }
        

        /// <summary>
        /// returns the unique Type with the specified name in any of the loaded assemblies
        /// </summary>
        public static Type? FindType(string typeName, bool errorIfNotFound = false, bool errorIfDuplicate = false)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().Select(a => FindType(a.FullName, typeName));

            if (!types.Any())
                if (errorIfNotFound)
                    throw new Exception($"Type {typeName} not found in the current domain.");
                else
                    return null;
            else if (types.Count() > 1 && errorIfDuplicate)
                throw new Exception("Type {typeName} was not unique in the current domain.");
            else
                return types.First();
        }

        /// <summary>
        /// returns the unique Type with the specified name in the specified assembly
        /// </summary>
        public static Type? FindType(string assembly, string typeName, bool errorIfUnfound = false, bool errorIfDuplicate = false)
        {
            var types = Assembly.Load(assembly).GetTypes().Where(t => t.Name == typeName);

            if (!types.Any())
                if (errorIfUnfound)
                    throw new Exception($"Type {typeName} not found in the current domain.");
                else
                    return null;
            else if (types.Count() > 1 && errorIfDuplicate)
                throw new Exception($"Type {typeName} was not unique in the current domain.");
            else
                return types.First();
        }
    }
    

    public static void Repeat(Action function, int times)
    {
        for (int i = 0; i < times; ++i)
            function();
    }
    
}
