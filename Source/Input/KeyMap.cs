using BearsEngine.Win32API;

namespace BearsEngine.Input;

internal static class KeyMap
{
    #region GetKey
    //Deprecated
    public static Key GetKey(uint scancode)
    {
        //see https://msdn.microsoft.com/en-us/library/aa299374(v=vs.60).aspx
        switch (scancode)
        {
            case 0: return Key.Unknown;
            case 1: return Key.ESC;
            case 2: return Key.Num1;
            case 3: return Key.Num2;
            case 4: return Key.Num3;
            case 5: return Key.Num4;
            case 6: return Key.Num5;
            case 7: return Key.Num6;
            case 8: return Key.Num7;
            case 9: return Key.Num8;
            case 10: return Key.Num9;
            case 11: return Key.Num0;
            case 12: return Key.Minus;
           // case 13: return Key.Plus;
            case 14: return Key.Backspace;
            case 15: return Key.Tab;
            case 16: return Key.Q;
            case 17: return Key.W;
            case 18: return Key.E;
            case 19: return Key.R;
            case 20: return Key.T;
            case 21: return Key.Y;
            case 22: return Key.U;
            case 23: return Key.I;
            case 24: return Key.O;
            case 25: return Key.P;
            case 26: return Key.LeftBracket;
            case 27: return Key.RightBracket;
            case 28: return Key.Enter;
            case 29: return Key.LeftControl;
            case 30: return Key.A;
            case 31: return Key.S;
            case 32: return Key.D;
            case 33: return Key.F;
            case 34: return Key.G;
            case 35: return Key.H;
            case 36: return Key.J;
            case 37: return Key.K;
            case 38: return Key.L;
            case 39: return Key.Semicolon;
            case 40: return Key.Quote;
            case 41: return Key.GraveAccent;
            case 42: return Key.LeftShift;
            case 43: return Key.Hash;
            case 44: return Key.Z;
            case 45: return Key.X;
            case 46: return Key.C;
            case 47: return Key.V;
            case 48: return Key.B;
            case 49: return Key.N;
            case 50: return Key.M;
            case 51: return Key.Comma;
            case 52: return Key.Period;
            case 53: return Key.Slash;
            case 54: return Key.RightShift;
            case 55: return Key.PrintScreen;
            case 56: return Key.LeftAlt;
            case 57: return Key.Space;
            case 58: return Key.CapsLock;
            case 59: return Key.F1;
            case 60: return Key.F2;
            case 61: return Key.F3;
            case 62: return Key.F4;
            case 63: return Key.F5;
            case 64: return Key.F6;
            case 65: return Key.F7;
            case 66: return Key.F8;
            case 67: return Key.F9;
            case 68: return Key.F10;
            case 69: return Key.NumLock;
            case 70: return Key.ScrollLock;
            case 71: return Key.Home;
            case 72: return Key.Up;
            case 73: return Key.PageUp;
            case 74: return Key.KeypadSubtract;
            case 75: return Key.Left;
         //   case 76: return Key.Centre;
            case 77: return Key.Right;
            case 78: return Key.KeypadAdd;
            case 79: return Key.End;
            case 80: return Key.Down;
            case 81: return Key.PageDown;
            case 82: return Key.Insert;
            case 83: return Key.Delete;
            case 84: return Key.PrintScreen;
            case 86: return Key.BackSlash;


            case 87: return Key.F11;
            case 88: return Key.F12;
            case 91: return Key.LeftWindows;

            //case 82: return Key.Keypad0;
            //case 79: return Key.Keypad1;
            //case 80: return Key.Keypad2;
            //case 81: return Key.Keypad3;
            //case 75: return Key.Keypad4;
            //case 76: return Key.Keypad5;
            //case 77: return Key.Keypad6;
            //case 71: return Key.Keypad7;
            //case 72: return Key.Keypad8;
            //case 73: return Key.Keypad9;

            default: return Key.Unknown;
        }
    }
    #endregion

