using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIScript : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerStat playerStat;
    public Slider slider;

    void Start()
    {
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
        slider.maxValue = playerStat.baseStamina;
        slider.value = playerStat.baseStamina;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerStat.stamina;
    }
}
