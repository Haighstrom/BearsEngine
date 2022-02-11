using HaighFramework;
using HaighFramework.Input;
using HaighFramework.Window;
using BearsEngine.Graphics;
using HaighFramework.DisplayDevices;

namespace BearsEngine
{
    public static class HV
    {
        #region Engine
        public static IScene Scene
        {
            get => Engine.Instance.Scene;
            set => Engine.Instance.Scene = value;
        }
        public static IWindow Window => Engine.Instance.Window;
        public static DisplayDeviceManager DisplayDeviceManager => Engine.Instance.DisplayDM;
        public static IMouseManager Mouse => Engine.Instance.InputDM.MouseManager;
        public static IKeyboardManager Keyboard => Engine.Instance.InputDM.KeyboardManager;
        #endregion

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
    }
}