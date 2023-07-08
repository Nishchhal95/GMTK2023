using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    private BlockType blockType;
    [SerializeField] private Renderer renderer;

    private static Dictionary<BlockType, Color> BlockToColorDictionary = new Dictionary<BlockType, Color>()
    {
        { BlockType.Normal, Color.green },
        { BlockType.Stone, Color.gray },
        { BlockType.Barren, Color.black }
    };

    public void SetBlockType(BlockType blockType)
    {
        this.blockType = blockType;
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetColor("_Color", BlockToColorDictionary[blockType]);
        renderer.SetPropertyBlock(materialPropertyBlock);
    }

    public BlockType GetBlockType()
    {
        return blockType;
    }
}

public enum BlockType
{
    Normal,
    Stone,
    Barren
}