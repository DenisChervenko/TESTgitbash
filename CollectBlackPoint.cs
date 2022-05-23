using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class CollectBlackPoint : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private ParticleSystem _blackPoint;
    [SerializeField] private ParticleSystem _redPoint;
    private int _scoreInt;
    [SerializeField] private TMP_Text _dieText;
    [SerializeField] private GameObject _homeButton;
    [SerializeField] private int _rewardForOnePoint;
    private SpriteRenderer _player;

    [SerializeField] private AudioSource _collectPointSound;

    private void Start()
    {
        _player = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("BlackPoint"))
        {
            _blackPoint.Play();
            _collectPointSound.Play();

            _scoreInt += _rewardForOnePoint;
            _score.text = Convert.ToString(_scoreInt);

            if(_scoreInt % 10 == 0)
            {
                Time.timeScale += 0.2f;
            }
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            _redPoint.Play();
            _collectPointSound.Play();

            _score.enabled = false;
            _dieText.enabled = true;
            _homeButton.SetActive(true);

            PlayerPrefs.SetInt("Score", _scoreInt);

            _player.enabled = false;
        }
    }
}
