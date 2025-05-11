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

    public void SpawnNewTetromino()
    {
        int index = Random.Range(0, tetrominoes.Length);
        Instantiate(tetrominoes[index], transform.position, Quaternion.identity);
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
}