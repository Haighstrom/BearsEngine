//using System;
//using System.Collections.Generic;
//using System.Linq;
//using HaighFramework;
//using HaighFramework.Worlds;

//namespace HaighEngineDemo.Source.Animations
//{
//    public class AnimWorld : World
//    {
//        public static void Run()
//        {
//            using (IGameEngine engine = new HaighEngine(800, 600, true, "Animations", new AnimWorld()))
//            {
//                engine.Run(60);
//            }
//        }

//        public override void Start()
//        {
//            base.Start();
//            Add(new AnimEntity());
//        }
//    }
//}
