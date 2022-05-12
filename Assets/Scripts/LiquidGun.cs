using UnityEngine.XR.MagicLeap;
using UnityEngine;

public class LiquidGun : MonoBehaviour, IShooting
{
    [SerializeField] private int _damage;
    [SerializeField] private int _maxAmmoAmount;
    private int _ammoAmount;

    [SerializeField] GameObject _particlesPrefab;
    [SerializeField] Transform _muzzle;

    private MLInput.Controller _controller;

    private void Start()
    {
        _ammoAmount = _maxAmmoAmount - 1;
        MLInput.OnTriggerDown += OnGunTriggerPressed;
    }
    public void OnGunTriggerPressed(byte controllerId, float value)
    {
        TryToShoot();
    }
    private void TryToShoot()
    {
        if (_ammoAmount > 0)
        {
            //ShootParticles();
            GiveDamage();
            ShootSleeve();
            _ammoAmount = (_ammoAmount <= 0) ? (0) : (_ammoAmount - 1);
        }
    }
    private void OnDestroy()
    {
        MLInput.OnTriggerDown -= OnGunTriggerPressed;
    }
    public void ShootParticles()
    {
        GameObject particles = Instantiate(_particlesPrefab, _muzzle);
    }
    [SerializeField] private LiquidAmmoDisplay _liquidAmmoDisplay;
    public void ShootSleeve()
    {
        _liquidAmmoDisplay.UpdateAmount(_ammoAmount, _maxAmmoAmount);
    }
    public void GiveDamage()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Enemy enemy;
            if (hit.collider.TryGetComponent<Enemy>(out enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }
    }
}
