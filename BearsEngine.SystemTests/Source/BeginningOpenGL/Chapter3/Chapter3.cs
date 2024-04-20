//using System;
//using System.Collections.Generic;
//using BearsEngine;
//using BearsEngine.PInvoke;
//using BearsEngine.Worlds;
//using BearsEngine.OpenGL;

//namespace HaighEngineDemo.Source.BeginningOpenGL.Chapter3
//{
//    public class Chapter3 : World
//    {
//        private Camera _camera;

//        public static void Run()
//        {
//            using (IGameEngine engine = new HaighEngine(GP.DefaultClientSize.W, GP.DefaultClientSize.H, true, "Chapter 3", new Chapter3()))
//            {
//                engine.Run(30);
//            }
//        }

//        //SolidColour SCVBO;

//        public Chapter3()
//        {
//        }

//        public override void Start()
//        {
//            Add(_camera = new Camera(GL.CAMERA, GP.DefaultCameraViewport, GP.DefaultCameraSize));
//            _camera.SetCameraScrolling(false, true);
//            _camera.ScrollSpeed = 200;
//            Add(new Bear(0, 0), _camera);
//            Add(new Bear(170, 0), _camera);
//            Add(new Bear(0, 160), _camera);
//            Add(new Bear(170, 160), _camera);
//            Add(new Bear(85, 80), _camera);
//            //SCVBO = new SolidColour();
//            HV.ScreenColour = Colour.CornflowerBlue;
//        }

//        public override void Render(double elapsed)
//        {
//            base.Render(elapsed);
//            //SCVBO.Draw(new Rect(100, 100, 50, 50), Colour.Blue);
//        }
//    }
//}
