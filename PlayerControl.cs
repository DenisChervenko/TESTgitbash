using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : SimpleInput
{
    [SerializeField] private float _maxDistanceX;
    [SerializeField] private float _minDistanceX;
    [SerializeField] private float _speedMove;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(SimpleInput.GetAxis("Horizontal") > 0)
        {
            _rb.velocity = new Vector2(_speedMove, 0); 
        }
        else if(SimpleInput.GetAxis("Horizontal") < 0)
        {
            _rb.velocity = new Vector2(-_speedMove, 0);
        }

        if(gameObject.transform.position.x >= _maxDistanceX)
        {
            _rb.velocity = new Vector2(-_speedMove, 0);
        }
        else if(gameObject.transform.position.x <= _minDistanceX)
        {
            _rb.velocity = new Vector2(_speedMove, 0);
        }
    }
}
