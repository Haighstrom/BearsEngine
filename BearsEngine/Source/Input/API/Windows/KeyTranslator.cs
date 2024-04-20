using BearsEngine.WinAPI;

namespace BearsEngine.Input.Windows;

internal static class KeyTranslator
{
    private static Key GetExtendedKey(uint scancode)
    {
        switch (scancode)
        {
            case 22020096: return Key.Down;
            case 21692416: return Key.Left;
            case 21823488: return Key.Right;
            case 21495808: return Key.Up;


            case 3866624: return Key.F1;
            case 3932160: return Key.F2;
            case 3997696: return Key.F3;
            case 4063232: return Key.F4;
            case 4128768: return Key.F5;
            case 4194304: return Key.F6;
            case 4259840: return Key.F7;
            case 4325376: return Key.F8;
            case 4390912: return Key.F9;
            case 4456448: return Key.F10;
            case 5701632: return Key.F11;
            case 5767168: return Key.F12;


            case 1966080: return Key.A;
            case 3145728: return Key.B;
            case 3014656: return Key.C;
            case 2097152: return Key.D;
            case 1179648: return Key.E;
            case 2162688: return Key.F;
            case 2228224: return Key.G;
            case 2293760: return Key.H;
            case 1507328: return Key.I;
            case 2359296: return Key.J;
            case 2424832: return Key.K;
            case 2490368: return Key.L;
            case 3276800: return Key.M;
            case 3211264: return Key.N;
            case 1572864: return Key.O;
            case 1638400: return Key.P;
            case 1048576: return Key.Q;
            case 1245184: return Key.R;
            case 2031616: return Key.S;
            case 1310720: return Key.T;
            case 1441792: return Key.U;
            case 3080192: return Key.V;
            case 1114112: return Key.W;
            case 2949120: return Key.X;
            case 1376256: return Key.Y;
            case 2883584: return Key.Z;


            case 3801088: return Key.CapsLock;
            case 3670016: return Key.LeftAlt;
            case 1900544: return Key.LeftControl;
            case 2752512: return Key.LeftShift;
            case 22740992: return Key.LeftWindows;
            case 22872064: return Key.Menu;
            case 20447232: return Key.RightAlt;
            case 18677760: return Key.RightControl;
            // case (21299200): return Key.RightShift;


            case 5373952: return Key.Keypad0;
            case 5177344: return Key.Keypad1;
            case 5242880: return Key.Keypad2;
            case 5308416: return Key.Keypad3;
            case 4915200: return Key.Keypad4;
            case 4980736: return Key.Keypad5;
            case 5046272: return Key.Keypad6;
            case 4653056: return Key.Keypad7;
            case 4718592: return Key.Keypad8;
            case 4784128: return Key.Keypad9;
            case 5111808: return Key.KeypadAdd;
            case 20250624: return Key.KeypadDivide;
            case 18612224: return Key.KeypadEnter;
            case 3604480: return Key.KeypadMultiply;
            case 5439488: return Key.KeypadPeriod;
            case 4849664: return Key.KeypadSubtract;


            case 720896: return Key.Num0;
            case 131072: return Key.Num1;
            case 196608: return Key.Num2;
            case 262144: return Key.Num3;
            case 327680: return Key.Num4;
            case 393216: return Key.Num5;
            case 458752: return Key.Num6;
            case 524288: return Key.Num7;
            case 589824: return Key.Num8;
            case 655360: return Key.Num9;


            case 917504: return Key.Backspace;
            case 22216704: return Key.Delete;
            case 65536: return Key.ESC;
            case 21954560: return Key.End;
            case 1835008: return Key.Enter;
            case 21430272: return Key.Home;
            case 21299200: return Key.NumLock;
            case 0: return Key.Pause;
            case 22282240: return Key.PrintScreen;
            case 16777216: return Key.PrintScreen;  //Had to add 16777216 for my Razer keyboard, which was otherwise identical to my lappy and the On-Screen Keyboard. Reverse decoding luckily unaffected becausse it's a special case anyway!
            case 5505024: return Key.PrintScreen; //this one is haigh's keyboard apparently?
            case 3735552: return Key.Space;
            case 983040: return Key.Tab;


            case 5636096: return Key.BackSlash;
            case 3342336: return Key.Comma;
            case 851968: return Key.Equals;
            case 2686976: return Key.GraveAccent;
            case 2818048: return Key.Hash;
            case 22151168: return Key.Insert;
            case 1703936: return Key.LeftBracket;
            case 786432: return Key.Minus;
            case 22085632: return Key.PageDown;
            case 21561344: return Key.PageUp;
            case 2621440: return Key.Quote;
            case 1769472: return Key.RightBracket;
            case 3407872: return Key.Period;
            case 4587520: return Key.ScrollLock;
            case 2555904: return Key.Semicolon;
            case 3473408: return Key.Slash;


            default:
                Log.Warning($"Unknown extended Key Scancode {scancode}");
                return Key.Unknown;
        }
    }

