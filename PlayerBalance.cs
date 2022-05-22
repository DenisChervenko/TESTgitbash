using UnityEngine;

public class PlayerBalance : MonoBehaviour
{
    private int _balancePlayer;

    private int _coinTemp;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Coin"))
        {
            return;
        }
    }
}
