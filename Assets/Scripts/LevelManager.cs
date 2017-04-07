using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }
    
    public void OpenSubmenu(string name)
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
