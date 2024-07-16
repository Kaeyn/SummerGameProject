using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LogicGameHandler : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> spawners;
    [SerializeField] TMP_Text mainText, score, btnText, countdown;
    [SerializeField] Button button;
    [SerializeField] Slider slider;
    float countdownTimer = 3;
    
    string gameName = "Rapit Dance";

    int point = 0;
    // Start is called before the first frame update
    bool isCountingDown = false;
    bool isStarted = false;
    public bool isGameOver = false;
    [SerializeField] float interval;
    float cdInterval;

    void Start(){
        player.SetActive(false);
        foreach (var spawner in spawners)
        {
            spawner.SetActive(false);
        }
        mainText.gameObject.SetActive(true);
        slider.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
        countdown.gameObject.SetActive(false);
        mainText.text = gameName;
        btnText.text = "Start";
        score.text = point.ToString();
        cdInterval = interval;
    }
    private void Update() {
        score.text = point.ToString();
        if(isCountingDown && countdownTimer >= 0){
            countdown.text = Math.Round(countdownTimer).ToString();
            countdownTimer -= Time.deltaTime;
        }
        if(countdownTimer < 0 && !isStarted && !isGameOver){
            startGame();
            isStarted = true;
        }
        if(isStarted){
            cdInterval -= Time.deltaTime;
            if(cdInterval <= 0){
                gainPoint(1);
                cdInterval = interval;
            }
        }
    }
    public void play(){
        isCountingDown = true;
        player.SetActive(false);
        foreach (var spawner in spawners)
        {
            spawner.SetActive(false);
        }
        mainText.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        countdown.gameObject.SetActive(true);
    }
    public void startGame(){
        isCountingDown = false;
        player.SetActive(true);
        foreach (var spawner in spawners)
        {
            spawner.SetActive(true);
        }
        mainText.gameObject.SetActive(false);
        score.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
        button.gameObject.SetActive(false);
        countdown.gameObject.SetActive(false);
    }
    public void gainPoint(int pointGained){
        point += pointGained;
    }
    public void gameover(){
        isGameOver = true;
        isStarted = false;
        player.SetActive(true);
        foreach (var spawner in spawners)
        {
            spawner.SetActive(true);
        }
        mainText.gameObject.SetActive(true);
        mainText.text = "Game Over";
        btnText.text = "Restart";
        score.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        countdown.gameObject.SetActive(false);
    }
    public void restart(){
        countdownTimer = 3;
        // player.SetActive(true);
        // spawner.SetActive(true);
        // canvas.SetActive(false);
    }
    // public void setActiveSpawner(){
    //     spawner.SetActive(true);
    // }
    // public void setActivePlayer(){
    //     canvas.SetActive();
    // }

}
