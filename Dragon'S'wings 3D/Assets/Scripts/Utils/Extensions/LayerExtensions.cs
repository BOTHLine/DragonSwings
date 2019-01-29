using UnityEngine;

public static class LayerExtensions
{
    public static int GetLayerMask(this Layer layer)
    {
        int layerMask = 0;
        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision((int)layer, i))
            {
                layerMask = layerMask | 1 << i;
            }
        }
        return layerMask;
    }
}