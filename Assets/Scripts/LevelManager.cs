using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    Vector3 currentPosition;
    public GameObject mainMenu;
    public GameObject highscore;
    public GameObject playmenu;
    public GameObject settingsMenu;

    public InputField SingleName;
    public InputField MultiName1;
    public InputField MultiName2;


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
        //EditorUtility.DisplayDialog("Missing scene on GIT", "Once we have scene to load place here", "OK");
        if(SingleName.text != "")
        {
            GameData.Name1 = SingleName.text;
            Application.LoadLevel("Singleplayer");
        }
    }

    public void playMultiPlayer()
    {
        //EditorUtility.DisplayDialog("Missing scene on GIT", "Once we have scene to load place here", "OK");]
        if(MultiName1.text != "" && MultiName2.text != "")
        {
            GameData.Name1 = MultiName1.text;
            GameData.Name2 = MultiName2.text;
            SceneManager.LoadScene("Multiplayer");
        }
    }

    public void backToMainMenuScene()
    {
        SceneManager.LoadScene("Menus");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
