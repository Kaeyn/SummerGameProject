using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Coin : Enemy
{
    [SerializeField] int coin;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();  
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(!logicGameHandler.isGameOver){
            rigidbody2D.velocity = new Vector2(Vector2.left.normalized.x * speed,Vector2.left.normalized.y * speed) ;
        }else{
            rigidbody2D.velocity = new Vector2(0,0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            AudioManager.PlaySFX(SoundType.COIN);
            logicGameHandler.gainPoint(coin);
            Destroy(gameObject);
        }
    }
}
