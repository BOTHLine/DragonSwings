using UnityEngine;

public static class GameObjectExtensions
{
    public static string GetFullName(this GameObject gameObject)
    {
        Transform transform = gameObject.transform;
        string output = transform.name;
        transform = transform.parent;
        while (transform != null)
        {
            output = transform.name + "." + output;
            transform = transform.parent;
        }
        return output;
    }
}