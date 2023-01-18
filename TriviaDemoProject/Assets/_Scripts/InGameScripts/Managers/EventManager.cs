using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

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


    #region QuestionEvents

    public delegate Questions.QuestionData SelectRandomQuestion();
    public static event SelectRandomQuestion OnSelectRandomQuestion;
    public static Questions.QuestionData SelectRandomQuestionMethod()
    {
        return OnSelectRandomQuestion.Invoke();
    }

    #endregion


    #region TimerEvents

    public delegate void StopTimer();
    public static event StopTimer OnStopTimer;
    public static void StopTimerMethod()
    {
        OnStopTimer?.Invoke();
    }

    //***********************************************************//

    public delegate void ResetRemainingTime();
    public static event ResetRemainingTime OnResetRemainingTime;
    public static void ResetRemainingTimeMethod()
    {
        OnResetRemainingTime?.Invoke();
    }

    #endregion


    #region ScoreEvents

    public delegate Task CorrectIncrementalScore();
    public static event CorrectIncrementalScore OnCorrectIncrementalScore;
    public static Task CorrectIncrementalScoreMethod()
    {
        return OnCorrectIncrementalScore?.Invoke();
    }

    //***********************************************************//

    public delegate Task WrongDecreaseScore();
    public static event WrongDecreaseScore OnWrongDecreaseScore;
    public static Task WrongDecreaseScoreMethod()
    {
        return OnWrongDecreaseScore?.Invoke();
    }

    //***********************************************************//

    public delegate Task TimeLatenessDecreaseScore();
    public static event TimeLatenessDecreaseScore OnTimeLatenessDecreaseScore;
    public static Task TimeLatenessDecreaseScoreMethod()
    {
        return OnTimeLatenessDecreaseScore?.Invoke();
    }
    #endregion


    #region ViewEvents

    public delegate void DisableChoices();
    public static event DisableChoices OnDisableChoices;
    public static void DisableChoicesMethod()
    {
        OnDisableChoices?.Invoke();
    }

    //***********************************************************//

    public delegate void EnableChoices();
    public static event EnableChoices OnEnableChoices;
    public static void EnableChoicesMethod()
    {
        OnEnableChoices?.Invoke();
    }

    //***********************************************************//

    public delegate void SetQuestionData();
    public static event SetQuestionData OnSetQuestionData;
    public static void SetQuestionDataMethod()
    {
        OnSetQuestionData?.Invoke();
    }

    //***********************************************************//

    public delegate Task AnimationTextAsync(Transform text);
    public static event AnimationTextAsync OnAnimationTextAsync;
    public static Task AnimationTextAsyncMethod(Transform text)
    {
        return OnAnimationTextAsync?.Invoke(text);
    }
    #endregion

    #region DiamondsEvent

    public delegate void PileOfDiamonds();
    public static event PileOfDiamonds OnPileOfDiamonds;
    public static void PileOfDiamondsMethod()
    {
        OnPileOfDiamonds?.Invoke();
    }

    #endregion
}
