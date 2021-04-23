using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerAM : MonoBehaviour
{
    public static int score;
    Text text;
    
    private void Awake()
    {
        text = GetComponent<Text>();
        score = 0; //reset score at game start
    }

    private void Update()
    {
        text.text = "Score: " + score;
    }
}
