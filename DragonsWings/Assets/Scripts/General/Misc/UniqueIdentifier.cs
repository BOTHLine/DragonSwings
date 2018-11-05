public static class UniqueIdentifier
{
    private static System.Collections.Generic.Dictionary<System.Type, int> identifier = new System.Collections.Generic.Dictionary<System.Type, int>();

    public static int GetUniqueIdentifier(System.Type type)
    {
        if (!identifier.ContainsKey(type))
        {
            identifier.Add(type, 0);
        }
        return identifier[type]++;
    }
}