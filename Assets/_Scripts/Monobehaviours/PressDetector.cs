using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class PressDetector : MonoBehaviour
{
    float _timePressed = 0f;
    float _maxTime = 0.3f;
    float _horizontalInput;

    bool _maxTimeReached = false;

    [SerializeField] TextMeshProUGUI _keyTimer;
    [SerializeField] TextMeshProUGUI _timeElapsed;

    private void Update()
    {
        _timeElapsed.text = "Time elapsed: " + Time.time.ToString("0.00") + "s.";
    }

    public bool IsKeyPressed()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        if (_horizontalInput != 0f && !_maxTimeReached)
        {
            _timePressed += Time.deltaTime;
            _keyTimer.text = "Button pressed: " + _timePressed.ToString("0.00") + "s.";
        }
        else if (_horizontalInput == 0f)
        {
            _timePressed = 0f;
            _maxTimeReached = false;
            _keyTimer.text = "Button pressed: " + _timePressed.ToString("0.00") + "s.";
        }
        if(_timePressed > _maxTime)
        {
            _maxTimeReached = true;
            _keyTimer.text = "Button pressed: MAX";
            return true;
        }
        return false;
    }

    public void Reset()
    {
        _timePressed = 0f;
        _maxTimeReached = false;
    }
}
