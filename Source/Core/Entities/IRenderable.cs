namespace BearsEngine;

public interface IRenderable
{
    bool Visible { get; set; }
    void Render(ref Matrix3 projection, ref Matrix3 modelView);
}