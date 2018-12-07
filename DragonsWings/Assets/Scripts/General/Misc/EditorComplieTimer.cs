#if UNITY_EDITOR
using UnityEngine;

[ExecuteInEditMode]
public class EditorComplieTimer : MonoBehaviour
{
    private double compileStartTime;
    private bool isCompiling = false;

    private void Update()
    {
        if (isCompiling)
        {
            if (!UnityEditor.EditorApplication.isCompiling)
            { CompileFinished(); }
        }
        else
        {
            if (UnityEditor.EditorApplication.isCompiling)
            { CompileStarted(); }
        }
    }

    private void CompileStarted()
    {
        isCompiling = true;
        Debug.Log("Compile started...");
        compileStartTime = UnityEditor.EditorApplication.timeSinceStartup;
    }

    private void CompileFinished()
    {
        isCompiling = false;
        double compileTime = UnityEditor.EditorApplication.timeSinceStartup - compileStartTime;
        Debug.Log("Compile Finished: " + compileTime.ToString("F2") + "s");
    }
}
#endif