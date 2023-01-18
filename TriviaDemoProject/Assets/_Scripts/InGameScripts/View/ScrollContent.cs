using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollContent : MonoBehaviour
{
    #region References

    private Vector3 _resizeCenter;

    private float _areaWidth;

    #endregion

    #region Serialize

    [SerializeField] private RectTransform _rectTransform;

    [Tooltip("You can adjust the curve as you want and adjust the enlargement and shrinkage of the data. ")]
    [SerializeField] private AnimationCurve _resizeRatio;

    #endregion

    #region Methods
    private void Start() => Initialize();
    private void Initialize()
    {
        _resizeCenter = _rectTransform.position;
        VisibleAreaWidthCalculator();
    }
    public void ResizeObjects()
    {
        foreach (Transform t in transform)
        {
            var distance = Vector3.Distance(_resizeCenter, t.position);
            distance = Mathf.Abs(distance);
            var offSet = distance / _areaWidth;
            t.localScale = Vector3.one * _resizeRatio.Evaluate(offSet);
        }
    }
    private void VisibleAreaWidthCalculator() => _areaWidth = _rectTransform.rect.height;

    #endregion

}
