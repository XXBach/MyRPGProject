using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class GridTesting : MonoBehaviour
{
    [SerializeField]private Font _font;
    [SerializeField]private InputActionAsset _inputActionAsset;
    [SerializeField]private HeatMapVisual _heatMapVisual;
    private InputAction _selectAction;
    private Grid _gridTest;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 WorldPoint = Camera.main.ViewportToWorldPoint(Vector3.zero);
        WorldPoint.z = 0;
        _gridTest = new Grid (60, 100, 1f, _font, WorldPoint);
        _selectAction = _inputActionAsset.FindAction("Select");
        _heatMapVisual.SetGrid(_gridTest);
    }

    // Update is called once per frame
    void Update()
    {
        if (_selectAction.WasPressedThisFrame())
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            //int currentCellValue = _gridTest.GetCellValue(Camera.main.ScreenToWorldPoint(mousePosition));
            //_gridTest.SetCellValue(Camera.main.ScreenToWorldPoint(mousePosition), currentCellValue + 5);
            _gridTest.AddValue(Camera.main.ScreenToWorldPoint(mousePosition), 100, 5, 20);
            Debug.Log(_gridTest.GetCellValue(Camera.main.ScreenToWorldPoint(mousePosition)).ToString());
        }
    }
    private void OnEnable()
    {
        _inputActionAsset.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        _inputActionAsset.FindActionMap("Player").Disable();
    }
}
