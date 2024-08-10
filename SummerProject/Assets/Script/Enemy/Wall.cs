// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Wall : MonoBehaviour
// {
//     public float moveSpeed = 3;
//     public float verticalSpeed = 2; // Tốc độ di chuyển lên xuống
//     public float verticalRange = 2; // Phạm vi di chuyển lên xuống

//     private Vector3 startPos;

//     // Start is called before the first frame update
//     void Start()
//     {
//         startPos = transform.position; // Lưu vị trí bắt đầu
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Di chuyển sang trái
//         transform.position += Vector3.left * moveSpeed * Time.deltaTime;

//         // Di chuyển lên xuống
//         float newY = startPos.y + Mathf.PingPong(Time.time * verticalSpeed, verticalRange) - (verticalRange / 2);
//         transform.position = new Vector3(transform.position.x, newY, transform.position.z);

//         // Debug giá trị newY
//         Debug.Log("New Y Position: " + newY);
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float moveSpeed = 1;
    public float verticalSpeed = 2;
    public float margin = 0.2f; // Khoảng cách từ các cạnh màn hình để đổi hướng

    private bool movingUp = true; // Để theo dõi hướng di chuyển
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Lấy Camera chính
    }

    void Update()
    {
        // Di chuyển sang trái
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Lấy vị trí của tâm đối tượng trong không gian màn hình
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Kiểm tra xem đối tượng có gần chạm vào các cạnh trên hoặc dưới của màn hình không
        if (screenPosition.y >= 1 - margin && movingUp)
        {
            movingUp = false; // Chuyển hướng xuống dưới
        }
        else if (screenPosition.y <= margin && !movingUp)
        {
            movingUp = true; // Chuyển hướng lên trên
        }

        // Di chuyển lên hoặc xuống dựa trên hướng hiện tại
        if (movingUp)
        {
            transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.down * verticalSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm có tag không phải là "Player"
        if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "bullet")
        {
            // Bỏ qua va chạm bằng cách vô hiệu hóa va chạm
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}





