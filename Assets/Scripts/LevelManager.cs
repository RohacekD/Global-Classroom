using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelManager : MonoBehaviour {

    Vector3 currentPosition;
    public GameObject mainMenu;
    public GameObject highscore;
    public GameObject playmenu;
    public GameObject settingsMenu;

    private void hideSubmenus()
    {
        GameObject[] submenus = GameObject.FindGameObjectsWithTag("Submenu");
        foreach (GameObject submenu in submenus)
        {
            submenu.SetActive(false);
        }
    }

    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        hideSubmenus();
    }

    public void openSettings()
    {
        hideSubmenus();
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void openPlayMenu()
    {
        hideSubmenus();
        mainMenu.SetActive(false);
        playmenu.SetActive(true);
    }

    public void openHighscore()
    {
        hideSubmenus();
        mainMenu.SetActive(false);
        highscore.SetActive(true);
    }

    public void playSinglePlayer()
    {
        EditorUtility.DisplayDialog("Missing scene on GIT", "Once we have scene to load place here", "OK");
        //Application.LoadLevel("Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
