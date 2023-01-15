using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MainView : MonoBehaviour
{
    #region References

    private Vector3 _previousLbPos;

    #endregion

    #region Serialize

    [SerializeField] private GameObject _leaderboardPanel;

    #endregion

    public void EnableLbPanel()
    {
        _previousLbPos = _leaderboardPanel.transform.localPosition;
        IsActive(_leaderboardPanel, true);
        _leaderboardPanel.transform.DOLocalMove(Vector3.zero, 0.8f).SetEase(Ease.InOutBack);
    }

    public void DisableLbPanel()
    {
        _leaderboardPanel.transform.DOLocalMove(_previousLbPos, 0.8f).SetEase(Ease.InOutBack).OnComplete(() =>
            IsActive(_leaderboardPanel, false));
    }

    private void IsActive(GameObject obj, bool on) => obj.SetActive(on);

}

