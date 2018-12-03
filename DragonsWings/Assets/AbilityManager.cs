using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private System.Collections.Generic.List<GameObject> _HookAbilityGameObjects;
    [SerializeField] private System.Collections.Generic.List<GameObject> _ThrowAbilityGameObjects;

    public void EnterHookAbilityMode()
    {
        DisableGameObjectList(_ThrowAbilityGameObjects);
        EnableGameObjectList(_HookAbilityGameObjects);
    }

    public void EnterThrowAbilityMode()
    {
        DisableGameObjectList(_HookAbilityGameObjects);
        EnableGameObjectList(_ThrowAbilityGameObjects);
    }

    public void DisableAllAbilities()
    {
        DisableGameObjectList(_HookAbilityGameObjects);
        DisableGameObjectList(_ThrowAbilityGameObjects);
    }

    private void DisableGameObjectList(System.Collections.Generic.List<GameObject> list)
    {
        foreach (GameObject gameObject in list)
        { gameObject.SetActive(false); }
    }

    private void EnableGameObjectList(System.Collections.Generic.List<GameObject> list)
    {
        foreach (GameObject gameObject in list)
        { gameObject.SetActive(true); }
    }
}