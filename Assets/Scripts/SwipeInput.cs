using UnityEngine;

public class MobileInput : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Tetromino tetromino;

    void Start()
    {
        tetromino = GetComponent<Tetromino>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    HandleSwipe();
                    break;
            }
        }
    }

    void HandleSwipe()
    {
        Vector2 delta = endTouchPosition - startTouchPosition;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 30)
                tetromino.MoveRight(); // sağa kaydır
            else if (delta.x < -30)
                tetromino.MoveLeft();  // sola kaydır
        }
        else
        {
            if (delta.y > 30)
                tetromino.Rotate();    // yukarı kaydır = döndür
            else if (delta.y < -30)
                tetromino.MoveDown();       // aşağı kaydır = düşür
        }
    }
}
