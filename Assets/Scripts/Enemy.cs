using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    public void TakeDamage(int damage)
    {
        Debug.Log("Damaged");
        _health -= damage;
        if(_health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Died");
        Destroy(gameObject);
    }
}
