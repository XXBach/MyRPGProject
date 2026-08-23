using UnityEngine;
using UnityEngine.InputSystem;

public class DebugCameraController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera _camera;

    [Header("Zoom")]
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _minZoom = 2f;
    [SerializeField] private float _maxZoom = 50f;

    [Header("Pan")]
    [SerializeField] private float _panSpeed = 1f;

    [Header("Select")]
    [SerializeField] private InputActionReference _selectAction;

    private void Awake()
    {
        if (_camera == null)
            _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        _selectAction.action.performed += OnSelect;
    }

    private void OnDisable()
    {
        _selectAction.action.performed -= OnSelect;
    }

    private void Update()
    {
        HandleZoom();
        HandlePan();
    }

    // =========================
    // Zoom
    // =========================

    private void HandleZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll == 0f)
            return;

        _camera.orthographicSize -= scroll * _zoomSpeed * Time.deltaTime;

        _camera.orthographicSize = Mathf.Clamp(
            _camera.orthographicSize,
            _minZoom,
            _maxZoom
        );
    }

    // =========================
    // Pan
    // =========================

    private void HandlePan()
    {
        if (!Mouse.current.middleButton.isPressed)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        Vector3 movement = new Vector3(
            mouseDelta.x,
            mouseDelta.y,
            0f
        );

        transform.position -= movement * _panSpeed * Time.deltaTime;
    }

    // =========================
    // Select
    // =========================

    private void OnSelect(InputAction.CallbackContext context)
    {
        MoveToMouse();
    }

    private void MoveToMouse()
    {
        Vector2 mouseScreenPosition =
            Mouse.current.position.ReadValue();

        Vector3 mouseWorldPosition =
            _camera.ScreenToWorldPoint(mouseScreenPosition);

        mouseWorldPosition.z = transform.position.z;

        transform.position = mouseWorldPosition;
    }
}