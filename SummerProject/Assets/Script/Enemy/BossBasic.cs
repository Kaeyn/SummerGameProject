using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBasic : Enemy
{
    BossPhase bossPhase;
    [SerializeField] float maxHealth;
    enum BossPhase{
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
        if(health <= 0){
            MoveToNextPhase();
        }
    }
    void MoveToNextPhase(){
        switch (bossPhase){
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
