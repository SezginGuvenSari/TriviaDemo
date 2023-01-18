using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    #region Awake

    private void Awake() => _scoreText.text = _playerData.TotalScore.ToString();

    #endregion

    #region Async Methods
    private async Task CorrectIncrementalScore()
    {
        var result = _playerData.TotalScore + _correctValue;
        ScoreAnimation(result);
        await Task.Delay(2000);
    }

    private async Task WrongDecreaseScore()
    {
        var result = _playerData.TotalScore - _wrongValue;
        ScoreAnimation(result);
        await Task.Delay(2000);
    }

    private async Task TimeLatenessDecreaseScore()
    {
        var result = _playerData.TotalScore - _timeLatenessValue;
        ScoreAnimation(result);
        await Task.Delay(2000);
    }

    #endregion

    #region AnimationMethod

    private  void ScoreAnimation(int result)
    {
        if (result < 0) return;
        DOTween.To(() => _playerData.TotalScore, x => _playerData.TotalScore = x, result, 1.5f).SetEase(Ease.InOutExpo).OnUpdate(() =>
        {
            _scoreText.text = _playerData.TotalScore.ToString();
            
        }).OnComplete(() =>
        {
            _playerData.TotalScore = result;
           
        });
    }

    #endregion

}
