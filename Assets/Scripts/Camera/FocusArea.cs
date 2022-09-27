using UnityEngine;

public struct FocusArea
{
    public Vector2 Center;
    public Vector2 Velocity;

    private float _left;
    private float _right;
    private float _top;
    private float _bottom;

    public FocusArea(Bounds targetBounds, Vector2 size)
    {
        _left = targetBounds.center.x - size.x / 2;
        _right = targetBounds.center.x + size.x / 2;
        _bottom = targetBounds.min.y;
        _top = targetBounds.min.y + size.y;

        Velocity = Vector2.zero;
        Center = new Vector2((_left + _right) / 2, (_top + _bottom) / 2);
    }

    public void Update(Bounds target)
    {
        float shiftX = 0;

        if (target.min.x < _left)
            shiftX = target.min.x - _left;
        else if (target.max.x > _right)
            shiftX = target.max.x - _right;

        _left += shiftX;
        _right += shiftX;

        float shiftY = 0;

        if (target.min.y < _bottom)
            shiftY = target.min.y - _bottom;
        else if (target.max.y > _top)
            shiftY = target.max.y - _top;

        _bottom += shiftY;
        _top += shiftY;

        Center = new Vector2((_left + _right) / 2, (_top + _bottom) / 2);
        Velocity = new Vector2(shiftX, shiftY);
    }
}