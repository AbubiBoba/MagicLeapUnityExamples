using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Pistol : MonoBehaviour, IShooting
{
    [SerializeField] private int _damage;
    //[SerializeField] private Vector3 _force;
    [SerializeField] private int _force;

    [SerializeField] GameObject _particlesPrefab;
    [SerializeField] Transform _muzzle;

    [SerializeField] GameObject _sleevePrefab;
    [SerializeField] Transform _shutter;

    private MLInput.Controller _controller;

    private void Start()
    {
        MLInput.OnTriggerDown += OnGunTriggerPressed;
    }
    public void OnGunTriggerPressed(byte controllerId, float value)
    {
        //ShootParticles();
        ShootSleeve();
        GiveDamage();
    }
    private void OnDestroy()
    {
        MLInput.OnTriggerDown -= OnGunTriggerPressed;
    }
    public void ShootParticles()
    {
        GameObject particles = Instantiate(_particlesPrefab, _muzzle);
    }
    public void ShootSleeve()
    {
        GameObject sleeve = Instantiate(_sleevePrefab, _shutter.position, Quaternion.identity);
        sleeve.GetComponent<Rigidbody>().AddForce(_shutter.right + _shutter.up * _force, ForceMode.Force);
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
