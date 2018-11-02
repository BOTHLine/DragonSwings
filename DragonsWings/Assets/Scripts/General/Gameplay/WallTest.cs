using UnityEngine;

public class WallTest : MonoBehaviour, Hookable
{
    public Weight Weight { get { return Weight.Heavy; } }

    public void OnHookHit() { }
}