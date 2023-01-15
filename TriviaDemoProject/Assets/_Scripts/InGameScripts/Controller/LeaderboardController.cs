using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    #region References

    private List<PersonController> _personList;

    private List<Data> _listData;

    #endregion

    #region OnEnable & OnDisable

    private void OnEnable() => EventManager.OnSetLeaderboardData += SetLeaderboardData;

    private void OnDisable() => EventManager.OnSetLeaderboardData -= SetLeaderboardData;

    #endregion

    private void SetLeaderboardData(bool isDone, LeaderboardPages[] pages)
    {
        if (!isDone) return;

        _listData = new List<Data>();
        SetContentData();
        SortAlgorithm.SortAllData(GetListOfData(pages), 0, _listData.Count - 1);
        for (int i = 0; i < _listData.Count; i++)
        {
            var nickname = _personList[i].PersonData.NickName;
            var rank = _personList[i].PersonData.Rank;
            var score = _personList[i].PersonData.Score;
            nickname.text = _listData[i].nickname;
            rank.text = (i + 1).ToString();
            score.text = _listData[i].score.ToString();
            _personList[i].PersonData.IsReal = _listData[i].isReal; ;
        }
    }
    private void SetContentData()
    {
        _personList = new List<PersonController>();
        foreach (Transform t in transform)
        {
            var data = t.GetComponent<PersonController>();
            _personList.Add(data);
        }
    }
    private List<Data> GetListOfData(LeaderboardPages[] pages)
    {
        foreach (var t in pages)
        {
            for (int i = 0; i < t.data.Count; i++)
            {
                _listData.Add(t.data[i]);
            }
        }
        return _listData;
    }

}
