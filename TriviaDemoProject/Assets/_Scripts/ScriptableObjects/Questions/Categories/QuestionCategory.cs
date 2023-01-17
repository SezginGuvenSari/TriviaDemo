using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Category", menuName = "ScriptableObjects/QuestionCategories/Category", order = 3)]
public class QuestionCategory : ScriptableObject
{
    #region References

    public List<Questions.QuestionData> categoryDataList;

    #endregion

}
