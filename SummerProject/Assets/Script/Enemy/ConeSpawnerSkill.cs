using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSpawnerSkill : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float spawnRate = 0.1f;  // Time between each projectile spawn
    public float coneAngle = 45f;   // Total angle of the cone
    public float duration = 15f;     // Duration to spawn projectiles
    public float projectileSpeed = 10f;
    public int projectilesPerWave = 4;
    public float scaleUpDuration = 2f;
    private float spawnTimer = 0f;
    private float shootTimer = 0f;
    private bool isShooting = false;


    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
        StartCoroutine(ScaleUp());
    }

    void Update()
    {
        if (isShooting)
        {
            shootTimer += Time.deltaTime;
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnRate)
            {
                SpawnConeProjectiles();
                spawnTimer = 0f;
            }

            if (shootTimer >= duration)
            {
                isShooting = false;
                Destroy(gameObject);
            }
        }
    }

    public void StartShooting()
    {
        isShooting = true;
        shootTimer = 0f;
        spawnTimer = 0f;
    }

    /*void SpawnConeProjectiles()
    {
        // Calculate a random angle within the cone
        Vector3 directionToPlayer = (Player.transform.position - transform.position).normalized;

        // Calculate a random angle within the cone
        float halfConeAngle = coneAngle / 2;
        float randomAngle = Random.Range(-halfConeAngle, halfConeAngle);

        // Rotate the direction by the random angle
        Vector3 projectileDirection = Quaternion.Euler(0, 0, randomAngle) * directionToPlayer;

        // Instantiate and set direction for the projectile
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().SetDirection(projectileDirection);

    }*/
    void SpawnConeProjectiles()
    {

        // Calculate the direction to the player
        Vector3 directionToPlayer = (Player.transform.position - transform.position).normalized;

        // Calculate the starting angle of the cone
        float halfConeAngle = coneAngle / 2;
        float startAngle = -halfConeAngle;

        // Calculate the angle step between projectiles
        float angleStep = coneAngle / (projectilesPerWave - 1);

        for (int i = 0; i < projectilesPerWave; i++)
        {
            List<string> add = new List<string>();
            // Calculate the current angle
            float currentAngle = startAngle + (angleStep * i);

            // Rotate the direction by the current angle
            Vector3 projectileDirection = Quaternion.Euler(0, 0, currentAngle) * directionToPlayer;

            // Instantiate and set direction for the projectile
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            proj.GetComponent<Projectile>().SetDirection(projectileDirection);
        }
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
        StartShooting();
        // Ensure it's set to the final scale
    }
}
