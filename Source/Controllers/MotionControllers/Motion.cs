﻿namespace BearsEngine.Controllers;

public class MotionTween : Tween
{
    public MotionTween(float duration, PersistType persistType = PersistType.Persist, Action actionOnCompleted = null, Func<float, float>? easer = null)
        : base(duration, persistType, actionOnCompleted, easer)
    {
    }

    public float X { get; protected set; }

    public float Y { get; protected set; }
}