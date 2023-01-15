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

    [SerializeField] [Range(1, 10)] private int _pageNumber;

    [SerializeField] private bool _isDone = false;

    #endregion

    #region References

    private LeaderboardPages[] _pages;

    #endregion
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
        EventManager.InitializeMyDataMethod(_pages);
        EventManager.GetEnableObjectsMethod(GetDataLength());
        EventManager.SetLeaderboardDataMethod(_isDone, _pages);
    }

    private LeaderboardPages[] GetPage() => !_isDone ? null : _pages;

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

}

