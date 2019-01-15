using UnityEngine;

public static class ComponentExtensions
{
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