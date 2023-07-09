using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private EnergyManagement energyManagement;

    private Vector2Int moveDir = new Vector2Int();
    private Vector2Int currentGridPos = new Vector2Int();
    private bool isMoving = false;
    private int energyLevel = 5;
    


    private void Start()
    {
        MoveToSpawnPoint();
    }

    private void MoveToSpawnPoint()
    {
        List<GridItem> movableGridItems = new List<GridItem>();

        for (int i = 0; i < GridManager.Instance.GridItems.GetLength(0); i++)
        {
            for (int y = 0; y < GridManager.Instance.GridItems.GetLength(1); y++)
            {
                if (GridManager.Instance.GridItems[i, y].BlockType != BlockType.Stone)
                {
                    movableGridItems.Add(GridManager.Instance.GridItems[i, y]);
                }
            }
        }

        int randomIndex = Random.Range(0, movableGridItems.Count);
        int randomRow = GridManager.Instance.GetGridItemGridPosition(movableGridItems[randomIndex]).x;
        int randomCol = GridManager.Instance.GetGridItemGridPosition(movableGridItems[randomIndex]).y;
        MoveToTargetPosition(randomRow, randomCol, true);
    }

    private void MoveToTargetPosition(int row, int col, bool instant = false)
    {
        currentGridPos = new Vector2Int(row, col);
        Vector3 targetPosition = GridManager.Instance.GetGridItemRealWorldPosition(currentGridPos);
        targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        StartCoroutine(MoveToTargetPositionRoutine(targetPosition, instant));
    }

    private IEnumerator MoveToTargetPositionRoutine(Vector3 targetPosition, bool instant = false)
    {
        if (instant)
        {
            transform.position = targetPosition;
            isMoving = false;
            yield break;
        }
        
        float waitTime = .5f;
        float elapsedTime = 0;
        Vector3 currentPos = transform.position;
        playerAnimator.SetTrigger("Hop");
        AudioManager.Instance.PlayHopAudio();
        while (elapsedTime < waitTime)
        {
            transform.position = Vector3.Lerp(currentPos, targetPosition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
        OnFinishedMove();
    }

    private void OnFinishedMove()
    {
        GridItem currentGridItem = GridManager.Instance.GetGridItem(currentGridPos.x, currentGridPos.y);
        if(currentGridItem.BlockType == BlockType.Sun)
        {
            AudioManager.Instance.PlayCollectSunlightAudio();
        }
        else if(currentGridItem.BlockType == BlockType.Water)
        {
            AudioManager.Instance.PlayCollectWaterAudio();
        }

        HandleTypeCollection(currentGridItem);
    }

    private void HandleTypeCollection(GridItem currentGridItem)
    {
        energyLevel = energyManagement.CostEnergy(energyLevel);
        

    }

    private void Update()
    {
        HandleMovement();
        MovePlayer();
    }

    private void HandleMovement()
    {
        moveDir = Vector2Int.zero;
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDir = new Vector2Int(0, 1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveDir = new Vector2Int(-1, 0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveDir = new Vector2Int(0, -1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveDir = new Vector2Int(1, 0);
        }
    }

    private void MovePlayer()
    {
        if (moveDir == Vector2Int.zero || isMoving)
        {
            return;
        }

        if (CheckCanMove())
        {
            isMoving = true;
            MovePlayerToDestination();
        }
    }

    private void MovePlayerToDestination()
    {
        Vector2Int destinationGridPos = currentGridPos + moveDir;
        MoveToTargetPosition(destinationGridPos.x, destinationGridPos.y);
    }

    private bool CheckCanMove()
    {
        Vector2Int destinationGridPos = currentGridPos + moveDir;
        if (destinationGridPos.x < 0 || destinationGridPos.y < 0 ||
            destinationGridPos.x >= GridManager.Instance.GridItems.GetLength(0) ||
            destinationGridPos.y >= GridManager.Instance.GridItems.GetLength(1))
        {
            return false;
        }

        if (GridManager.Instance.GetGridItem(destinationGridPos.x, destinationGridPos.y).BlockType ==
            BlockType.Stone)
        {
            return false;
        }

        if(energyLevel <= 0)
        {
            Debug.Log(energyLevel);
            return false;
        }    

        return true;
    }
}