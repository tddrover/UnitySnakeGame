using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource select;
    public void PlayGame()
    {
        select.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame()
    {
        select.Play();
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void ShopGame()
    {
        select.Play();
        Debug.Log("Shop");
    }
}
