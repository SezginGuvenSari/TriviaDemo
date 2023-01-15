using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    #region References

    private Data _myData;


    #endregion

    #region Serialize

    [SerializeField] private int _totalScore;

    [SerializeField] private string _myName;

    #endregion



    #region OnEnable & OnDisable

    private void OnEnable()
    {
        EventManager.OnInitializeMyData += InitializePlayerData;
    }

    private void OnDisable()
    {
        EventManager.OnInitializeMyData += InitializePlayerData;
    }

    #endregion

    private void InitializePlayerData(LeaderboardPages[] pages)
    {
        _myData = new Data
        {
            nickname = _myName,
            rank = 1,
            isReal = true,
            score = _totalScore
        };
        pages[1].data[^1] = _myData;
    }
}
