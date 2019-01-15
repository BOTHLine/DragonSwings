using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerArea : MonoBehaviour
{
    public string[] tags;

    public UnityEngine.Events.UnityEvent OnTriggerEnter;
    public UnityEngine.Events.UnityEvent OnTriggerStay;
    public UnityEngine.Events.UnityEvent OnTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter.Invoke();
        return;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.tag == tags[i])
            {
                OnTriggerStay.Invoke();
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.tag == tags[i])
            {
                OnTriggerExit.Invoke();
                return;
            }
        }
    }
}