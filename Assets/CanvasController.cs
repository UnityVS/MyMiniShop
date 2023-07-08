using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;
    [SerializeField] private TradingView tradingView;
    [SerializeField] private WinWindowView winWindowView;
    [SerializeField] private GamePlayView gamePlayView;
    public WinWindowView GetWinWindowView => winWindowView;
    public TradingView GetTradingWindow => tradingView;
    public GamePlayView GetGamePlayView => gamePlayView;

    private void Awake()
    {
        if (Instance) Destroy(gameObject); else Instance = this;
    }
}