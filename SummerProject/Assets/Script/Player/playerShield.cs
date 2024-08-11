using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShield : MonoBehaviour
{
    
    LogicGameHandler logicGameHandler;
    bool shielded = true;
    [SerializeField] float shieldCountdown;
    float counter = 0f;
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    void Start()
    {
        logicGameHandler = GameObject.Find("GameLogicHandler").GetComponent<LogicGameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("shielded", shielded);
        if(!shielded){
            if(counter >= shieldCountdown){
                shielded = true;
                AudioManager.PlaySFX(SoundType.SHIELDED,0.5f);
                counter = 0f;
            }else{
                counter += Time.deltaTime;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("enemy") || other.transform.CompareTag("wall")){
            if(shielded){
                Destroy(other.gameObject);
                AudioManager.PlaySFX(SoundType.SHIELD_DEPLET,0.5f);
                shielded = false;
            }else{
                logicGameHandler.gameover();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("enemy") || collision.gameObject.tag == "projectiles")
        {
            if(shielded){
                Destroy(collision.gameObject);
                AudioManager.PlaySFX(SoundType.SHIELD_DEPLET,0.5f);
                shielded = false;
            }else{
                logicGameHandler.gameover();
            }
        }
    }
}
