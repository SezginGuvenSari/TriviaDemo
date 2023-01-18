using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region References

    private Questions.QuestionData _currentQuestionData;

    #endregion

    #region Serialize

    [SerializeField] private Questions _questions;

    [SerializeField] private Transform _correctText;

    [SerializeField] private Transform _wrongText;

    [SerializeField] private Transform _timeOverText;

    [SerializeField] private QuestionManager _questionManager;

    #endregion

    #region Properties

    public Questions.QuestionData CurrentQuestionData
    {
        get => _currentQuestionData;
        set => _currentQuestionData = value;
    }

    #endregion

    #region Methods

    public bool IsQuestionsCompleted()
    {
        var categories = _questions.Categories;

        foreach (var t in categories)
        {
            for (int j = 0; j < t.categoryDataList.Count; j++)
            {
                var data = t.categoryDataList[j];
                if (!data.isCompleted) return false;
            }
        }
        return true;
    }
    public Questions.QuestionData GetCurrentQuestion()
    {
        _currentQuestionData = EventManager.SelectRandomQuestionMethod();
        return _currentQuestionData;
    }
    public async void CorrectAnswerAsync()
    {
        CompletedQuestion();
        EventManager.StopTimerMethod();
        var correctTaskGroup = await EventManager.AnimationTextAsyncMethod(_correctText).ContinueWith
            (async _ =>
            {
                await EventManager.CorrectIncrementalScoreMethod();
            });
        EventManager.PileOfDiamondsMethod();
        await Task.WhenAll(correctTaskGroup);
        EventManager.EnableChoicesMethod();
        EventManager.SetQuestionDataMethod();
        EventManager.ResetRemainingTimeMethod();

    }
    public async void WrongAnswerAsync()
    {
        EventManager.StopTimerMethod();
        var wrongTaskGroup = await EventManager.AnimationTextAsyncMethod(_wrongText).ContinueWith(async _ =>
        {
            await EventManager.WrongDecreaseScoreMethod();
        });
        await Task.WhenAll(wrongTaskGroup);
        EventManager.EnableChoicesMethod();
        EventManager.ResetRemainingTimeMethod();
    }
    public async void TimeOverAsync()
    {
        var timeOverTaskGroup =
            await EventManager.AnimationTextAsyncMethod(_timeOverText).ContinueWith(async _questions =>
            {
                await EventManager.TimeLatenessDecreaseScoreMethod();
            });
        await Task.WhenAll(timeOverTaskGroup);
        EventManager.EnableChoicesMethod();
        EventManager.ResetRemainingTimeMethod();
    }
    private void CompletedQuestion()
    {
        var categoryIndex = _questionManager.CategoryIndex;
        var questionIndex = _questionManager.QuestionIndex;
        _currentQuestionData.isCompleted = true;
        _questions.Categories[categoryIndex].categoryDataList[questionIndex] = _currentQuestionData;
    }

    #endregion

}
