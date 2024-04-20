namespace BearsEngine;

internal interface IClickable : IRenderableOnLayer, IUpdatable
{
    bool Clickable { get; }

    bool MouseIntersecting { get; }

    float TimeToHover { get; }

    void OnLeftPressed();

    void OnLeftReleased();

    void OnLeftClicked();

    void OnMouseEntered();

    void OnMouseExited();

    void OnMouseHovered();

    void OnNoMouseEvent();
}