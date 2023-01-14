using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    #region References

    private List<PersonController> _personList;

    #endregion

    #region OnEnable & OnDisable

    private void OnEnable() => EventManager.OnSetLeaderboardData += SetLeaderboardData;

    private void OnDisable() => EventManager.OnSetLeaderboardData -= SetLeaderboardData;

    #endregion

    private void SetLeaderboardData(bool isDone, LeaderboardPages[] pages)
    {
        if (!isDone) return;

        SetDataList();
        int counter = 0;
        foreach (var t in pages)
        {
            for (var j = 0; j < t.data.Length; j++)
            {
                var nickname = _personList[counter].PersonData.NickName;
                var rank = _personList[counter].PersonData.Rank;
                var score = _personList[counter].PersonData.Score;
                nickname.text = t.data[j].nickname;
                rank.text = t.data[j].rank.ToString();
                score.text = t.data[j].score.ToString();
                counter++;
            }
        }
    }

    private void SetDataList()
    {
        _personList = new List<PersonController>();
        foreach (Transform t in transform)
        {
            var data = t.GetComponent<PersonController>();
            _personList.Add(data);
        }
    }
}
