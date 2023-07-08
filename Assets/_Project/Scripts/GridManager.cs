using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int gridRows;
    [SerializeField] private int gridColumns;
    
    [SerializeField] private Transform startPoint;
    [SerializeField] private GridItem gridItemPrefab;
    [SerializeField] private float gridItemWidth;
    [SerializeField] private float gridItemHeight;

    public GridItem[,] GridItems { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        GridItems = new GridItem[gridRows, gridColumns];
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridColumns; col++)
            {
                SpawnGridElement(col, row);
            }
        }
    }

    private void SpawnGridElement(int column, int row)
    {
        GridItem gridItem = Instantiate(gridItemPrefab, startPoint.position + new Vector3(column * gridItemWidth, 0, row * gridItemHeight), Quaternion.identity, transform);
        //gridItem.SetBlockType((BlockType)Random.Range(0,3));
        gridItem.transform.name = $"Grid Item {column},{row}";
        GridItems[column, row] = gridItem;
    }

    public GridItem GetGridItem(int x, int y)
    {
        return GridItems[x, y];
    }
    
    public Vector3 GetGridItemRealWorldPosition(int x, int y)
    {
        return GridItems[x, y].transform.position;
    }
    
    public Vector3 GetGridItemRealWorldPosition(Vector2Int vector2Int)
    {
        return GridItems[vector2Int.x, vector2Int.y].transform.position;
    }

    public Vector2Int GetGridItemGridPosition(GridItem gridItem)
    {
        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridColumns; col++)
            {
                if (GridItems[col, row] == gridItem)
                {
                    return new Vector2Int(col, row);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }

    public int GetGridRows()
    {
        return gridRows;
    }
    
    public int GetGridColumns()
    {
        return gridColumns;
    }
}
