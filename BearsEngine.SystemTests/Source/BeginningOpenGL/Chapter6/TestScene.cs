//using HaighFramework;
//using HaighFramework.PInvoke;
//using HaighFramework.OpenGL;

//namespace HaighEngineDemo.Source.BeginningOpenGL.Chapter6
//{
//    public class TestScene :IScene
//    {
//        VBO VBO;

//        public bool Active { get; set; } = true;

//        public TestScene()
//        {
//        }

//        public void Start()
//        {
//            VBO = new VBO(SolidColourShader.Instance, 100, 100);
//            VBO.Colour = Colour.Red;
//        }

//        public void Update(double elapsed)
//        {

//        }

//        public void Render(double elapsed)
//        {
//            OpenGL32.glClear(ClearBufferMask.GL_COLOR_BUFFER_BIT | ClearBufferMask.GL_DEPTH_BUFFER_BIT);
//            OpenGL32.glClearColor(Colour.CornflowerBlue);

//            Rect r = new Rect(100, 100, 100, 100);
//            Matrix4 m = Matrix4.CreateOrtho(800, 600);
//            VBO.Draw(ref m);

//            OpenGLErrorCode error = OpenGL32.glGetError();
//            if (error != OpenGLErrorCode.NO_ERROR)
//                HF.Log(error);
//        }

//        public void OnResize(object sender, SizeEventArgs e)
//        {
//            OpenGL32.glViewport(0, 0, e.Width, e.Height);
//        }

//        public void End()
//        {
//        }
//    }
//}