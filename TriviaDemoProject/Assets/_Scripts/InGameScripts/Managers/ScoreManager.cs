using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{

    #region Serialize

    [Tooltip("If u want u can check playerData scriptableObject.")]
    [SerializeField] private PlayerData _playerData;

    [SerializeField] private TMP_Text _scoreText;

    [Tooltip("It shows the value that will increase when you answer correctly.")]
    [SerializeField] private int _correctValue;

    [Tooltip("It shows the value that will decrease when you answer incorrectly.")]
    [SerializeField] private int _wrongValue;

    [Tooltip("It shows how many scores you will lose when you have no  time left.")]
    [SerializeField] [Range(1, 20)] private int _timeLatenessValue;



    #endregion


    #region OnEnable & OnDisable

    private void OnEnable()
    {
        EventManager.OnCorrectIncrementalScore += CorrectIncrementalScore;
        EventManager.OnWrongDecreaseScore += WrongDecreaseScore;
        EventManager.OnTimeLatenessDecreaseScore += TimeLatenessDecreaseScore;
    }

    private void OnDisable()
    {
        EventManager.OnCorrectIncrementalScore -= CorrectIncrementalScore;
        EventManager.OnWrongDecreaseScore -= WrongDecreaseScore;
        EventManager.OnTimeLatenessDecreaseScore -= TimeLatenessDecreaseScore;
    }


    #endregion

    private void Awake() => _scoreText.text = _playerData.TotalScore.ToString();
    private void CorrectIncrementalScore()
    {
        var result = _playerData.TotalScore + _correctValue;
        ScoreAnimation(result);
    }

    private void WrongDecreaseScore()
    {
        var result = _playerData.TotalScore - _wrongValue;
        ScoreAnimation(result);
    }

    private void TimeLatenessDecreaseScore()
    {
        var result = _playerData.TotalScore - _timeLatenessValue;
        ScoreAnimation(result);
    }

    private void ScoreAnimation(int result)
    {
        if (result < 0) return;
        DOTween.To(() => _playerData.TotalScore, x => _playerData.TotalScore = x, result, 1f).OnUpdate(() =>
        {
            _scoreText.text = _playerData.TotalScore.ToString();
        });
    }

}