    public static uint GetExtendedScanCode(Key key)
    {
        switch (key)
        {
            case Key.Down: return 22020096;
            case Key.Left: return 21692416;
            case Key.Right: return 21823488;
            case Key.Up: return 21495808;


            case Key.F1: return 3866624;
            case Key.F2: return 3932160;
            case Key.F3: return 3997696;
            case Key.F4: return 4063232;
            case Key.F5: return 4128768;
            case Key.F6: return 4194304;
            case Key.F7: return 4259840;
            case Key.F8: return 4325376;
            case Key.F9: return 4390912;
            case Key.F10: return 4456448;
            case Key.F11: return 5701632;
            case Key.F12: return 5767168;


            case Key.A: return 1966080;
            case Key.B: return 3145728;
            case Key.C: return 3014656;
            case Key.D: return 2097152;
            case Key.E: return 1179648;
            case Key.F: return 2162688;
            case Key.G: return 2228224;
            case Key.H: return 2293760;
            case Key.I: return 1507328;
            case Key.J: return 2359296;
            case Key.K: return 2424832;
            case Key.L: return 2490368;
            case Key.M: return 3276800;
            case Key.N: return 3211264;
            case Key.O: return 1572864;
            case Key.P: return 1638400;
            case Key.Q: return 1048576;
            case Key.R: return 1245184;
            case Key.S: return 2031616;
            case Key.T: return 1310720;
            case Key.U: return 1441792;
            case Key.V: return 3080192;
            case Key.W: return 1114112;
            case Key.X: return 2949120;
            case Key.Y: return 1376256;
            case Key.Z: return 2883584;


            case Key.CapsLock: return 3801088;
            case Key.LeftAlt: return 3670016;
            case Key.LeftControl: return 1900544;
            case Key.LeftShift: return 2752512;
            case Key.LeftWindows: return 22740992;
            case Key.Menu: return 22872064;
            case Key.RightAlt: return 20447232;
            case Key.RightControl: return 18677760;
            case Key.RightShift: return 99999999;    //Not a real value, this is a hack and is explictly handled


            case Key.Keypad0: return 5373952;
            case Key.Keypad1: return 5177344;
            case Key.Keypad2: return 5242880;
            case Key.Keypad3: return 5308416;
            case Key.Keypad4: return 4915200;
            case Key.Keypad5: return 4980736;
            case Key.Keypad6: return 5046272;
            case Key.Keypad7: return 4653056;
            case Key.Keypad8: return 4718592;
            case Key.Keypad9: return 4784128;
            case Key.KeypadAdd: return 5111808;
            case Key.KeypadDivide: return 20250624;
            case Key.KeypadEnter: return 18612224;
            case Key.KeypadMultiply: return 3604480;
            case Key.KeypadPeriod: return 5439488;
            case Key.KeypadSubtract: return 4849664;


            case Key.Num0: return 720896;
            case Key.Num1: return 131072;
            case Key.Num2: return 196608;
            case Key.Num3: return 262144;
            case Key.Num4: return 327680;
            case Key.Num5: return 393216;
            case Key.Num6: return 458752;
            case Key.Num7: return 524288;
            case Key.Num8: return 589824;
            case Key.Num9: return 655360;


            case Key.Backspace: return 917504;
            case Key.Delete: return 22216704;
            case Key.ESC: return 65536;
            case Key.End: return 21954560;
            case Key.Enter: return 1835008;
            case Key.Home: return 21430272;
            case Key.NumLock: return 21299200;
            case Key.Pause: return 0;
            case Key.PrintScreen: return 22282240;
            case Key.Space: return 3735552;
            case Key.Tab: return 983040;


            case Key.BackSlash: return 5636096;
            case Key.Comma: return 3342336;
            case Key.Equals: return 851968;
            case Key.GraveAccent: return 2686976;
            case Key.Hash: return 2818048;
            case Key.Insert: return 22151168;
            case Key.LeftBracket: return 1703936;
            case Key.Minus: return 786432;
            case Key.PageDown: return 22085632;
            case Key.PageUp: return 21561344;
            case Key.Quote: return 2621440;
            case Key.RightBracket: return 1769472;
            case Key.Period: return 3407872;
            case Key.ScrollLock: return 4587520;
            case Key.Semicolon: return 2555904;
            case Key.Slash: return 3473408;


            default:
                return 0;
        }
    }

    //c.f.  https://blog.molecular-matters.com/2011/09/05/properly-handling-keyboard-input/
    public static Key TranslateKey(short scancode, VIRTUALKEYCODE vkey, bool extended0)
    {
        uint modifiedScancode = User32.MapVirtualKey(vkey, VIRTUALKEYMAPTYPE.MAPVK_VK_TO_VSC);
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

    /// <summary>
    /// Extension method for Key to get the official Windows readable string for the key. May not handle Left/Right shift properly because eat a dick Bill Gates
    /// </summary>
    public static string ToWindowsString(Key k) => k switch
    {
        Key.Pause => "Pause/Break",
        Key.PrintScreen => "Print screen",
        Key.RightShift => "RIGHT SHIFT",
        _ => User32.GetKeyNameText(KeyTranslator.GetExtendedScanCode(k)),
    };
}
