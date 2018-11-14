using UnityEngine;

public class Box : MonoBehaviour, Hookable, Throwable
{
    public Weight Weight
    { get { return Weight.Light; } }

    public void OnHookHit()
    {
     //   PickUp();
    }

    public void PickUp(StateController stateController)
    {
        throw new System.NotImplementedException();
    }

    public void Throw(Vector2 targetPosition)
    {
        throw new System.NotImplementedException();
    }
}