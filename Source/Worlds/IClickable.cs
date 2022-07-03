namespace BearsEngine.Worlds
{
    public interface IClickable
    {
        bool Clickable { get; }
        bool Visible { get; }
        bool MouseIntersecting { get; }

        void OnMouseEnter();
        void OnMouseExit();
        void OnLeftDown();
        void OnLeftPressed();
        void OnLeftReleased();
        void OnLeftClicked();
        void OnLeftDoubleClicked();
        void OnRightDown();
        void OnRightPressed();
        void OnRightReleased();
        void OnRightClicked();
        void OnHover();
        void OnNoMouseEvent();
    }
}
