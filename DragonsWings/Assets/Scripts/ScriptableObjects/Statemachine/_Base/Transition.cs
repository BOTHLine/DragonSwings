using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Statemachine/Transition")]
public class Transition : ScriptableObject
{
    [SerializeField] private Decision[] _Decisions;
    [SerializeField] private State _TrueState;
    [SerializeField] private State _FalseState;
    // [SerializeField] private Action[] _Actions; -> Not real Actions with Enter/Exit State, just simple Commands

    public void EnterState(StateController controller)
    {
        foreach (Decision decision in _Decisions)
        {
            decision.EnterState(controller);
        }
    }

    public State GetTargetState(StateController controller)
    {
        for (int i = 0; i < _Decisions.Length; i++)
        {
            if (!_Decisions[i].Decide(controller))
                return _FalseState;
        }
        return _TrueState;
    }

    public void ExitState(StateController controller)
    {
        foreach (Decision decision in _Decisions)
        {
            decision.ExitState(controller);
        }
    }
}