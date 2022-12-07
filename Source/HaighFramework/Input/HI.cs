using BearsEngine.Input;

namespace BearsEngine
{
    public static class HI
    {
        private static MouseState _prevMouseState;
        private static MouseState _curMouseState;
        private static KeyboardState _prevKBState;
        private static KeyboardState _curKBState;
        

        public static void SetStates(MouseState mouseState, KeyboardState kbState)
        {
            _prevMouseState = _curMouseState;
            _curMouseState = mouseState;
            _prevKBState = _curKBState;
            _curKBState = kbState;
        }
        

        public static int MouseScreenX => _curMouseState.ScreenX;
        public static int MouseScreenY => _curMouseState.ScreenY;
        public static Point MouseScreenP => new(MouseScreenX, MouseScreenY);

        public static float MouseWindowX => BE.Window.ScreenToClient(MouseScreenP).X;
        public static float MouseWindowY => BE.Window.ScreenToClient(MouseScreenP).Y;
        public static Point MouseWindowP => new(MouseWindowX, MouseWindowY);

        public static int MouseXDelta => _curMouseState.AbsX - _prevMouseState.AbsX;
        public static int MouseYDelta => _curMouseState.AbsY - _prevMouseState.AbsY;
        

        public static int MouseWheelDelta => _curMouseState.Wheel - _prevMouseState.Wheel;
        

        public static bool MouseDown(MouseButton m)
        {
            return _curMouseState.IsButtonDown(m);
        }
        

        public static bool MouseUp(MouseButton m)
        {
            return _curMouseState.IsButtonUp(m);
        }
        

        public static bool MousePressed(MouseButton m)
        {

            return _prevMouseState.IsButtonUp(m) &&
                _curMouseState.IsButtonDown(m);
        }
        

        public static bool MouseReleased(MouseButton m)
        {
            return _prevMouseState.IsButtonDown(m) &&
                _curMouseState.IsButtonUp(m);
        }
        

        public static bool MouseLeftDown => MouseDown(MouseButton.Left);
        public static bool MouseLeftUp => MouseUp(MouseButton.Left);
        public static bool MouseLeftPressed => MousePressed(MouseButton.Left);
        public static bool MouseLeftReleased => MouseReleased(MouseButton.Left);
        public static bool MouseLeftDoubleClicked { get; internal set; }
        

        public static bool MouseRightDown => MouseDown(MouseButton.Right);
        public static bool MouseRightUp => MouseUp(MouseButton.Right);
        public static bool MouseRightPressed => MousePressed(MouseButton.Right);
        public static bool MouseRightReleased => MouseReleased(MouseButton.Right);
        
        
        

        public static bool KeyDown(Key k)
        {
            return _curKBState.IsKeyDown(k);
        }
        /// <summary>
        /// returns true if any of the list of keys is down
        /// </summary>
        public static bool KeyDown(List<Key> keys)
        {
            foreach (Key k in keys)
                if (KeyDown(k))
                    return true;
            return false;
        }
        /// <summary>
        /// returns true if any of the list of keys is down
        /// </summary>
        public static bool KeyDown(params Key[] keys)
        {
            foreach (Key k in keys)
                if (KeyDown(k))
                    return true;
            return false;
        }
        

        public static bool KeyUp(Key k)
        {
            return _curKBState.IsKeyUp(k);
        }
        /// <summary>
        /// returns true if any of the list of keys is up
        /// </summary>
        public static bool KeyUp(List<Key> keys)
        {
            foreach (Key k in keys)
                if (KeyUp(k))
                    return true;
            return false;
        }
        /// <summary>
        /// returns true if any of the list of keys is up
        /// </summary>
        public static bool KeyUp(params Key[] keys)
        {
            foreach (Key k in keys)
                if (KeyUp(k))
                    return true;
            return false;
        }
        

        public static bool KeyPressed(Key k)
        {
            return _prevKBState.IsKeyUp(k) &&
                _curKBState.IsKeyDown(k);
        }
        /// <summary>
        /// returns true if any of the list of keys is pressed
        /// </summary>
        public static bool KeyPressed(List<Key> keys)
        {
            foreach (Key k in keys)
                if (KeyPressed(k))
                    return true;
            return false;
        }
        /// <summary>
        /// returns true if any of the list of keys is pressed
        /// </summary>
        public static bool KeyPressed(params Key[] keys)
        {
            foreach (Key k in keys)
                if (KeyPressed(k))
                    return true;
            return false;
        }
        

        public static bool KeyReleased(Key k)
        {
            return _prevKBState.IsKeyDown(k) &&
                _curKBState.IsKeyUp(k);
        }
        /// <summary>
        /// returns true if any of the list of keys is released
        /// </summary>
        public static bool KeyReleased(List<Key> keys)
        {
            foreach (Key k in keys)
                if (KeyReleased(k)) return true;
            return false;
        }
        /// <summary>
        /// returns true if any of the list of keys is released
        /// </summary>
        public static bool KeyReleased(params Key[] keys)
        {
            foreach (Key k in keys)
                if (KeyReleased(k)) return true;
            return false;
        }
        
        
    }
}