using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerController : MonoBehaviour
{
    #region References

    private Image _image;

    #endregion

    #region Serialize

    [SerializeField] private AnswerType _answerType;

    [SerializeField] private bool _isClick = false;

    #endregion

    #region Properties

    public bool IsClick
    {
        get => _isClick;
        set => _isClick = value;
    }

    #endregion

    #region Methods

    private void Awake() => _image = GetComponent<Image>();

    public void AnswerControl()
    {
        _isClick = true;
        EventManager.DisableChoicesMethod();
        var answer = GameManager.Instance.CurrentQuestionData.answer;
        if (_answerType.ToString() == answer)
        {
            SetAnswerButtonColor(_image, Color.green);
            GameManager.Instance.CorrectAnswerAsync();
        }
        else
        {
            SetAnswerButtonColor(_image, Color.red);
            GameManager.Instance.WrongAnswerAsync();
        }

    }

    private void SetAnswerButtonColor(Image image, Color color) => image.color = color;

    #endregion

}
