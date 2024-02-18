using System;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public Vector2 tiling = new Vector2(-1f, -0.2f);
    public Vector2 offset = new Vector2(0, 0f);
    Renderer rend;
    
    public float delayTime = 2f;
    private float timeElapsed = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>(); // Hole den Renderer des GameObjects
        if (rend != null && rend.sharedMaterial != null)
        {
            rend.sharedMaterial.mainTextureScale = tiling;
            rend.sharedMaterial.mainTextureOffset = offset;
        }
    }

    private void Update()
    {
        for (int i = 0; i < 10000; i++)
        {
            if (i == 9999)
            {
                updateBlock();
                i = 1;
            }
        }
    }

    void updateBlock()
    {
        rend.sharedMaterial.mainTextureOffset += offset;
    }
}