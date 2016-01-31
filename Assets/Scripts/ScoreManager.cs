using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CompleteProject;

public class ScoreManager : MonoBehaviour
{
    public static int score;
	public static int endScore = 250;
	GameObject[] enemies;

    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
		
        
		if (score >= endScore) {
			
			text.text = "You won!";

		} else {
			text.text = "Score: " + score;
		}
		
    }
}
