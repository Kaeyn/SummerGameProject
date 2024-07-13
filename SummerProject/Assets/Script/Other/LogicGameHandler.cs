using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicGameHandler : MonoBehaviour
{
    [SerializeField] GameObject player, spawner;
    [SerializeField] TMP_Text mainText, score, btnText, countdown;
    [SerializeField] Button button;
    float timer = 3;
    string gameName = "Rapit Dance";

    int point = 0;
    // Start is called before the first frame update
    bool isCountingDown = false;
    bool isStarted = false;
    public bool isGameOver = false;
    void Start()
    {
        player.SetActive(false);
        spawner.SetActive(false);
        mainText.gameObject.SetActive(true);
        score.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
        countdown.gameObject.SetActive(false);
        mainText.text = gameName;
        btnText.text = "Start";
        score.text = point.ToString();
    }
    private void Update() {
        score.text = point.ToString();
        if(isCountingDown && timer >= 0){
            countdown.text = Math.Round(timer).ToString();
            timer -= Time.deltaTime;
        }
        if(timer < 0 && !isStarted){
            startGame();
            isStarted = true;
        }
    }
    public void play(){
        isCountingDown = true;
        player.SetActive(false);
        spawner.SetActive(false);
        mainText.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        countdown.gameObject.SetActive(true);
    }
    public void startGame(){
        isCountingDown = false;
        player.SetActive(true);
        spawner.SetActive(true);
        mainText.gameObject.SetActive(false);
        score.gameObject.SetActive(true);
        button.gameObject.SetActive(false);
        countdown.gameObject.SetActive(false);
    }
    public void gainPoint(){
        point += 1;
    }
    public void gameover(){
        isGameOver = true;
        isStarted = false;
        player.SetActive(true);
        spawner.SetActive(true);
        mainText.gameObject.SetActive(true);
        mainText.text = "Game Over";
        btnText.text = "Restart";
        score.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        countdown.gameObject.SetActive(false);
    }
    public void restart(){
        timer = 3;
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
