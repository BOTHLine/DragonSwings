using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerScript : MonoBehaviour
{
    public bool _TagsAsBlackList;
    public string[] _Tags;

    public LayerMask _LayerMask;

    public UnityEvent _OnEnterActions;
    public UnityEvent _OnStayActions;
    public UnityEvent _OnExitActions;

    private void OnTriggerEnter(Collider other)
    {
        if (!HasValidTag(other.tag)) return;
        if (!HasValidLayer(other.gameObject.layer)) return;

        _OnEnterActions.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!HasValidTag(other.tag)) return;
        if (!HasValidLayer(other.gameObject.layer)) return;

        _OnStayActions.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!HasValidTag(other.tag)) return;
        if (!HasValidLayer(other.gameObject.layer)) return;

        _OnExitActions.Invoke();
    }

    private bool HasValidTag(string tag)
    {
        if (_TagsAsBlackList)
        {
            foreach (string currentTag in _Tags)
            {
                if (tag.Equals(currentTag))
                { return false; }
            }
            return true;
        }
        else
        {
            bool hasValidTag = false;
            foreach (string currentTag in _Tags)
            {
                if (tag.Equals(currentTag))
                {
                    hasValidTag = true;
                    break;
                }
            }
            return hasValidTag;
        }
    }

    private bool HasValidLayer(int layer)
    {
        return _LayerMask == (_LayerMask | (1 << layer));
    }
}