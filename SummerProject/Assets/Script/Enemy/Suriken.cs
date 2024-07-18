using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suriken : Enemy
{
    public float rotateSpeed = 1200f; // Tốc độ xoay của phi tiêu
    public float moveSpeed = 6f; // Tốc độ di chuyển lên xuống
    private Vector3 startPosition;
    private float screenBottomLimit; // Giới hạn cạnh dưới của màn hình
    private bool movingDown = true; // Trạng thái di chuyển của phi tiêu

    private void Spin()
    {
        gameObject.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;

        // Tính giới hạn cạnh dưới của màn hình
        screenBottomLimit = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).y;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Spin();
        MoveVertical();
        base.Update();
    }

    private void MoveVertical()
    {
        float newY;

        if (movingDown)
        {
            newY = transform.position.y - moveSpeed * Time.deltaTime;

            // Kiểm tra nếu phi tiêu chạm tới cạnh dưới của màn hình
            if (newY <= screenBottomLimit)
            {
                newY = screenBottomLimit;
                movingDown = false;
            }
        }
        else
        {
            newY = transform.position.y + moveSpeed * Time.deltaTime;

            // Kiểm tra nếu phi tiêu chạm tới vị trí bắt đầu
            if (newY >= startPosition.y)
            {
                newY = startPosition.y;
                movingDown = true;
            }
        }

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void FixedUpdate()
    {
        if (!logicGameHandler.isGameOver)
        {
            rigidbody2D.velocity = new Vector2(Vector2.left.normalized.x * speed, rigidbody2D.velocity.y);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, 0);
        }
    }
}
