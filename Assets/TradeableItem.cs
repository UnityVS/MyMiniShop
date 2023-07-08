using UnityEngine;
using UnityEngine.UI;

public class TradeableItem : MonoBehaviour
{
    [SerializeField] private Image imageItem;
    private Button _buttonBuy;
    public Image GetImageItem => imageItem;

    private void Awake() => _buttonBuy = GetComponent<Button>();

    private void OnEnable() => _buttonBuy.onClick.AddListener(delegate { Debug.Log("Buyed!"); gameObject.SetActive(false); });

    private void OnDisable() => _buttonBuy.onClick = null;
}
