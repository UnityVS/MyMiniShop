using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinWindowView : MonoBehaviour
{
    [SerializeField] private Button buttonContinue;
    [SerializeField] private TextMeshProUGUI textWinComponent;
    private CanvasGroup canvasGroup;
    private Tween _tween;
    public TextMeshProUGUI GetTextWinComponent => textWinComponent;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        switch (BallsController.GetInstance.GetTask)
        {
            case EWinTask.BlowUpAllBalls:
                textWinComponent.text = $"<size=100>Victory!</size>\r\nCollected: All balls";
                break;
            case EWinTask.BlowUpAllOneSomeColorBalls:
                textWinComponent.text = $"<size=100>Victory!</size>\r\nCollected: All <color={BallsController.GetInstance.GetEBallType.ToString().ToLower()}>{BallsController.GetInstance.GetEBallType}</color> balls";
                break;
        }
        CanvasController.Instance.GetGamePlayView.HideWindow();
        ShowWinWindow();
    }

    private void OnEnable() => buttonContinue.onClick.AddListener(ContinuePlaying);

    public void ShowWinWindow()
    {
        _tween = canvasGroup.DOFade(1f, 1f).OnComplete(() => _tween.Kill());
    }

    private void ContinuePlaying() => SceneManager.LoadScene(0);

    private void OnDisable()
    {
        _tween.Kill();
        buttonContinue.onClick.RemoveListener(ContinuePlaying);
    }
}