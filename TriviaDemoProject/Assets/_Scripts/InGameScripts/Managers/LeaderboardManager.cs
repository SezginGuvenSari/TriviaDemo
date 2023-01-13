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

    #endregion

    #region References

    private LeaderboardPages[] _pages;

    #endregion

    private void Start()
    {
        GetPagesData();
    }

    public async void GetPagesData()
    {
        _pages = new LeaderboardPages[_pageNumber];
        for (int i = 0; i < _pageNumber; i++)
        {
            var page = $"https://magegamessite.web.app/case1/leaderboard_page_{i}.json";
            var httpClient = new HttpClient(new JsonSerializationOption());
            _pages[i] = await httpClient.GetRequest<LeaderboardPages>(page);
            SendData(_pages[i]);
        }
        print("End Function");
    }

    private void SendData(LeaderboardPages page)
    {
        foreach (var data in page.data)
        {
            print(data.nickname);
        }

    }

}

