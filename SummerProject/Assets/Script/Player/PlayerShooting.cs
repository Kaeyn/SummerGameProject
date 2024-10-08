using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate = 2f;
    [SerializeField] float offsetX = 1;
    LogicGameHandler logicGameHandler;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        logicGameHandler = GameObject.Find("GameLogicHandler").GetComponent<LogicGameHandler>();

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !logicGameHandler.isPause)
        {
            Instantiate(bullet, new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z), Quaternion.Euler(Vector2.right));
            AudioManager.PlaySFX(SoundType.PLAYER_SHOOTING,0.4f);
            timer = 0;
        }
    }

    public void IncreaseFireRate(float newFireRate)
    {
        fireRate -= newFireRate;
        if(fireRate <= 0.05f) fireRate = 0.05f;
    }
}
