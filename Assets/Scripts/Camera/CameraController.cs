using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Collider2D Target;
    public Vector2 FocusAreaSize;
    public float VerticalOffset;
    public float LookAheadDstX;
    public float LookSmoothX;

    [Header("Debug Settings")]
    public bool IsDebug;
    public Color AreaColor;

    private FocusArea _focusArea;
    private Vector2 _focusPosition = new Vector2();
    private float _offsetZ = -10f;
    private float _currentLookAheadX;
    private float _targetLookAheadX;
    private float _lookAheadDirX;
    private float _smoothLookVelocityX;

    private void Start()
    {
        _focusArea = new FocusArea(Target.bounds, FocusAreaSize);
    }

    private void LateUpdate()
    {
        _focusArea.Update(Target.bounds);
        _focusPosition = _focusArea.Center + Vector2.up * VerticalOffset;

        if (_focusArea.Velocity.x != 0)
        {
            _lookAheadDirX = Mathf.Sign(_focusArea.Velocity.x);
        }

        _targetLookAheadX = _lookAheadDirX * LookAheadDstX;
        _currentLookAheadX = Mathf.SmoothDamp(_currentLookAheadX, _targetLookAheadX, ref _smoothLookVelocityX, LookSmoothX);

        _focusPosition += Vector2.right * _currentLookAheadX;

        transform.position = (Vector3)_focusPosition + Vector3.forward * _offsetZ;
    }

    private void OnDrawGizmos()
    {
        if (IsDebug)
        {
            Gizmos.color = AreaColor;
            Gizmos.DrawCube(_focusArea.Center, FocusAreaSize);
        }
    }
}