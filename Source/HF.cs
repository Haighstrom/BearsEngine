using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;
using HaighFramework.OpenGL4;
using BearsEngine.Graphics;
using BearsEngine.Pathfinding;
using System.IO;

namespace BearsEngine
{
    using Encoding = Encoding;

    public static class HF
    {
        public static void Log(string message, params object[] args) => HConsole.Log(message, args);
        public static void Log(object o, params object[] args) => HConsole.Log(o, args);

        #region BitOps
        public static class BitOps
        {
            #region TrailingZeroCount
            //.net core3 = System.Numerics.BitwiseOperations.TrailingZeroCount(int n)

            static readonly int[] _DeBruijnPositions =
            {
            0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8,
            31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9
            };

            public static int TrailingZeroCount(int number) => _DeBruijnPositions[unchecked((uint)(number & -number) * 0x077CB531U) >> 27];
            #endregion
        }
        #endregion

        #region Arrays
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
        #endregion

        #region Geom
        public static class Geom
        {
            #region QuadToTris
            public static List<Vertex> QuadToTris(Vertex topLeft, Vertex topRight, Vertex bottomLeft, Vertex bottomRight)
            {
                return new List<Vertex>()
                {
                    bottomLeft, topRight, topLeft,
                    bottomLeft, topRight, bottomRight
                };
            }
            #endregion

            #region AngleToPoint
            /// <summary>
            /// returns the unit Point that points in the angle requested clockwise from up
            /// </summary>
            /// <param name="angleInDegrees"></param>
            /// <returns></returns>
            public static Point AngleToPoint(float angleInDegrees) => new((float)Math.Sin(angleInDegrees * Math.PI / 180), (float)Math.Cos(angleInDegrees * Math.PI / 180));
            #endregion
        }
        #endregion

        #region Graphics
        public static class Graphics
        {
            /// <summary>
            /// How many pixels of padding to add when using LoadPaddedTexture for spritesheeets.
            /// </summary>
            public const int TEXTURE_SPRITE_PADDING = 2;

            #region BindShader
            public static void BindShader(uint id)
            {
                OpenGL.UseProgram(id);
                HV.LastBoundShader = id;
            }
            #endregion

            #region UnbindShader
            public static void UnbindShader()
            {
                OpenGL.UseProgram(0);
                HV.LastBoundShader = 0;
            }
            #endregion

            #region CreateShader
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
                uint programID = OpenGL.CreateProgram();

                int vs, gs = 0, fs = 0;
                vs = CompileShader(programID, ShaderType.VertexShader, vertexSource);
                if (geometrySource != null)
                    gs = CompileShader(programID, ShaderType.GeometryShader, geometrySource);
                if (fragmentSource != null)
                    fs = CompileShader(programID, ShaderType.FragmentShader, fragmentSource);

                OpenGL.LinkProgram(programID);

                //Check for errors in compiling shader
                var log = OpenGL.GetProgramInfoLog(programID);
                if (log.Length > 0)
                    HConsole.Log("Shader compilation error: " + log);

                //Cleanup
                OpenGL.DetachShader(programID, vs);
                OpenGL.DeleteShader(vs);
                if (geometrySource != null)
                {
                    OpenGL.DetachShader(programID, gs);
                    OpenGL.DeleteShader(gs);
                }
                if (fragmentSource != null)
                {
                    OpenGL.DetachShader(programID, fs);
                    OpenGL.DeleteShader(fs);
                }

                //Assign this shader to be the selected one in OpenGL
                OpenGL.UseProgram(programID);

                return programID;
            }
            #endregion

            #region CompileShader
            private static int CompileShader(uint programID, ShaderType shaderType, string shaderSrc)
            {
                int shaderID = OpenGL.CreateShader(shaderType);
                OpenGL.ShaderSource(shaderID, shaderSrc);
                OpenGL.CompileShader(shaderID);
                OpenGL.AttachShader(programID, shaderID);

                //Check for errors in compiling shader
                var log = OpenGL.GetShaderInfoLog(shaderID);

                if (log.Length > 0)
                    HConsole.Log("Shader compilation error: " + log);

                return shaderID;
            }
            #endregion

