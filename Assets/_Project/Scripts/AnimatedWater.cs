using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedWater : MonoBehaviour
{

    public float speedX = 0.1f;
    public float speedY = 0.1f;
    private float curX; 
    private float curY; 
    [SerializeField] private Renderer _renderer; 
    // Start is called before the first frame update
    void Start()
    {
        curX = _renderer.material.mainTextureOffset.x;
        curY = _renderer.material.mainTextureOffset.y;
    }

    // Update is called once per frame
    void Update()
    {
        curX += Time.deltaTime * speedX;
        curY += Time.deltaTime * speedY;
        _renderer.material.SetTextureOffset("_MainTex", new Vector2(curX,curY));
    }
}
