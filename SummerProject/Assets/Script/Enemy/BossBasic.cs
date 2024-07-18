using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BossBasic : Enemy
{
    BossPhase bossPhase;
    [SerializeField] float maxHealth, amplitude, initialY;
    [SerializeField] List<GameObject> SkillHolder;
    [SerializeField] float xSkillOffset;
    [SerializeField] float ySkillOffset;
    private bool isChanneling = false;

    enum BossPhase
    {
        FirstPhase,
        FinalPhase,
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        bossPhase = BossPhase.FirstPhase;
        health = maxHealth;
    }

    // Update is called once per frame
    protected override void Update()
    {
        SkillTimer();
        if (health <= 0)
        {
            MoveToNextPhase();
        }
        if (!isChanneling)
        {
            BossMovement();
        }
    }
    void SkillTimer()
    {
        timer -= Time.deltaTime;
        foreach (var skill in SkillHolder)
        {
            if (timer < 0)
            {
                firstSkill(skill);
                // secondSkill(skill);
            }
        }
    }
    void firstSkill(GameObject skill)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x + xSkillOffset, transform.position.y + i * ySkillOffset, transform.position.z);
            Instantiate(skill, spawnPosition, Quaternion.identity);
        }
        timer = 5f;
    }
    void secondSkill(GameObject skill)
    {

        Vector3 spawnPosition = new Vector3(transform.position.x + xSkillOffset, transform.position.y  * ySkillOffset, transform.position.z);
        Instantiate(skill, spawnPosition, Quaternion.identity);
        timer = 5f;
    }
    void BossMovement()
    {
        float newY = initialY + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    public void SetRunning(bool channeling)
    {
        isChanneling = channeling;
    }
    void MoveToNextPhase()
    {
        switch (bossPhase)
        {
            case BossPhase.FirstPhase:
                bossPhase += 1;
                health = maxHealth;
                break;
            case BossPhase.FinalPhase:
                //End
                break;
        }
    }
}