            #region DeleteShader
            public static void DeleteShader(uint id)
            {
                OpenGL.UseProgram(0);
                OpenGL.DeleteProgram(id);
            }
            #endregion

            #region CreateFramebuffer
            public static void CreateFramebuffer(int width, int height, out uint framebufferID, out Texture framebufferTexture)
            {
                framebufferID = OpenGL.GenFramebuffer();
                framebufferTexture = new Texture(OpenGL.GenTexture(), width, height);

                OpenGL.BindTexture(TextureTarget.Texture2D, framebufferTexture.ID);
                OpenGL.TexStorage2D(TextureTarget.Texture2D, 1, TexInternalFormat.RGBA8, width, height);

                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);
            }
            #endregion

            #region ResizeFramebuffer
            public static void ResizeFramebuffer(uint framebufferID, ref Texture framebufferTexture, int newW, int newH)
            {
                OpenGL.DeleteTexture(framebufferTexture.ID);
                framebufferTexture = new Texture(OpenGL.GenTexture(), newW, newH);

                OpenGL.BindTexture(TextureTarget.Texture2D, framebufferTexture.ID);
                OpenGL.TexStorage2D(TextureTarget.Texture2D, 1, TexInternalFormat.RGBA8, newW, newH);

                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);
            }
            #endregion

            #region CreateMSAAFramebuffer
            public static void CreateMSAAFramebuffer(int width, int height, MSAA_Samples samples, out uint framebufferID, out Texture framebufferTexture)
            {
                //Generate FBO and texture to use with the MSAA antialising pass
                framebufferTexture = new Texture(OpenGL.GenTexture(), width, height);

                OpenGL.BindTexture(TextureTarget.Texture2DMultisample, framebufferTexture.ID);
                OpenGL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, (int)samples, PixelInternalFormat.Rgba8, width, height, false);

                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);

                framebufferID = OpenGL.GenFramebuffer();
            }
            #endregion

            #region ResizeMSAAFramebuffer
            public static void ResizeMSAAFramebuffer(uint framebufferID, ref Texture framebufferTexture, int newW, int newH, MSAA_Samples newSamples)
            {
                OpenGL.DeleteTexture(framebufferTexture.ID);
                framebufferTexture = new Texture(OpenGL.GenTexture(), newW, newH);

                OpenGL.BindTexture(TextureTarget.Texture2DMultisample, framebufferTexture.ID);
                OpenGL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, newSamples, PixelInternalFormat.Rgba8, newW, newH, false);

                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureParameter.Linear);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);
            }
            #endregion

            #region GenTexture
            /// <summary>
            /// Create a Texture from an image file
            /// </summary>
            private static Texture GenTexture(string path, TextureParameter minMagFilter = TextureParameter.Nearest) => GenTexture(new System.Drawing.Bitmap(path), minMagFilter);

            /// <summary>
            /// Creates a Texture from a Bitmap
            /// </summary>
            public static Texture GenTexture(System.Drawing.Bitmap bmp, TextureParameter minMagFilter = TextureParameter.Nearest)
            {
                bmp = OpenGL.PremultiplyAlpha(bmp);

                Texture t = new()
                {
                    Width = bmp.Width,
                    Height = bmp.Height,

                    ID = OpenGL.GenTexture()
                };

                OpenGL.BindTexture(TextureTarget.Texture2D, t.ID);
                HV.LastBoundTexture = t.ID;

                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, minMagFilter);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, minMagFilter);

                System.Drawing.Imaging.BitmapData bmpd = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                OpenGL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmpd.Width, bmpd.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bmpd.Scan0);
                bmp.UnlockBits(bmpd);

                return t;
            }

            public static Texture GenTexture(Colour[,] pixels, TextureParameter minMagFilter = TextureParameter.Nearest)
            {
                pixels = pixels.Transpose();
                Texture t = new(OpenGL.GenTexture(), pixels.GetLength(1), pixels.GetLength(0));

                OpenGL.PixelStore(PixelStoreParameter.GL_UNPACK_ALIGNMENT, 1);

                t.Bind();

                GCHandle pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                IntPtr pointer = pinned.AddrOfPinnedObject();

                OpenGL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, t.Width, t.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pointer);

                pinned.Free();

                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, minMagFilter);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, minMagFilter);

                return t;
            }
            #endregion

            #region GenPaddedTexture    
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
                paddedBMP = OpenGL.PremultiplyAlpha(paddedBMP);

                var bmpData = paddedBMP.LockBits(new System.Drawing.Rectangle(0, 0, paddedBMP.Width, paddedBMP.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                OpenGL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmpData.Width, bmpData.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);

                paddedBMP.UnlockBits(bmpData);

                //Apply interpolation filters for scaled images - normally linear for smoothing, nearest for pixel graphics
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, minFilter);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, magFilter);

                //I had the 3rd parameter in these set to repeat, but it gave the artefact of the top of textures being drawn across the bottom eg line across Haighman's feet. Will need to do %1f modulus on uv coords now..
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, wrapMode);
                OpenGL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, wrapMode);

                BMP.Dispose();
                paddedBMP.Dispose();

                return t;
            }
            #endregion

            #region GenSquare
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
            #endregion

            #region GenTrianglePolygon
            public static Polygon GenTrianglePolygon(IRect boundingRect, int border, Direction direction, Colour colour)
            {
                IRect r = boundingRect;

                return direction switch
                {
                    Direction.Up => new Polygon(colour, new Point(r.W / 2, border), new Point(r.W - border, r.W - border), new Point(border, r.W - border)),
                    Direction.Right => new Polygon(colour, new Point(r.H - border, r.H / 2), new Point(border, r.H - border), new Point(border, border)),
                    Direction.Down => new Polygon(colour, new Point(border, border), new Point(r.W - border, border), new Point(r.W / 2, r.W - border)),
                    Direction.Left => new Polygon(colour, new Point(border, r.H / 2), new Point(r.H - border, r.H - border), new Point(r.H - border, border)),
                    _ => throw new HException("Unexpected Direction {0} passed to HF.Graphics.GenTrianglePolygon", direction),
                };
            }
            #endregion

            #region LoadTexture
            /// <summary>
            /// If TextureDictionary contains the image, get it, otherwise create it and add it to the dictionary, then get it
            /// </summary>
            public static Texture LoadTexture(string path, TextureParameter minMagFilter = TextureParameter.Nearest)
            {
                if (HV.TextureDictionary.ContainsKey(path))
                    return HV.TextureDictionary[path];

                Texture t = GenTexture(path, minMagFilter);
                HV.TextureDictionary.Add(path, t);

                return t;
            }
            /// <summary>
            /// Provide a bitmap and its string name manually
            /// </summary>
            internal static Texture LoadTexture(System.Drawing.Bitmap bufferedImage, string textureName, TextureParameter minMagFilter = TextureParameter.Nearest)
            {
                if (HV.TextureDictionary.ContainsKey(textureName))
                    return HV.TextureDictionary[textureName];

                Texture t = GenTexture(bufferedImage, minMagFilter);
                HV.TextureDictionary.Add(textureName, t);

                return t;
            }
            #endregion

            #region LoadSpriteTexture
            /// <summary>
            /// Creates a Texture2D by loading it from file (32bit png type), or retrieves it from the texture dictionary if it has already been loaded. Pads transparent border around the cells of a spritesheet to prevent black lines appearing at top and bottom of images etc
            /// </summary>
            internal static Texture LoadSpriteTexture(string path, int spriteRows, int spriteColumns, TextureParameter minFilter, TextureParameter maxFilter, TextureParameter wrapMode)
            {
                if (HV.TextureDictionary.ContainsKey(path))
                    return HV.TextureDictionary[path];

                Texture t = GenPaddedTexture(path, spriteRows, spriteColumns, minFilter, maxFilter, wrapMode);
                HV.TextureDictionary.Add(path, t);

                return t;
            }

            /// <summary>
            /// Creates a Texture2D by loading it from file (32bit png type), or retrieves it from the texture dictionary if it has already been loaded. Pads transparent border around the cells of a spritesheet to prevent black lines appearing at top and bottom of images etc
            /// </summary>
            public static Texture LoadSpriteTexture(string path, int spriteRows, int spriteColumns, TextureParameter filter)
            {
                return LoadSpriteTexture(path, spriteRows, spriteColumns, filter, filter, TextureParameter.ClampToEdge);
            }

            #endregion

            #region NonZeroAlphaRegion
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
            #endregion

            #region SaveTextureToFile
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
            #endregion

            #region TextureToBitmap
            public static System.Drawing.Bitmap TextureToBitmap(Texture t)
            {
                System.Drawing.Bitmap bmp = new(t.Width, t.Height);
                OpenGL.BindTexture(TextureTarget.Texture2D, t.ID);
                System.Drawing.Imaging.BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                OpenGL.GetTexImage(TextureTarget.Texture2D, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bmp.UnlockBits(data);
                return bmp;
            }
            #endregion

            #region WriteBitmapToFile
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
            #endregion

            #region SaveBitmapToPNGFile
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
            #endregion
        }
        #endregion

        #region Maths
        public static class Maths
        {

            #region Angle
            /// <summary>
            /// Returns angle in degrees clockwise starting UP from P1
            /// </summary>
            public static double Angle(Point p1, Point p2)
            {
                var x = Math.Atan2(p2.X - p1.X, p1.Y - p2.Y);

                x = x / Math.PI * 180;

                if (x < 0)
                    x += 360;

                return x;
            }
            #endregion

            #region Clamp
            public static int Clamp(int num, int min, int max)
            {
                return Math.Max(Math.Min(num, max), min);
            }
            public static float Clamp(float num, float min, float max)
            {
                return Math.Max(Math.Min(num, max), min);
            }
            public static void Clamp(ref float num, float min, float max)
            {
                num = Math.Max(Math.Min(num, max), min);
            }
            public static void Clamp(ref int num, int min, int max)
            {
                num = Math.Max(Math.Min(num, max), min);
            }
            #endregion

            #region Dist
            public static float Dist(Point p1, Point p2)
            {
                return (float)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
            }
            public static float DistSquared(Point p1, Point p2)
            {
                return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
            }
            public static float DistGrid(Point p1, Point p2) => Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
            #endregion

            #region GetDirection
            /// <summary>
            /// Gets the nearest direction in moving between two points
            /// </summary>
            public static Direction GetDirection(Point start, Point end)
            {
                return (end - start).ToDirection();
            }
            #endregion

            #region IsInt
            public static bool IsInt(string s)
            {
                return int.TryParse(s, out _);
            }
            #endregion

            #region IsMultipleOf
            public static bool IsMultipleOf(int n, int mult)
            {
                while (n < 0)
                    n += mult;
                return n % mult == 0;
            }
            #endregion

            #region Min
            public static float Min(params float[] numbers)
            {
                if (numbers.Length == 0)
                    throw new ArgumentException("Min requires at least 1 number passing to it");

                var ret = numbers[0];

                for (int i = 0; i < numbers.Length; ++i)
                    if (numbers[i] < ret)
                        ret = numbers[i];

                return ret;
            }

            public static int Min(params int[] numbers)
            {
                if (numbers.Length == 0)
                    throw new ArgumentException("Min requires at least 1 number passing to it");

                var ret = numbers[0];

                for (int i = 0; i < numbers.Length; ++i)
                    if (numbers[i] < ret)
                        ret = numbers[i];

                return ret;
            }
            #endregion

            #region Max
            public static float Max(params float[] numbers)
            {
                if (numbers.Length == 0)
                    throw new ArgumentException("Min requires at least 1 number passing to it");

                var ret = numbers[0];

                for (int i = 0; i < numbers.Length; ++i)
                    if (numbers[i] > ret)
                        ret = numbers[i];

                return ret;
            }

            public static int Max(params int[] numbers)
            {
                if (numbers.Length == 0)
                    throw new ArgumentException("Min requires at least 1 number passing to it");

                var ret = numbers[0];

                for (int i = 0; i < numbers.Length; ++i)
                    if (numbers[i] > ret)
                        ret = numbers[i];

                return ret;
            }
            #endregion

            #region Mod
            public static int Mod(int x, int m)
            {
                while (x < 0)
                    x += m;

                return x % m;
            }

            public static float Mod(float x, float m)
            {
                while (x < 0)
                    x += m;

                return x % m;
            }

            public static double Mod(double x, double m)
            {
                while (x < 0)
                    x += m;

                return x % m;
            }
            #endregion

            #region Round
            public static int Round(float value) => (int)Math.Round(value);
            #endregion

            #region RectanglesIntersect
            public static bool RectanglesIntersect(float x1, float y1, float w1, float h1, float x2, float y2, float w2, float h2, bool touchingCounts = false)
            {
                if (touchingCounts)
                {
                    if (x1 > x2 + w2) return false;
                    if (x1 + w1 < x2) return false;
                    if (y1 > y2 + h2) return false;
                    if (y1 + h1 < y2) return false;
                    return true;
                }
                else
                {
                    if (x1 >= x2 + w2) return false;
                    if (x1 + w1 <= x2) return false;
                    if (y1 >= y2 + h2) return false;
                    if (y1 + h1 <= y2) return false;
                    return true;
                }
            }
            #endregion
        }
        #endregion

        #region Pathfinding
        public static class Pathfinding
        {
            #region ChooseRandomRoute
            public static List<INode> ChooseRandomRoute<N>(N startNode, Func<N, bool> passableTest, int maximumSteps)
                where N : INode
            {
                int steps = 0;
                List<N> openNodes = new();
                openNodes.Add(startNode);
                List<INode> path = new();
                N currentNode;

                while (steps <= maximumSteps)
                {
                    currentNode = openNodes[Randomisation.Rand(openNodes.Count)];
                    //replace open nodes with current node's connections, skipping any already in path
                    openNodes.Clear();
                    foreach (N n in currentNode.ConnectedNodes)
                    {
                        if (passableTest(n) && !path.Contains(n))
                            openNodes.Insert(Randomisation.Rand(0, openNodes.Count), n);
                    }

                    path.Add(currentNode);
                    steps++;
                    if (openNodes.Count == 0)
                        break;
                }
                return path;
            }
            #endregion

            #region GetAStarRoute
            public static List<N>? GetAStarRoute<N>(N start, N end, Func<N, bool> passableTest)
                where N : INode
            {
                List<N> openNodes = new();
                List<N> closedNodes = new();

                N currentNode = start;
                N testNode;

                float f, g, h;

                currentNode.PG = 0;
                currentNode.PH = Heuristic(currentNode, end);
                currentNode.PF = currentNode.PG + currentNode.PH;

                while (!currentNode.Equals(end))
                {
                    for (int i = 0; i < currentNode.ConnectedNodes.Count; ++i)
                    {
                        testNode = (N)currentNode.ConnectedNodes[i];

                        if (testNode.Equals(currentNode) || !passableTest(testNode))
                            continue;

                        g = (float)currentNode.PG + 1;
                        h = (float)Heuristic(testNode, end);
                        f = g + h;

                        if (openNodes.Contains(testNode) || closedNodes.Contains(testNode))
                        {
                            if (testNode.PF > f)
                            {
                                testNode.PF = f;
                                testNode.PG = g;
                                testNode.PH = h;
                                testNode.ParentNode = currentNode;
                            }
                        }
                        else
                        {
                            testNode.PF = f;
                            testNode.PG = g;
                            testNode.PH = h;
                            testNode.ParentNode = currentNode;
                            openNodes.Add(testNode);
                        }

                    }
                    closedNodes.Add(currentNode);

                    //HANDLE FAILURE TO FIND PATH
                    if (openNodes.Count == 0)
                    {
                        return null;
                    }

                    openNodes.Sort(CompareNodeF);
                    currentNode = openNodes[0];
                    openNodes.RemoveAt(0);
                }

                return BuildPath(start, end);
            }
            #endregion

            #region BuildPath
            private static List<N> BuildPath<N>(N start, N end)
                where N : INode
            {
                List<N> path = new();
                N node = end;
                path.Add(node);
                while (!(node.X == start.X && node.Y == start.Y))
                {
                    node = (N)node.ParentNode;

                    path.Insert(0, node);
                }

                return path;
            }
            #endregion

            #region Heuristic
            private static double Heuristic(INode node1, INode node2) => Math.Abs(node1.X - node2.X) + Math.Abs(node1.Y - node2.Y);
            #endregion

            #region CompareNodeF
            private static int CompareNodeF<N>(N x, N y)
                where N : INode
            {
                if (x.PF < y.PF) return -1;
                else if (x.PF == y.PF) return 0;
                else return 1;
            }
            #endregion
        }
        #endregion

        #region Randomisation
        public static class Randomisation
        {
            private static readonly Random _random = new();

            #region Chance
            /// <summary>
            /// returns true with probability (chanceOutOfAHundred)%
            /// </summary>
            public static bool Chance(float chanceOutOfAHundred) => Rand(0, 100) < chanceOutOfAHundred;
            #endregion

            #region Choose
            /// <summary>
            /// Returns one of a list of things, at random
            /// </summary>
            public static T Choose<T>(params T[] things) => things[Rand(things.Length)];
            /// <summary>
            /// Returns one of a list of things, at random
            /// </summary>
            public static T Choose<T>(List<T> things) => things[Rand(things.Count)];
            #endregion

            #region Rand
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
                    throw new HException("T ({0}) must be an enum.", typeof(T).Name);

                var values = Enum.GetValues(typeof(T));
                return (T)values.GetValue(_random.Next(values.Length));
            }
            #endregion

            public static Colour RandColour() => new((byte)Rand(255), (byte)Rand(255), (byte)Rand(255), 255);

            public static Point RandPoint() => new(RandF(2) - 1, RandF(2) - 1);

            #region RandDirection
            public static Direction RandDirection()
            {
                return Choose(Direction.Up, Direction.Right, Direction.Down, Direction.Left);
            }
            #endregion

            #region RandF
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
            #endregion

            #region RandGaussianApprox
            public static float RandGaussianApprox(float min, float max)
            {
                if (max <= min) return max;

                //guassian = approx (1 + cos x)/2PI [SD = 1] 
                //therefore (x+sinx)/2PI is integral, x from 0 to 2 PI gives 0-1 with normal distribution ish results

                double zeroTo2PI = 2 * Math.PI * _random.NextDouble();
                double zeroToOne = (zeroTo2PI + Math.Sin(zeroTo2PI)) / (2 * Math.PI);
                return min + (float)(zeroToOne * (max - min));
            }
            #endregion

            #region RandUpperCaseString
            public static string RandUpperCaseString(int chars)
            {
                string def = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                StringBuilder ret = new();
                for (int i = 0; i < chars; i++)
                    ret.Append(def.AsSpan(_random.Next(def.Length), 1));
                return ret.ToString();
            }
            #endregion

            #region RandWindowsColour
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
            #endregion

            #region Shuffle<T>
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
            #endregion
        }
        #endregion

        #region Types
        public static class Types
        {
            public static int GetSizeOf(Type t) => Marshal.SizeOf(t);

            #region GetUniqueFlags
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
            #endregion

            #region PrintCSVToConsole
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
            #endregion

            #region FindType
            /// <summary>
            /// returns the unique Type with the specified name in any of the loaded assemblies
            /// </summary>
            public static Type? FindType(string typeName, bool errorIfUnfound = false, bool errorIfDuplicate = false)
            {
                var types = AppDomain.CurrentDomain.GetAssemblies().Select(a => FindType(a.FullName, typeName));

                if (!types.Any())
                    if (errorIfUnfound)
                        throw new HException("Type {0} not found in the current domain.", typeName);
                    else
                        return null;
                else if (types.Count() > 1 && errorIfDuplicate)
                    throw new HException("Type {0} was not unique in the current domain.", typeName);
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
                        throw new HException("Type {0} not found in the current domain.", typeName);
                    else
                        return null;
                else if (types.Count() > 1 && errorIfDuplicate)
                    throw new HException("Type {0} was not unique in the current domain.", typeName);
                else
                    return types.First();
            }
            #endregion
        }
        #endregion

        #region Repeat
        public static void Repeat(Action function, int times)
        {
            for (int i = 0; i < times; ++i)
                function();
        }
        #endregion

        #region Windows
        public static class Windows
        {
            public static bool RunningOnWindows => Environment.OSVersion.Platform == PlatformID.Win32NT || Environment.OSVersion.Platform == PlatformID.Win32S || Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.WinCE;
        }
        #endregion
    }
}