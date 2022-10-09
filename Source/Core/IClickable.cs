namespace BearsEngine;

internal interface IClickable : IRenderableOnLayer, IUpdatable
{
    bool Clickable { get; }
    bool MouseIntersecting { get; }

    void OnMouseEntered();
    void OnMouseExited();
    void OnLeftPressed();
    void OnLeftReleased();
    void OnLeftClicked();
    void OnMouseHovered();
}
