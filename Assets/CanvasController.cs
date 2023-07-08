using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;
    [SerializeField] private TradingView tradingView;
    [SerializeField] private WinWindowView winWindowView;
    public WinWindowView GetWinWindowView => winWindowView;
    public TradingView GetTradingWindow => tradingView;

    private void Awake()
    {
        if (Instance) Destroy(gameObject); else Instance = this;
    }
}