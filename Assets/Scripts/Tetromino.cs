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
                Spawner.CheckForLines(); // ✅ Yeni satır silme fonksiyonu
                FindObjectOfType<Spawner>().SpawnNewTetromino();
                enabled = false;
            }
            previousTime = Time.time;
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
            if (!IsValidMove()) transform.position += Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
            if (!IsValidMove()) transform.position += Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += Vector3.down;
            if (!IsValidMove())
            {
                transform.position += Vector3.up;
                Spawner.AddToGrid(transform);
                FindObjectOfType<Spawner>().SpawnNewTetromino();
                enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
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