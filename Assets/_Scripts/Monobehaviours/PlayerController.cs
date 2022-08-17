using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool _disableMovement = false;
    bool _onGround = true;

    //Components
    Rigidbody2D _rb; //Container for RigidBody2D

    //Movement variables
    float _moveHorizontal;
    float _moveVertical;
    [SerializeField] float _moveSpeed = 60f;
    [SerializeField] float _jumpPower = 650f;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (_moveHorizontal != 0f && !_disableMovement)
        {
            //_rb.velocity = new Vector2(_moveHorizontal * _moveSpeed, 0f);
            _rb.AddForce(new Vector2(_moveHorizontal * _moveSpeed, 0f));
        }

        if(_moveVertical > 0.1 && !_disableMovement && _onGround)
        {
            //_rb.velocity = new Vector2(0f, _moveVertical * _jumpPower);
            _rb.AddForce(new Vector2(0f, _moveVertical * _jumpPower));
        }
    }

    public void AttachPlayer(GameObject _obj)
    {
        _disableMovement = true;
        transform.parent = _obj.transform;
    }
    public void DetachPlayer()
    {
        _disableMovement = false;
        transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box")
        {
            _onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box")
        {
            _onGround = false;
        }
    }
}
