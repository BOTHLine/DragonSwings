[System.Serializable
    ]
public class Transition
{
    public Decision[] decisions;
    public State trueState;
    public State falseState;
}