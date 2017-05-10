using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalSceneManager : MonoBehaviour {


    public Text player1Name;
    public Text player1Score;
    public Text player2Name;
    public Text player2Score;

    // Use this for initialization
    void Start ()
    {
        player1Name.text = GameData.Name1;
        player1Score.text = GameData.Score1.ToString();
        player2Name.text = GameData.Name2;
        player2Score.text = GameData.Score2.ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
