using UnityEngine;
using UnityEngine.EventSystems;

namespace core
{
    public class RaycastManager : MonoBehaviour
    {
        public static core.RaycastManager Instance;
        private Camera _cameraMain;
        private Trader _traderHitted;
        public bool GetTraderHittedInfo() => _traderHitted ? true : false;

        private void Awake()
        {
            if (Instance) Destroy(gameObject); else Instance = this;
            _cameraMain = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = _cameraMain.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                    if (hit.collider.TryGetComponent(out Trader trader))
                    {
                        _traderHitted = trader;
                        trader.ShowMarker();
                    }
                    else _traderHitted = null;
            }
        }
    }
}