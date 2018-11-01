using UnityEngine;

public class Enemy : MonoBehaviour, Hookable, Aimable
{
    [SerializeField] private Weight weight;
    public Weight Weight { get { return weight; } }
}