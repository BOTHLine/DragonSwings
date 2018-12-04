using UnityEngine;

public static class Utils
{
    public static Quaternion GetLookAtRotation(Vector2 direction, float rotation = 0.0f)
    {
        float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0.0f, 0.0f, zRotation + rotation);
    }

    public static Quaternion GetLookAtRotation(Vector2 fromPosition, Vector2 toPosition, float rotation = 0.0f)
    { return GetLookAtRotation(toPosition - fromPosition, rotation); }

    public static Vector2 CalculatePositionOnParabola(Vector2 start, Vector2 end, float height, float time)
    {
        float parabolicT = time * 2 - 1;
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector2 travelDirection = end - start;
            Vector2 result = start + time * travelDirection;
            result.y += (-parabolicT * parabolicT + 1) * height;
            return result;
        }
        else
        {
            //start and end are not level, gets more complicated
            Vector2 travelDirection = end - start;
            Vector2 levelDirecteion = end - new Vector2(start.x, end.y);
            Vector2 up = new Vector2(0.0f, 1.0f);
            //if (end.y > start.y) up = -up;
            Vector2 result = start + time * travelDirection;
            result += ((-parabolicT * parabolicT + 1) * height) * up;
            return result;
        }
    }
}