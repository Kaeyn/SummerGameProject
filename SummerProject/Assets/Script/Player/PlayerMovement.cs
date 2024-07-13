using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] float speed;
    LogicGameHandler logicGameHandler;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        logicGameHandler = GameObject.Find("GameLogicHandler").GetComponent<LogicGameHandler>();
    }

    // Update is called once per frame
     void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(horizontalInput,verticalInput).normalized;
        if(!logicGameHandler.isGameOver){
            rigidbody2D.velocity = new Vector2(move.x * speed,move.y* speed );
        }else{
            rigidbody2D.velocity = new Vector2(0,0);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("enemy")){
            logicGameHandler.gameover();
        }
    }
}
