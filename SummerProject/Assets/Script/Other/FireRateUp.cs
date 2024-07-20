using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateUp : PowerUp
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
    void FixedUpdate()
    {
        direction = Vector3.down;
        isTargeted = true;
        rigidbody2D.velocity = new Vector2(0, direction.y * speed);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.tag == "Player")
        {
            // Increase player's fire rate
            PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();
            playerShooting.IncreaseFireRate(0.02f); // Call the method to increase fire rate
            Destroy(gameObject);
        }
    }
}
