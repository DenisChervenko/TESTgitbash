using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInteractableObjects : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _sideMoveSpeed;

    [Header("Direction")]
    private bool _toLeft = false;
    private bool _toRight = false;

    [Header("InitializeObject")]
    private bool _isEnemy = false;

    private Rigidbody2D _rb;

    private void Start()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(!_isEnemy)
        {
            if(_toLeft)
            {
                _rb.velocity = new Vector2(-_sideMoveSpeed, -_moveSpeed);
            }
            else if(_toRight)
            {
                _rb.velocity = new Vector2(_sideMoveSpeed, -_moveSpeed);
            }
        }
        else
        {
            _rb.velocity = new Vector2(0, (-_moveSpeed - 2));
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Left"))
        {
            _toLeft = true;
            _toRight = false;
        }
        else if(other.gameObject.CompareTag("Right"))
        {
            _toRight = true;
            _toLeft = false;
        }

        if(other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
