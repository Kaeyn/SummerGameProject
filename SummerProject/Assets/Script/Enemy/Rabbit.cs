using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] float speed;
    Rigidbody2D rigidbody2D;
    float counter = 0;
    [SerializeField] float health = 5;
    LogicGameHandler logicGameHandler;
    public void takeDamage(float damage){
        health -= damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        logicGameHandler = GameObject.Find("GameLogicHandler").GetComponent<LogicGameHandler>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(!logicGameHandler.isGameOver){
        rigidbody2D.velocity = new Vector2(Vector2.left.normalized.x * speed,Vector2.left.normalized.y * speed) ;
        }else{
            rigidbody2D.velocity = new Vector2(0,0);
        }
    }
    void Update()
    {
        counter += Time.deltaTime;
        if(counter > timer){
            Destroy(gameObject);
        }
        if(health <= 0){
            logicGameHandler.gainPoint();
            Destroy(gameObject);
        }
    }
}
