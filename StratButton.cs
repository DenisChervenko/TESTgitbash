using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StratButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private GameObject _poolManager;

    [SerializeField] private GameObject[] _scoreMenu;
    public void OnClick()
    {
        _score.enabled = true;
        _poolManager.SetActive(true);

        for(int i = 0; i <= (_scoreMenu.Length -1); i++)
        {
            _scoreMenu[i].SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
