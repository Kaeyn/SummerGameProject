using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BossBasic : Enemy
{
    [SerializeField] BossPhase bossPhase;
    float maxHealth;
    [SerializeField] float amplitude, initialY, xSkillOffset;
    [SerializeField] List<SkillSet> firstSkillHolder;
    [SerializeField] List<SkillSet> secondSkillHolder;
    Vector3 skillSpawnPos;
    private GameObject currentSkill;
    private bool wasDead = false;
    private bool isChanneling = false;
    GameObject bossHealthBar;
    Slider sliderHealthBar;
    TMP_Text healthText;
    int skillIndex = 0;
    enum BossPhase
    {
        FirstPhase,
        FinalPhase,
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        bossHealthBar = GameObject.FindGameObjectWithTag("boss");
        healthText = GameObject.FindGameObjectWithTag("bossHealthText").GetComponent<TMP_Text>();
        bossHealthBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-480,0);
        sliderHealthBar = bossHealthBar.GetComponentInChildren<Slider>();
        maxHealth = health;
        sliderHealthBar.maxValue = maxHealth;
        bossPhase = BossPhase.FirstPhase;
        logicGameHandler.isBossSpawn = true;
        AudioManager.PlaySFX(SoundType.BOSS_SCREAM,1f);
        castSkill();
    }

    // Update is called once per frame
    protected override void Update()
    {
        sliderHealthBar.value = health;
        healthText.text = health +"/"+maxHealth;
        if (health <= 0 && !wasDead)
        {
            MoveToNextPhase();
        }
        if(!wasDead){
            SkillTimer();
        }
        if (!isChanneling)
        {
            BossMovement();
        }
    }
    void SkillTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            castSkill();
            
        }
    }
    void castSkill()
    {
        skillSpawnPos = SkillPos();
        List<SkillSet> listToSpawn = new List<SkillSet>();
        if (bossPhase == BossPhase.FirstPhase)
        {
            listToSpawn = firstSkillHolder;
        }
        else if (bossPhase == BossPhase.FinalPhase)
        {
            listToSpawn = secondSkillHolder;
        }
        if (listToSpawn.Count - 1 == skillIndex)
        {
            skillIndex = 0;
        }
        else
        {
            skillIndex++;
        }
        if (currentSkill)
        {
            Destroy(currentSkill);
        }
        currentSkill = Instantiate(listToSpawn[skillIndex].skill, skillSpawnPos, Quaternion.identity);
        timer = listToSpawn[skillIndex].duration;
    }
    //     void firstSkill(GameObject skill)
    //     {
    //         for (int i = 0; i < 3; i++)
    //         {
    //             Vector3 spawnPosition = new Vector3(transform.position.x + xSkillOffset, transform.position.y + i * ySkillOffset, transform.position.z);
    //             Instantiate(skill, spawnPosition, Quaternion.identity);
    //         }
    //         timer = 5f;
    //     }
    //     void secondSkill(GameObject skill)
    //     {
    // Vector3 spawnPosition = new Vector3(transform.position.x + xSkillOffset, transform.position.y  * ySkillOffset, transform.position.z);
    //         Instantiate(skill, spawnPosition, Quaternion.identity);

    //         timer = 5f;
    //     }

    // void coneSkill(GameObject skill)
    // {
    //     Vector3 spawnPosition = new Vector3(coneSkillSpawnPos.position.x, coneSkillSpawnPos.position.y , coneSkillSpawnPos.position.z);
    //     Instantiate(skill, spawnPosition, Quaternion.identity);
    //     timer = 13f;
    // }
    void BossMovement()
    {
        float newY = initialY + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    Vector3 SkillPos()
    {
        float randomY = UnityEngine.Random.Range(-2, 2);
        return new Vector3(transform.position.x+xSkillOffset, randomY, transform.position.z);
    }
    public void SetRunning(bool channeling)
    {
        isChanneling = channeling;
    }
    void MoveToNextPhase()
    {
        AudioManager.PlaySFX(SoundType.BOSS_SCREAM,0.8f);
        switch (bossPhase)
        {
            case BossPhase.FirstPhase:
                bossPhase += 1;
                health = maxHealth;
                break;
            case BossPhase.FinalPhase:
                //End
                if (!wasDead)
                {
                    rigidbody2D.AddForce(new Vector2(-2, 6), ForceMode2D.Impulse);
                    wasDead = true;
                    SetRunning(true);
                    rigidbody2D.gravityScale = 1;
                    AudioManager.PlaySFX(SoundType.BOSS_SCREAM,0.6f);
                    bossHealthBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-1000,0);
                    Destroy(gameObject, 2f);
                    logicGameHandler.winGame();
                }
                break;
        }
    }
    public override void takeDamage(float damage)
    {
        base.takeDamage(damage);
        AudioManager.PlaySFX(SoundType.BOSS_HURT,0.6f);
    }
}
[Serializable]
public struct SkillSet
{
    public GameObject skill;
    public float duration;
}
