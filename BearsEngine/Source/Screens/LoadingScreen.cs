using HaighFramework.OpenGL;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BearsEngine;

/// <summary>
/// Abstract base class to inherit from when making loading screen worlds that load assets on a seperate thread
/// </summary>
public abstract class LoadingScreen : Screen
{
    private static IntPtr CreateRenderContext(IntPtr deviceContext, (int major, int minor) openGLversion)
    {
        int[] attribs = {
            (int)ARBCREATECONTEXT_ATTRIBUTE.WGL_CONTEXT_MAJOR_VERSION_ARB, openGLversion.major,
            (int)ARBCREATECONTEXT_ATTRIBUTE.WGL_CONTEXT_MINOR_VERSION_ARB, openGLversion.minor,
            (int)ARBCREATECONTEXT_ATTRIBUTE.WGL_CONTEXT_PROFILE_MASK_ARB, (int)ARBCREATECONTEXT_ATTRIBUTE_VALUES.WGL_CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB,
            0 };

        var rC = OpenGL32.wglCreateContextAttribsARB(deviceContext, sharedContext: IntPtr.Zero, attribs);

        if (rC == IntPtr.Zero)
            throw new Win32Exception($"Something went wrong with wglCreateContextAttribsARB: {Marshal.GetLastWin32Error()}");

        return rC;
    }

    private IntPtr _loadingRenderContext;


    public LoadingScreen()
        : base()
    {
    }
    protected int ProgressPercent { get; private set; }

    /// <summary>
    /// Set this to false if you want assets to be loaded on-thread, instead of in a separate thread with a new render context and a BackgroundWorker. Do this before calling StartLoadingAssets!
    /// </summary>
    protected bool Multithreaded { get; set; } = true;

    /// <summary>
    /// Put all the loading ie GTex.Initialise in here - overide it in the Game
    /// </summary>
    /// <param name="worker">BackgroundWorker thread manager to do the loading on. Can be null if the operation was chosen not to be multithreaded.</param>
    protected abstract void LoadAssets(BackgroundWorker? worker);

    /// <summary>
    /// Load eg the background art for the loading screen on the main thread, before the loading thread is started and before the world draws.
    /// </summary>
    protected abstract void LoadAssetsForTheLoadingScreen();

    /// <summary>
    /// Function called when loading is completed
    /// </summary>
    protected abstract void LoadingComplete();

    private void DoWork(object? sender, DoWorkEventArgs e)
    {
        if (sender is not BackgroundWorker worker)
            throw new NullReferenceException();

        //Make the loading context current on the loading thread
        OpenGL32.wglMakeCurrent(Window.Instance.DeviceContextHandle, _loadingRenderContext);

        //Load the assets
        LoadAssets(worker);

        OpenGL32.glFlush();

        //Delete the loading graphics context
        OpenGL32.wglDeleteContext(_loadingRenderContext);

        //Dispose the loading thread
        worker.Dispose();
    }

    private void ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        ProgressPercent = e.ProgressPercentage;
    }

    private void RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        LoadingComplete();
    }

    public override void Start()
    {
        base.Start();
        LoadAssetsForTheLoadingScreen();
    }

    /// <summary>
    /// Push an action to load assets onto a new thread. A new OpenGL context with shared context will be created so that GL functions can be used eg Texture loading
    /// </summary>
    protected void StartLoadingAssets()
    {
        OpenGL.CheckOpenGLError("LoadingScreenWorld StartLoadingAssets  Start");

        if (!Multithreaded)
        {
            LoadAssets(null);
            LoadingComplete();
            return;
        }

        //Create a new OpenGL context to do the loading on
        _loadingRenderContext = CreateRenderContext(Window.Instance.DeviceContextHandle, (4, 5)); //todo: how to make this align to window?
        OpenGL.CheckOpenGLError("LoadingScreenWorld StartLoadingAssets CreateRenderContext Completed");

        //Share assets between the two contexts
        OpenGL32.wglShareLists(Window.Instance.RenderContextHandle, _loadingRenderContext);
        OpenGL.CheckOpenGLError("LoadingScreenWorld StartLoadingAssets WGLShareLists Completed");

        //Make sure the default render context remains current on this main thread
        OpenGL32.wglMakeCurrent(Window.Instance.DeviceContextHandle, Window.Instance.RenderContextHandle);
        OpenGL.CheckOpenGLError("LoadingScreenWorld StartLoadingAssets WGLMakeCurrent Completed");

        //Create a background worker thread to laod the assets and report progress
        BackgroundWorker loadingThread = new()
        {
            WorkerReportsProgress = true
        };

        //Subscribe to the threadss events
        loadingThread.DoWork += DoWork;
        loadingThread.ProgressChanged += ProgressChanged;
        loadingThread.RunWorkerCompleted += RunWorkerCompleted;

        OpenGL.CheckOpenGLError("LoadingScreenWorld StartLoadingAssets  End");

        //Start the thread
        loadingThread.RunWorkerAsync();
    }
}