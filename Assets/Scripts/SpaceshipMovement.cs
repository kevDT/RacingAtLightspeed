using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SpaceshipMovement : MonoBehaviour
{
    private const float MAX_SPEED = 15.0f;
    private const float ACCEL_RATE = 2.0f;

    private Vector3 _playerVelocity = Vector3.zero;
    private float _playabilityfactor = 50.0f;

    private bool _reset = false;
    private bool _resetRotation = false;

    bool _forward, _up, _down, _left, _right = false;

    private float _maxRoll = 6.0f;
    private float _roll = 0.0f;
    private float _rollSpeed = 3.0f;


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
        transform.Translate(_playerVelocity / _playabilityfactor);
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
            _playerVelocity.z += ACCEL_RATE * Time.smoothDeltaTime;
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
            _playerVelocity.y += ACCEL_RATE * Time.smoothDeltaTime;
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
            _playerVelocity.y -= ACCEL_RATE * Time.smoothDeltaTime;
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
        else if (!_resetRotation)
            StartCoroutine(ResetRotation());
    }

    private IEnumerator MoveLeft()
    {
        _resetRotation = false;

        while (_left)
        {
            _playerVelocity.x -= ACCEL_RATE * Time.smoothDeltaTime;
            _playerVelocity = _playerVelocity.magnitude > MAX_SPEED ? Vector3.Normalize(_playerVelocity) * MAX_SPEED : _playerVelocity;


            _roll += _rollSpeed * Time.smoothDeltaTime;

            if (_roll < _maxRoll)
                transform.Rotate(Vector3.forward, _rollSpeed * Time.smoothDeltaTime);
            else
                _roll = _maxRoll;


            yield return null;
        }
    }

    public void OnMoveRight()
    {
        _reset = _right;
        _right = !_right;

        if (_right)
            StartCoroutine(MoveRight());
        else if (!_resetRotation)
            StartCoroutine(ResetRotation());
    }

    private IEnumerator MoveRight()
    {
        _resetRotation = false;

        while (_right)
        {
            _playerVelocity.x += ACCEL_RATE * Time.smoothDeltaTime;
            _playerVelocity = _playerVelocity.magnitude > MAX_SPEED ? Vector3.Normalize(_playerVelocity) * MAX_SPEED : _playerVelocity;

            _roll -= _rollSpeed * Time.smoothDeltaTime;


            if (_roll > -_maxRoll)
                transform.Rotate(Vector3.forward, -_rollSpeed * Time.smoothDeltaTime);
            else
                _roll = -_maxRoll;

            yield return null;
        }
    }

    private IEnumerator ResetVelocity()
    {
        float vel = _playerVelocity.magnitude;
        Vector3 originalVector = _playerVelocity;

        float progress = ACCEL_RATE * Time.smoothDeltaTime;

        while (!_playerVelocity.Equals(Vector3.zero) && _reset)
        {
            progress += ACCEL_RATE * Time.smoothDeltaTime;
            progress = Mathf.Min(progress, 1);
            _playerVelocity = Vector3.Lerp(originalVector, Vector3.zero, progress);
            yield return null;
        }

        _reset = false;
    }

    private IEnumerator ResetRotation()
    {
        _resetRotation = true;
        bool reset = false;
        bool left = _roll > 0.0f;

        float resetSpeed = 4.0f;

        while (!reset && _resetRotation)
        {
            float timeStep = resetSpeed * Time.smoothDeltaTime;
            if (left)
            {
                _roll -= timeStep;
                reset = _roll <= 0.0f;
                transform.Rotate(Vector3.forward, -timeStep);
            }
            else
            {
                _roll += timeStep;
                reset = _roll >= 0.0f;
                transform.Rotate(Vector3.forward, timeStep);
            }

            yield return null;
        }

        _resetRotation = false;
    }

    //Quick implementation for demo's sake
    public void OnRestart()
    {
        SceneManager.LoadScene(0);
    }
}
