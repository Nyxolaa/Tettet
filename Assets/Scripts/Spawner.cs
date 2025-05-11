using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] tetrominoes;
    public static int width = 10;
    public static int height = 20;
    public static Transform[,] grid = new Transform[width, height];

    void Start()
    {
        SpawnNewTetromino();
    }

    public static bool IsValidPosition(Transform tetromino)
    {
        foreach (Transform block in tetromino)
        {
            Vector2Int pos = Vector2Int.RoundToInt(block.position);

            // Yüksekliği 20 olan grid'de son geçerli y = 19
            if (pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= height)
                continue; // üst sınırda blok varsa geçici olarak izin ver

            if (grid[pos.x, pos.y] != null)
                return false;
        }
        return true;
    }


    public void SpawnNewTetromino()
    {
        int index = Random.Range(0, tetrominoes.Length);
        GameObject newTetromino = Instantiate(tetrominoes[index], transform.position, Quaternion.identity);

        // Game Over kontrolü
        if (!IsValidPosition(newTetromino.transform))
        {
            Debug.Log("GAME OVER!");
            Destroy(newTetromino);
            Time.timeScale = 0f;
            FindObjectOfType<GameManager>().ShowGameOver();
        }

    }

    public static Vector2 Round(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool InsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }

    public static void AddToGrid(Transform tetromino)
    {
        foreach (Transform child in tetromino)
        {
            Vector2 pos = Round(child.position);
            grid[(int)pos.x, (int)pos.y] = child;
        }
    }

    public static bool IsOccupied(Vector2 pos)
    {
        if ((int)pos.y >= height) return false;
        return grid[(int)pos.x, (int)pos.y] != null;
    }

    public static void CheckForLines()
    {
        int linesCleared = 0;

        for (int y = 0; y < height; y++)
        {
            if (IsLineFull(y))
            {
                DeleteLine(y);
                MoveAllRowsDown(y);
                y--;
                linesCleared++;
            }
        }

        if (linesCleared >= 0)
        {
            GameManager.AddScore(linesCleared);
        }
    }


    public static bool IsLineFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
                return false;
        }
        return true;
    }

    public static void DeleteLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            GameObject.Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void MoveAllRowsDown(int startY)
    {
        for (int y = startY + 1; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].position += Vector3.down;
                }
            }
        }
    }


}