using UnityEngine;

[RequireComponent(typeof(StateController))]
public class Enemy : MonoBehaviour
{
    public void Die()
    {
        gameObject.SetActive(false);
        //    Destroy(gameObject);
    }
}