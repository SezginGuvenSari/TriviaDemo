using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersonData : MonoBehaviour
{
    #region References


    [SerializeField] private TMP_Text _rank;

    [SerializeField] private TMP_Text _score;

    [SerializeField] private TMP_Text _nickName;

    #endregion


    #region Properties

    public TMP_Text Rank
    {
        get => _rank;
        set => _rank = value;
    }
    public TMP_Text Score
    {
        get => _score;
        set => _score = value;
    }

    public TMP_Text NickName
    {
        get => _nickName;
        set => _nickName = value;
    }

    #endregion
}
