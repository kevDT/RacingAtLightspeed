using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipMovement : MonoBehaviour
{
    private const float MAX_SPEED = 8.0f;

    private Vector3 _playerVelocity = Vector3.zero;
    private float _velocityAddition = 2.0f;
    private float _playabilityFactor = 100; //Relativistic effects will be showed 'quicker' because they think the ship moves at a higher speed

    private bool _reset = false;

    bool _forward, _up, _down, _left, _right = false;


    public Vector3 PlayerVelocity
    {
        get { return _playerVelocity; }
    }

    public Vector3 PlayerPosition
    {
        get { return transform.position; }
    }


    private void Update()
    {
        transform.Translate(_playerVelocity / _playabilityFactor);
        GameState.Instance.UpdatePlayerStatus(transform.position, _playerVelocity);

        if (!Input.anyKey && _reset)
            StartCoroutine(ResetVelocity());
    }


    public void OnMoveForward()
    {
        _reset = _forward;
        _forward = !_forward;

        if (_forward)
            StartCoroutine(MoveForward());

    }

    private IEnumerator MoveForward()
    {
        while (_forward)
        {
            _playerVelocity.z += _velocityAddition * Time.deltaTime;
            _playerVelocity = _playerVelocity.magnitude > MAX_SPEED ? Vector3.Normalize(_playerVelocity) * MAX_SPEED : _playerVelocity;
            yield return null;
        }
    }

    public void OnMoveUp()
    {
        _reset = _up;
        _up = !_up;

        if (_up)
            StartCoroutine(MoveUp());

    }

    private IEnumerator MoveUp()
    {
        while (_up)
        {
            _playerVelocity.y += _velocityAddition * Time.deltaTime;
            _playerVelocity = _playerVelocity.magnitude > MAX_SPEED ? Vector3.Normalize(_playerVelocity) * MAX_SPEED : _playerVelocity;
            yield return null;
        }
    }

    public void OnMoveDown()
    {
        _reset = _down;
        _down = !_down;

        if (_down)
            StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        while (_down)
        {
            _playerVelocity.y -= _velocityAddition * Time.deltaTime;
            _playerVelocity = _playerVelocity.magnitude > MAX_SPEED ? Vector3.Normalize(_playerVelocity) * MAX_SPEED : _playerVelocity;
            yield return null;
        }
    }

    public void OnMoveLeft()
    {
        _reset = _left;
        _left = !_left;

        if (_left)
            StartCoroutine(MoveLeft());
    }

    private IEnumerator MoveLeft()
    {
        while (_left)
        {
            _playerVelocity.x -= _velocityAddition * Time.deltaTime;
            _playerVelocity = _playerVelocity.magnitude > MAX_SPEED ? Vector3.Normalize(_playerVelocity) * MAX_SPEED : _playerVelocity;
            yield return null;
        }
    }

    public void OnMoveRight()
    {
        _reset = _right;
        _right = !_right;

        if (_right)
            StartCoroutine(MoveRight());
    }

    private IEnumerator MoveRight()
    {
        while (_right)
        {
            _playerVelocity.x += _velocityAddition * Time.deltaTime;
            _playerVelocity = _playerVelocity.magnitude > MAX_SPEED ? Vector3.Normalize(_playerVelocity) * MAX_SPEED : _playerVelocity;
            yield return null;
        }
    }

    private IEnumerator ResetVelocity()
    {
        float vel = _playerVelocity.magnitude;
        Vector3 originalVector = _playerVelocity;

        float progress = _velocityAddition * Time.deltaTime;

        while (!_playerVelocity.Equals(Vector3.zero) && _reset)
        {
            progress += _velocityAddition * Time.deltaTime;
            progress = Mathf.Min(progress, 1);
            _playerVelocity = Vector3.Lerp(originalVector, Vector3.zero, progress);
            yield return null;
        }

        _reset = false;
    }
}
