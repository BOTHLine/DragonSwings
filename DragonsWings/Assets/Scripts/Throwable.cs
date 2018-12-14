using UnityEngine;

public interface Throwable
{
    void PickUp(StateController stateController);
    void Throw(Vector2 targetPosition);

    float Damage { get; }
}