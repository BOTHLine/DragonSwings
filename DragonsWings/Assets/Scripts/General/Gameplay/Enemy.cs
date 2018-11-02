using UnityEngine;

[RequireComponent(typeof(StateController))]
public class Enemy : MonoBehaviour, Hookable, Aimable
{
    [SerializeField] private Weight weight;
    public Weight Weight { get { return weight; } }

    public FloatReference healthMax;
    public FloatReference health;

    private void Awake()
    {
        health.Value = healthMax;
    }

    public void OnHookHit()
    {
        if (--health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}