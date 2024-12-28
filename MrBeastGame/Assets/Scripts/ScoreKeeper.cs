using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
    private static float score;  // everyone has the same score
    private static Text scoreText; // everyone has the same text

    // Use this for initialization
    internal void Start () {
        scoreText = GetComponent<Text>();
        UpdateText();
    }

    public static void AddToScore(float points)
    {
        score += points;
        UpdateText();
    }

    private static void UpdateText()
    {
        scoreText.text = String.Format("$ {0}", score);
    }
}
