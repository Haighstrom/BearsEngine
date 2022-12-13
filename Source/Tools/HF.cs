using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;
using System.IO;

using Encoding = System.Text.Encoding;
using HaighFramework.WinAPI;
using HaighFramework.OpenGL;

namespace BearsEngine;
public static class HF
{
    public static class BitOps
    {
        //.net core3 = System.Numerics.BitwiseOperations.TrailingZeroCount(int n)

        static readonly int[] _DeBruijnPositions =
        {
        0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8,
        31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9
        };

        public static int TrailingZeroCount(int number) => _DeBruijnPositions[unchecked((uint)(number & -number) * 0x077CB531U) >> 27];
        
    }

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

        public static void BindShader(uint id)
        {
            OpenGL32.UseProgram(id);
            BE.LastBoundShader = id;
        }

        public static void UnbindShader()
        {
            OpenGL32.UseProgram(0);
            BE.LastBoundShader = 0;
        }

        public static uint CreateShader(byte[] vertexSource, byte[] fragmentSource)
            => CreateShader(
                Encoding.UTF8.GetString(vertexSource),
                Encoding.UTF8.GetString(fragmentSource));

        public static uint CreateShader(string vertexSource, string fragmentSource) => CreateShader(vertexSource, null, fragmentSource);

        public static uint CreateShader(byte[] vertexSource, byte[] geometrySource, byte[] fragmentSource)
            => CreateShader(
                Encoding.UTF8.GetString(vertexSource),
                Encoding.UTF8.GetString(geometrySource),
                Encoding.UTF8.GetString(fragmentSource));

        public static uint CreateShader(string vertexSource, string geometrySource, string fragmentSource)
        {
            uint programID = OpenGL32.CreateProgram();

            int vs, gs = 0, fs = 0;
            vs = CompileShader(programID, ShaderType.VertexShader, vertexSource);
            if (geometrySource != null)
                gs = CompileShader(programID, ShaderType.GeometryShader, geometrySource);
            if (fragmentSource != null)
                fs = CompileShader(programID, ShaderType.FragmentShader, fragmentSource);

            OpenGL32.LinkProgram(programID);

            //Check for errors in compiling shader
            var log = OpenGL32.GetProgramInfoLog(programID);
            if (log.Length > 0)
                Log.Error("Shader compilation error: " + log);

            //Cleanup
            OpenGL32.DetachShader(programID, vs);
            OpenGL32.DeleteShader(vs);
            if (geometrySource != null)
            {
                OpenGL32.DetachShader(programID, gs);
                OpenGL32.DeleteShader(gs);
            }
            if (fragmentSource != null)
            {
                OpenGL32.DetachShader(programID, fs);
                OpenGL32.DeleteShader(fs);
            }

            //Assign this shader to be the selected one in OpenGL
            OpenGL32.UseProgram(programID);

            return programID;
        }

        private static int CompileShader(uint programID, ShaderType shaderType, string shaderSrc)
        {
            int shaderID = OpenGL32.CreateShader(shaderType);
            OpenGL32.ShaderSource(shaderID, shaderSrc);
            OpenGL32.CompileShader(shaderID);
            OpenGL32.AttachShader(programID, shaderID);

            //Check for errors in compiling shader
            var log = OpenGL32.GetShaderInfoLog(shaderID);

            if (log.Length > 0)
                Log.Error("Shader compilation error: " + log);

            return shaderID;
        }

        public static void DeleteShader(uint id)
        {
            OpenGL32.UseProgram(0);
            OpenGL32.DeleteProgram(id);
        }

