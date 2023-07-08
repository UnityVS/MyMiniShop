using DG.Tweening;
using TMPro;
using UnityEngine;

public class GamePlayView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTask;
    private CanvasGroup _canvasGroup;
    private Tween _tween;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }

    public void ShowTask(ETask taskOrder)
    {
        _tween = _canvasGroup.DOFade(1f, 1f);
        switch (taskOrder)
        {
            case ETask.BlowUpAllBalls:
                textTask.text = "<b>Task:</b> Blow up all balls. \nNeed find all balls on scene";
                break;
            case ETask.BlowUpAllOneSomeColorBalls:
                textTask.text = "<b>Task:</b> Blow up all balls with one  color. \nNeed find all balls one color on scene";
                break;
        }
    }

    private void OnDisable() => _tween.Kill();
}