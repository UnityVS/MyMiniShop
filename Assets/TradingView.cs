using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TradingView : MonoBehaviour
{
    [SerializeField] private Button buttonBack;
    [SerializeField] private List<Sprite> fruitsList;
    [SerializeField] private List<Sprite> armorsList;
    [SerializeField] private List<TradeableItem> _tradeableItemsList = new List<TradeableItem>();
    private CanvasGroup canvasGroup;
    private Tween _tween;
    private Coroutine _coroutine;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        UnFillItems();
    }

    public void ShowWindow(ETraderState traderState)
    {
        switch (traderState)
        {
            case ETraderState.ArmorTrading:
                if (_coroutine == null) _coroutine = StartCoroutine(FillItems(armorsList));
                break;
            case ETraderState.FruitsTrading:
                if (_coroutine == null) _coroutine = StartCoroutine(FillItems(fruitsList));
                break;
        }
    }

    private void UnFillItems()
    {
        foreach (TradeableItem item in _tradeableItemsList)
            item.GetImageItem.gameObject.SetActive(false);
    }

    private IEnumerator FillItems(List<Sprite> spritesList)
    {
        int maxFillItemsCount = Random.Range(1, _tradeableItemsList.Count);
        int currentIndex = 0;
        foreach (TradeableItem item in _tradeableItemsList)
        {
            item.GetImageItem.sprite = spritesList[Random.Range(0, spritesList.Count)];
            item.GetImageItem.gameObject.SetActive(true);
            ++currentIndex;
            if (currentIndex == maxFillItemsCount) break;
        }
        yield return new WaitUntil(() => currentIndex == maxFillItemsCount);
        gameObject.SetActive(true);
        _coroutine = null;
    }

    private void OnEnable()
    {
        canvasGroup.alpha = 0f;
        buttonBack.onClick.AddListener(ContinuePlaying);
    }

    private void ContinuePlaying() => _tween = canvasGroup.DOFade(0f, 1f).OnComplete(() => gameObject.SetActive(false));

    private void OnDisable()
    {
        _tween.Kill();
        buttonBack.onClick.RemoveListener(ContinuePlaying);
        UnFillItems();
    }
}