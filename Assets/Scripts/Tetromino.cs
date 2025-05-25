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
        if (Time.timeScale == 0f)
            return;

        HandleInput();

        if (Time.time - previousTime > fallTime)
        {
            MoveDown();
            previousTime = Time.time;
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            MoveRight();

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            MoveLeft();

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            MoveDown();

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            Rotate();
    }

    // Mobil & Klavye metodlar
    public void MoveLeft()
    {
        transform.position += Vector3.left;
        if (!IsValidMove()) transform.position += Vector3.right;
    }

    public void MoveRight()
    {
        transform.position += Vector3.right;
        if (!IsValidMove()) transform.position += Vector3.left;
    }
 
    public void MoveDown()
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

    public void Rotate()
    {
        transform.Rotate(0, 0, -90);
        if (!IsValidMove()) transform.Rotate(0, 0, 90);
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
