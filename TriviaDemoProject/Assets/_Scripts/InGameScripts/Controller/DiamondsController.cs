using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DiamondsController : MonoBehaviour
{
    #region References

    private Vector3[] _initialPos;

    private Quaternion[] _initialRot;

    #endregion

    #region Serialize

    [SerializeField] private GameObject _diamonds;

    [SerializeField] private int _diamondNumber;

    [SerializeField] private RectTransform _diamondsRectTransform;

    #endregion

    #region OnEnable & OnDisable

    private void OnEnable() => EventManager.OnPileOfDiamonds += PileOfDiamonds;

    private void OnDisable() => EventManager.OnPileOfDiamonds -= PileOfDiamonds;

    #endregion

    #region Methods

    private void Start() => InitializeData();

    private void InitializeData()
    {
        _initialPos = new Vector3[_diamondNumber];
        _initialRot = new Quaternion[_diamondNumber];
        for (var i = 0; i < _diamonds.transform.childCount; i++)
        {
            foreach (Transform diamond in _diamonds.transform)
            {
                _initialPos[i] = diamond.position;
                _initialRot[i] = diamond.rotation;
            }
        }
    }

    private void DiamondsDataReset()
    {
        for (int i = 0; i < _diamonds.transform.childCount; i++)
        {
            foreach (Transform diamond in _diamonds.transform)
            {
                diamond.position = _initialPos[i];
                diamond.rotation = _initialRot[i];
            }
        }
    }

    private void PileOfDiamonds()
    {
        DiamondsDataReset();

        float delay = 0;
        foreach (Transform diamond in _diamonds.transform)
        {
            diamond.gameObject.SetActive(true);
            diamond.DOScale(0.5f, 0.2f).SetDelay(delay).SetEase(Ease.OutBack);
            diamond.GetComponent<RectTransform>().DOAnchorPos(_diamondsRectTransform.pivot, 1f).SetDelay(delay).SetEase(Ease.OutBack).OnComplete(
                () =>
                {
                    diamond.gameObject.SetActive(false);
                    diamond.DOScale(0f, 0f).SetDelay(delay).SetEase(Ease.OutBack);

                });
            delay += 0.1f;
        }
    }

    #endregion

}
