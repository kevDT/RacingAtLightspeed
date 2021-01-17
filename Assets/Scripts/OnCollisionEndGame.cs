using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnCollisionEndGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _winMessage = null;
    [SerializeField] private TextMeshProUGUI _loseMessage = null;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_loseMessage.gameObject.activeSelf)
            _winMessage.gameObject.SetActive(true);
        else if (!_winMessage.gameObject.activeSelf)
            _loseMessage.gameObject.SetActive(true);
    }
}
