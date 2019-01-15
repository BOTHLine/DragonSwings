using UnityEngine;

public class SkipTheFun : MonoBehaviour
{
    public Transform _TargetObject;

    private System.Collections.Generic.List<Vector2> _TargetPositions = new System.Collections.Generic.List<Vector2>();

    public Transform[] _SkipPositions;

    public void Awake()
    {
        _TargetPositions.Add(_TargetObject.position);
        for (int i = 0; i < _SkipPositions.Length; i++)
        {
            _TargetPositions.Add(_SkipPositions[i].position);
        }
    }

    public void TeleportToPosition(int positionIndex)
    {
        Debug.Log(positionIndex);
        _TargetObject.transform.position = _TargetPositions[positionIndex];
    }
}