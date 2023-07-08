using System.Collections.Generic;

public enum ETask
{
    BlowUpAllBalls,
    BlowUpAllOneSomeColorBalls
}

public class BallsController
{
    public static BallsController GetInstance => _instance;
    private static readonly BallsController _instance = new();
    private BallsController() { }
    private ETask _task;
    private int _eBallsTypeCount;
    private Dictionary<EBallType, int> _allCollectableCountOfBallsOfType = new();
    private Dictionary<EBallType, int> _currentCollectableCountOfBallsOfType = new();
    public Dictionary<EBallType, int> GetHowBallsNeeded => _allCollectableCountOfBallsOfType;
    public Dictionary<EBallType, int> GetHowBallsCollected => _currentCollectableCountOfBallsOfType;
    public void SetGameType(ETask value) => _task = value;
    public void SetEBallsTypeCount(int value) => _eBallsTypeCount = value;

    public void CheckWin()
    {
        switch (_task)
        {
            case ETask.BlowUpAllBalls:
                int doneCount = 0;
                foreach (var item in _currentCollectableCountOfBallsOfType)
                {
                    if (item.Value == _allCollectableCountOfBallsOfType[item.Key])
                        doneCount++;
                    if (doneCount == _eBallsTypeCount)
                        CanvasController.Instance.GetWinWindowView.ShowWinWindow();
                }
                break;
            case ETask.BlowUpAllOneSomeColorBalls:
                foreach (var item in _currentCollectableCountOfBallsOfType)
                {
                    if (item.Value == _allCollectableCountOfBallsOfType[item.Key])
                    {
                        CanvasController.Instance.GetWinWindowView.ShowWinWindow();
                        break;
                    }
                }
                break;
        }
    }
}