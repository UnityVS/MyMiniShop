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
    private CanvasGroup _canvasGroup;
    private Tween _tween;
    private Coroutine _coroutine;
    private ETraderState _currentTraderState;
    public CanvasGroup GetCanvasGroup => _canvasGroup;
    public void SetTraderState(ETraderState value) => _currentTraderState = value;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowWindow(ETraderState traderState)
    {

    }

    private void WaitForFilling()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(FillItems(fruitsList));
            CancelInvoke();
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
        _coroutine = null;
    }

    private void OnEnable()
    {
        _canvasGroup.alpha = 0f;
        buttonBack.onClick.AddListener(ContinuePlaying);
        _tween = _canvasGroup.DOFade(1f, 0.25f);
        switch (_currentTraderState)
        {
            case ETraderState.ArmorTrading:
                if (_coroutine == null) _coroutine = StartCoroutine(FillItems(armorsList));
                else InvokeRepeating(nameof(WaitForFilling), 0, 0.1f);
                break;
            case ETraderState.FruitsTrading:
                if (_coroutine == null) _coroutine = StartCoroutine(FillItems(fruitsList));
                else InvokeRepeating(nameof(WaitForFilling), 0, 0.1f);
                break;
        }
    }

    private void ContinuePlaying() => _tween = _canvasGroup.DOFade(0f, 0.15f).OnComplete(() => gameObject.SetActive(false));

    private void OnDisable()
    {
        _tween.Kill();
        buttonBack.onClick.RemoveListener(ContinuePlaying);
        UnFillItems();
    }
}