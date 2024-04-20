using BearsEngine.SystemTests.Source.Globals;

namespace BearsEngine.SystemTests.Source.BearSpinner;

public class Bear : Entity
{
    private readonly float rotateSpeed, swaySpeed, alphaShift, alphaSpeed;
    private readonly int xAnchor, yAnchor, xSway, ySway;
    private double totalElapsed;
    private readonly Image _image;

    public Bear(int x, int y)
        : base(100, new Rect(x, y, 60, 80))
    {
        Add(_image = new(GA.GFX.WhiteBear, 60, 80)
        {
            Angle = Randomisation.Rand(360),
            Colour = Randomisation.RandSystemColour(),
        });

        alphaShift = Randomisation.RandF(0, 100);
        rotateSpeed = 10 * Randomisation.RandF(-10, 10);
        swaySpeed = Randomisation.RandF(-4, 4);
        alphaSpeed = Randomisation.RandF(0, 5);
        xAnchor = x;
        yAnchor = y;
        xSway = Randomisation.Rand(0, 500);
        ySway = Randomisation.Rand(0, 500);
    }

    public override void Update(float elapsed)
    {
        base.Update(elapsed);
        totalElapsed += elapsed;
        _image.Alpha = (byte)((1 + Math.Sin(alphaShift + alphaSpeed * totalElapsed)) * 128);
        _image.Angle += rotateSpeed * (float)elapsed;
        X = xAnchor + xSway * (float)Math.Sin(swaySpeed * totalElapsed);
        Y = yAnchor + ySway * (float)Math.Cos(swaySpeed * totalElapsed);
    }
}
