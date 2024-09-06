using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicGameHandler : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> spawners;
    [SerializeField] TMP_Text mainText, score, countdown;
    [SerializeField] Button  restartButton, returnButton, quitButton;
    [SerializeField] Slider staminaSlider, musicSlider, sfxSlider;
    [SerializeField] GameObject shieldIcon, overlay;
    float countdownTimer = 3;
    int point = 0;
    bool isCountingDown = false;
    bool isStarted = false;
    public bool isGameOver = false;
    public bool isGameVictory = false;
    public bool isBossSpawn = false;
    public bool isPause = false;
    [SerializeField] float interval;
    float cdInterval;

    void Start()
    {
        player.SetActive(false);
        foreach (var spawner in spawners)
        {
            spawner.SetActive(false);
        }
        mainText.gameObject.SetActive(false);
        staminaSlider.gameObject.SetActive(false);
        musicSlider.gameObject.SetActive(false);
        sfxSlider.gameObject.SetActive(false);
        shieldIcon.SetActive(false);
        score.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        countdown.gameObject.SetActive(false);
        overlay.SetActive(false);
        score.text = point.ToString();
        cdInterval = interval;
        play();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isGameOver && !isGameVictory){
            pause();
            return;
        }
        score.text = point.ToString();
        if (isCountingDown && countdownTimer >= 0)
        {
            countdown.text = Math.Round(countdownTimer).ToString();
            countdownTimer -= Time.deltaTime;
        }
        if (countdownTimer < 0 && !isStarted && !isGameOver && !isGameVictory)
        {
            startGame();
            isStarted = true;
        }
        if (isStarted)
        {
            cdInterval -= Time.deltaTime;
            if (cdInterval <= 0)
            {
                gainPoint(1);
                cdInterval = interval;
            }
        }
        
    }
    void pause(){
        if(isPause){
            foreach (var spawner in spawners)
            {
                if (spawner)
                {
                    spawner.SetActive(true);
                } else {

                }
            }
            overlay.SetActive(false);
            musicSlider.gameObject.SetActive(false);
            sfxSlider.gameObject.SetActive(false);
            score.gameObject.SetActive(false);
            mainText.gameObject.SetActive(false);
            mainText.text = "Pause";
            restartButton.gameObject.SetActive(false);
            returnButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            Time.timeScale = 1;
        }else{
            foreach (var spawner in spawners)
            {
                if (spawner)
                {
                    spawner.SetActive(false);
                } else {

                }
            }
            overlay.SetActive(true);
            score.gameObject.SetActive(true);
            mainText.gameObject.SetActive(true);
            
            musicSlider.gameObject.SetActive(true);
            sfxSlider.gameObject.SetActive(true);
            mainText.text = "Pause";
            restartButton.gameObject.SetActive(true);
            returnButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        isPause = !isPause;
    }
    public void play()
    {
        Debug.Log("play");
        isCountingDown = true;
        player.SetActive(false);
        foreach (var spawner in spawners)
        {
            spawner.SetActive(false);
        }
        score.gameObject.SetActive(false);
        countdown.gameObject.SetActive(true);
    }
    public void startGame()
    {
        AudioManager.muteMusic(false);
        isCountingDown = false;
        player.SetActive(true);
        foreach (var spawner in spawners)
        {
            if (spawner)
            {
                spawner.SetActive(true);
            } else {
                
            }
        }
        score.gameObject.SetActive(true);
        staminaSlider.gameObject.SetActive(true);
        shieldIcon.SetActive(true);
        countdown.gameObject.SetActive(false);
    }
    public void gainPoint(int pointGained)
    {
        point += pointGained;
    }
    public void gameover()
    {
        AudioManager.muteMusic(true);
        AudioManager.PlaySFX(SoundType.GAMEOVER,1f);
        isGameOver = true;
        isStarted = false;
        player.SetActive(true);
        foreach (var spawner in spawners)
        {
            if (spawner)
            {
                spawner.SetActive(false);
            } else {

            }
        }
        overlay.SetActive(true);
        score.gameObject.SetActive(true);
        mainText.gameObject.SetActive(true);
        
        mainText.text = "Game Over";
        restartButton.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        countdown.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
    public void restartGame()
    {   
        overlay.SetActive(false);
        score.gameObject.SetActive(false);
        countdown.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        isGameOver = false;
        isGameVictory = false;
        isStarted = false;
        Time.timeScale = 1;
        countdownTimer = 3;
        countdown.text = "3";
        point = 0;
        SceneManager.LoadScene("MainScene");
    }

    public void returnToStart()
    {   
        SceneManager.LoadScene("StartScene");
        isGameOver = false;
        isGameVictory = false;
        isStarted = false;
        Time.timeScale = 1;
        countdownTimer = 3;
        countdown.text = "3";
    }
    public void quit()
    {   
        Application.Quit();
    }

    public void winGame()
    {
        AudioManager.muteMusic(true);
        AudioManager.PlaySFX(SoundType.VICTORY,1f);
        isGameVictory = true;
        isStarted = false;
        player.SetActive(true);
        foreach (var spawner in spawners)
        {
            if (spawner)
            {
                spawner.SetActive(false);
            } else {

            }
        }
        overlay.SetActive(true);
        score.gameObject.SetActive(true);
        mainText.gameObject.SetActive(true);
        
        mainText.text = "Congratulations!";
        mainText.fontSize = 55;
        mainText.transform.position = new Vector3(mainText.transform.position.x, 292, mainText.transform.position.z);
        restartButton.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        countdown.gameObject.SetActive(false);
        Time.timeScale = 0;
    }


}
