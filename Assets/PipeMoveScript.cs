using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float deadZone = -30.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move the pipe left
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        
        if(transform.position.x < deadZone)
        {
            Debug.Log($"PipeMoveScript: Destroying pipe {gameObject.name} at x={transform.position.x}");
            Destroy(gameObject);
        } 
    }
}
