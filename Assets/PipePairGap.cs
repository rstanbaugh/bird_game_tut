using UnityEngine;

public class PipePairGap : MonoBehaviour
{

    public Transform topPipe;
    public Transform bottomPipe;

    public float birdHeight = 4.25f; // approx diameter of bird sprite in units
    [Range(0f, 3f)] public float gapMultiplier = 2.0f;  // 200% default

    void OnValidate() => Apply();
    void Reset() => Apply();
    void OnEnable() => Apply();

    void Apply()
    {
        if (!topPipe || !bottomPipe) return;

        float targetGap = birdHeight * gapMultiplier;

        // 2) Measure current gap using renderers' world bounds
        var topR = topPipe.GetComponentInChildren<Renderer>();
        var botR = bottomPipe.GetComponentInChildren<Renderer>();
        if (!topR || !botR) return;

        float topBottomEdge = topR.bounds.min.y;   // bottom edge of top pipe
        float bottomTopEdge = botR.bounds.max.y;   // top edge of bottom pipe
        float currentGap = topBottomEdge - bottomTopEdge;

        // 3) Compute how much to add/remove
        float deltaGapWorld = targetGap - currentGap;
        if (Mathf.Abs(deltaGapWorld) < 1e-4f) return; // already good

        // Convert world delta to this pair's local space (handles parent scaling)
        float toLocalY = 1f / transform.lossyScale.y;
        float halfMoveLocal = 0.5f * deltaGapWorld * toLocalY;

        // 4) Move each pipe by half the difference (top up, bottom down)
        topPipe.localPosition += new Vector3(0f, +halfMoveLocal, 0f);
        bottomPipe.localPosition += new Vector3(0f, -halfMoveLocal, 0f);
    }
    
    public void ClampToCamera(Camera cam, float margin = 0.5f)
    {
        var topR = topPipe.GetComponentInChildren<Renderer>();
        var botR = bottomPipe.GetComponentInChildren<Renderer>();
        if (!topR || !botR) return;

        float camTop    = cam.transform.position.y + cam.orthographicSize;
        float camBottom = cam.transform.position.y - cam.orthographicSize;

        // current edges after Apply()
        float topBottomEdge = topR.bounds.min.y; // bottom edge of the TOP pipe
        float botTopEdge    = botR.bounds.max.y; // top edge of the BOTTOM pipe

        // If TOP pipe intrudes above camera top -> push pair down
        float overTop = (topBottomEdge) - (camTop - margin);
        if (overTop > 0f) transform.position += Vector3.down * overTop;

        // If BOTTOM pipe intrudes below camera bottom -> push pair up
        float underBottom = (camBottom + margin) - (botTopEdge);
        if (underBottom > 0f) transform.position += Vector3.up * underBottom;
    }
}


