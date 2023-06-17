namespace BearsEngine.Graphics
{
    public class HPBar : FillableBar
    {
        public HPBar(string graphicPath, Rect r, float initialPercentage = 1)
            : base(graphicPath, r, initialPercentage)
        {
        }

        public HPBar(string graphicPath, float width, float height, float initialPercentage = 1)
            : base(graphicPath, width, height, initialPercentage)
        {
        }

        public HPBar(string graphicPath, Point size, float initialPercentage = 1)
            : base(graphicPath, size, initialPercentage)
        {
        }

        public HPBar(string graphicPath, float x, float y, float width, float height, float initialPercentage = 1)
            : base(graphicPath, x, y, width, height, initialPercentage)
        {
        }

        public HPBar(string graphicPath, Point size, Point offset, float initialPercentage = 1)
            : base(graphicPath, offset, size, initialPercentage)
        {
        }
        

        public override float AmountFilled
        {
            get => base.AmountFilled;
            set
            {
                base.AmountFilled = value;
                if (value > HighHPThreshold)
                    Colour = HighHPColour;
                else if (value > MidHPThreshold)
                    Colour = MidHPColour;
                else
                    Colour = LowHPColour;
            }
        }
        

        public float HighHPThreshold { get; set; } = 0.70f;
        public float MidHPThreshold { get; set; } = 0.30f;
        public Colour HighHPColour { get; set; } = Colour.Green;
        public Colour MidHPColour { get; set; } = Colour.Yellow;
        public Colour LowHPColour { get; set; } = Colour.Red;
        
    }
}