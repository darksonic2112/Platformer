using UnityEngine;

public class QuestionBlockAnimation : MonoBehaviour
{
    public Vector2 tiling = new Vector2(-1f, -0.2f);
    public Vector2 startOffset = new Vector2(0f, 0f);
    public Vector2 endOffset = new Vector2(1f, 0f);
    public float transitionDuration = 10f;
    public float animationSpeed = 1f;
    public float yOffset = -0.2f;

    private Renderer rend;
    private Vector2 currentOffset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial.mainTextureScale = tiling;
        rend.sharedMaterial.mainTextureOffset = startOffset;
        currentOffset = startOffset; 
    }

    void Update()
    {
        float time = Mathf.Repeat(Time.time * animationSpeed, transitionDuration);
        float t = Mathf.InverseLerp(0f, transitionDuration, time);
        Vector2 newOffset = Vector2.Lerp(startOffset, endOffset, t);
        if (newOffset.y < currentOffset.y + yOffset)
        {
            currentOffset.y += yOffset;
            rend.sharedMaterial.mainTextureOffset = currentOffset;
        }
    }
}