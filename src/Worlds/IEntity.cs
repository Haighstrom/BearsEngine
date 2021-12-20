using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine.Tweens;
using BearsEngine.Worlds.Controllers;

namespace BearsEngine.Worlds
{
    public interface IEntity : IRectAddable, IUpdatable, IRenderableOnLayer, IContainer, ICollideable, IClickable
    {
        #region Properties
        bool IsOnScreen { get; }

        #region Angle
        /// <summary>
        /// Angle in Degrees
        /// </summary>
        float Angle { get; set; }
        #endregion

        List<IGraphic> Graphics { get; }
        #endregion

        #region Methods
        void ClearGraphics();

        #region MoveTowards
        /// <summary>
        /// returns how much overshoot there was (i.e. how much asked to be moved minus what did move), will be zero if didn't reach target yet
        /// </summary>
        /// <param name="target"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        float MoveTowards(Point target, float amount);

        /// <summary>
        /// returns how much overshoot there was (i.e. how much asked to be moved minus what did move), will be zero if didn't reach target yet
        /// </summary>
        /// <param name="target"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        float MoveTowards(float x, float y, float amount);
        #endregion
        #endregion
    }
}