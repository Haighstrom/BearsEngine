using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaighFramework;
using HaighFramework.Input;
using HaighFramework.Window;
using BearsEngine.Worlds;
using BearsEngine.Graphics;

namespace BearsEngine
{
    public static class HV
    {
        public static IEngine Engine { get; set; }
        public static HEngine2 Engine2 { get; set; }

        //Window Info
        public static IWindow Window => Engine.Window;
        public static IMouseManager Mouse => Engine.InputManager.MouseManager;
        public static IKeyboardManager Keyboard => Engine.InputManager.KeyboardManager;

        public static int FramesPerSecond => Engine.RenderFPS;
        public static int UpdatesPerSecond => Engine.UpdateFPS;
        //public static decimal MemoryUse { get; set; }

        //Timing
        public static double GameSpeed { get; set; } = 1;
        public static double ElapsedRenderTime { get; internal set; }
        public static double ElapsedUpdateTime { get; internal set; }
        public static double ElapsedGameTime => ElapsedUpdateTime * GameSpeed;

        //Graphics
        public static Colour ScreenColour { get; set; } = Colour.CornflowerBlue;
        public static Matrix4 OrthoMatrix;
        public static uint LastBoundTexture;
        public static uint LastBoundVertexBuffer;
        public static uint LastBoundFrameBuffer;
        public static uint LastBoundShader;

        public static Dictionary<string, Texture> TextureDictionary { get; set; } = new Dictionary<string, Texture>();

        //Worlds
        public static Screen Screen
        {
            get => Engine.Scene as Screen;
            set => Engine.Scene = value;
        }
    }
}
