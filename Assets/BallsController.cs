using System.Collections.Generic;

public enum EWinTask
{
    BlowUpAllBalls,
    BlowUpAllOneSomeColorBalls
}

public class BallsController
{
    public static BallsController GetInstance => _instance;
    private static readonly BallsController _instance = new();
    private BallsController() { }
    private EWinTask _task;
    private EBallType eBallType;
    private int _eBallsTypeCount;
    private Dictionary<EBallType, int> _allCollectableCountOfBallsOfType = new();
    private Dictionary<EBallType, int> _currentCollectableCountOfBallsOfType = new();
    public Dictionary<EBallType, int> GetHowBallsNeeded => _allCollectableCountOfBallsOfType;
    public Dictionary<EBallType, int> GetHowBallsCollected => _currentCollectableCountOfBallsOfType;
    public void SetGameType(EWinTask value) => _task = value;
    public EWinTask GetTask => _task;
    public EBallType GetEBallType => eBallType;
    public void SetEBallsTypeCount(int value) => _eBallsTypeCount = value;

    public void CheckWin()
    {
        switch (_task)
        {
            case EWinTask.BlowUpAllBalls:
                int doneCount = 0;
                foreach (var item in _currentCollectableCountOfBallsOfType)
                {
                    if (item.Value == _allCollectableCountOfBallsOfType[item.Key])
                        doneCount++;
                    if (doneCount == _eBallsTypeCount)
                        CanvasController.Instance.GetWinWindowView.gameObject.SetActive(true);
                }
                break;
            case EWinTask.BlowUpAllOneSomeColorBalls:
                foreach (var item in _currentCollectableCountOfBallsOfType)
                {
                    if (item.Value == _allCollectableCountOfBallsOfType[item.Key])
                    {
                        eBallType = item.Key;
                        CanvasController.Instance.GetWinWindowView.gameObject.SetActive(true);
                        break;
                    }
                }
                break;
        }
    }
}