    private static Key GetExtendedKey(uint scancode)
    {
        switch (scancode)
        {
            #region Arrow Keys
            case (22020096): return Key.Down;
            case (21692416): return Key.Left;
            case (21823488): return Key.Right;
            case (21495808): return Key.Up;
            #endregion

            #region Function Keys
            case (3866624): return Key.F1;
            case (3932160): return Key.F2;
            case (3997696): return Key.F3;
            case (4063232): return Key.F4;
            case (4128768): return Key.F5;
            case (4194304): return Key.F6;
            case (4259840): return Key.F7;
            case (4325376): return Key.F8;
            case (4390912): return Key.F9;
            case (4456448): return Key.F10;
            case (5701632): return Key.F11;
            case (5767168): return Key.F12;
            #endregion

            #region Letter Keys
            case (1966080): return Key.A;
            case (3145728): return Key.B;
            case (3014656): return Key.C;
            case (2097152): return Key.D;
            case (1179648): return Key.E;
            case (2162688): return Key.F;
            case (2228224): return Key.G;
            case (2293760): return Key.H;
            case (1507328): return Key.I;
            case (2359296): return Key.J;
            case (2424832): return Key.K;
            case (2490368): return Key.L;
            case (3276800): return Key.M;
            case (3211264): return Key.N;
            case (1572864): return Key.O;
            case (1638400): return Key.P;
            case (1048576): return Key.Q;
            case (1245184): return Key.R;
            case (2031616): return Key.S;
            case (1310720): return Key.T;
            case (1441792): return Key.U;
            case (3080192): return Key.V;
            case (1114112): return Key.W;
            case (2949120): return Key.X;
            case (1376256): return Key.Y;
            case (2883584): return Key.Z;
            #endregion

            #region Modifier Keys
            case (3801088): return Key.CapsLock;
            case (3670016): return Key.LeftAlt;
            case (1900544): return Key.LeftControl;
            case (2752512): return Key.LeftShift;
            case (22740992): return Key.LeftWindows;
            case (22872064): return Key.Menu;
            case (20447232): return Key.RightAlt;
            case (18677760): return Key.RightControl;
           // case (21299200): return Key.RightShift;
            #endregion

            #region Numpad/Keypad keys 
            case (5373952): return Key.Keypad0;
            case (5177344): return Key.Keypad1;
            case (5242880): return Key.Keypad2;
            case (5308416): return Key.Keypad3;
            case (4915200): return Key.Keypad4;
            case (4980736): return Key.Keypad5;
            case (5046272): return Key.Keypad6;
            case (4653056): return Key.Keypad7;
            case (4718592): return Key.Keypad8;
            case (4784128): return Key.Keypad9;                    
            case (5111808): return Key.KeypadAdd;
            case (20250624): return Key.KeypadDivide;
            case (18612224): return Key.KeypadEnter;
            case (3604480): return Key.KeypadMultiply;
            case (5439488): return Key.KeypadPeriod;
            case (4849664): return Key.KeypadSubtract;
            #endregion

            #region Number keys - along the top of the keyboard
            case (720896): return Key.Num0;
            case (131072): return Key.Num1;
            case (196608): return Key.Num2;
            case (262144): return Key.Num3;
            case (327680): return Key.Num4;
            case (393216): return Key.Num5;
            case (458752): return Key.Num6;
            case (524288): return Key.Num7;
            case (589824): return Key.Num8;
            case (655360): return Key.Num9;
            #endregion

            #region Special Keys
            case (917504): return Key.Backspace;
            case (22216704): return Key.Delete;
            case (65536): return Key.ESC;
            case (21954560): return Key.End;
            case (1835008): return Key.Enter;
            case (21430272): return Key.Home;
            case (21299200): return Key.NumLock;
            case (0): return Key.Pause;
            case (22282240): return Key.PrintScreen;
            case (16777216): return Key.PrintScreen;  //Had to add 16777216 for my Razer keyboard, which was otherwise identical to my lappy and the On-Screen Keyboard. Reverse decoding luckily unaffected becausse it's a special case anyway!
            case (5505024): return Key.PrintScreen; //this one is haigh's keyboard apparently?
            case (3735552): return Key.Space;
            case (983040): return Key.Tab;
            #endregion

            #region SymbolKeys
            case (5636096): return Key.BackSlash;
            case (3342336): return Key.Comma;
            case (851968): return Key.Equals;
            case (2686976): return Key.GraveAccent;
            case (2818048): return Key.Hash;
            case (22151168): return Key.Insert;
            case (1703936): return Key.LeftBracket;
            case (786432): return Key.Minus;
            case (22085632): return Key.PageDown;
            case (21561344): return Key.PageUp;
            case (2621440): return Key.Quote;
            case (1769472): return Key.RightBracket;
            case (3407872): return Key.Period;
            case (4587520): return Key.ScrollLock;
            case (2555904): return Key.Semicolon;
            case (3473408): return Key.Slash;
            #endregion

            default:
                HConsole.Warning("Unknown extended Key Scancode {0}", scancode);
                return Key.Unknown;
        }
    }

