using UnityEngine;

[RequireComponent(typeof(StateController))]
public class Enemy : MonoBehaviour, Hookable, Aimable
{
    [SerializeField] private Weight weight;
    public Weight Weight { get { return weight; } }

    public void OnHookHit()
    {
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}