using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartGameView : MonoBehaviour
{
    [SerializeField] private Toggle weaponTypeManual;
    [SerializeField] private Toggle weaponTypeSemiAutomatic;
    [SerializeField] private Toggle weaponTypeAutomatic;

    [SerializeField] private Toggle gameTaskTypeAllBalls;
    [SerializeField] private Toggle gameTaskTypeOneColoredBalls;

    [SerializeField] private Button buttonStartGame;
    private EWeaponType _weaponType;
    private EWinTask _winTask;
    private Tween _tween;
    private CanvasGroup _canvasGroup;
    private bool _weaponON, _taskON;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 1;
    }

    private void OnEnable()
    {
        buttonStartGame.onClick.AddListener(SetGameData);
        weaponTypeManual.onValueChanged.AddListener(delegate { _weaponType = EWeaponType.Manual; _weaponON = true; CheckToggles(); });
        weaponTypeSemiAutomatic.onValueChanged.AddListener(delegate { _weaponType = EWeaponType.SemiAutomatic; _weaponON = true; CheckToggles(); });
        weaponTypeAutomatic.onValueChanged.AddListener(delegate { _weaponType = EWeaponType.Automatic; _weaponON = true; CheckToggles(); });
        gameTaskTypeAllBalls.onValueChanged.AddListener(delegate { _winTask = EWinTask.BlowUpAllBalls; _taskON = true; CheckToggles(); });
        gameTaskTypeOneColoredBalls.onValueChanged.AddListener(delegate { _winTask = EWinTask.BlowUpAllOneSomeColorBalls; _taskON = true; CheckToggles(); });
    }

    private void CheckToggles()
    {
        if (_weaponON && _taskON) buttonStartGame.gameObject.SetActive(true);
    }

    private void SetGameData()
    {
        BallsController.GetInstance.SetGameType(_winTask);
        Weapon.Instance.SetWeaponType(_weaponType);
        _tween = _canvasGroup.DOFade(0f, 0.5f).OnComplete(() => { CanvasController.Instance.GetGamePlayView.gameObject.SetActive(true); CanvasController.Instance.GetGamePlayView.ShowTask(_winTask); gameObject.SetActive(false); });
    }

    private void OnDisable()
    {
        _tween.Kill();
        buttonStartGame.onClick.RemoveListener(SetGameData);
        weaponTypeManual.onValueChanged = null;
        weaponTypeSemiAutomatic.onValueChanged = null;
        weaponTypeAutomatic.onValueChanged = null;
        gameTaskTypeAllBalls.onValueChanged = null;
        gameTaskTypeOneColoredBalls.onValueChanged = null;
    }
}