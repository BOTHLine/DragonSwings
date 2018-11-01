using UnityEngine;

public static class LayerList
{
    public class Layer
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int LayerMask { get; private set; }

        public Layer(int id, string name)
        {
            ID = id;
            Name = name;
            LayerMask = CreateLayerMask(id);
        }
    }

    public static readonly Layer Default = new Layer(0, "Default");
    public static readonly Layer TransparentFX = new Layer(1, "TransparentFX");
    public static readonly Layer IgnoreRaycast = new Layer(2, "Ignore Raycast");
    public static readonly Layer Water = new Layer(4, "Water");
    public static readonly Layer UI = new Layer(5, "UI");

    public static readonly Layer Background = new Layer(8, "Background");
    public static readonly Layer Ground = new Layer(9, "Ground");
    public static readonly Layer Foreground = new Layer(11, "Foreground");
    public static readonly Layer Objects = new Layer(13, "Objects");
    public static readonly Layer PlayerLow = new Layer(15, "Player Low");
    public static readonly Layer PlayerHigh = new Layer(16, "Player High");
    public static readonly Layer EnemyLow = new Layer(18, "Enemy Low");
    public static readonly Layer EnemyHigh = new Layer(19, "Enemy High");
    public static readonly Layer Hook = new Layer(21, "Hook");

    public static readonly Layer Pit = new Layer(30, "Pit");
    public static readonly Layer FallCheck = new Layer(31, "FallCheck");

    public static int CreateLayerMask(int layer)
    {
        int layermask = 0;
        for (int i = 0; i < 32; i++)
        {
            if (!Physics2D.GetIgnoreLayerCollision(layer, i))
            {
                layermask = layermask | 1 << i;
            }
        }
        return layermask;
    }
}