        public static void CreateFramebuffer(int width, int height, out uint framebufferID, out Texture framebufferTexture)
        {
            framebufferID = OpenGL32.GenFramebuffer();
            framebufferTexture = new Texture(OpenGLHelpers.GenTexture(), width, height);

            OpenGL32.glBindTexture(TextureTarget.Texture2D, framebufferTexture.ID);
            OpenGL32.TexStorage2D(TextureTarget.Texture2D, 1, TexInternalFormat.RGBA8, width, height);

            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);
        }

        public static void ResizeFramebuffer(ref Texture framebufferTexture, int newW, int newH)
        {
            OpenGL32.glDeleteTextures(1, new uint[1] { framebufferTexture.ID });
            framebufferTexture = new Texture(OpenGLHelpers.GenTexture(), newW, newH);

            OpenGL32.glBindTexture(TextureTarget.Texture2D, framebufferTexture.ID);
            OpenGL32.TexStorage2D(TextureTarget.Texture2D, 1, TexInternalFormat.RGBA8, newW, newH);

            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);
        }

        public static void CreateMSAAFramebuffer(int width, int height, MSAA_Samples samples, out uint framebufferID, out Texture framebufferTexture)
        {
            //Generate FBO and texture to use with the MSAA antialising pass
            framebufferTexture = new Texture(OpenGLHelpers.GenTexture(), width, height);

            OpenGL32.glBindTexture(TextureTarget.Texture2DMultisample, framebufferTexture.ID);
            OpenGL32.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, (int)samples, PixelInternalFormat.Rgba8, width, height, false);

            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);

            framebufferID = OpenGL32.GenFramebuffer();
        }
        
        public static void ResizeMSAAFramebuffer(ref Texture framebufferTexture, int newW, int newH, MSAA_Samples newSamples)
        {
            OpenGL32.glDeleteTextures(1, new uint[1] { framebufferTexture.ID });
            framebufferTexture = new Texture(OpenGLHelpers.GenTexture(), newW, newH);

            OpenGL32.glBindTexture(TextureTarget.Texture2DMultisample, framebufferTexture.ID);
            OpenGL32.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, newSamples, PixelInternalFormat.Rgba8, newW, newH, false);

            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);
        }
        
        /// <summary>
        /// Create a Texture from an image file
        /// </summary>
        private static Texture GenTexture(string path, TextureParameter minMagFilter = TextureParameter.Nearest) => GenTexture(new System.Drawing.Bitmap(path), minMagFilter);

        /// <summary>
        /// Creates a Texture from a Bitmap
        /// </summary>
        public static Texture GenTexture(System.Drawing.Bitmap bmp, TextureParameter minMagFilter = TextureParameter.Nearest)
        {
            bmp = BitmapTools.PremultiplyAlpha(bmp);

            Texture t = new()
            {
                Width = bmp.Width,
                Height = bmp.Height,

                ID = OpenGLHelpers.GenTexture()
            };

            OpenGL32.glBindTexture(TextureTarget.Texture2D, t.ID);
            BE.LastBoundTexture = t.ID;

            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, minMagFilter);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, minMagFilter);

            System.Drawing.Imaging.BitmapData bmpd = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            OpenGL32.glTexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmpd.Width, bmpd.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bmpd.Scan0);
            bmp.UnlockBits(bmpd);

            return t;
        }

        public static Texture GenTexture(Colour[,] pixels, TextureParameter minMagFilter = TextureParameter.Nearest)
        {
            pixels = pixels.Transpose();
            Texture t = new(OpenGLHelpers.GenTexture(), pixels.GetLength(1), pixels.GetLength(0));

            OpenGL32.glPixelStorei(PixelStoreParameter.GL_UNPACK_ALIGNMENT, 1);

            t.Bind();

            GCHandle pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            IntPtr pointer = pinned.AddrOfPinnedObject();

            OpenGL32.glTexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, t.Width, t.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pointer);

            pinned.Free();

            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, minMagFilter);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, minMagFilter);

            return t;
        }
        

        /// <summary>
        /// Create a Texture from an image file, and insert transparent pixel padding borers around each cell of the spritesheet to prevent artefacts
        /// </summary>
        private static Texture GenPaddedTexture(string path, int spriteRows, int spriteColumns, TextureParameter minFilter = TextureParameter.Nearest, TextureParameter magFilter = TextureParameter.Nearest, TextureParameter wrapMode = TextureParameter.ClampToEdge)
        {
            if (string.IsNullOrEmpty(path)) 
                throw new ArgumentException(path);
            if (!File.Exists(path)) 
                throw new FileNotFoundException(path);

            var t = new Texture()
            {
                ID = OpenGLHelpers.GenTexture()
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

            OpenGL32.glTexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmpData.Width, bmpData.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);

            paddedBMP.UnlockBits(bmpData);

            //Apply interpolation filters for scaled images - normally linear for smoothing, nearest for pixel graphics
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, minFilter);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, magFilter);

            //I had the 3rd parameter in these set to repeat, but it gave the artefact of the top of textures being drawn across the bottom eg line across Haighman's feet. Will need to do %1f modulus on uv coords now..
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, wrapMode);
            OpenGL32.glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, wrapMode);

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
        public static Texture LoadTexture(string path, TextureParameter minMagFilter = TextureParameter.Nearest)
        {
            if (BE.TextureDictionary.ContainsKey(path))
                return BE.TextureDictionary[path];

            Texture t = GenTexture(path, minMagFilter);
            BE.TextureDictionary.Add(path, t);

            return t;
        }
        /// <summary>
        /// Provide a bitmap and its string name manually
        /// </summary>
        internal static Texture LoadTexture(System.Drawing.Bitmap bufferedImage, string textureName, TextureParameter minMagFilter = TextureParameter.Nearest)
        {
            if (BE.TextureDictionary.ContainsKey(textureName))
                return BE.TextureDictionary[textureName];

            Texture t = GenTexture(bufferedImage, minMagFilter);
            BE.TextureDictionary.Add(textureName, t);

            return t;
        }
        

        /// <summary>
        /// Creates a Texture2D by loading it from file (32bit png type), or retrieves it from the texture dictionary if it has already been loaded. Pads transparent border around the cells of a spritesheet to prevent black lines appearing at top and bottom of images etc
        /// </summary>
        internal static Texture LoadSpriteTexture(string path, int spriteRows, int spriteColumns, TextureParameter minFilter, TextureParameter maxFilter, TextureParameter wrapMode)
        {
            if (BE.TextureDictionary.ContainsKey(path))
                return BE.TextureDictionary[path];

            Texture t = GenPaddedTexture(path, spriteRows, spriteColumns, minFilter, maxFilter, wrapMode);
            BE.TextureDictionary.Add(path, t);

            return t;
        }

        /// <summary>
        /// Creates a Texture2D by loading it from file (32bit png type), or retrieves it from the texture dictionary if it has already been loaded. Pads transparent border around the cells of a spritesheet to prevent black lines appearing at top and bottom of images etc
        /// </summary>
        public static Texture LoadSpriteTexture(string path, int spriteRows, int spriteColumns, TextureParameter filter)
        {
            return LoadSpriteTexture(path, spriteRows, spriteColumns, filter, filter, TextureParameter.ClampToEdge);
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
            OpenGL32.glBindTexture(TextureTarget.Texture2D, t.ID);
            System.Drawing.Imaging.BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            OpenGL32.glGetTexImage(TextureTarget.Texture2D, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
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
        

        public static void PrintCSVToConsole(string[,] array)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                Console.Write(j.ToString("D3") + ": ");
                for (var i = 0; i < array.GetLength(0); i++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
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
