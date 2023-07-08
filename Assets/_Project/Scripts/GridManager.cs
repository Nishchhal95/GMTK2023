using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int gridRows;
    [SerializeField] private int gridColumns;
    
    [SerializeField] private Transform startPoint;
    [SerializeField] private TilePrefabsToProbability[] gridItemPrefabs;
    [SerializeField] private TilePrefabsToProbabilityLimits[] gridItemLimits;
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
        gridItemLimits = new TilePrefabsToProbabilityLimits[gridItemPrefabs.Length];
        SetupLimits();
        SpawnGrid();
    }

    private void SetupLimits()
    {
        float maxNumber = 0;
        for (int i = 0; i < gridItemPrefabs.Length; i++)
        {
            gridItemLimits[i] = new TilePrefabsToProbabilityLimits
            {
                tilePrefab = gridItemPrefabs[i].tilePrefab,
                lowerLimit = maxNumber,
                upperLimit = maxNumber + gridItemPrefabs[i].probability
            };
            maxNumber = gridItemLimits[i].upperLimit;
        }
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
        float maxProbablity = 0;
        for (int i = 0; i < gridItemPrefabs.Length; i++)
        {
            maxProbablity += gridItemPrefabs[i].probability;
        }
        float randomProbability = Random.Range(0f, maxProbablity);
        GridItem randomPrefab = null;

        for (int i = 0; i < gridItemLimits.Length; i++)
        {
            if (randomProbability >= gridItemLimits[i].lowerLimit && randomProbability < gridItemLimits[i].upperLimit)
            {
                randomPrefab = gridItemLimits[i].tilePrefab;
                break;
            }
        }
        
        GridItem gridItem = Instantiate(randomPrefab, startPoint.position + new Vector3(column * gridItemWidth, 0, row * gridItemHeight), Quaternion.identity, transform);
        gridItem.transform.name = $"Grid Item {column},{row}";

        if ((column == 0 && row == 5) || (column == 0 && row == 6) || (column == 0 && row == 7) || (column == 1 && row == 7) || (column == 7 && row == 7))
        {
            gridItem.gameObject.SetActive(false);
            gridItem.BlockType = BlockType.Stone;
        }
        
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

    [Serializable]
    public class TilePrefabsToProbability
    {
        public GridItem tilePrefab;
        [Range(0f,100f)]
        public float probability;
    }
    
    [Serializable]
    public class TilePrefabsToProbabilityLimits
    {
        public GridItem tilePrefab;
        public float lowerLimit;
        public float upperLimit;
    }
}