    internal static uint GetExtendedScanCodeFromKey(Key key)
    {
        switch (key)
        {
            #region Arrow Keys
            case (Key.Down):            return 22020096;
            case (Key.Left):            return 21692416;
            case (Key.Right):           return 21823488;
            case (Key.Up):              return 21495808;
            #endregion

            #region Function Keys
            case (Key.F1):              return 3866624;
            case (Key.F2):              return 3932160;
            case (Key.F3):              return 3997696;
            case (Key.F4):              return 4063232;
            case (Key.F5):              return 4128768;
            case (Key.F6):              return 4194304;
            case (Key.F7):              return 4259840;
            case (Key.F8):              return 4325376;
            case (Key.F9):              return 4390912;
            case (Key.F10):             return 4456448;
            case (Key.F11):             return 5701632;
            case (Key.F12):             return 5767168;
            #endregion

            #region Letter Keys
            case (Key.A):               return 1966080;
            case (Key.B):               return 3145728;
            case (Key.C):               return 3014656;
            case (Key.D):               return 2097152;
            case (Key.E):               return 1179648;
            case (Key.F):               return 2162688;
            case (Key.G):               return 2228224;
            case (Key.H):               return 2293760;
            case (Key.I):               return 1507328;
            case (Key.J):               return 2359296;
            case (Key.K):               return 2424832;
            case (Key.L):               return 2490368;
            case (Key.M):               return 3276800;
            case (Key.N):               return 3211264;
            case (Key.O):               return 1572864;
            case (Key.P):               return 1638400;
            case (Key.Q):               return 1048576;
            case (Key.R):               return 1245184;
            case (Key.S):               return 2031616;
            case (Key.T):               return 1310720;
            case (Key.U):               return 1441792;
            case (Key.V):               return 3080192;
            case (Key.W):               return 1114112;
            case (Key.X):               return 2949120;
            case (Key.Y):               return 1376256;
            case (Key.Z):               return 2883584;
            #endregion

            #region Modifier Keys
            case (Key.CapsLock):        return 3801088;
            case (Key.LeftAlt):         return 3670016;
            case (Key.LeftControl):     return 1900544;
            case (Key.LeftShift):       return 2752512;
            case (Key.LeftWindows):     return 22740992;
            case (Key.Menu):            return 22872064;
            case (Key.RightAlt):        return 20447232;
            case (Key.RightControl):    return 18677760;
            case (Key.RightShift):      return 99999999;    //Not a real value, this is a hack and is explictly handled
            #endregion

            #region Numpad/Keypad keys 
            case (Key.Keypad0):         return 5373952;
            case (Key.Keypad1):         return 5177344;
            case (Key.Keypad2):         return 5242880;
            case (Key.Keypad3):         return 5308416;
            case (Key.Keypad4):         return 4915200;
            case (Key.Keypad5):         return 4980736;
            case (Key.Keypad6):         return 5046272;
            case (Key.Keypad7):         return 4653056;
            case (Key.Keypad8):         return 4718592;
            case (Key.Keypad9):         return 4784128;
            case (Key.KeypadAdd):       return 5111808;
            case (Key.KeypadDivide):    return 20250624;
            case (Key.KeypadEnter):     return 18612224;
            case (Key.KeypadMultiply):  return 3604480;
            case (Key.KeypadPeriod):    return 5439488;
            case (Key.KeypadSubtract):  return 4849664;
            #endregion

            #region Number keys - along the top of the keyboard
            case (Key.Num0):            return 720896;
            case (Key.Num1):            return 131072;
            case (Key.Num2):            return 196608;
            case (Key.Num3):            return 262144;
            case (Key.Num4):            return 327680;
            case (Key.Num5):            return 393216;
            case (Key.Num6):            return 458752;
            case (Key.Num7):            return 524288;
            case (Key.Num8):            return 589824;
            case (Key.Num9):            return 655360;
            #endregion

            #region Special Keys
            case (Key.Backspace):       return 917504;
            case (Key.Delete):          return 22216704;
            case (Key.ESC):             return 65536;
            case (Key.End):             return 21954560;
            case (Key.Enter):           return 1835008;
            case (Key.Home):            return 21430272;
            case (Key.NumLock):         return 21299200;
            case (Key.Pause):           return 0;
            case (Key.PrintScreen):     return 22282240;
            case (Key.Space):           return 3735552;
            case (Key.Tab):             return 983040;
            #endregion

            #region SymbolKeys
            case (Key.BackSlash):       return 5636096;
            case (Key.Comma):           return 3342336;
            case (Key.Equals):          return 851968;
            case (Key.GraveAccent):     return 2686976;
            case (Key.Hash):            return 2818048;
            case (Key.Insert):          return 22151168;
            case (Key.LeftBracket):     return 1703936;
            case (Key.Minus):           return 786432;
            case (Key.PageDown):        return 22085632;
            case (Key.PageUp):          return 21561344;
            case (Key.Quote):           return 2621440;
            case (Key.RightBracket):    return 1769472;
            case (Key.Period):          return 3407872;
            case (Key.ScrollLock):      return 4587520;
            case (Key.Semicolon):       return 2555904;
            case (Key.Slash):           return 3473408;
            #endregion
             
            default:
                return 0;
        }
    }        

