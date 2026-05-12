using BearsEngine.Input;
using BearsEngine.Worlds.Cameras;

namespace BearsEngine.Source.Controllers.CameraControllers;

public class CameraRightDragPanController(ICamera camera, IMouse mouse) : UpdateableBase
{
    private bool _dragging;

    public override void Update(float elapsed)
    {
        if (mouse.RightUp)
        {
            _dragging = false;
        }

        if (_dragging && mouse.RightDown)
        {
            var cam = camera;
            var camMoveX = mouse.XDelta / cam.TileWidth;
            var camMoveY = mouse.YDelta / cam.TileHeight;

            if (cam.MinX < cam.MaxX - cam.View.W)
            {
                cam.View.X = Maths.Clamp(cam.View.X + camMoveX, cam.MinX, cam.MaxX - cam.View.W);
            }

            if (cam.MinY < cam.MaxY - cam.View.H)
            {
                cam.View.Y = Maths.Clamp(cam.View.Y + camMoveY, cam.MinY, cam.MaxY - cam.View.H);
            }
        }

        if (mouse.RightPressed && camera.MouseIntersecting)
        {
            _dragging = true;
        }
    }
}
