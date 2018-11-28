using UnityEngine;

[RequireComponent(typeof(StateController))]
public class Enemy : MonoBehaviour
{
    public void Die()
    {
        Destroy(gameObject);
    }
}