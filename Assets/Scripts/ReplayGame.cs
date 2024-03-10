using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReplayGame : MonoBehaviour
{

    public int score = 0;
    public int highscore = 0;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI highscoreText;
    [SerializeField] private AudioSource select;

    void Start()
    {
        score = PlayerPrefs.GetInt("score", 0);
        highscore = PlayerPrefs.GetInt("highscore", 0);

        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
        }

        scoreText2.text = score.ToString();
        highscoreText.text = highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void LoadGame()
    {
        select.Play();
        SceneManager.LoadScene("Snake");
        PlayerPrefs.DeleteKey("score");
    }
    public void LoadMenu()
    {
        select.Play();
        PlayerPrefs.DeleteKey("score");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        select.Play();
        PlayerPrefs.DeleteKey("score");
        Debug.Log("Quitting game!");
        Application.Quit();
    }

}
