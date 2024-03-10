using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] private AudioSource select;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (GameIsPaused) // Check if game is paused
            {
                Resume();
            }
            else
            {
                Pause();
            }
            // Add this block to update the GameIsPaused state in every frame
            if (GameIsPaused && pauseMenuUI.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
    public void Resume()
    {
        select.Play();
        Debug.Log("Playing game!");
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

        // Set input focus back to the game window
        EventSystem.current.SetSelectedGameObject(null);

    }
    public void Pause()
    {
        select.Play();
        Debug.Log("PAUSE game!");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }
    public void LoadMenu()
    {
        select.Play();
       // pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void QuitGame()
    {
        select.Play();
        pauseMenuUI.SetActive(false);
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}
