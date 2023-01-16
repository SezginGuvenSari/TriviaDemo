using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager
{
    #region PoolEvents

    public delegate GameObject GetObjectsInPool(int objectType);
    public static event GetObjectsInPool OnGetObjectsInPool;
    public static GameObject GetObjectsInPoolMethod(int objectType)
    {
        return OnGetObjectsInPool?.Invoke(objectType);
    }

    //***********************************************************//

    public delegate void GetEnableObjects(int length);
    public static event GetEnableObjects OnGetEnableObjects;
    public static void GetEnableObjectsMethod(int length)
    {
        OnGetEnableObjects?.Invoke(length);
    }


    #endregion


    #region LeaderBoard Events

    public delegate LeaderboardPages[] GetPage();
    public static event GetPage OnGetPage;
    public static LeaderboardPages[] GetPageMethod()
    {
        return OnGetPage?.Invoke();
    }

    //***********************************************************//

    public delegate void SetLeaderboardData(bool isDone, LeaderboardPages[] pages);
    public static event SetLeaderboardData OnSetLeaderboardData;
    public static void SetLeaderboardDataMethod(bool isDone, LeaderboardPages[] pages)
    {
        OnSetLeaderboardData?.Invoke(isDone, pages);
    }

    #endregion


}
