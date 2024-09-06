using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginStartScene : MonoBehaviour
{
    [SerializeField] TMP_Text mainText;
    [SerializeField] Button button;
    string gameName = "Space Invaders";

    // Start is called before the first frame update
    void Start()
    {
        mainText.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        mainText.text = gameName;
        button.onClick.AddListener(play);
    }

    private void Update()
    {

    }

    public void play()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void quit(){
        Application.Quit();
    }
}