    //[Deprecated]  Better support for things like the numpad having Home on the 7 key, stuff like that. However different keyboard layouts, eg US may mess this up
    private static Key GetKey(VirtualKeys vkey)
    {
        //cf https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx

        switch (vkey)
        {
            //Mouse - why is this even handled here?
            case VirtualKeys.LBUTTON:
                return Key.None;
            case VirtualKeys.RBUTTON:
                return Key.None;
            case VirtualKeys.CANCEL:
                return Key.None;
            case VirtualKeys.MBUTTON:
                return Key.None;
            case VirtualKeys.XBUTTON1:
                return Key.None;
            case VirtualKeys.XBUTTON2:
                return Key.None;

            case VirtualKeys.BACK:
                return Key.Backspace;
            case VirtualKeys.TAB:
                return Key.Tab;
            case VirtualKeys.RETURN:
                return Key.Enter;
            case VirtualKeys.SHIFT:
                return Key.LeftShift;
            case VirtualKeys.CONTROL:
                return Key.LeftControl;
            case VirtualKeys.MENU:
                return Key.LeftAlt;
            case VirtualKeys.PAUSE:
                return Key.Pause;
            case VirtualKeys.CAPITAL:
                return Key.CapsLock;
            case VirtualKeys.ESCAPE:
                return Key.ESC;
            case VirtualKeys.SPACE:
                return Key.Space;

            //Arrow keys
            case VirtualKeys.LEFT:
                return Key.Left;
            case VirtualKeys.UP:
                return Key.Up;
            case VirtualKeys.RIGHT:
                return Key.Right;
            case VirtualKeys.DOWN:
                return Key.Down;

            case VirtualKeys.SNAPSHOT:
                return Key.PrintScreen;


            case VirtualKeys.INSERT:
                return Key.Insert;
            case VirtualKeys.DELETE:
                return Key.Delete;
            case VirtualKeys.PRIOR:
                return Key.PageUp;
            case VirtualKeys.NEXT:
                return Key.PageDown;
            case VirtualKeys.END:
                return Key.End;
            case VirtualKeys.HOME:
                return Key.Home;

            //Numbers along the top of the keyboard
            case VirtualKeys.NUM0:
                return Key.Num0;
            case VirtualKeys.NUM1:
                return Key.Num1;
            case VirtualKeys.NUM2:
                return Key.Num2;
            case VirtualKeys.NUM3:
                return Key.Num3;
            case VirtualKeys.NUM4:
                return Key.Num4;
            case VirtualKeys.NUM5:
                return Key.Num5;
            case VirtualKeys.NUM6:
                return Key.Num6;
            case VirtualKeys.NUM7:
                return Key.Num7;
            case VirtualKeys.NUM8:
                return Key.Num8;
            case VirtualKeys.NUM9:
                return Key.Num9;

            case VirtualKeys.A:
                return Key.A;
            case VirtualKeys.B:
                return Key.B;
            case VirtualKeys.C:
                return Key.C;
            case VirtualKeys.D:
                return Key.D;
            case VirtualKeys.E:
                return Key.E;
            case VirtualKeys.F:
                return Key.F;
            case VirtualKeys.G:
                return Key.G;
            case VirtualKeys.H:
                return Key.H;
            case VirtualKeys.I:
                return Key.I;
            case VirtualKeys.J:
                return Key.J;
            case VirtualKeys.K:
                return Key.K;
            case VirtualKeys.L:
                return Key.L;
            case VirtualKeys.M:
                return Key.M;
            case VirtualKeys.N:
                return Key.N;
            case VirtualKeys.O:
                return Key.O;
            case VirtualKeys.P:
                return Key.P;
            case VirtualKeys.Q:
                return Key.Q;
            case VirtualKeys.R:
                return Key.R;
            case VirtualKeys.S:
                return Key.S;
            case VirtualKeys.T:
                return Key.T;
            case VirtualKeys.U:
                return Key.U;
            case VirtualKeys.V:
                return Key.V;
            case VirtualKeys.W:
                return Key.W;
            case VirtualKeys.X:
                return Key.X;
            case VirtualKeys.Y:
                return Key.Y;
            case VirtualKeys.Z:
                return Key.Z;

            case VirtualKeys.LWIN:
                return Key.LeftWindows;
           // case VirtualKeys.RWIN:
           //     return Key.RightWindows;

            //Numbers on the keypad/numpad
            case VirtualKeys.NUMPAD0:
                return Key.Keypad0;
            case VirtualKeys.NUMPAD1:
                return Key.Keypad1;
            case VirtualKeys.NUMPAD2:
                return Key.Keypad2;
            case VirtualKeys.NUMPAD3:
                return Key.Keypad3;
            case VirtualKeys.NUMPAD4:
                return Key.Keypad4;
            case VirtualKeys.NUMPAD5:
                return Key.Keypad5;
            case VirtualKeys.NUMPAD6:
                return Key.Keypad6;
            case VirtualKeys.NUMPAD7:
                return Key.Keypad7;
            case VirtualKeys.NUMPAD8:
                return Key.Keypad8;
            case VirtualKeys.NUMPAD9:
                return Key.Keypad9;

            case VirtualKeys.MULTIPLY:
                return Key.KeypadMultiply;
            case VirtualKeys.ADD:
                return Key.KeypadAdd;
            case VirtualKeys.SEPARATOR: //?
                return Key.None;
            case VirtualKeys.SUBTRACT:
                return Key.KeypadSubtract;
            case VirtualKeys.DECIMAL:
                return Key.KeypadPeriod;
            case VirtualKeys.DIVIDE:
                return Key.KeypadDivide;

            //Function keys
            case VirtualKeys.F1:
                return Key.F1;
            case VirtualKeys.F2:
                return Key.F2;
            case VirtualKeys.F3:
                return Key.F3;
            case VirtualKeys.F4:
                return Key.F4;
            case VirtualKeys.F5:
                return Key.F5;
            case VirtualKeys.F6:
                return Key.F6;
            case VirtualKeys.F7:
                return Key.F7;
            case VirtualKeys.F8:
                return Key.F8;
            case VirtualKeys.F9:
                return Key.F9;
            case VirtualKeys.F10:
                return Key.F10;
            case VirtualKeys.F11:
                return Key.F11;
            case VirtualKeys.F12:
                return Key.F12;
            
            //Modifiers
            case VirtualKeys.NUMLOCK:
                return Key.NumLock;
            case VirtualKeys.SCROLL:
                return Key.ScrollLock;
            case VirtualKeys.OEM_NEC_EQUAL:
                return Key.Equals;
            case VirtualKeys.LSHIFT:
                return Key.LeftShift;
            case VirtualKeys.RSHIFT:
                return Key.RightShift;
            case VirtualKeys.LCONTROL:
                return Key.LeftControl;
            case VirtualKeys.RCONTROL:
                return Key.RightControl;

            //case VirtualKeys.OEM_PLUS:
             //   return Key.Plus;
            case VirtualKeys.OEM_COMMA:
                return Key.Comma;
            case VirtualKeys.OEM_MINUS:
                return Key.Minus;
            case VirtualKeys.OEM_PERIOD:
                return Key.Period;
           // case VirtualKeys.APPS:
            //    return Key.List;

            //These can vary by keyboard layout
            case VirtualKeys.OEM_1:
                return Key.Semicolon;
            case VirtualKeys.OEM_2:
                return Key.Slash;
            case VirtualKeys.OEM_3:
                return Key.Quote;   //I think this is means to be tilde on US layout
            case VirtualKeys.OEM_4:
                return Key.LeftBracket;
            case VirtualKeys.OEM_5:
                return Key.BackSlash;
            case VirtualKeys.OEM_6:
                return Key.RightBracket;
            case VirtualKeys.OEM_7:
                return Key.Hash;    //I think this is Quote on US layout
            case VirtualKeys.OEM_8:
                return Key.GraveAccent;

            //Unsupported wierd keys
            case VirtualKeys.BROWSER_BACK:
            case VirtualKeys.BROWSER_FORWARD:
            case VirtualKeys.BROWSER_REFRESH:
            case VirtualKeys.BROWSER_STOP:
            case VirtualKeys.BROWSER_SEARCH:
            case VirtualKeys.BROWSER_FAVORITES:
            case VirtualKeys.BROWSER_HOME:
            case VirtualKeys.VOLUME_MUTE:
            case VirtualKeys.VOLUME_DOWN:
            case VirtualKeys.VOLUME_UP:
            case VirtualKeys.MEDIA_NEXT_TRACK:
            case VirtualKeys.MEDIA_PREV_TRACK:
            case VirtualKeys.MEDIA_STOP:
            case VirtualKeys.MEDIA_PLAY_PAUSE:
            case VirtualKeys.F13:
            case VirtualKeys.F14:
            case VirtualKeys.F15:
            case VirtualKeys.F16:
            case VirtualKeys.F17:
            case VirtualKeys.F18:
            case VirtualKeys.F19:
            case VirtualKeys.F20:
            case VirtualKeys.F21:
            case VirtualKeys.F22:
            case VirtualKeys.F23:
            case VirtualKeys.F24:
            case VirtualKeys.OEM_FJ_MASSHOU:
            case VirtualKeys.OEM_FJ_TOUROKU:
            case VirtualKeys.OEM_FJ_LOYA:
            case VirtualKeys.OEM_FJ_ROYA:
            case VirtualKeys.SLEEP:
            case VirtualKeys.HELP:
            case VirtualKeys.EXECUTE:
            case VirtualKeys.SELECT:
            case VirtualKeys.PRINT:
            case VirtualKeys.KANA:
            case VirtualKeys.JUNJA:
            case VirtualKeys.FINAL:
            case VirtualKeys.HANJA:
            case VirtualKeys.CONVERT:
            case VirtualKeys.NONCONVERT:
            case VirtualKeys.ACCEPT:
            case VirtualKeys.MODECHANGE:
            case VirtualKeys.CLEAR:
            case VirtualKeys.LMENU:
            case VirtualKeys.RMENU:
            case VirtualKeys.OEM_AX:
            case VirtualKeys.OEM_102:
            case VirtualKeys.ICO_HELP:
            case VirtualKeys.ICO_00:
            case VirtualKeys.PROCESSKEY:
            case VirtualKeys.ICO_CLEAR:
            case VirtualKeys.PACKET:
            case VirtualKeys.OEM_RESET:
            case VirtualKeys.OEM_JUMP:
            case VirtualKeys.OEM_PA1:
            case VirtualKeys.OEM_PA2:
            case VirtualKeys.OEM_PA3:
            case VirtualKeys.OEM_WSCTRL:
            case VirtualKeys.OEM_CUSEL:
            case VirtualKeys.OEM_ATTN:
            case VirtualKeys.OEM_FINISH:
            case VirtualKeys.OEM_COPY:
            case VirtualKeys.OEM_AUTO:
            case VirtualKeys.OEM_ENLW:
            case VirtualKeys.OEM_BACKTAB:
            case VirtualKeys.ATTN:
            case VirtualKeys.CRSEL:
            case VirtualKeys.EXSEL:
            case VirtualKeys.EREOF:
            case VirtualKeys.PLAY:
            case VirtualKeys.ZOOM:
            case VirtualKeys.NONAME:
            case VirtualKeys.PA1:
            case VirtualKeys.OEM_CLEAR:
            case VirtualKeys.Last:

            default:
                return Key.Unknown;
        }
    }

    #region TranslateKey
    //c.f.  https://blog.molecular-matters.com/2011/09/05/properly-handling-keyboard-input/
    internal static Key TranslateKey(short scancode, VirtualKeys vkey, bool extended0)
    {
        uint modifiedScancode = User32.MapVirtualKey(vkey, MapVirtualKeyType.VirtualKeyToScanCode);
        uint extendedScanCode = (modifiedScancode << 16) | ((extended0 ? 1 : (uint)0) << 24);
        
        Key key = GetExtendedKey(extendedScanCode);

        //Hack for right control and Alt/AltGr - these will have extended flags set
        if (extended0)
            switch (key)
            {
                case Key.LeftAlt: key = Key.RightAlt; break;
                case Key.LeftControl: key = Key.RightControl; break;
            }

        //Hack for right shift. Come on Windows.. also pretty wierd that MapVirtualKey just blissfully ignores this one?
        if (scancode == 54 && key == Key.LeftShift)
            key = Key.RightShift;

        return key;
    }
    #endregion
}
