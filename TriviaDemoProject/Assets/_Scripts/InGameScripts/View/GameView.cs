using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    #region Serialize

    [SerializeField] private TMP_Text _questionText;

    [SerializeField] private List<TMP_Text> _choices;

    [SerializeField] private Transform _choicesTransform;

    [SerializeField] private Transform _finishPanel;

    #endregion

    #region OnEnable & OnDisable

    private void OnEnable()
    {
        EventManager.OnDisableChoices += DisableChoices;
        EventManager.OnEnableChoices += EnableChoices;
        EventManager.OnSetQuestionData += SetQuestionData;
        EventManager.OnAnimationTextAsync += AnimationTextAsync;
    }

    private void OnDisable()
    {
        EventManager.OnDisableChoices -= DisableChoices;
        EventManager.OnEnableChoices -= EnableChoices;
        EventManager.OnSetQuestionData -= SetQuestionData;
        EventManager.OnAnimationTextAsync -= AnimationTextAsync;
    }

    #endregion

    #region Methods

    private void Start() => SetQuestionData();
    public void SetQuestionData()
    {
        CheckGameFinish();
        if (GameManager.Instance.IsQuestionsCompleted()) return;
        var data = GameManager.Instance.GetCurrentQuestion();
        if (!data.isCompleted)
        {
            _questionText.text = data.question;
            SetChoicesData(data);
        }
        else SetQuestionData();
    }
    private void SetChoicesData(Questions.QuestionData data)
    {
        for (var i = 0; i < _choices.Count; i++)
        {
            var newDataText = data.choices[i].Replace("\n", "");
            _choices[i].text = newDataText;
        }
    }
    private void DisableChoices()
    {
        foreach (Transform choices in _choicesTransform)
        {
            var isClick = choices.GetComponent<AnswerController>().IsClick;
            var button = choices.GetComponent<Button>();
            if (!isClick) button.interactable = false;
        }
    }

    private void EnableChoices()
    {
        foreach (Transform choices in _choicesTransform)
        {
            choices.GetComponent<AnswerController>().IsClick = false;
            var button = choices.GetComponent<Button>();
            var image = choices.GetComponent<Image>();
            image.color = Color.white;
            button.interactable = true;
        }
    }

    private async Task AnimationTextAsync(Transform text)
    {
        var currentPos = text.position;
        text.DOScale(Vector3.one, 0.1f).OnComplete(() =>
        {
            text.DOLocalMoveY(200f, 1f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                text.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() =>
                {
                    text.position = currentPos;
                });
            });
        });
        await Task.Delay(1000);
    }
    private void CheckGameFinish()
    {
        if (!GameManager.Instance.IsQuestionsCompleted()) return;
        _finishPanel.gameObject.SetActive(true);
        _finishPanel.DOLocalMoveX(0, 0.5f).SetEase(Ease.InCubic);
    }

    #endregion

}
