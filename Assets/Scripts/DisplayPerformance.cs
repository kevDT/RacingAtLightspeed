using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPerformance : MonoBehaviour
{
    private float _deltaTime = 0.0f;
    private float _timer = 1.0f;

    private bool _updateGUI = true;

    private float _fps = 0.0f;
    private float _msec = 0.0f;

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

        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);

        if (_updateGUI)
        {
            _msec = _deltaTime * 1000.0f;
            _fps = 1.0f / _deltaTime;

            _updateGUI = false;
        }

        string text = string.Format("{0:0.0} ms ({1:0.} fps)", _msec, _fps);

        GUI.Label(rect, text, style);
    }
}