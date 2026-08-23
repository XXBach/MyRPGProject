using UnityEngine;
using UnityEngine.Android;
using UnityEngine.TextCore.Text;
using UnityEngine.UnityConsent;
using UnityEngine.Events;
using Unity.Jobs;
using System;
public class Grid
{
    public const int MAX_CELL_VALUE = 100;
    public const int MIN_CELL_VALUE = 0;

    public UnityEvent<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs
    {
        public int x { get; set; }
        public int y { get; set; }

        public OnGridValueChangedEventArgs(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private int _width;
    private int _height;
    private float _cellSize;
    private int[,] _gridArray;
    private Vector3 _originPosition; // The origin position of the grid in world space
    private TextMesh[,] _DebugtextMesh;
    public int Width => _width;
    public int Height => _height;
    public float CellSize => _cellSize;
    public Grid(int width, int height, float cellSize, Font font, Vector3 originPosition)
    {
        _width = width;
        _height = height;
        _originPosition = originPosition;
        _cellSize = cellSize;
        _gridArray = new int[width, height];
        _DebugtextMesh = new TextMesh[width, height];
        OnGridValueChanged = new UnityEvent<OnGridValueChangedEventArgs>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _DebugtextMesh[i, j] = TextUtils.CreateWorldText(null, _gridArray[i, j].ToString(), GetCellWorldPosition(i, j) + new Vector3(cellSize, cellSize) * 0.5f, 50, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center, font);
                Debug.DrawLine(GetCellWorldPosition(i, j), GetCellWorldPosition(i, j + 1), Color.white, 100f);
                Debug.DrawLine(GetCellWorldPosition(i, j), GetCellWorldPosition(i + 1, j), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetCellWorldPosition(0, height), GetCellWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetCellWorldPosition(width, 0), GetCellWorldPosition(width, height), Color.white, 100f);
    }
    public Vector3 GetCellWorldPosition(int x, int y)
    {
        return _originPosition + new Vector3(x, y) * _cellSize;
    }
    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x - _originPosition.x) / _cellSize);
        int y = Mathf.FloorToInt((worldPosition.y - _originPosition.y) / _cellSize);
        return new Vector2Int(x, y);
    }
    public void SetCellValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridArray[x, y] = Mathf.Clamp(value, MIN_CELL_VALUE, MAX_CELL_VALUE);
            _DebugtextMesh[x, y].text = _gridArray[x, y].ToString();
            OnGridValueChanged?.Invoke(new OnGridValueChangedEventArgs(x, y));
        }
    }
    public void SetCellValue(Vector3 worldPosition, int value)
    {
        Vector2Int gridPosition = GetGridPosition(worldPosition);
        SetCellValue(gridPosition.x, gridPosition.y, value);
    }
    public static Vector3 GetMousePositionWithZ()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(Vector3.zero).z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    public int GetCellValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x, y];
        }
        else
        {
            return -1; // Return -1 if the cell is out of bounds
        }
    }
    public int GetCellValue(Vector3 worldPosition)
    {
        Vector2Int gridPosition = GetGridPosition(worldPosition);
        return GetCellValue(gridPosition.x, gridPosition.y);
    }
    public void AddValue(int x, int y, int value)
    {
        SetCellValue(x,y,GetCellValue(x, y) + value);
    }
    public void AddValue(Vector3 worldPosition, int fullValue, int fullValueRange, int totalRange)
    {
        Vector2 currentGridCellPosition = GetGridPosition(worldPosition); //CurrentGridPosition store Origin x and y of the current grid cell that the mouse pointer pointed to  
        int lowerValueAmount = Mathf.RoundToInt((float)fullValue / (totalRange - fullValueRange));
        for (int offsetX = 0; offsetX < totalRange; offsetX++) 
        {
            for (int offsetY = 0; offsetY < totalRange - offsetX; offsetY++) 
            {
                int radius = offsetX + offsetY;
                int addValueAmount = fullValue;
                if(radius > fullValueRange)
                {
                    addValueAmount -= lowerValueAmount * (radius - fullValueRange);
                }
                AddValue((int)currentGridCellPosition.x + offsetX, (int)currentGridCellPosition.y + offsetY, addValueAmount);
                if(offsetX != 0)
                {
                    AddValue((int)currentGridCellPosition.x - offsetX, (int)currentGridCellPosition.y + offsetY, addValueAmount);
                }
                if(offsetY != 0)
                {
                    AddValue((int)currentGridCellPosition.x + offsetX, (int)currentGridCellPosition.y - offsetY, addValueAmount); 
                    if(offsetX != 0) AddValue((int)currentGridCellPosition.x - offsetX, (int)currentGridCellPosition.y - offsetY, addValueAmount);
                }
            }
        }
    }
}
