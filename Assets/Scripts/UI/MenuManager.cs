using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject Menu = null;
    [SerializeField] GameObject settingsMenu = null;


    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            Menu.SetActive(true);
        }
    }

    #region Main Menu
    public void NewGame()
    {
        Debug.Log("Starting New Game");
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //gets the next scene in the builds order.
    }

    public void GameQuit()
    {
        Application.Quit();
    }
    #endregion

    #region Pause Menu

    public void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    #endregion

    #region Settings Menu

    public void Settings()
    {
        Menu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void GoBack()
    {
        settingsMenu.SetActive(false);
        Menu.SetActive(true);
    }

    #endregion

    private void Update()
    {
        Pause();
    }
}
