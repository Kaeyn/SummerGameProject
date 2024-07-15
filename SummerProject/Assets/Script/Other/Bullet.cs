using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] float speed;
    [SerializeField] float damage;
    
    LogicGameHandler logicGameHandler;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        logicGameHandler = GameObject.Find("GameLogicHandler").GetComponent<LogicGameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!logicGameHandler.isGameOver){
            Vector2 move = new Vector2(10,0).normalized;
            rigidbody2D.velocity = new Vector2(move.x * speed,move.y* speed ); 
        }else{
            rigidbody2D.velocity = new Vector2(0,0);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "enemy"){
            Rabbit rabbit = other.gameObject.GetComponent<Rabbit>();
            if(rabbit != null)
            {
                rabbit.takeDamage(damage);
            }
            CapyScript capy = other.gameObject.GetComponent<CapyScript>();
            if (capy != null)
            {
                capy.takeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
