using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Questions", menuName = "ScriptableObjects/Questions", order = 2)]
public class Questions : ScriptableObject
{
    #region References

    public List<QuestionData> questions;

    public List<QuestionCategory> Categories;


    #endregion

    #region Struct

    [System.Serializable]
    public struct QuestionData
    {

        public string category;

        public string question;

        public List<string> choices;

        public string answer;

        public bool isCompleted;
    }

    #endregion

}

