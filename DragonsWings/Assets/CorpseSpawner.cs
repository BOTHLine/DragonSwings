using UnityEngine;

public class CorpseSpawner : MonoBehaviour
{
    public GameObject _PrefabCorpse;

    public void SpawnCorpse()
    {
        Instantiate(_PrefabCorpse, transform.position, Quaternion.identity, transform.parent.parent);
    }
}