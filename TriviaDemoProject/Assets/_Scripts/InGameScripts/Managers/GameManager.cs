using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private QuestionManager _questionManager;

    #endregion

    #region Properties

    public Questions.QuestionData CurrentQuestionData
    {
        get => _currentQuestionData;
        set => _currentQuestionData = value;
    }

    #endregion


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


    public async void CorrectAnswerAysnc()
    {
       
        CompletedQuestion();
        EventManager.StopTimerMethod();
        await EventManager.AnimationTextAsyncMethod(_correctText);
        EventManager.CorrectIncrementalScoreMethod();
        EventManager.SetQuestionDataMethod();
        EventManager.EnableChoicesMethod();
        EventManager.ResetRemainingTimeMethod();

    }

    public void WrongAnswer()
    {

    }

    private void CompletedQuestion()
    {
        var categoryIndex = _questionManager.CategoryIndex;
        var questionIndex = _questionManager.QuestionIndex;
        _currentQuestionData.isCompleted = true;
        _questions.Categories[categoryIndex].categoryDataList[questionIndex] = _currentQuestionData;
    }
}
