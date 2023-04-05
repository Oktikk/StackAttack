using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _isGrounded;
    private bool _isFacingRight = true;

    Rigidbody2D _rb;

    private float _moveHorizontal;
    private float _moveVertical;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpPower;
    [SerializeField] float _fallMultiplier;

    private Animator _animator;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private Vector2 _gravityVector;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _gravityVector = new Vector2(0, -Physics2D.gravity.y);
    }

    void Update()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _isGrounded = Physics2D.OverlapCapsule(_groundCheck.position, new Vector2(0.43f, 0.1f), CapsuleDirection2D.Horizontal, 0, _groundLayer);

        if (_moveHorizontal != 0f)
        {
            _rb.velocity = new Vector2(_moveHorizontal * _moveSpeed, _rb.velocity.y);
        }
        
        _animator.SetFloat("xDir", _moveHorizontal);

        if (_moveVertical > 0.1 && _isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _moveVertical * _jumpPower);
        }

        if(_rb.velocity.y < 0)
        {
            _rb.velocity -= _gravityVector * _fallMultiplier * Time.deltaTime;
        }

        if(_moveHorizontal > 0f && !_isFacingRight)
        {
            Flip();
        }
        else if(_moveHorizontal < 0f && _isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;

        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
