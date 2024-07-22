using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class firstBossSkill : Projectile
{
    [SerializeField] float leanDuration = 2f;
    [SerializeField] float moveDuration = 3f;
    private Vector3 targetPosition;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(-12, 0, 0);
        }
        StartCoroutine(MoveToPlayer());

    }
    IEnumerator MoveToPlayer()
    {
        Vector3 startPosition = transform.position;
        // Lean towards the player
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg - 90f));

        while (elapsedTime < leanDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / leanDuration;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null; // Wait for the next frame
        }

        elapsedTime = 0f;
        Vector3 finalPosition = targetPosition + (targetPosition - startPosition).normalized * 10f; // Extend target position by 10 units

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, speed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        if (transform.position.x < -11f)
        {
            Destroy(gameObject);
        }
    }
}

