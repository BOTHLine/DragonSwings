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
    // 3
    public static readonly Layer Water = new Layer(4, "Water");
    public static readonly Layer UI = new Layer(5, "UI");
    // 6
    // 7
    public static readonly Layer Wall = new Layer(8, "Wall");
    public static readonly Layer Object = new Layer(9, "Object");
    public static readonly Layer Pit = new Layer(10, "Pit");
    public static readonly Layer FallCheck = new Layer(11, "Fall Check");
    // 12
    public static readonly Layer PlayerLow = new Layer(13, "Player Low");
    public static readonly Layer PlayerHigh = new Layer(14, "Player High");
    public static readonly Layer PlayerHurtBox = new Layer(15, "Player Hurt Box");
    public static readonly Layer PlayerAttack = new Layer(16, "Player Attack");
    public static readonly Layer PlayerProjectile = new Layer(17, "Player Projectile");
    // 18
    // 19
    // 20
    // 21
    // 22
    public static readonly Layer EnemyLow = new Layer(13, "Enemy Low");
    public static readonly Layer EnemyHigh = new Layer(14, "Enemy High");
    public static readonly Layer EnemyHurtBox = new Layer(15, "Enemy Hurt Box");
    public static readonly Layer EnemyAttack = new Layer(16, "Enemy Attack");
    public static readonly Layer EnemyProjectile = new Layer(17, "Enemy Projectile");
    // 28
    // 29
    // 30
    public static readonly Layer Minimap = new Layer(25, "Minimap");

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