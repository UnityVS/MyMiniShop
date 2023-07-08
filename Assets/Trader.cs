using DG.Tweening;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ETraderState
{
    NoTradeWithPlayer,
    ArmorTrading,
    FruitsTrading
}

public class Trader : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private ETraderState _currentTraderState;
    private Tween _tween;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void ShowMarker()
    {
        _currentTraderState = (ETraderState)Random.Range(0, Enum.GetValues(typeof(ETraderState)).Length + 1);

        if (_currentTraderState == ETraderState.NoTradeWithPlayer)
        {
            _tween = canvasGroup.DOFade(1f, 1f).OnComplete(() => _tween.SetDelay(1.5f).OnComplete(() => canvasGroup.DOFade(0f, 0.5f)).OnComplete(() => _tween.Kill()));
            return;
        }

        CanvasController.Instance.GetTradingWindow.ShowWindow(_currentTraderState);
    }

    private void OnDisable()
    {
        _tween.Kill();
    }
}