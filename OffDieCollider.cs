using UnityEngine;

public class OffDieCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("LineDeath"))
        {
            gameObject.SetActive(false);
        }
    }

    private void TESTmethod()
    {
        return;
    }
}
