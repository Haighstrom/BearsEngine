using HaighFramework.DisplayDevices;
using HaighFramework.Input;
using HaighFramework.Window;

namespace BearsEngine
{
    public interface IEngine : IDisposable
    {
        IScene Scene { get; set; }

        IDisplayDeviceManager DisplayManager { get; }

        IInputDeviceManager InputManager { get; }

        IWindow Window { get; }

        bool DoPeriodicLogging { get; set; }

        void Run();
        void Run(int frameRate);
        void Run(int frameRate, int updateRate);

        int UpdateFPS { get; }
        int RenderFPS { get; }
    }
}