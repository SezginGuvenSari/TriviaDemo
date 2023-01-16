using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{

    #region References



    #endregion


    #region Serialize

    [SerializeField] private Questions _questions;

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
}
