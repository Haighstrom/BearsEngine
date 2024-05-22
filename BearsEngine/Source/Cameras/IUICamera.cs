using BearsEngine.Graphics.Shaders;

namespace BearsEngine.Worlds.Cameras;

public interface IUICamera : IRectAddable, IUpdatable, IRenderableOnLayer, IContainer, IPosition
{
    MSAA_SAMPLES MSAASamples { get; set; }

    IShader Shader { get; set; }
}
