using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerShield : MonoBehaviour
{
    
    LogicGameHandler logicGameHandler;
    bool shielded = true;
    [SerializeField] float shieldCountdown;
    float counter;
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text shieldCountdownText;
    void Start()
    {
        logicGameHandler = GameObject.Find("GameLogicHandler").GetComponent<LogicGameHandler>();
        counter = shieldCountdown;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("shielded", shielded);
        if(!shielded){
            shieldCountdownText.text = Math.Round(Convert.ToDouble(counter)).ToString()+"s";
            if(counter <= 0){
                shielded = true;
                AudioManager.PlaySFX(SoundType.SHIELDED,1f);
                counter = shieldCountdown;
            }else{
                counter -= Time.deltaTime;
            }
        }else{
            shieldCountdownText.text = "";
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("enemy") || other.transform.CompareTag("wall")){
            if(shielded){
                Destroy(other.gameObject);
                AudioManager.PlaySFX(SoundType.SHIELD_DEPLET,1f);
                shielded = false;
            }else{
                logicGameHandler.gameover();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Boss")){
            logicGameHandler.gameover();
        }
        if (collision.transform.CompareTag("enemy") || collision.gameObject.tag == "projectiles")
        {
            if(shielded){
                Destroy(collision.gameObject);
                AudioManager.PlaySFX(SoundType.SHIELD_DEPLET,1f);
                shielded = false;
            }else{
                logicGameHandler.gameover();
            }
        }
    }
}
