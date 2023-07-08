using UnityEngine;

public class GridItem : MonoBehaviour
{
    [field: SerializeField]public BlockType BlockType { get; set; }
}

public enum BlockType
{
    Normal,
    Stone,
    Water,
    Sun,
}