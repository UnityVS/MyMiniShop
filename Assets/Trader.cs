using DG.Tweening;
using System;
using TMPro;
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
    [SerializeField] private CanvasGroup _currentTextErrorCanvasGroup;
    private Tween _tween;

    public void ShowMarker()
    {
        ETraderState _currentTraderState = (ETraderState)Random.Range(0, Enum.GetValues(typeof(ETraderState)).Length);
        Debug.Log(_currentTraderState);
        if (_currentTraderState == ETraderState.NoTradeWithPlayer)
        {
            _tween = _currentTextErrorCanvasGroup.DOFade(1f, 1f).OnComplete(() => WaitALittle());
        }
        else
        {
            CanvasController.Instance.GetTradingWindow.SetTraderState(_currentTraderState);
            CanvasController.Instance.GetTradingWindow.gameObject.SetActive(true);
        }
    }

    private void WaitALittle()
    {
        _currentTextErrorCanvasGroup.DOFade(0f, 0.5f).SetDelay(1f);
    }

    private void OnDisable()
    {
        _tween.Kill();
    }
}