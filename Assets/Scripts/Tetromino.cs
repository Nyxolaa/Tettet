using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public float fallTime = 1f;
    private float previousTime;

    void Start()
    { previousTime = Time.time; }

    void Update()
    {
        if (Time.time - previousTime > fallTime)
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
        }
    }
}
