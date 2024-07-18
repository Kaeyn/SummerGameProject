using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class BossBasic : Enemy
{
    [SerializeField] BossPhase bossPhase;
    float maxHealth;
    [SerializeField] float amplitude, initialY;
    [SerializeField] List<GameObject> firstSkillHolder;
    [SerializeField] List<GameObject> secondSkillHolder;
    [SerializeField] Vector3 skillSpawnPos;
    private GameObject currentSkill;
    private bool wasDead = false;
    private bool isChanneling = false;

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
        bossPhase = BossPhase.FirstPhase;
        logicGameHandler.isBossSpawn = true; 
        maxHealth = health;
        castSkill();
    }

    // Update is called once per frame
    protected override void Update()
    {
        // SkillTimer();
        if (health <= 0){
            MoveToNextPhase();
        } else{
            SkillTimer() ;
        }
        if (!isChanneling)
        {
            BossMovement();
        }
    }
    void SkillTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0){
            castSkill();
            timer = 10f;
        }
    }
    void castSkill(){
        // SetRunning(false);
        List<GameObject> listToSpawn = new List<GameObject>();
        if(bossPhase == BossPhase.FirstPhase){
            listToSpawn = firstSkillHolder;
        }else if(bossPhase == BossPhase.FinalPhase){
            listToSpawn = secondSkillHolder;
        }
        if(listToSpawn.Count - 1 == skillIndex){
            skillIndex = 0;
        }else{
            skillIndex++;
        }
        if(currentSkill){
            Destroy(currentSkill);
        }
        currentSkill = Instantiate(listToSpawn[skillIndex], skillSpawnPos, Quaternion.identity);
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
                if(!wasDead){
                    rigidbody2D.AddForce(new Vector2(-2,6),ForceMode2D.Impulse);
                    wasDead = true;
                    SetRunning(true);
                    rigidbody2D.gravityScale = 1;
                    Destroy(gameObject, 5f);
                }
                transform.Rotate(new Vector3(0,90,0));
                break;
        }
    }
}
