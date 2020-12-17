using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    private Vector3 _playerVelocity = Vector3.zero;

    private float _velocityAddition = 0.5f;

    private const float MAX_SPEED = 10;

    private bool _moveForward = false;
    private bool _moveRight = false;
    private bool _moveLeft = false;
    private bool _moveUp = false;
    private bool _moveDown = false;

    public Vector3 PlayerVelocity
    {
        get { return _playerVelocity; }
    }

    public Vector3 PlayerPosition
    {
        get { return transform.position; }
    }

    public void FixedUpdate()
    {
        transform.Translate(_playerVelocity);
    }

    public void OnMoveForward()
    {
        //Hold button press input system not cooperating, use coroutines alternatively/temporarily
        _moveForward = !_moveForward;

        if (_moveForward)
            StartCoroutine(MoveForward());
    }

    private IEnumerator MoveForward()
    {
        while (_moveForward)
        {
            _playerVelocity.z += _velocityAddition * Time.deltaTime;
            _playerVelocity.z = Mathf.Min(_playerVelocity.z, MAX_SPEED);
            yield return null;
        }

        _playerVelocity.z = 0;
    }


    public void OnMoveUp()
    {
        //Hold button press input system not cooperating, use coroutines alternatively/temporarily
        _moveUp = !_moveUp;

        if (_moveUp)
            StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {
        while (_moveUp)
        {
            _playerVelocity.y += _velocityAddition * Time.deltaTime;
            _playerVelocity.y = Mathf.Min(_playerVelocity.y, MAX_SPEED);
            yield return null;
        }

        _playerVelocity.y = 0;
    }

    public void OnMoveDown()
    {
        //Hold button press input system not cooperating, use coroutines alternatively/temporarily
        _moveDown = !_moveDown;

        if (_moveDown)
            StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        while (_moveDown)
        {
            _playerVelocity.y -= _velocityAddition * Time.deltaTime;
            _playerVelocity.y = Mathf.Max(_playerVelocity.y, -MAX_SPEED);
            yield return null;
        }

        _playerVelocity.y = 0;
    }

    public void OnMoveLeft()
    {
        //Hold button press input system not cooperating, use coroutines alternatively/temporarily
        _moveLeft = !_moveLeft;

        if (_moveLeft)
            StartCoroutine(MoveLeft());
    }

    private IEnumerator MoveLeft()
    {
        while (_moveLeft)
        {
            _playerVelocity.x -= _velocityAddition * Time.deltaTime;
            _playerVelocity.x = Mathf.Max(_playerVelocity.x, -MAX_SPEED);
            yield return null;
        }

        _playerVelocity.x = 0;
    }

    public void OnMoveRight() 
    {
        //Hold button press input system not cooperating, use coroutines alternatively/temporarily
        _moveRight = !_moveRight;

        if (_moveRight)
            StartCoroutine(MoveRight());
    }

    private IEnumerator MoveRight()
    {
        while (_moveRight)
        {
            _playerVelocity.x += _velocityAddition * Time.deltaTime;
            _playerVelocity.x = Mathf.Min(_playerVelocity.x, MAX_SPEED);
            yield return null;
        }

        _playerVelocity.x = 0;
    }
}
