    using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Fly : Enemy
{
    
    GameObject player;
    Vector3 direction = Vector3.zero;
    Animator animator;
    public bool isDead = false;
    ParticleSystem particle;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        direction = transform.position - player.transform.position;
        float angle = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;   
        transform.rotation = Quaternion.Euler(0,0,angle);
        direction = -direction.normalized;
        animator = GetComponent<Animator>();
        particle = GetComponent<ParticleSystem>();

    }   

    // Update is called once per frame
    void FixedUpdate() {
        if(!logicGameHandler.isGameOver && health > 0){   
            rigidbody2D.velocity = new Vector2(direction.x * speed,direction.y * speed);
        }
    }
    protected override void Update()
    {
        base.Update();
        if(health <= 0){
            particle.Stop();
            rigidbody2D.velocity = Vector2.zero;
            animator.SetTrigger("Explose");
        }
        if(isDead){Destroy(gameObject);}
    }
    void idleSFX(){
        AudioManager.PlaySFX(SoundType.ROCKET,0.03f);
    }
    void exploseSFX(){
        logicGameHandler.gainPoint(15);
        AudioManager.PlaySFX(SoundType.EXPLOSION,0.03f);
    }
}
