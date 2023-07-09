using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image fillerImage;
    [SerializeField] private float MAX_TIME_TO_MOVE = 10;
    [SerializeField] private float currentTimer = 10;
    private bool canMove = true;
    [SerializeField] private EnergyManagement _energyManagement;
    [SerializeField] private float timeAlive;

    private void Start()
    {
        currentTimer = MAX_TIME_TO_MOVE;
    }

    public void AddTimeValue()
    {
        currentTimer += 2f;
        currentTimer = Mathf.Clamp(currentTimer, 0, MAX_TIME_TO_MOVE);
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        timeAlive += Time.deltaTime;
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            canMove = false;
            _energyManagement.ShowGameOver();
        }
        SetFill(currentTimer, 0, MAX_TIME_TO_MOVE);
    }

    public void SetFill(float fillValue, float minValue, float maxValue)
    {
        float imageFillValue = Remap(fillValue, minValue, maxValue, 0, 1);
        fillerImage.fillAmount = imageFillValue;
    }
    
    public float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public float GetTimeAlive()
    {
        return timeAlive;
    }
}
