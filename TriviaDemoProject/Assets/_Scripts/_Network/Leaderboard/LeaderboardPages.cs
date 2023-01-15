using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardPages : MonoBehaviour
{
    #region References

    public int page;
    public bool is_last;
    public List<Data> data;

    #endregion

}

public struct Data
{
    public int rank;
    public string nickname;
    public int score;
    public bool isReal;
}
