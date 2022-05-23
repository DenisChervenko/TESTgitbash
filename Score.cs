using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TMP_Text _scoreText;
    private int _score;

    private void Start()
    {
        _scoreText = gameObject.GetComponent<TMP_Text>();

        _score = PlayerPrefs.GetInt("Score");

        _scoreText.text = Convert.ToString(_score);
    }
}
