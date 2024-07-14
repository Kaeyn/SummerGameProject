using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Fly : Enemy
{
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
      base.Start();  
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(!logicGameHandler.isGameOver){
            // float x = 0;
            // float y = 0;
            // float xRotation = Math.Abs(transform.rotation.x % 360);
            // float yRotation = Math.Abs(transform.rotation.y % 360);
            // // if(xRotation == 0){}
            // // float b = a % 4;
            // switch (xRotation)
            // {
            //     case 0:
            //         x = 1;
            //         break;
            //     case 90:
            //         x = -1;
            //         break;
            //     case 180:
            //         x = 0;
            //         break;
            //     case 270:
            //         x = 0;
            //         break;
            //     default:
            //         if(xRotation > 0 && xRotation < 90 ){

            //         }else if( xRotation < 180){

            //         }
            //         else if( xRotation < 270){

            //         } else{

            //         }
            //         break;
            // }
            // switch (yRotation)
            // {
            //     case 0:
            //         y = -1;
            //         break;
            //     case 90:
            //         y = -1;
            //         break;
            //     case 180:
            //         y = 0;
            //         break;
            //     case 270:
            //         y = 0;
            //         break;
            // }

// new Vector2(x,y)
            rigidbody2D.AddForce(Vector2.left * speed* Time.deltaTime, ForceMode2D.Impulse );




        }else{
            rigidbody2D.velocity = Vector2.zero;
        }
    }
    protected override void Update()
    {
        base.Update();
    }
}
