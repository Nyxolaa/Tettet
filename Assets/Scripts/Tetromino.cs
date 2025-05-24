using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public float fallTime = 1f;
    private float previousTime;

    void Start()
    {
        previousTime = Time.time;
    }

    void Update()
    {
        HandleInput();

        if (Time.time - previousTime > fallTime)
        {
            transform.position += Vector3.down;
            if (!IsValidMove())
            {
                transform.position += Vector3.up;
                Spawner.AddToGrid(transform);
                Spawner.CheckForLines(); // Satır silme fonksiyonu
                FindObjectOfType<Spawner>().SpawnNewTetromino();
                enabled = false;
            }
            previousTime = Time.time;
        }
    }

    void HandleInput()
    {
        // → or D
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += Vector3.right;
            if (!IsValidMove()) transform.position += Vector3.left;
        }

        // ← or A
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            transform.position += Vector3.left;
            if (!IsValidMove()) transform.position += Vector3.right;
        }

        // ↓ or S
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            transform.position += Vector3.down;
            if (!IsValidMove())
            {
                transform.position += Vector3.up;
                Spawner.AddToGrid(transform);

                Spawner.CheckForLines();
                FindObjectOfType<Spawner>().SpawnNewTetromino();
                enabled = false;
            }
        }

        // ↑ or W
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            transform.Rotate(0, 0, -90);
            if (!IsValidMove()) transform.Rotate(0, 0, 90);
        }
    }

    bool IsValidMove()
    {
        foreach (Transform child in transform)
        {
            Vector2 pos = Spawner.Round(child.position);
            if (!Spawner.InsideGrid(pos)) return false;
            if (Spawner.IsOccupied(pos)) return false;
        }
        return true;
    }
}
