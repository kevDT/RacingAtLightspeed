using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    private TextMeshProUGUI _text = null;
    private string _textContent = null;

    bool _alive = true;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _textContent = _text.text;

        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        bool on = true;
        float blinkInterval = 0.5f;

        while (_alive)
        {
            yield return new WaitForSeconds(blinkInterval);
            on = !on;
            _text.text = on ? _textContent : "";
        }
    }

    private void OnDestroy()
    {
        _alive = false;
    }
}
