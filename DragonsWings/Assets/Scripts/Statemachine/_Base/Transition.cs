using UnityEngine;

[System.Serializable]
public class Transition
{
    [SerializeField] private Decision[] decisions;
    [SerializeField] private State trueState;
    [SerializeField] private State falseState;

    //    [SerializeField] private bool[] invertDecisions;

    public State GetTargetState(StateController controller)
    {
        for (int i = 0; i < decisions.Length; i++)
        {
            if (!decisions[i].Decide(controller))
                return falseState;
        }
        return trueState;
    }

    public void EnterState(StateController controller)
    {
        foreach (Decision decision in decisions)
        {
            decision.EnterState(controller);
        }
    }

    public void ExitState(StateController controller)
    {
        foreach (Decision decision in decisions)
        {
            decision.ExitState(controller);
        }
    }
}