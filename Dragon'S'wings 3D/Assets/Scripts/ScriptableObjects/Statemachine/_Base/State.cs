using UnityEngine;

[CreateAssetMenu(menuName = "Statemachine/State")]
[System.Serializable]
public class State : ScriptableObject
{
    public Layer _Layer;

    public Behaviour[] _Behaviours;

    public void DoActions(StateController controller)
    {
        foreach (Behaviour behaviour in _Behaviours)
        { behaviour.Act(controller); }
    }

    public void EnterState(StateController controller)
    {
        PushBox pushBox = controller.GetComponentInChildren<PushBox>();
        if (pushBox != null) { pushBox.gameObject.layer = (int)_Layer; };

        foreach (Behaviour behaviour in _Behaviours)
        { behaviour.EnterState(controller); }
    }

    public void ExitState(StateController controller)
    {
        foreach (Behaviour behaviour in _Behaviours)
        { behaviour.ExitState(controller); }
    }
}