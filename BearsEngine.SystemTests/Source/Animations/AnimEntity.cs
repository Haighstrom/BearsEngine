//using System;
//using System.Collections.Generic;
//using System.Linq;
//using HaighFramework;
//using HaighFramework.Input;

//namespace HaighEngineDemo.Source.Animations
//{
//    public class AnimEntity : UIEntity
//    {
//        public AnimEntity()
//        : base(GL.CHARACTER, new Rect(100, 100, 160, 240), new Animation(GA.GFX.SPRITE_MAN, 160, 240, 3, 4))
//        {
//        }

//        public override void Update(double elapsed)
//        {
//            base.Update(elapsed);
//            if (HI.KeyPressed(Key.Up))
//                Animation.Play(LoopType.Looping, 0, 1, 0, 2);
//            else if (HI.KeyPressed(Key.Right))
//                Animation.Play(LoopType.Looping, 3, 4, 3, 5);
//            else if (HI.KeyPressed(Key.Down))
//                Animation.Play(LoopType.Looping, 6, 7, 6, 8);
//            else if (HI.KeyPressed(Key.Left))
//                Animation.Play(LoopType.Looping, 9, 10, 9, 11);

//            if (HI.KeyReleased(Key.Up))
//                Animation.Play(0);
//            else if (HI.KeyReleased(Key.Right))
//                Animation.Play(3);
//            else if (HI.KeyReleased(Key.Down))
//                Animation.Play(6);
//            else if (HI.KeyReleased(Key.Left))
//                Animation.Play(9);
//        }
//    }
//}