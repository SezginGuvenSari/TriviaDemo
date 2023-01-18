using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Http;
using Unity.VisualScripting;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class LeaderboardManager : MonoBehaviour
{

    #region Serialize

    [Tooltip("If we have how many pages, we enter that number here.")]
    [SerializeField] [Range(1, 10)] private int _pageNumber;

    [Tooltip("Checks if the web request has been made.")]
    [SerializeField] private bool _isDone = false;

    [Tooltip("You can set playerData scriptable object.")]
    [SerializeField] private PlayerData _playerData;

    #endregion

    #region References

    private LeaderboardPages[] _pages;

    private Data _myData;

    #endregion

    #region Methods

    public async void GetPagesData()
    {
        _pages = new LeaderboardPages[_pageNumber];
        for (int i = 0; i < _pageNumber; i++)
        {
            var page = $"https://magegamessite.web.app/case1/leaderboard_page_{i}.json";
            var httpClient = new HttpClient(new JsonSerializationOption());
            _pages[i] = await httpClient.GetRequest<LeaderboardPages>(page);
        }
        _isDone = true;
        InitializePlayerData();
        EventManager.GetEnableObjectsMethod(GetDataLength());
        EventManager.SetLeaderboardDataMethod(_isDone, _pages);
    }

    private int GetDataLength()
    {
        if (!_isDone) return 0;
        var dataLength = 0;
        foreach (var t in _pages)
        {
            var length = t.data.Count;
            dataLength += length;
        }
        return dataLength;
    }

    private void InitializePlayerData()
    {
        _myData = new Data
        {
            nickname = _playerData.PlayerName,
            rank = 1,
            isReal = true,
            score = _playerData.TotalScore
        };
        _pages[1].data.Add(_myData);
    }

    #endregion

}

