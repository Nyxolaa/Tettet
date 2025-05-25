using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Tetromino tetromino;

    private float swipeThreshold = 50f;

    void Start()
    {
        tetromino = FindObjectOfType<Tetromino>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && tetromino != null)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    Vector2 delta = endTouchPosition - startTouchPosition;

                    if (delta.magnitude > swipeThreshold)
                    {
                        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                        {
                            if (delta.x > 0)
                                tetromino.MoveRight();
                            else
                                tetromino.MoveLeft();
                        }
                        else
                        {
                            if (delta.y < 0)
                                tetromino.MoveDown();
                            else
                                tetromino.Rotate();
                        }
                    }
                    break;
            }
        }
    }
}
