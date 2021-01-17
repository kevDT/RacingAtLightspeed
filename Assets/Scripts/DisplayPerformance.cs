using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPerformance : MonoBehaviour
{
    private float _deltaTime = 0.0f;
    private float _timer = 1.0f;
    private float _fps = 0.0f;
    private float _msec = 0.0f;

    private bool _updateGUI = true;

    private string _text = null;

    private GUIStyle _guiStyle = null;
    private Rect _rect = Rect.zero;

    private void Awake()
    {
        int w = Screen.width, h = Screen.height;

        _guiStyle = new GUIStyle();
        _guiStyle.alignment = TextAnchor.UpperLeft;
        _guiStyle.fontSize = h * 2 / 100;
        _guiStyle.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);

        _rect = new Rect(0, 0, w, h * 2 / 100);
    }


    void Update()
    {
        _timer += Time.deltaTime;
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;

        if (_timer > 1.0f) //show updated FPS every second
        {
            _updateGUI = true;
            _timer = 0.0f;
        }
    }

    void OnGUI()
    {

        if (_updateGUI)
        {
            _msec = _deltaTime * 1000.0f;
            _fps = 1.0f / _deltaTime;

            _updateGUI = false;

            _text = string.Format("{0:0.0} ms ({1:0.} fps)", _msec, _fps);
        }

        GUI.Label(_rect, _text, _guiStyle);
    }
}