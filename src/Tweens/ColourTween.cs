using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaighFramework;

namespace BearsEngine.Tweens
{
    public class ColourTween : Tween
    {
        public Colour Colour;
        private Colour _startColour;
        private Colour _colourRange;

        public ColourTween(Colour fromColour, Colour toColour, float duration, PersistType persistence = PersistType.Persist, Action actionOnComplete = null, Easer easer = null)
            : base(duration, persistence, actionOnComplete, easer)
        {
            Colour = _startColour = fromColour;
            _colourRange = new Colour(toColour.R - fromColour.R, toColour.G - fromColour.G, toColour.B - fromColour.B, toColour.A - fromColour.A);
        }

        public override void Update(double elapsed)
        {
            base.Update(elapsed);

            Colour = _startColour + _colourRange * Progress;
        }
    }
}