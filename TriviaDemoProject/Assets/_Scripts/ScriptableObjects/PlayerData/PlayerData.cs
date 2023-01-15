using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{

    #region Serialize

    [SerializeField] private int _totalScore;

    [SerializeField] private string _playerName;

    #endregion


    #region Properties

    public int TotalScore
    {
        get => _totalScore;
        set => _totalScore = value;
    }

    public string PlayerName
    {
        get => _playerName;
        set => _playerName = value;
    }

    #endregion

}
