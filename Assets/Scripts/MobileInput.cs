using UnityEngine;

public class MobileInput : MonoBehaviour
{
    private Tetromino tetromino;

    void Start()
    {
        tetromino = FindObjectOfType<Tetromino>();
    }

    public void OnLeftButton()
    {
        if (tetromino != null)
            tetromino.MoveLeft();
    }

    public void OnRightButton()
    {
        if (tetromino != null)
            tetromino.MoveRight();
    }

    public void OnDownButton()
    {
        if (tetromino != null)
            tetromino.MoveDown();
    }

    public void OnRotateButton()
    {
        if (tetromino != null)
            tetromino.Rotate();
    }
}
