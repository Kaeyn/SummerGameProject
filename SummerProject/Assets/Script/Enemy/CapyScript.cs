using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapyScript : Enemy
{
    // Start is called before the first frame update
    GameObject player;
    Vector3 direction = Vector3.zero;

    [SerializeField] GameObject projectilePrefab;
    public float atackCoolDown = 2f;
    private float attackTimer;

    private Animator animator;
    public float scaleUpDuration = 2f;
    private bool canAttack = false;
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        attackTimer = atackCoolDown;
        StartCoroutine(ScaleUp());
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        attackTimer -= Time.deltaTime;
        if(attackTimer < 0 && canAttack)
        {
            Attack();
            attackTimer = atackCoolDown;
        }

        if(health <= 0)
        {
            logicGameHandler.gainPoint(10);
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        if(player != null )
        {
            animator.SetTrigger("Shooting");
        }
    }

    void SpawnProjectile()
    {
        Vector3 spawnPos = transform.position;

        Vector3 direction = (player.transform.position - spawnPos).normalized;
        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetDirection(direction);
    }
    IEnumerator ScaleUp()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        float timer = 0f;

        while (timer < scaleUpDuration)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, timer / scaleUpDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
            canAttack = true;
        // Ensure it's set to the final scale
    }
}
