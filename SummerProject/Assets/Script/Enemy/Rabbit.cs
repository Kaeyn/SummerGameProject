using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Enemy
{
    [SerializeField] float timer;
    float counter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
