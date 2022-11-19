using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace BearsEngine.Win32API;

/// <summary>
/// Windows management functions for message handling, timers, menus, and communications.
/// </summary>
[SuppressUnmanagedCodeSecurity]
internal static class User32
{
    private const string Library = "User32.dll";

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-adjustwindowrect
    /// Calculates the required size of the window rectangle, based on the desired client-rectangle size. The window rectangle can then be passed to the CreateWindow function to create a window whose client area is the desired size.
    ///To specify an extended window style, use the AdjustWindowRectEx function.
    /// </summary>
    /// <param name="lpRect">A pointer to a RECT structure that contains the coordinates of the top-left and bottom-right corners of the desired client area. When the function returns, the structure contains the coordinates of the top-left and bottom-right corners of the window to accommodate the desired client area.</param>
    /// <param name="dwStyle">The window style of the window whose required size is to be calculated. Note that you cannot specify the WS_OVERLAPPED style.</param>
    /// <param name="bMenu">Indicates whether the window has a menu.</param>
    ///<returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool AdjustWindowRect(ref RECT lpRect, WINDOWSTYLE dwStyle, [MarshalAs(UnmanagedType.Bool)] bool bMenu);

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-adjustwindowrectex
    /// Calculates the required size of the window rectangle, based on the desired size of the client rectangle. The window rectangle can then be passed to the CreateWindowEx function to create a window whose client area is the desired size.
    /// </summary>
    /// <param name="lpRect">A pointer to a RECT structure that contains the coordinates of the top-left and bottom-right corners of the desired client area. When the function returns, the structure contains the coordinates of the top-left and bottom-right corners of the window to accommodate the desired client area.</param>
    /// <param name="dwStyle">The window style of the window whose required size is to be calculated. Note that you cannot specify the WS_OVERLAPPED style.</param>
    /// <param name="bMenu">Indicates whether the window has a menu.</param>
    /// <param name="dwExStyle">The extended window style of the window whose required size is to be calculated.</param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool AdjustWindowRectEx(ref RECT lpRect, WINDOWSTYLE dwStyle, [MarshalAs(UnmanagedType.Bool)] bool bMenu, WINDOWSTYLEEX dwExStyle);

    /// <summary>
    /// Creates an overlapped, pop-up, or child window with an extended window style; otherwise, this function is identical to the CreateWindow function. For more information about creating a window and for full descriptions of the other parameters of CreateWindowEx, see CreateWindow.
    /// </summary>
    /// <param name="dwExStyle">The extended window style of the window being created. For a list of possible values, see Extended Window Styles.</param>
    /// <param name="lpClassName">A null-terminated string or a class atom created by a previous call to the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the high-order word must be zero. If lpClassName is a string, it specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, provided that the module that registers the class is also the module that creates the window. The class name can also be any of the predefined system class names.</param>
    /// <param name="lpWindowName">The window name. If the window style specifies a title bar, the window title pointed to by lpWindowName is displayed in the title bar. When using CreateWindow to create controls, such as buttons, check boxes, and static controls, use lpWindowName to specify the text of the control. When creating a static control with the SS_ICON style, use lpWindowName to specify the icon name or identifier. To specify an identifier, use the syntax "#num".</param>
    /// <param name="dwStyle">The style of the window being created. This parameter can be a combination of the window style values, plus the control styles indicated in the Remarks section.</param>
    /// <param name="X">The initial horizontal position of the window. For an overlapped or pop-up window, the x parameter is the initial x-coordinate of the window's upper-left corner, in screen coordinates. For a child window, x is the x-coordinate of the upper-left corner of the window relative to the upper-left corner of the parent window's client area. If x is set to CW_USEDEFAULT, the system selects the default position for the window's upper-left corner and ignores the y parameter. CW_USEDEFAULT is valid only for overlapped windows; if it is specified for a pop-up or child window, the x and y parameters are set to zero.</param>
    /// <param name="Y">The initial vertical position of the window. For an overlapped or pop-up window, the y parameter is the initial y-coordinate of the window's upper-left corner, in screen coordinates. For a child window, y is the initial y-coordinate of the upper-left corner of the child window relative to the upper-left corner of the parent window's client area. For a list box y is the initial y-coordinate of the upper-left corner of the list box's client area relative to the upper-left corner of the parent window's client area.
    /// If an overlapped window is created with the WS_VISIBLE style bit set and the x parameter is set to CW_USEDEFAULT, then the y parameter determines how the window is shown.If the y parameter is CW_USEDEFAULT, then the window manager calls ShowWindow with the SW_SHOW flag after the window has been created. If the y parameter is some other value, then the window manager calls ShowWindow with that value as the nCmdShow parameter.</param>
    /// <param name="nWidth">The width, in device units, of the window. For overlapped windows, nWidth is the window's width, in screen coordinates, or CW_USEDEFAULT. If nWidth is CW_USEDEFAULT, the system selects a default width and height for the window; the default width extends from the initial x-coordinates to the right edge of the screen; the default height extends from the initial y-coordinate to the top of the icon area. CW_USEDEFAULT is valid only for overlapped windows; if CW_USEDEFAULT is specified for a pop-up or child window, the nWidth and nHeight parameter are set to zero.</param>
    /// <param name="nHeight">The height, in device units, of the window. For overlapped windows, nHeight is the window's height, in screen coordinates. If the nWidth parameter is set to CW_USEDEFAULT, the system ignores nHeight.</param>
    /// <param name="hWndParent">A handle to the parent or owner window of the window being created. To create a child window or an owned window, supply a valid window handle. This parameter is optional for pop-up windows.
    /// To create a message-only window, supply HWND_MESSAGE or a handle to an existing message-only window.</param>
    /// <param name="hMenu">A handle to a menu, or specifies a child-window identifier, depending on the window style. For an overlapped or pop-up window, hMenu identifies the menu to be used with the window; it can be NULL if the class menu is to be used. For a child window, hMenu specifies the child-window identifier, an integer value used by a dialog box control to notify its parent about events. The application determines the child-window identifier; it must be unique for all child windows with the same parent window.</param>
    /// <param name="hInstance">A handle to the instance of the module to be associated with the window.</param>
    /// <param name="lpParam">Pointer to a value to be passed to the window through the CREATESTRUCT structure (lpCreateParams member) pointed to by the lParam param of the WM_CREATE message. This message is sent to the created window by this function before it returns.
    /// If an application calls CreateWindow to create a MDI client window, lpParam should point to a CLIENTCREATESTRUCT structure.If an MDI client window calls CreateWindow to create an MDI child window, lpParam should point to a MDICREATESTRUCT structure.lpParam may be NULL if no additional data is needed.</param>
    /// <returns>If the function succeeds, the return value is a handle to the new window.
    /// If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern IntPtr CreateWindowEx(WINDOWSTYLEEX dwExStyle, IntPtr lpClassName, IntPtr lpWindowName, WINDOWSTYLE dwStyle, int X, int Y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

