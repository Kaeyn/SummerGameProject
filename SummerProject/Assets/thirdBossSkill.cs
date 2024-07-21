using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class thirdBossSkill : MonoBehaviour
{
    [SerializeField] GameObject laserFocus, laserBeam;
    Vector3 laserBeamPos, focusPointPos;
    GameObject focusPoint, laser;
    bool isDone = false;
    void Start()
    {
        // Instantiate focusPoint at the current position with no rotation
        focusPoint = Instantiate(laserFocus, transform.position, Quaternion.identity);
        // Start the scaling coroutine
        StartCoroutine(ScaleUp());
    }
    IEnumerator ScaleUp()
    {
        Vector3 originalScale = laserFocus.transform.localScale;
        focusPoint.transform.localScale = Vector3.zero; // Start with zero scale
        float timer = 0f;
        // Scale up the focusPoint over 3 seconds
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            float scaleProgress = timer / 2f;
            focusPoint.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, scaleProgress);
            yield return null; // Wait for the next frame
        }
        // Ensure the final scale is set correctly
        focusPoint.transform.localScale = originalScale;
        focusPointPos = focusPoint.transform.position;
        laserBeamPos = new Vector3(focusPointPos.x - 8f, focusPointPos.y, focusPointPos.z);
        laser = Instantiate(laserBeam, laserBeamPos, Quaternion.identity);
        Done(isDone = true);
    }
    void Done(bool isDone)
    {
        if (isDone)
        {
            Destroy(gameObject);
            Destroy(focusPoint, 3f);
            Destroy(laser, 3f);

        }
    }
}
