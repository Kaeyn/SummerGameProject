    using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Fly : Enemy
{
    
    [SerializeField] bool isTargeted = false;
    GameObject player;
    Vector3 direction = Vector3.zero;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
    }   

    // Update is called once per frame
    void FixedUpdate() {
        if(!logicGameHandler.isGameOver){   
            if(!isTargeted){
                direction = (player.transform.position - transform.position).normalized;
                isTargeted = true;
            }
            rigidbody2D.velocity = new Vector2(direction.x * speed,direction.y * speed);
        }else{
            rigidbody2D.velocity = Vector2.zero;
        }
    }
    protected override void Update()
    {
        base.Update();
    }
}
