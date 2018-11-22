using UnityEngine;

public static class Utils
{
    public static Quaternion GetLookAtRotation(Vector2 direction, float rotation = 0.0f)
    {
        float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0.0f, 0.0f, zRotation + rotation);
    }

    public static Quaternion GetLookAtRotation(Vector2 startPosition, Vector2 targetPosition, float rotation = 0.0f)
    {
        return GetLookAtRotation((targetPosition - startPosition).normalized, rotation);
    }

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

    public static T GetComponentInSiblings<T>(this Component component) where T : Component
    {
        Transform parent = component.transform.parent;
        if (parent != null)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                T output = parent.GetChild(i).GetComponent<T>();
                if (output != null)
                    return output;
            }
        }
        return null;
    }

    public static T[] GetComponentsInSiblings<T>(this Component component) where T : Component
    {
        System.Collections.Generic.List<T> componentList = new System.Collections.Generic.List<T>();
        Transform parent = component.transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            T output = parent.GetChild(i).GetComponent<T>();
            if (output != null)
                componentList.Add(output);
        }
        T[] componentArray = new T[componentList.Count];
        componentList.CopyTo(componentArray);
        return componentArray;
    }
}