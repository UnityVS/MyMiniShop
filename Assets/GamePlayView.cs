using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTask;
    [SerializeField] private ContentSizeFitter fitter;
    private CanvasGroup _canvasGroup;
    private Tween _tween;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }

    public void HideWindow()
    {
        _tween = _canvasGroup.DOFade(0f, 0.5f);
    }

    public void ShowTask(EWinTask taskOrder)
    {
        _tween = _canvasGroup.DOFade(1f, 0.5f);
        switch (taskOrder)
        {
            case EWinTask.BlowUpAllBalls:
                textTask.text = "<b>Task:</b> Blow up all the balls of all colors on the scene";
                break;
            case EWinTask.BlowUpAllOneSomeColorBalls:
                textTask.text = "<b>Task:</b> Blow up all the balls of the same color to choose from";
                break;
        }
        StartCoroutine(UpdateFitter());
    }

    private IEnumerator UpdateFitter()
    {
        fitter.enabled = false;
        yield return new WaitForSeconds(0.1f);
        fitter.enabled = true;
    }

    private void OnDisable() => _tween.Kill();
}