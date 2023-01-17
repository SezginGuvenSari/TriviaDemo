using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    #region References

    private bool _stopTimer = false;

    private float _currentTime = 20f;

    private bool _timeOut = false;

    #endregion

    #region Serialize

    [SerializeField] private float _timeLeft;

    [SerializeField] private Image _progressImage;

    [SerializeField] [CanBeNull] private TMP_Text _timerText;

    #endregion

    #region OnEnable & OnDisable

    private void OnEnable()
    {
        EventManager.OnStopTimer += StopTimer;
        EventManager.OnResetRemainingTime += ResetRemainingTime;
    }

    private void OnDisable()
    {
        EventManager.OnStopTimer -= StopTimer;
        EventManager.OnResetRemainingTime -= ResetRemainingTime;
    }

    #endregion

    void Update() => StartTimer();

    private void StartTimer()
    {
        if (!_stopTimer)
        {
            _timeLeft -= Time.deltaTime;
            _progressImage.fillAmount = _timeLeft / _currentTime;

        }
    }
    private void OnGUI()
    {
        if (_timeOut != false) return;

        if (_timeLeft > 0)
        {
            _timerText.text = _timeLeft.ToString("0");
        }
        else
        {
            _stopTimer = true;
            _timeOut = true;
        }

    }
    private void StopTimer() => _stopTimer = true;

    private void ResetRemainingTime()
    {
        _timeLeft = _currentTime;
        _stopTimer = false;
    }

}
