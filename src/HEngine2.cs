using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using HaighFramework.DisplayDevices;
using HaighFramework.Input;
using HaighFramework.Window;
using HaighFramework;
using HaighFramework.OpenGL4;

namespace BearsEngine
{
    public class HEngine2 : IDisposable
    {
        private bool _disposed;
        public static void Run(WindowSettings settings)
        { 
            HV.Engine2 = new HEngine2(settings);
            HV.Engine2.Run();
        }

        #region Constructors
        public HEngine2(WindowSettings windowSettings)
        {
#if DEBUG
            HConsole.Show();
            HConsole.MoveConsoleTo(-7, 0, 450, HConsole.MaxHeight);
#endif

            Window = new HaighWindow2(windowSettings);
            Window.Resized += (s, e) => OpenGL.Viewport(0, 0, (int)e.Width, (int)e.Height);
        }
        #endregion

        public IWindow2 Window { get; set; }

        #region Run
        public void Run()
        {
            while (Window.IsOpen)
            {
                OpenGL.ClearColour(HV.ScreenColour);
                OpenGL.Clear(ClearBufferMask.ColourBufferBit | ClearBufferMask.DepthBufferBit);

                Window.ProcessEvents();

                if (Window.IsOpen)
                    Window.SwapBuffers();
            }
        }

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    Window.Dispose();
                }
                // TODO: warning if disposed from destructor

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposed = true;
            }
            // TODO: warning if disposed more than once
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~HEngine2()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
        #endregion
    }
}
