using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{

    #region References

    private int _categoryIndex;

    private int _questionIndex;

    #endregion


    #region Serialize

    [SerializeField] private Questions _questions;


    #endregion

    #region Properties

    public int CategoryIndex => _categoryIndex;

    public int QuestionIndex => _questionIndex;

    #endregion

    #region OnEnable/OnDisable

    private void OnEnable() => EventManager.OnSelectRandomQuestion += SelectRandomQuestion;

    private void OnDisable() => EventManager.OnSelectRandomQuestion -= SelectRandomQuestion;

    #endregion

    private async void Start() => GetQuestions();

    private async void GetQuestions()
    {
        var questionPage = $"https://magegamessite.web.app/case1/questions.json";
        var httpClient = new HttpClient(new JsonSerializationOption());
        var questions = await httpClient.GetRequest<Questions>(questionPage);
        _questions.questions = questions.questions;
        SetDataByCategory();
    }

    private void SetDataByCategory()
    {
        ClearCategoryDataList();
        for (var i = 0; i < _questions.questions.Count; i++)
        {
            foreach (var t in _questions.Categories)
            {
                if (_questions.questions[i].category == t.name)
                {
                    t.categoryDataList.Add(_questions.questions[i]);
                }
            }
        }
    }

    private void ClearCategoryDataList()
    {
        foreach (var category in _questions.Categories)
        {
            category.categoryDataList.Clear();
        }
    }

    private Questions.QuestionData SelectRandomQuestion()
    {
        _categoryIndex = Random.Range(0, _questions.Categories.Count);

        var category = _questions.Categories[_categoryIndex];

        _questionIndex = Random.Range(0, category.categoryDataList.Count);

        var data = category.categoryDataList[_questionIndex];
       
        return data;
    }

}