    /// <summary>
    /// Calls the default window procedure to provide default processing for any window messages that an application does not process. This function ensures that every message is processed. DefWindowProc is called with the same parameters received by the window procedure.
    /// </summary>
    /// <param name="hWnd">A handle to the window procedure that received the message.</param>
    /// <param name="msg">The message.</param>
    /// <param name="wParam">Additional message information. The content of this parameter depends on the value of the Msg parameter.</param>
    /// <param name="lParam">Additional message information. The content of this parameter depends on the value of the Msg parameter.</param>
    /// <returns>The return value is the result of the message processing and depends on the message.</returns>
    [DllImport(Library, CharSet = CharSet.Auto)]
    public extern static IntPtr DefWindowProc(IntPtr hWnd, WINDOWMESSAGE msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Destroys a cursor and frees any memory the cursor occupied. Do not use this function to destroy a shared cursor.
    /// </summary>
    /// <param name="hCursor">A handle to the cursor to be destroyed. The cursor must not be in use.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool DestroyCursor(IntPtr hCursor);

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-destroywindow
    /// Destroys the specified window. The function sends WM_DESTROY and WM_NCDESTROY messages to the window to deactivate it and remove the keyboard focus from it. The function also destroys the window's menu, flushes the thread message queue, destroys timers, removes clipboard ownership, and breaks the clipboard viewer chain (if the window is at the top of the viewer chain).
    /// If the specified window is a parent or owner window, DestroyWindow automatically destroys the associated child or owned windows when it destroys the parent or owner window. The function first destroys child or owned windows, and then it destroys the parent or owner window.
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library)]
    public static extern bool DestroyWindow(IntPtr hWnd);

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-dispatchmessage
    /// Dispatches a message to a window procedure. It is typically used to dispatch a message retrieved by the GetMessage function.
    /// </summary>
    /// <param name="lpMsg">A pointer to a structure that contains the message.</param>
    /// <returns>The return value specifies the value returned by the window procedure. Although its meaning depends on the message being dispatched, the return value generally is ignored.</returns>
    [DllImport(Library)]
    public static extern IntPtr DispatchMessage(ref MSG lpMsg);

    /// <summary>
    /// Retrieves the position of the mouse cursor, in screen coordinates. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getcursorpos
    /// </summary>
    /// <param name="point">Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.A pointer to a POINT structure that receives the screen coordinates of the cursor.</param>
    /// <returns>Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool GetCursorPos(ref POINT lpPoint);

    /// <summary>
    /// Retrieves a string that represents the name of a key. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeynametexta
    /// </summary>
    /// <param name="lParam">The second parameter of the keyboard message (such as WM_KEYDOWN) to be processed. The function interprets the following bit positions in the lParam.
    /// 16-23	Scan code.
    /// 24	Extended-key flag. Distinguishes some keys on an enhanced keyboard.
    /// 25	"Do not care" bit. The application calling this function sets this bit to indicate that the function should not distinguish between left and right CTRL and SHIFT keys, for example.</param>
    /// <param name="lpString">The buffer that will receive the key name.</param>
    /// <param name="cchSize">The maximum length, in characters, of the key name, including the terminating null character. (This parameter should be equal to the size of the buffer pointed to by the lpString parameter.)</param>
    /// <returns>If the function succeeds, a null-terminated string is copied into the specified buffer, and the return value is the length of the string, in characters, not counting the terminating null character. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern int GetKeyNameText(int lParam, [Out] StringBuilder lpString, int cchSize);

    /// <summary>
    /// Retrieves a string that represents the name of a key. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeynametexta
    /// </summary>
    /// <param name="extendedScanCode"></param>
    /// <returns>If the function succeeds, a null-terminated string is copied into the specified buffer, and the return value is the length of the string, in characters, not counting the terminating null character. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    public static string GetKeyNameText(uint extendedScanCode)
    {
        int StringMaxLength = 512;
        int key = (int)extendedScanCode;
        StringBuilder s = new(StringMaxLength);
        _ = GetKeyNameText(key, s, StringMaxLength);

        return s.ToString();
    }

    /// <summary>
    /// Retrieves a string that represents the name of a key. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeynametexta
    /// </summary>
    /// <param name="scanCode"></param>
    /// <param name="isE0"></param>
    /// <returns>If the function succeeds, a null-terminated string is copied into the specified buffer, and the return value is the length of the string, in characters, not counting the terminating null character. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    public static string GetKeyNameText(uint scanCode, bool isE0)
    {
        int StringMaxLength = 512;
        int key = (int)((scanCode << 16) | ((isE0 ? 1 : (uint)0) << 24));
        StringBuilder s = new(StringMaxLength);
        _ = GetKeyNameText(key, s, StringMaxLength);

        return s.ToString();
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmessage
    /// Retrieves a message from the calling thread's message queue. The function dispatches incoming sent messages until a posted message is available for retrieval.
    /// Unlike GetMessage, the PeekMessage function does not wait for a message to be posted before returning.
    /// </summary>
    /// <param name="lpMsg">A pointer to an MSG structure that receives message information from the thread's message queue.</param>
    /// <param name="hWnd">A handle to the window whose messages are to be retrieved. The window must belong to the current thread
    /// If hWnd is NULL, GetMessage retrieves messages for any window that belongs to the current thread, and any messages on the current thread's message queue whose hwnd value is NULL (see the MSG structure). Therefore if hWnd is NULL, both window messages and thread messages are processed.
    /// If hWnd is -1, GetMessage retrieves only messages on the current thread's message queue whose hwnd value is NULL, that is, thread messages as posted by PostMessage (when the hWnd parameter is NULL) or PostThreadMessage.</param>
    /// <param name="wMsgFilterMin">The integer value of the lowest message value to be retrieved. Use WM_KEYFIRST (0x0100) to specify the first keyboard message or WM_MOUSEFIRST (0x0200) to specify the first mouse message.
    /// Use WM_INPUT here and in wMsgFilterMax to specify only the WM_INPUT messages.
    /// If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages(that is, no range filtering is performed).</param>
    /// <param name="wMsgFilterMax">The integer value of the highest message value to be retrieved. Use WM_KEYLAST to specify the last keyboard message or WM_MOUSELAST to specify the last mouse message.
    /// Use WM_INPUT here and in wMsgFilterMin to specify only the WM_INPUT messages.
    /// If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages(that is, no range filtering is performed).</param>
    /// <returns>If the function retrieves a message other than WM_QUIT, the return value is nonzero. If the function retrieves the WM_QUIT message, the return value is zero. If there is an error, the return value is -1. For example, the function fails if hWnd is an invalid window handle or lpMsg is an invalid pointer.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern int GetMessage(ref MSG lpMsg, IntPtr hWnd, int wMsgFilterMin, int wMsgFilterMax);

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getqueuestatus
    /// Retrieves the type of messages found in the calling thread's message queue.
    /// </summary>
    /// <param name="flags">The types of messages for which to check.</param>
    /// <returns>The high-order word of the return value indicates the types of messages currently in the queue. The low-order word indicates the types of messages that have been added to the queue and that are still in the queue since the last call to the GetQueueStatus, GetMessage, or PeekMessage function.</returns>
    [DllImport(Library)]
    public static extern uint GetQueueStatus(GetQueueStatus_flags flags);

    /// <summary>
    /// Retrieves the raw input from the specified device. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getrawinputdata
    /// </summary>
    /// <param name="hRawInput">A handle to the RAWINPUT structure. This comes from the lParam in WM_INPUT.</param>
    /// <param name="uiCommand">
    /// The command flag. This parameter can be one of the following values.
    /// RID_HEADER: Get the header information from the RAWINPUT structure.
    /// RID_INPUT: Get the raw data from the RAWINPUT structure.
    /// </param>
    /// <param name="pData">A pointer to the data that comes from the RAWINPUT structure. This depends on the value of uiCommand. If pData is NULL, the required size of the buffer is returned in *pcbSize.</param>
    /// <param name="pcbSize">Pointer to a variable that specifies the size, in bytes, of the data in Data.</param>
    /// <param name="cbSizeHeader">Size, in bytes, of RawInputHeader.</param>
    /// <remarks>GetRawInputData gets the raw input one RAWINPUT structure at a time. In contrast, GetRawInputBuffer gets an array of RAWINPUT structures.</remarks>
    [DllImport(Library)]
    public static extern int GetRawInputData(IntPtr hRawInput, GetRawInputData_uiCommand uiCommand, [Out] IntPtr pData, [In, Out] ref int pcbSize, int cbSizeHeader);

    /// <summary>
    /// Retrieves the raw input from the specified device. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getrawinputdata
    /// </summary>
    /// <param name="hRawInput">A handle to the RAWINPUT structure. This comes from the lParam in WM_INPUT.</param>
    /// <param name="uiCommand">
    /// The command flag. This parameter can be one of the following values.
    /// RID_HEADER: Get the header information from the RAWINPUT structure.
    /// RID_INPUT: Get the raw data from the RAWINPUT structure.
    /// </param>
    /// <param name="pData">A pointer to the data that comes from the RAWINPUT structure. This depends on the value of uiCommand. If pData is NULL, the required size of the buffer is returned in *pcbSize.</param>
    /// <param name="pcbSize">Pointer to a variable that specifies the size, in bytes, of the data in Data.</param>
    /// <param name="cbSizeHeader">Size, in bytes, of RawInputHeader.</param>
    /// <remarks>GetRawInputData gets the raw input one RAWINPUT structure at a time. In contrast, GetRawInputBuffer gets an array of RAWINPUT structures.</remarks>
    [DllImport(Library)]
    public static extern int GetRawInputData(IntPtr hRawInput, GetRawInputData_uiCommand uiCommand, [Out] out RawInput pData, [In, Out] ref int pcbSize, int cbSizeHeader);

    /// <summary>
    /// Retrieves information about the raw input device.
    /// </summary>
    /// <param name="hDevice">A handle to the raw input device. This comes from the hDevice member of RAWINPUTHEADER or from GetRawInputDeviceList.</param>
    /// <param name="uiCommand">Specifies what data will be returned in pData.</param>
    /// <param name="pData">A pointer to a buffer that contains the information specified by uiCommand. If uiCommand is RIDI_DEVICEINFO, set the cbSize member of RID_DEVICE_INFO to sizeof(RID_DEVICE_INFO) before calling GetRawInputDeviceInfo.</param>
    /// <param name="pcbSize">The size, in bytes, of the data in pData.</param>
    /// <returns>If successful, this function returns a non-negative number indicating the number of bytes copied to pData. If pData is not large enough for the data, the function returns -1. If pData is NULL, the function returns a value of zero.In both of these cases, pcbSize is set to the minimum size required for the pData buffer. Call GetLastError to identify any other errors.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern int GetRawInputDeviceInfo(IntPtr hDevice, GetRawInputDeviceInfo_uiCommand uiCommand, RID_DEVICE_INFO pData, ref uint pcbSize);

    /// <summary>
    /// Retrieves information about the raw input device.
    /// </summary>
    /// <param name="hDevice">A handle to the raw input device. This comes from the hDevice member of RAWINPUTHEADER or from GetRawInputDeviceList.</param>
    /// <param name="pData">A pointer to a buffer that contains the information specified by uiCommand. If uiCommand is RIDI_DEVICEINFO, set the cbSize member of RID_DEVICE_INFO to sizeof(RID_DEVICE_INFO) before calling GetRawInputDeviceInfo.</param>
    /// <returns>If successful, this function returns a non-negative number indicating the number of bytes copied to pData. If pData is not large enough for the data, the function returns -1. If pData is NULL, the function returns a value of zero.In both of these cases, pcbSize is set to the minimum size required for the pData buffer. Call GetLastError to identify any other errors.</returns>
    public static int GetRawInputDeviceInfo(IntPtr hDevice, out RID_DEVICE_INFO pData)
    {
        pData = new RID_DEVICE_INFO();
        uint size = pData.cbSize;
        return GetRawInputDeviceInfo(hDevice, GetRawInputDeviceInfo_uiCommand.RIDI_DEVICEINFO, pData, ref size);
    }

    /// <summary>
    /// Retrieves the current color of the specified display element. Display elements are the parts of a window and the display that appear on the system display screen. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsyscolor
    /// </summary>
    /// <param name="nIndex">The display element whose color is to be retrieved.</param>
    /// <returns>The function returns the red, green, blue (RGB) color value of the given element.
    /// If the nIndex parameter is out of range, the return value is zero.Because zero is also a valid RGB value, you cannot use GetSysColor to determine whether a system color is supported by the current platform.Instead, use the GetSysColorBrush function, which returns NULL if the color is not supported.</returns>
    [DllImport(Library)]
    public static extern uint GetSysColor(GetSysColor_nIndex nIndex);

    /// <summary>
    /// The GetSysColorBrush function retrieves a handle identifying a logical brush that corresponds to the specified color index. https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsyscolorbrush
    /// </summary>
    /// <param name="nIndex">A color index. This value corresponds to the color used to paint one of the window elements. See GetSysColor for system color index values.</param>
    /// <returns>The return value identifies a logical brush if the nIndex parameter is supported by the current platform. Otherwise, it returns NULL.</returns>
    [DllImport(Library)]
    public static extern uint GetSysColorBrush(uint nIndex);

    /// <summary>
    /// It is recommended that you set the process-default DPI awareness via application manifest. See Setting the default DPI awareness for a process for more information. Setting the process-default DPI awareness via API call can lead to unexpected application behavior. Sets the current process to a specified dots per inch(dpi) awareness context. The DPI awareness contexts are from the DPI_AWARENESS_CONTEXT value.
    /// </summary>
    /// <param name="value">A DPI_AWARENESS_CONTEXT handle to set.</param>
    /// <returns>This function returns TRUE if the operation was successful, and FALSE otherwise. To get extended error information, call GetLastError. Possible errors are ERROR_INVALID_PARAMETER for an invalid input, and ERROR_ACCESS_DENIED if the default API awareness mode for the process has already been set(via a previous API call or within the application manifest).</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool SetProcessDpiAwarenessContext(IntPtr value);

    /// <summary>
    /// It is recommended that you set the process-default DPI awareness via application manifest. See Setting the default DPI awareness for a process for more information. Setting the process-default DPI awareness via API call can lead to unexpected application behavior. Sets the current process to a specified dots per inch(dpi) awareness context. The DPI awareness contexts are from the DPI_AWARENESS_CONTEXT value.
    /// </summary>
    /// <param name="value">A DPI_AWARENESS_CONTEXT handle to set.</param>
    /// <returns>This function returns TRUE if the operation was successful, and FALSE otherwise. To get extended error information, call GetLastError. Possible errors are ERROR_INVALID_PARAMETER for an invalid input, and ERROR_ACCESS_DENIED if the default API awareness mode for the process has already been set(via a previous API call or within the application manifest).</returns>
    public static bool SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT value) => SetProcessDpiAwarenessContext(new IntPtr((int)value));

    /// <summary>
    /// Set the DPI awareness for the current thread to the provided value.
    /// </summary>
    /// <param name="dpiContext">The new DPI_AWARENESS_CONTEXT for the current thread. This context includes the DPI_AWARENESS value.</param>
    /// <returns>The old DPI_AWARENESS_CONTEXT for the thread. If the dpiContext is invalid, the thread will not be updated and the return value will be NULL. You can use this value to restore the old DPI_AWARENESS_CONTEXT after overriding it with a predefined value.</returns>
    [DllImport(Library)]
    public static extern IntPtr SetThreadDpiAwarenessContext(IntPtr dpiContext);

    /// <summary>
    /// Set the DPI awareness for the current thread to the provided value.
    /// </summary>
    /// <param name="dpiContext">The new DPI_AWARENESS_CONTEXT for the current thread. This context includes the DPI_AWARENESS value.</param>
    /// <returns>The old DPI_AWARENESS_CONTEXT for the thread. If the dpiContext is invalid, the thread will not be updated and the return value will be NULL. You can use this value to restore the old DPI_AWARENESS_CONTEXT after overriding it with a predefined value.</returns>
    public static IntPtr SetThreadDpiAwarenessContext(DPI_AWARENESS_CONTEXT dpiContext) => SetThreadDpiAwarenessContext(new IntPtr((int)dpiContext));

    /// <summary>
    /// Posts messages when the mouse pointer leaves a window or hovers over a window for a specified amount of time.
    /// </summary>
    /// <param name="lpEventTrack">A pointer to a TRACKMOUSEEVENT structure that contains tracking information.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT lpEventTrack);

    /// <summary>
    /// Closes the specified device notification handle.
    /// </summary>
    /// <param name="handle">Device notification handle returned by the RegisterDeviceNotification function.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern bool UnregisterDeviceNotification(IntPtr handle);

    // ***CLEANED UP ABOVE THIS LINE***

    //todo: simplify
    //https://www.pinvoke.net/default.aspx/user32.getwindowlong
    //https://stackoverflow.com/questions/336633/how-to-detect-windows-64-bit-platform-with-net?rq=1

    public static UIntPtr GetWindowLong(IntPtr handle, GWL index)
    {
        if (IntPtr.Size == 4)
            return (UIntPtr)GetWindowLongInternal(handle, index);

        return GetWindowLongPtrInternal(handle, index);
    }

    [SuppressUnmanagedCodeSecurity]
    [DllImport(Library, SetLastError = true, EntryPoint = "GetWindowLong")]
    public static extern uint GetWindowLongInternal(IntPtr hWnd, GWL nIndex);

    [SuppressUnmanagedCodeSecurity]
    [DllImport(Library, SetLastError = true, EntryPoint = "GetWindowLongPtr")]
    public static extern UIntPtr GetWindowLongPtrInternal(IntPtr hWnd, GWL nIndex);
    
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtexta
    /// Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is a control, the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in another application.
    /// </summary>
    /// <param name="hWnd">A handle to the window or control containing the text.</param>
    /// <param name="lpString">The buffer that will receive the text. If the string is as long or longer than the buffer, the string is truncated and terminated with a null character.</param>
    /// <param name="nMaxCount">The maximum number of characters to copy to the buffer, including the null character. If the text exceeds this limit, it is truncated.</param>
    /// <returns></returns>
    [DllImport(Library, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextlengtha
    /// Retrieves the length, in characters, of the specified window's title bar text (if the window has a title bar). If the specified window is a control, the function retrieves the length of the text within the control. However, GetWindowTextLength cannot retrieve the length of the text of an edit control in another application.
    /// </summary>
    /// <param name="hWnd">A handle to the window or control.</param>
    /// <returns></returns>
    [DllImport(Library, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowTextLength(IntPtr hWnd);
    
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-loadimagew
    /// Loads an icon, cursor, animated cursor, or bitmap.
    /// </summary>
    /// <param name="hInst">A handle to the module of either a DLL or executable (.exe) that contains the image to be loaded. For more information, see GetModuleHandle. Note that as of 32-bit Windows, an instance handle (HINSTANCE), such as the application instance handle exposed by system function call of WinMain, and a module handle (HMODULE) are the same thing.
    /// To load an OEM image, set this parameter to NULL.
    /// To load a stand-alone resource (icon, cursor, or bitmap file) — for example, c:\myimage.bmp — set this parameter to NULL.</param>
    /// <param name="name"></param>
    /// <param name="type">The type of image to be loaded.</param>
    /// <param name="cx">The width, in pixels, of the icon or cursor. If this parameter is zero and the fuLoad parameter is LR_DEFAULTSIZE, the function uses the SM_CXICON or SM_CXCURSOR system metric value to set the width. If this parameter is zero and LR_DEFAULTSIZE is not used, the function uses the actual resource width.</param>
    /// <param name="cy">The height, in pixels, of the icon or cursor. If this parameter is zero and the fuLoad parameter is LR_DEFAULTSIZE, the function uses the SM_CYICON or SM_CYCURSOR system metric value to set the height. If this parameter is zero and LR_DEFAULTSIZE is not used, the function uses the actual resource height.</param>
    /// <param name="fuLoad"></param>
    /// <returns>If the function succeeds, the return value is the handle of the newly loaded image.
    /// If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr LoadImage(IntPtr hInst, ushort name, LoadImage_Type type, int cx, int cy, LoadImage_FULoad fuLoad);

    public static IntPtr LoadImage(PredefinedIcons icon) => LoadImage(IntPtr.Zero, (ushort)icon, LoadImage_Type.IMAGE_ICON, 0, 0, LoadImage_FULoad.LR_SHARED);
    public static IntPtr LoadImage(PredefinedCursors cursor) => LoadImage(IntPtr.Zero, (ushort)cursor, LoadImage_Type.IMAGE_CURSOR, 0, 0, LoadImage_FULoad.LR_SHARED);
    
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mapvirtualkeya
    /// </summary>
    /// <param name="uCode">The virtual key code or scan code for a key. How this value is interpreted depends on the value of the uMapType parameter.
    /// Starting with Windows Vista, the high byte of the uCode value can contain either 0xe0 or 0xe1 to specify the extended scan code.</param>
    /// <param name="uMapType">The translation to be performed. The value of this parameter depends on the value of the uCode parameter.
    /// MAPVK_VK_TO_VSC: The uCode parameter is a virtual-key code and is translated into a scan code.If it is a virtual-key code that does not distinguish between left- and right-hand keys, the left-hand scan code is returned.If there is no translation, the function returns 0.
    /// MAPVK_VSC_TO_VK: The uCode parameter is a scan code and is translated into a virtual-key code that does not distinguish between left- and right-hand keys.If there is no translation, the function returns 0.
    /// MAPVK_VK_TO_CHAR: The uCode parameter is a virtual-key code and is translated into an unshifted character value in the low order word of the return value.Dead keys(diacritics) are indicated by setting the top bit of the return value.If there is no translation, the function returns 0.
    /// MAPVK_VSC_TO_VK_EX: The uCode parameter is a scan code and is translated into a virtual-key code that distinguishes between left- and right-hand keys.If there is no translation, the function returns 0.
    /// MAPVK_VK_TO_VSC_EX: Windows Vista and later: The uCode parameter is a virtual-key code and is translated into a scan code.If it is a virtual-key code that does not distinguish between left- and right-hand keys, the left-hand scan code is returned.If the scan code is an extended scan code, the high byte of the uCode value can contain either 0xe0 or 0xe1 to specify the extended scan code.If there is no translation, the function returns 0.</param>
    /// <returns>The return value is either a scan code, a virtual-key code, or a character value, depending on the value of uCode and uMapType. If there is no translation, the return value is zero.</returns>
    [DllImport(Library, SetLastError = true)]
    public static extern uint MapVirtualKey(VirtualKeys uCode, MapVirtualKey_uMapType uMapType);
    
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-peekmessagew
    /// Dispatches incoming sent messages, checks the thread message queue for a posted message, and retrieves the message (if any exist).
    /// </summary>
    /// <param name="lpMsg">A pointer to an MSG structure that receives message information.</param>
    /// <param name="hWnd">A handle to the window whose messages are to be retrieved. The window must belong to the current thread.
    /// If hWnd is NULL, PeekMessage retrieves messages for any window that belongs to the current thread, and any messages on the current thread's message queue whose hwnd value is NULL (see the MSG structure). Therefore if hWnd is NULL, both window messages and thread messages are processed.
    /// If hWnd is -1, PeekMessage retrieves only messages on the current thread's message queue whose hwnd value is NULL, that is, thread messages as posted by PostMessage (when the hWnd parameter is NULL) or PostThreadMessage.</param>
    /// <param name="wMsgFilterMin">The value of the first message in the range of messages to be examined. Use WM_KEYFIRST (0x0100) to specify the first keyboard message or WM_MOUSEFIRST (0x0200) to specify the first mouse message.
    /// If wMsgFilterMin and wMsgFilterMax are both zero, PeekMessage returns all available messages(that is, no range filtering is performed).</param>
    /// <param name="wMsgFilterMax">The value of the last message in the range of messages to be examined. Use WM_KEYLAST to specify the last keyboard message or WM_MOUSELAST to specify the last mouse message.
    /// If wMsgFilterMin and wMsgFilterMax are both zero, PeekMessage returns all available messages(that is, no range filtering is performed).</param>
    /// <param name="wRemoveMsg">Specifies how messages are to be handled.</param>
    /// <returns>If a message is available, the return value is nonzero.
    /// If no messages are available, the return value is zero.</returns>
    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PeekMessage(ref MSG lpMsg, IntPtr hWnd, WINDOWMESSAGE wMsgFilterMin, WINDOWMESSAGE wMsgFilterMax, PeekMessage_Flags wRemoveMsg);
    
    /// <summary>
    /// Places (posts) a message in the message queue associated with the thread that created the specified window and returns without waiting for the thread to process the message.
    /// To post a message in the message queue associated with a thread, use the PostThreadMessage function.
    /// </summary>
    /// <param name="hWnd">A handle to the window whose window procedure is to receive the message.</param>
    /// <param name="Msg">The message to be posted. For lists of the system-provided messages, see System-Defined Messages.
    /// https://docs.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues </param>
    /// <param name="wParam">Additional message-specific information.</param>
    /// <param name="lParam">Additional message-specific information.</param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.GetLastError returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.</returns>
    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PostMessage(IntPtr hWnd, WINDOWMESSAGE Msg, IntPtr wParam, IntPtr lParam);
    
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-postquitmessage
    /// Indicates to the system that a thread has made a request to terminate (quit). It is typically used in response to a WM_DESTROY message.
    /// </summary>
    /// <param name="nExitCode"></param>
    [DllImport(Library)]
    public static extern void PostQuitMessage(int nExitCode);
    

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassexw
    /// Registers a window class for subsequent use in calls to the CreateWindow or CreateWindowEx function.
    /// </summary>
    /// <param name="unnamedParam1">A pointer to a WNDCLASSEX structure. You must fill the structure with the appropriate class attributes before passing it to the function.</param>
    /// <returns>If the function succeeds, the return value is a class atom that uniquely identifies the class being registered. This atom can only be used by the CreateWindow, CreateWindowEx, GetClassInfo, GetClassInfoEx, FindWindow, FindWindowEx, and UnregisterClass functions and the IActiveIMMap::FilterClientWindows method.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern ushort RegisterClassEx(ref WNDCLASSEX unnamedParam1);
    
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerdevicenotificationa
    /// Registers the device or type of device for which a window will receive notifications.
    /// </summary>
    /// <param name="hRecipient">A handle to the window or service that will receive device events for the devices specified in the NotificationFilter parameter. The same window handle can be used in multiple calls to RegisterDeviceNotification.
    /// Services can specify either a window handle or service status handle.</param>
    /// <param name="NotificationFilter">A pointer to a block of data that specifies the type of device for which notifications should be sent. This block always begins with the DEV_BROADCAST_HDR structure. The data following this header is dependent on the value of the dbch_devicetype member, which can be DBT_DEVTYP_DEVICEINTERFACE or DBT_DEVTYP_HANDLE. For more information, see Remarks.</param>
    /// <param name="Flags">This parameter can be one of the following values:
    /// DEVICE_NOTIFY_WINDOW_HANDLE: The hRecipient parameter is a window handle.
    /// DEVICE_NOTIFY_SERVICE_HANDLE: The hRecipient parameter is a service status handle.
    /// DEVICE_NOTIFY_ALL_INTERFACE_CLASSES: Notifies the recipient of device interface events for all device interface classes. (The dbcc_classguid member is ignored.) This value can be used only if the dbch_devicetype member is DBT_DEVTYP_DEVICEINTERFACE.
    /// </param>
    /// <returns>If the function succeeds, the return value is a device notification handle.
    /// If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
    /// <remarks>Applications send event notifications using the BroadcastSystemMessage function. Any application with a top-level window can receive basic notifications by processing the WM_DEVICECHANGE message. Applications can use the RegisterDeviceNotification function to register to receive device notifications.
    /// Services can use the RegisterDeviceNotification function to register to receive device notifications.If a service specifies a window handle in the hRecipient parameter, the notifications are sent to the window procedure. If hRecipient is a service status handle, SERVICE_CONTROL_DEVICEEVENT notifications are sent to the service control handler.For more information about the service control handler, see HandlerEx.
    /// Be sure to handle Plug and Play device events as quickly as possible.Otherwise, the system may become unresponsive.If your event handler is to perform an operation that may block execution (such as I/O), it is best to start another thread to perform the operation asynchronously.
    /// Device notification handles returned by RegisterDeviceNotification must be closed by calling the UnregisterDeviceNotification function when they are no longer needed.
    /// The DBT_DEVICEARRIVAL and DBT_DEVICEREMOVECOMPLETE events are automatically broadcast to all top-level windows for port devices. Therefore, it is not necessary to call RegisterDeviceNotification for ports, and the function fails if the dbch_devicetype member is DBT_DEVTYP_PORT.Volume notifications are also broadcast to top-level windows, so the function fails if dbch_devicetype is DBT_DEVTYP_VOLUME.OEM-defined devices are not used directly by the system, so the function fails if dbch_devicetype is DBT_DEVTYP_OEM.</remarks>
    [DllImport(Library)]
    public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr NotificationFilter, RegisterDeviceNotification_Flags Flags);
    
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerrawinputdevices
    /// Registers the devices that supply the raw input data.
    /// </summary>
    /// <param name="pRawInputDevices">An array of RAWINPUTDEVICE structures that represent the devices that supply the raw input.</param>
    /// <param name="uiNumDevices">The number of RAWINPUTDEVICE structures pointed to by pRawInputDevices.</param>
    /// <param name="cbSize">The size, in bytes, of a RAWINPUTDEVICE structure.</param>
    /// <returns>TRUE if the function succeeds; otherwise, FALSE. If the function fails, call GetLastError for more information.</returns>
    /// <remarks>To receive WM_INPUT messages, an application must first register the raw input devices using RegisterRawInputDevices. By default, an application does not receive raw input.
    /// To receive WM_INPUT_DEVICE_CHANGE messages, an application must specify the RIDEV_DEVNOTIFY flag for each device class that is specified by the usUsagePage and usUsage fields of the RAWINPUTDEVICE structure.By default, an application does not receive WM_INPUT_DEVICE_CHANGE notifications for raw input device arrival and removal.
    /// If a RAWINPUTDEVICE structure has the RIDEV_REMOVE flag set and the hwndTarget parameter is not set to NULL, then parameter validation will fail.
    /// Only one window per raw input device class may be registered to receive raw input within a process(the window passed in the last call to RegisterRawInputDevices). Because of this, RegisterRawInputDevices should not be used from a library, as it may interfere with any raw input processing logic already present in applications that load it./// </remarks>
    [DllImport(Library, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerrawinputdevices
    /// Registers the devices that supply the raw input data.
    /// </summary>
    /// <param name="rawInputDevices">An array of RAWINPUTDEVICE structures that represent the devices that supply the raw input.</param>
    /// <returns>TRUE if the function succeeds; otherwise, FALSE. If the function fails, call GetLastError for more information.</returns>
    public static bool RegisterRawInputDevices(RAWINPUTDEVICE[] rawInputDevices) => RegisterRawInputDevices(rawInputDevices, (uint)rawInputDevices.Length, RAWINPUTDEVICE.s_size);

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerrawinputdevices
    /// Registers the devices that supply the raw input data.
    /// </summary>
    /// <param name="rawInputDevice">A RAWINPUTDEVICE structure that represent the device that supplies the raw input.</param>
    /// <returns>TRUE if the function succeeds; otherwise, FALSE. If the function fails, call GetLastError for more information.</returns>
    public static bool RegisterRawInputDevices(RAWINPUTDEVICE rawInputDevice) => RegisterRawInputDevices(new[] { rawInputDevice });
    
    /// <summary>
    /// Releases the mouse capture from a window in the current thread and restores normal mouse input processing. A window that has captured the mouse receives all mouse input, regardless of the position of the cursor, except when a mouse button is clicked while the cursor hot spot is in the window of another thread.
    /// </summary>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
    [DllImport(Library)]
    public static extern bool ReleaseCapture();
    

    /// <summary>
    /// Sets the mouse capture to the specified window belonging to the current thread. SetCapture captures mouse input either when the mouse is over the capturing window, or when the mouse button was pressed while the mouse was over the capturing window and the button is still down. Only one window at a time can capture the mouse.
    /// If the mouse cursor is over a window created by another thread, the system will dRect mouse input to the specified window only if a mouse button is down.
    /// </summary>
    /// <param name="hWnd">A handle to the window in the current thread that is to capture the mouse.</param>
    /// <returns>The return value is a handle to the window that had previously captured the mouse. If there is no such window, the return value is NULL.</returns>
    [DllImport(Library)]
    public static extern IntPtr SetCapture(IntPtr hWnd);
    

    //todo: simplify
    //https://www.pinvoke.net/default.aspx/user32.getwindowlong
    //https://stackoverflow.com/questions/336633/how-to-detect-windows-64-bit-platform-with-net?rq=1
    internal static IntPtr SetWindowLong(IntPtr handle, WNDPROC newValue) => SetWindowLong(handle, GWL.GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(newValue));

    // SetWindowLongPtr does not exist on x86 platforms (it's a macro that resolves to SetWindowLong).
    // We need to detect if we are on x86 or x64 at runtime and call the correct function
    // (SetWindowLongPtr on x64 or SetWindowLong on x86). Fun!
    public static IntPtr SetWindowLong(IntPtr hWnd, GWL nIndex, IntPtr dwNewLong)
    {
        Kernal32.SetLastError(0);

        // SetWindowPos defines its error condition as an IntPtr.Zero retval and a non-0 GetLastError.
        // We need to SetLastError(0) to ensure we are not detecting on older error condition (from another function).

        IntPtr retval;
        if (IntPtr.Size == 4)
            retval = new IntPtr(SetWindowLongInternal(hWnd, nIndex, dwNewLong.ToInt32()));
        else
            retval = SetWindowLongPtrInternal(hWnd, nIndex, dwNewLong);

        if (retval == IntPtr.Zero)
        {
            int error = Marshal.GetLastWin32Error();
            if (error != 0)
                throw new Exception(string.Format("Failed to modify window border. Error: {0}", error));
        }

        return retval;
    }


    [SuppressUnmanagedCodeSecurity]
    [DllImport(Library, SetLastError = true, EntryPoint = "SetWindowLong")]
    public static extern int SetWindowLongInternal(IntPtr hWnd, GWL nIndex, int dwNewLong);

    [SuppressUnmanagedCodeSecurity]
    [DllImport(Library, SetLastError = true, EntryPoint = "SetWindowLongPtr")]
    public static extern IntPtr SetWindowLongPtrInternal(IntPtr hWnd, GWL nIndex, IntPtr dwNewLong);

    [SuppressUnmanagedCodeSecurity]
    [DllImport(Library, SetLastError = true, EntryPoint = "SetWindowLong")]
    public static extern int SetWindowLongInternal(IntPtr hWnd, GWL nIndex,
        [MarshalAs(UnmanagedType.FunctionPtr)] WNDPROC dwNewLong);

    [SuppressUnmanagedCodeSecurity]
    [DllImport(Library, SetLastError = true, EntryPoint = "SetWindowLongPtr")]
    public static extern IntPtr SetWindowLongPtrInternal(IntPtr hWnd, GWL nIndex,
        [MarshalAs(UnmanagedType.FunctionPtr)] WNDPROC dwNewLong);

    

    /// <summary>
    /// https://docs.microsoft.com/en-gb/windows/win32/api/winuser/nf-winuser-setwindowtexta?redirectedfrom=MSDN
    /// Changes the text of the specified window's title bar (if it has one). If the specified window is a control, the text of the control is changed. However, SetWindowText cannot change the text of a control in another application.
    /// </summary>
    /// <param name="hWnd">A handle to the window or control whose text is to be changed.</param>
    /// <param name="lpString">The new title or control text.</param>
    /// <returns></returns>
    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool SetWindowText(IntPtr hWnd, string lpString);
    

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
    /// Sets the specified window's show state.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="nCmdShow">Controls how the window is to be shown. This parameter is ignored the first time an application calls ShowWindow, if the program that launched the application provides a STARTUPINFO structure. Otherwise, the first time ShowWindow is called, the value should be the value obtained by the WinMain function in its nCmdShow parameter.</param>
    /// <returns>If the window was previously visible, the return value is nonzero.
    /// If the window was previously hidden, the return value is zero.</returns>
    [DllImport(Library)]
    public static extern bool ShowWindow(IntPtr hWnd, ShowWindow_Command nCmdShow);
    

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-translatemessage
    /// Translates virtual-key messages into character messages. The character messages are posted to the calling thread's message queue, to be read the next time the thread calls the GetMessage or PeekMessage function.
    /// </summary>
    /// <param name="lpMsg">A pointer to an MSG structure that contains message information retrieved from the calling thread's message queue by using the GetMessage or PeekMessage function.</param>
    /// <returns>If the message is translated (that is, a character message is posted to the thread's message queue), the return value is nonzero.
    /// If the message is WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP, the return value is nonzero, regardless of the translation.
    /// If the message is not translated (that is, a character message is not posted to the thread's message queue), the return value is zero.</returns>
    [DllImport(Library)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool TranslateMessage(ref MSG lpMsg);
    

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-updatewindow
    /// The UpdateWindow function updates the client area of the specified window by sending a WM_PAINT message to the window if the window's update region is not empty. The function sends a WM_PAINT message directly to the window procedure of the specified window, bypassing the application queue. If the update region is empty, no message is sent.
    /// </summary>
    /// <param name="hWnd">Handle to the window to be updated.</param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.</returns>
    [DllImport(Library)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool UpdateWindow(IntPtr hWnd);
    

    // * * * CLEANED UP ABOVE THIS LINE * * *

    [DllImport(Library)]
    public static extern int GetDpiForWindow(IntPtr hWnd);

    [DllImport(Library)]
    public static extern bool BringWindowToTop(IntPtr hWnd);
    

    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, WINDOWMESSAGE Msg,
        IntPtr wParam, IntPtr lParam);
    

    /// <summary>
    /// The ChangeDisplaySettings function changes the settings of the default display device to the specified graphics mode.
    /// </summary>
    /// <param name="device_mode">[in] Pointer to a DEVMODE structure that describes the new graphics mode. If lpDevMode is NULL, all the values currently in the registry will be used for the display setting. Passing NULL for the lpDevMode parameter and 0 for the dwFlags parameter is the easiest way to return to the default mode after a dynamic mode change.</param>
    /// <param name="flags">[in] Indicates how the graphics mode should be changed.</param>
    /// <returns></returns>
    /// <remarks>To change the settings of a specified display device, use the ChangeDisplaySettingsEx function.
    /// <para>To ensure that the DEVMODE structure passed to ChangeDisplaySettings is valid and contains only values supported by the display driver, use the DEVMODE returned by the EnumDisplaySettings function.</para>
    /// <para>When the display mode is changed dynamically, the WM_DISPLAYCHANGE message is sent to all running applications.</para>
    /// </remarks>
    [DllImport(Library, SetLastError = true)]
    public static extern int ChangeDisplaySettings(DeviceMode device_mode, ChangeDisplaySettingsEnum flags);

    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern DisplayDeviceSettingsChangedResult ChangeDisplaySettingsEx([MarshalAs(UnmanagedType.LPTStr)] string lpszDeviceName,
        DeviceMode lpDevMode, IntPtr hwnd, ChangeDisplaySettingsEnum dwflags, IntPtr lParam);
    

    /// <summary>
    /// Converts the client-area coordinates of a specified point to screen coordinates.
    /// </summary>
    /// <param name="hWnd">Handle to the window whose client area will be used for the conversion.</param>
    /// <param name="point">Pointer to a POINT structure that contains the client coordinates to be converted. The new screen coordinates are copied into this structure if the function succeeds.</param>
    [DllImport(Library, SetLastError = true), SuppressUnmanagedCodeSecurity]
    public static extern bool ClientToScreen(IntPtr hWnd, ref System.Drawing.Point point);
    [DllImport(Library, SetLastError = true), SuppressUnmanagedCodeSecurity]
    public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);
    

    [DllImport(Library, CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern bool ClipCursor(ref RECT rcClip);

    [DllImport(Library, CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern bool ClipCursor(IntPtr rcClip);
    

    [DllImport(Library)]
    public static extern IntPtr CreateIconIndirect([In] ref IconInfo piconinfo);
    

    [DllImport(Library, SetLastError = true)]
    public static extern bool DestroyIcon(IntPtr hIcon);
    

    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EnumDisplayDevices([MarshalAs(UnmanagedType.LPTStr)] string lpDevice,
        int iDevNum, [In, Out] DISPLAY_DEVICE lpDisplayDevice, int dwFlags);
    

    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool EnumDisplaySettingsEx([MarshalAs(UnmanagedType.LPTStr)] string lpszDeviceName, DisplayModeSettingsEnum iModeNum,
        [In, Out] DeviceMode lpDevMode, int dwFlags);
    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool EnumDisplaySettingsEx([MarshalAs(UnmanagedType.LPTStr)] string lpszDeviceName, int iModeNum,
        [In, Out] DeviceMode lpDevMode, int dwFlags);
    

    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    

    [DllImport(Library)]
    public static extern IntPtr GetCapture();
    

    /// <summary>
    /// The GetClientRect function retrieves the coordinates of a window's client area. The client coordinates specify the upper-left and lower-right corners of the client area. Because client coordinates are relative to the upper-left corner of a window's client area, the coordinates of the upper-left corner are (0,0).
    /// </summary>
    /// <param name="windowHandle">Handle to the window whose client coordinates are to be retrieved.</param>
    /// <param name="clientRectangle">Pointer to a RECT structure that receives the client coordinates. The left and top members are zero. The right and bottom members contain the width and height of the window.</param>
    [DllImport(Library)]
    public extern static bool GetClientRect(IntPtr windowHandle, out RECT clientRectangle);

    

    /// <summary>
    /// Retrieves a handle to the current cursor.
    /// </summary>
    /// <returns>
    /// The return value is the handle to the current cursor. If there is 
    /// no cursor, the return value is null.
    /// </returns>
    [DllImport(Library)]
    public static extern IntPtr GetCursor();
    

    /// <summary>Must initialize cbSize</summary>
    [DllImport(Library)]
    public static extern bool GetCursorInfo(ref CURSORINFO pci);

    /// <summary>
    /// Returns true if Cursor is currently shown, 0 if hidden. Note that hiding or showing hte cursor increments some windows value by 1, only having an effect when it crosses zero, so these checks are needed
    /// </summary>
    public static bool GetCursorInfo()
    {
        CURSORINFO pci = new()
        {
            cbSize = Marshal.SizeOf(typeof(CURSORINFO))
        };
        return GetCursorInfo(ref pci);
    }

    

    [DllImport(Library)]
    public static extern IntPtr GetDC(IntPtr hwnd);
    

    /// <summary>
    /// Gets the system settings defined delay between clicks for a double click
    /// </summary>
    [DllImport(Library)]
    public static extern int GetDoubleClickTime();
    

    [DllImport(Library)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
    

    [DllImport("User32.dll")]
    public static extern int GetMessageTime();
    

    [DllImport(Library)]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);
    

    [DllImport(Library, CharSet = CharSet.Auto, SetLastError = true)]
    unsafe public static extern int GetMouseMovePointsEx(
        uint cbSize, MouseMovePoint* pointsIn,
        MouseMovePoint* pointsBufferOut, int nBufPoints, GetMouseMovePointResolution resolution);
    

    /// <summary>
    /// Gets the raw input from the specified device.
    /// </summary>
    /// <param name="RawInput">Handle to the RawInput structure. This comes from the lParam in WM_INPUT.</param>
    /// <param name="Command">
    /// Command flag. This parameter can be one of the following values. 
    /// RawInputDateEnum.INPUT
    /// Get the raw data from the RawInput structure.
    /// RawInputDateEnum.HEADER
    /// Get the header information from the RawInput structure.
    /// </param>
    /// <param name="Data">Pointer to the data that comes from the RawInput structure. This depends on the value of uiCommand. If Data is NULL, the required size of the buffer is returned in Size.</param>
    /// <param name="Size">Pointer to a variable that specifies the size, in bytes, of the data in Data.</param>
    /// <param name="SizeHeader">Size, in bytes, of RawInputHeader.</param>
    [DllImport(Library)]
    public static extern int GetRawInputData(
        IntPtr RawInput,
        GetRawInputDataEnum Command,
        [Out] IntPtr Data,
        [In, Out] ref int Size,
        int SizeHeader
    );

    /// <summary>
    /// Gets the raw input from the specified device.
    /// </summary>
    /// <param name="RawInput">Handle to the RawInput structure. This comes from the lParam in WM_INPUT.</param>
    /// <param name="Command">
    /// Command flag. This parameter can be one of the following values. 
    /// RawInputDateEnum.INPUT
    /// Get the raw data from the RawInput structure.
    /// RawInputDateEnum.HEADER
    /// Get the header information from the RawInput structure.
    /// </param>
    /// <param name="Data">Pointer to the data that comes from the RawInput structure. This depends on the value of uiCommand. If Data is NULL, the required size of the buffer is returned in Size.</param>
    /// <param name="Size">Pointer to a variable that specifies the size, in bytes, of the data in Data.</param>
    /// <param name="SizeHeader">Size, in bytes, of RawInputHeader.</param>
    /// <returns>
    /// <para>If Data is NULL and the function is successful, the return value is 0. If Data is not NULL and the function is successful, the return value is the number of bytes copied into Data.</para>
    /// <para>If there is an error, the return value is (UINT)-1.</para>
    /// </returns>
    /// <remarks>
    /// GetRawInputData gets the raw input one RawInput structure at a time. In contrast, GetRawInputBuffer gets an array of RawInput structures.
    /// </remarks>
    [DllImport(Library)]
    public static extern int GetRawInputData(
        IntPtr RawInput,
        GetRawInputDataEnum Command,
        /*[MarshalAs(UnmanagedType.LPStruct)]*/ [Out] out RawInput Data,
        [In, Out] ref int Size,
        int SizeHeader
    );

    /// <summary>
    /// Gets information about the raw input device.
    /// </summary>
    /// <param name="Device">
    /// Handle to the raw input device. This comes from the lParam of the WM_INPUT message,
    /// from RawInputHeader.Device, or from GetRawInputDeviceList.
    /// It can also be NULL if an application inserts input data, for example, by using SendInput.
    /// </param>
    /// <param name="Command">
    /// Specifies what data will be returned in pData. It can be one of the following values. 
    /// RawInputDeviceInfoEnum.PREPARSEDDATA
    /// Data points to the previously parsed data.
    /// RawInputDeviceInfoEnum.DEVICENAME
    /// Data points to a string that contains the device name. 
    /// For this Command only, the value in Size is the character count (not the byte count).
    /// RawInputDeviceInfoEnum.DEVICEINFO
    /// Data points to an RawInputDeviceInfo structure.
    /// </param>
    /// <param name="Data">
    /// ointer to a buffer that contains the information specified by Command.
    /// If Command is RawInputDeviceInfoEnum.DEVICEINFO, set RawInputDeviceInfo.Size to sizeof(RawInputDeviceInfo)
    /// before calling GetRawInputDeviceInfo. (This is done automatically in OpenTK)
    /// </param>
    /// <param name="Size">
    /// Pointer to a variable that contains the size, in bytes, of the data in Data.
    /// </param>
    [DllImport(Library)]
    public static extern uint GetRawInputDeviceInfo(
        IntPtr Device,
        [MarshalAs(UnmanagedType.U4)] RawInputDeviceInfoEnum Command,
        [In, Out] IntPtr Data,
        [In, Out] ref uint Size
    );

    [DllImport(Library)]
    public static extern int GetRawInputDeviceInfo(
        IntPtr Device,
        [MarshalAs(UnmanagedType.U4)] RawInputDeviceInfoEnum Command,
        [In, Out] IntPtr Data,
        [In, Out] ref int Size
    );

    /// <summary>
    /// Gets information about the raw input device.
    /// </summary>
    /// <param name="Device">
    /// Handle to the raw input device. This comes from the lParam of the WM_INPUT message,
    /// from RawInputHeader.Device, or from GetRawInputDeviceList.
    /// It can also be NULL if an application inserts input data, for example, by using SendInput.
    /// </param>
    /// <param name="Command">
    /// Specifies what data will be returned in pData. It can be one of the following values. 
    /// RawInputDeviceInfoEnum.PREPARSEDDATA
    /// Data points to the previously parsed data.
    /// RawInputDeviceInfoEnum.DEVICENAME
    /// Data points to a string that contains the device name. 
    /// For this Command only, the value in Size is the character count (not the byte count).
    /// RawInputDeviceInfoEnum.DEVICEINFO
    /// Data points to an RawInputDeviceInfo structure.
    /// </param>
    /// <param name="Data">
    /// ointer to a buffer that contains the information specified by Command.
    /// If Command is RawInputDeviceInfoEnum.DEVICEINFO, set RawInputDeviceInfo.Size to sizeof(RawInputDeviceInfo)
    /// before calling GetRawInputDeviceInfo. (This is done automatically in OpenTK)
    /// </param>
    /// <param name="Size">
    /// Pointer to a variable that contains the size, in bytes, of the data in Data.
    /// </param>
    /// <returns>
    /// <para>If successful, this function returns a non-negative number indicating the number of bytes copied to Data.</para>
    /// <para>If Data is not large enough for the data, the function returns -1. If Data is NULL, the function returns a value of zero. In both of these cases, Size is set to the minimum size required for the Data buffer.</para>
    /// <para>Call GetLastError to identify any other errors.</para>
    /// </returns>
    [DllImport(Library, SetLastError = true)]
    public static extern uint GetRawInputDeviceInfo(
        IntPtr Device,
        [MarshalAs(UnmanagedType.U4)] RawInputDeviceInfoEnum Command,
        [In, Out] RawInputDeviceInfo Data,
        [In, Out] ref uint Size
    );

    [System.Security.SuppressUnmanagedCodeSecurity]
    [DllImport(Library, SetLastError = true)]
    public static extern int GetRawInputDeviceInfo(
        IntPtr Device,
        [MarshalAs(UnmanagedType.U4)] RawInputDeviceInfoEnum Command,
        [In, Out] RawInputDeviceInfo Data,
        [In, Out] ref int Size
    );

    /// <summary>
    /// Enumerates the raw input devices attached to the system.
    /// </summary>
    /// <param name="RawInputDeviceList">
    /// ointer to buffer that holds an array of RawInputDeviceList structures
    /// for the devices attached to the system.
    /// If NULL, the number of devices are returned in NumDevices.
    /// </param>
    /// <param name="NumDevices">
    /// Pointer to a variable. If RawInputDeviceList is NULL, it specifies the number
    /// of devices attached to the system. Otherwise, it contains the size, in bytes,
    /// of the preallocated buffer pointed to by pRawInputDeviceList.
    /// However, if NumDevices is smaller than needed to contain RawInputDeviceList structures,
    /// the required buffer size is returned here.
    /// </param>
    /// <param name="Size">
    /// Size of a RawInputDeviceList structure.
    /// </param>
    [DllImport(Library, SetLastError = true)]
    public static extern uint GetRawInputDeviceList(
        [In, Out] RawInputDeviceList[] RawInputDeviceList,
        [In, Out] ref uint NumDevices,
        uint Size
    );

    [DllImport(Library, SetLastError = true)]
    public static extern int GetRawInputDeviceList(
        [In, Out] RawInputDeviceList[] RawInputDeviceList,
        [In, Out] ref int NumDevices,
        int Size
    );



    /// <summary>
    /// Enumerates the raw input devices attached to the system.
    /// </summary>
    /// <param name="RawInputDeviceList">
    /// ointer to buffer that holds an array of RawInputDeviceList structures
    /// for the devices attached to the system.
    /// If NULL, the number of devices are returned in NumDevices.
    /// </param>
    /// <param name="NumDevices">
    /// Pointer to a variable. If RawInputDeviceList is NULL, it specifies the number
    /// of devices attached to the system. Otherwise, it contains the size, in bytes,
    /// of the preallocated buffer pointed to by pRawInputDeviceList.
    /// However, if NumDevices is smaller than needed to contain RawInputDeviceList structures,
    /// the required buffer size is returned here.
    /// </param>
    /// <param name="Size">
    /// Size of a RawInputDeviceList structure.
    /// </param>
    /// <returns>
    /// If the function is successful, the return value is the number of devices stored in the buffer
    /// pointed to by RawInputDeviceList.
    /// If RawInputDeviceList is NULL, the return value is zero. 
    /// If NumDevices is smaller than needed to contain all the RawInputDeviceList structures,
    /// the return value is (UINT) -1 and the required buffer is returned in NumDevices.
    /// Calling GetLastError returns ERROR_INSUFFICIENT_BUFFER.
    /// On any other error, the function returns (UINT) -1 and GetLastError returns the error indication.
    /// </returns>
    [DllImport(Library, SetLastError = true)]
    public static extern uint GetRawInputDeviceList(
        [In, Out] IntPtr RawInputDeviceList,
        [In, Out] ref uint NumDevices,
        uint Size
    );

    [DllImport(Library, SetLastError = true)]
    public static extern int GetRawInputDeviceList(
        [In, Out] IntPtr RawInputDeviceList,
        [In, Out] ref int NumDevices,
        int Size
    );

    /// <summary>
    /// The GetWindowRect function retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
    /// </summary>
    /// <param name="windowHandle">Handle to the window whose client coordinates are to be retrieved.</param>
    /// <param name="windowRectangle"> Pointer to a structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
    [DllImport(Library)]
    public extern static bool GetWindowRect(IntPtr windowHandle, out RECT windowRectangle);
    
    [DllImport(Library)]
    public extern static bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

    [DllImport(Library)]
    public static extern bool IsWindowVisible(IntPtr intPtr);
    
    [DllImport(Library)]
    public static extern IntPtr LoadCursorFromFile(string lpCursorName);
    
    [DllImport(Library, SetLastError = true)]
    public static extern bool KillTimer(IntPtr hWnd, UIntPtr uIDEvent);
    
    [DllImport(Library, SetLastError = true)]
    public static extern uint MapVirtualKey(uint uCode, MapVirtualKeyType uMapType);

    [DllImport(Library, SetLastError = true)]
    public static extern uint MapVirtualKey(VirtualKeys vkey, MapVirtualKeyType uMapType);

    /// <summary>
    /// Retrieves a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.
    /// </summary>
    [DllImport(Library)]
    public static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorFrom dwFlags);
    
    [DllImport(Library)]
    public static extern bool MoveWindow(
        IntPtr hWnd,
        int X,
        int Y,
        int nWidth,
        int nHeight,
        bool bRepaint
    );
    
    /// <summary>
    /// Low-level WINAPI function that checks the next message in the queue.
    /// </summary>
    /// <param name="msg">The pending message (if any) is stored here.</param>
    /// <param name="hWnd">The scope of messages to receive</param>
    /// <param name="messageFilterMin">The minimum value of message to receive</param>
    /// <param name="messageFilterMax">The maximum value of message to receive</param>
    /// <param name="flags">What action to take on messages after processing</param>
    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PeekMessage(ref MSG msg, IntPtr hWnd, int messageFilterMin, int messageFilterMax, PM flags);
    
    [DllImport(Library)]
    public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr NotificationFilter, DeviceNotification Flags);
    
    /// <summary>
    /// Registers the devices that supply the raw input data.
    /// </summary>
    /// <param name="RawInputDevices">
    /// Pointer to an array of RawInputDevice structures that represent the devices that supply the raw input.
    /// </param>
    /// <param name="NumDevices">
    /// Number of RawInputDevice structures pointed to by RawInputDevices.
    /// </param>
    /// <param name="Size">
    /// Size, in bytes, of a RAWINPUTDEVICE structure.
    /// </param>
    [DllImport(Library, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool RegisterRawInputDevices(
        RawInputDevice[] RawInputDevices,
        int NumDevices,
        int Size
    );
    public static bool RegisterRawInputDevices(RawInputDevice[] rawInputDevices)
    {
        return RegisterRawInputDevices(rawInputDevices, rawInputDevices.Length, RawInputDevice.Size);
    }
    public static bool RegisterRawInputDevices(RawInputDevice rawInputDevice)
    {
        RawInputDevice[] rids = { rawInputDevice };
        return RegisterRawInputDevices(rids, 1, RawInputDevice.Size);
    }
    
    [DllImport(Library)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ReleaseDC(IntPtr hwnd, IntPtr DC);
    
    /// <summary>
    /// Converts the screen coordinates of a specified point on the screen to client-area coordinates.
    /// </summary>
    /// <param name="hWnd">Handle to the window whose client area will be used for the conversion.</param>
    /// <param name="point">Pointer to a POINT structure that specifies the screen coordinates to be converted.</param>
    [DllImport(Library)]
    //internal static extern BOOL ScreenToClient(HWND hWnd, ref POINT point);
    public static extern bool ScreenToClient(IntPtr hWnd, ref System.Drawing.Point point);
    
    [DllImport(Library, CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, WINDOWMESSAGE Msg, IntPtr wParam, IntPtr lParam);
    
    [DllImport(Library)]
    public static extern bool SetCursorPos(int X, int Y);
    
    [DllImport(Library)]
    public static extern bool SetProcessDPIAware();
    
    [DllImport(Library)]
    public static extern bool SetProcessDpiAwarenessContext();
    
    [DllImport(Library, SetLastError = true)]
    public static extern IntPtr SetFocus(IntPtr hWnd);
    
    [DllImport(Library, SetLastError = true)]
    public static extern bool SetParent(IntPtr child, IntPtr newParent);
    
    [DllImport(Library)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

    public static bool SetWindowPos(IntPtr hWnd, RECT position, SetWindowPosFlags uFlags) => SetWindowPos(hWnd, IntPtr.Zero, position.left, position.top, position.Width, position.Height, uFlags);
    
    [DllImport(Library)]
    public static extern int ShowCursor(bool show);
    
    /// <summary>
    /// The ShowWindow function sets the specified window's show state.
    /// </summary>
    /// <param name="hWnd">[in] Handle to the window.</param>
    /// <param name="nCmdShow">[in] Specifies how the window is to be shown. This parameter is ignored the first time an application calls ShowWindow, if the program that launched the application provides a STARTUPINFO structure. Otherwise, the first time ShowWindow is called, the value should be the value obtained by the WinMain function in its nCmdShow parameter. In subsequent calls, this parameter can be one of the ShowWindowEnum values.</param>
    [DllImport(Library)]
    public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommand nCmdShow);
    
    [DllImport(Library)]
    public static extern IntPtr SetClassLong(IntPtr hInstance, int nIndex, IntPtr value);

    public static IntPtr SetClassLong(IntPtr windowHandle, NIndex nIndex, IntPtr value)
    {
        return SetClassLong(windowHandle, (int)nIndex, value);
    }
    
    /// <summary>
    /// Sets the cursor shape.
    /// </summary>
    /// <param name="hCursor">
    /// A handle to the cursor. The cursor must have been created by the 
    /// CreateCursor function or loaded by the LoadCursor or LoadImage 
    /// function. If this parameter is IntPtr.Zero, the cursor is removed 
    /// from the screen.
    /// </param>
    /// <returns>
    /// The return value is the handle to the previous cursor, if there was one.
    /// 
    /// If there was no previous cursor, the return value is null.
    /// </returns>
    /// <remarks>
    /// The cursor is set only if the new cursor is different from the 
    /// previous cursor; otherwise, the function returns immediately.
    /// 
    /// The cursor is a shared resource. A window should set the cursor 
    /// shape only when the cursor is in its client area or when the window 
    /// is capturing mouse input. In systems without a mouse, the window 
    /// should restore the previous cursor before the cursor leaves the 
    /// client area or before it relinquishes control to another window.
    /// 
    /// If your application must set the cursor while it is in a window, 
    /// make sure the class cursor for the specified window's class is set 
    /// to NULL. If the class cursor is not NULL, the system restores the 
    /// class cursor each time the mouse is moved.
    /// 
    /// The cursor is not shown on the screen if the internal cursor 
    /// display count is less than zero. This occurs if the application 
    /// uses the ShowCursor function to hide the cursor more times than to 
    /// show the cursor.
    /// </remarks>
    [DllImport(Library)]
    public static extern IntPtr SetCursor(IntPtr hCursor);
    
    [DllImport(Library)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
    
    [DllImport(Library, SetLastError = true)]
    public static extern UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);
    
    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern short UnregisterClass([MarshalAs(UnmanagedType.LPTStr)] string className, IntPtr instance);

    [DllImport(Library, SetLastError = true, CharSet = CharSet.Auto)]
    public static extern short UnregisterClass(IntPtr className, IntPtr instance);
}