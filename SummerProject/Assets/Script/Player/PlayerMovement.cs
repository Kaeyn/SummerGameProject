using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    [SerializeField] float speed;
    public float baseSpeed;
    public float sprintSpeedMulp = 1.5f;
    private float sprintCooldownTime;
    public float baseSprintCooldownTime = 4f;
    public float sprintStaminaCost = 50f;
    public float sprintStaminaRegen = 5f;

    private bool isSprinting;
    private bool isSprintCooldown;

    private float sprintTimer;
    LogicGameHandler logicGameHandler;
    PlayerStat playerStat;
    // Start is called before the first frame update
    void Start()
    {
        sprintCooldownTime = baseSprintCooldownTime;

        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        logicGameHandler = GameObject.Find("GameLogicHandler").GetComponent<LogicGameHandler>();
        playerStat = GetComponent<PlayerStat>();
        baseSpeed = speed;
    }

    private void Update()
    {
        PlayerInputHandler();

        if (!logicGameHandler.isGameOver)
        {
            if (isSprinting)
            {
                playerStat.stamina -= sprintStaminaCost * Time.deltaTime;
                if (playerStat.stamina <= 0)
                {
                    StopSprint();
                }
            }
            else
            {
                if (playerStat.stamina < playerStat.baseStamina)
                {
                    playerStat.stamina += sprintStaminaRegen * Time.deltaTime;

                }
                else if (playerStat.stamina >= playerStat.baseStamina)
                {
                    playerStat.stamina = playerStat.baseStamina;
                    isSprintCooldown = false;
                }

                if (isSprintCooldown)
                {
                    HandleSprintCooldown();
                }

            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
     {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(horizontalInput,verticalInput).normalized;
        if(!logicGameHandler.isGameOver){
            rigidbody2D.velocity = new Vector2(move.x * speed,move.y* speed );
        }
        else{
            rigidbody2D.velocity = Vector2.zero;
        }

        
     }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("enemy") || other.transform.CompareTag("wall")){
            logicGameHandler.gameover();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("enemy") || collision.gameObject.tag == "projectiles")
        {
            logicGameHandler.gameover();
        }
    }

    void PlayerInputHandler()
    {
        if (!isSprintCooldown && Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.StartSprint();
        }
    }

    void StartSprint()
    {
        isSprinting = true;
        speed = baseSpeed * sprintSpeedMulp;
    }

    void StopSprint()
    {
        isSprinting = false;
        isSprintCooldown = true;
        sprintCooldownTime = baseSprintCooldownTime;
        speed = baseSpeed;
    }

    void HandleSprintCooldown()
    {
        if(sprintCooldownTime > 0)
        {
            sprintCooldownTime -= 0.5f * Time.fixedDeltaTime;
        }
    }
}
