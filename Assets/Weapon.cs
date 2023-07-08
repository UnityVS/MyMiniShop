using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EWeaponType
{
    Manual,
    SemiAutomatic,
    Automatic
}

public class Weapon : MonoBehaviour
{
    public static Weapon Instance;
    [SerializeField] private ShootableBall shootableBall;
    private EWeaponType _currentWeaponType;
    private float _forceSpeed = 30f;
    private bool _canShoot = true;
    private float _shootingDelay = 0.1f;
    private float _shootingDelayCheck = 0;
    private int _shootCount = 3;
    private WaitForSeconds waitShootDelay = new WaitForSeconds(0.1f);
    public void SetWeaponType(EWeaponType value) => _currentWeaponType = value;

    private void Awake()
    {
        if (Instance) Destroy(gameObject); else Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && !core.RaycastManager.Instance.GetTraderHittedInfo()) //
        {
            ShootType();
        }
        if (Input.GetMouseButtonUp(0)) BreakShoot();
    }

    public void BreakShoot()
    {
        _canShoot = true;
    }

    public void ShootType()
    {
        switch (_currentWeaponType)
        {
            case EWeaponType.Manual:
                if (_canShoot)
                {
                    _canShoot = false;
                    Shoot();
                }
                break;
            case EWeaponType.SemiAutomatic:
                if (_canShoot)
                {
                    _canShoot = false;
                    StartCoroutine(ShootSemi());
                }
                break;
            case EWeaponType.Automatic:
                _shootingDelayCheck += Time.deltaTime;
                if (_shootingDelayCheck >= _shootingDelay)
                {
                    _shootingDelayCheck = 0;
                    Shoot();
                }
                break;
        }
    }

    public void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ShootableBall newShootableBall = Instantiate(shootableBall, transform.position, shootableBall.transform.rotation, null);
        newShootableBall.GetRigidbody.AddForce(ray.direction * _forceSpeed, ForceMode.Impulse);
        Destroy(newShootableBall, 8f);
    }
    public IEnumerator ShootSemi()
    {
        int currentShootCount = 0;
        while (true)
        {
            currentShootCount++;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ShootableBall newShootableBall = Instantiate(shootableBall, transform.position, shootableBall.transform.rotation, null);
            newShootableBall.GetRigidbody.AddForce(ray.direction * _forceSpeed, ForceMode.Impulse);
            Destroy(newShootableBall, 8f);
            if (_shootCount == currentShootCount) break;
            yield return waitShootDelay;
        }
    }
}