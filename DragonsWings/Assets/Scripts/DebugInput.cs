using UnityEngine;

public class DebugInput : MonoBehaviour
{
    // Variables
    public GameObject[] _SkipPoints;

    // Events
    public GameEvent _OnInputHookReset;
    public GameEvent _OnInputLevelReset;

    public GameEventMap _OnInputSkipPointTelport;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) { _OnInputHookReset.Raise(); }

        if (Input.GetKeyDown(KeyCode.L)) { _OnInputLevelReset.Raise(); }

        for (int i = 0; i < _SkipPoints.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            { _OnInputSkipPointTelport.Raise(_SkipPoints[i]); }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
    }

    public void ResetLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}