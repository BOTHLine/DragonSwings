using UnityEngine;

[System.Serializable]
public struct Vector2Complex
{
    [SerializeField] private Vector2 _Vector;
    public Vector2 Vector
    {
        get { return _Vector; }
        set
        {
            _Vector = value;
            CalculateEndPoint();
            CalculateDirection();
            CalculateMagnitude();
            //    CalculateSquaredMagnitude();
        }
    }

    [SerializeField] private Vector2 _StartPoint;
    public Vector2 StartPoint
    {
        get { return _StartPoint; }
        set
        {
            _StartPoint = value;
            CalculateEndPoint();
            CalculateDirection();
            CalculateMagnitude();
            //    CalculateSquaredMagnitude();
        }
    }

    [SerializeField] private Vector2 _EndPoint;
    public Vector2 EndPoint
    {
        get { return _EndPoint; }
        set
        {
            _EndPoint = value;
            CalculateVectorByPoints();
            CalculateDirection();
            CalculateMagnitude();
            //    CalculateSquaredMagnitude();
        }

    }

    [SerializeField] private Vector2 _Direction;
    public Vector2 Direction
    {
        get { return _Direction; }
        set
        {
            _Direction = value.normalized;
            CalculateVectorByValues();
            CalculateEndPoint();
        }
    }

    [SerializeField] private float _Magnitude;
    public float Magnitude
    {
        get { return _Magnitude; }
        set
        {
            _Magnitude = value;
            CalculateVectorByValues();
            CalculateEndPoint();
            //   CalculateSquaredMagnitude();
        }
    }

    /*
    [SerializeField] private float _SquaredMagnitude;
    public float SquaredMagnitude
    {
        get { return _SquaredMagnitude; }
        set
        {
            _SquaredMagnitude = value;
            _Magnitude = Mathf.Sqrt(_SquaredMagnitude);
            CalculateVectorByValues();
            CalculateEndPoint();
        }
    }
    */

    public Vector2Complex(Vector2Complex original)
    {
        _StartPoint = original.StartPoint;
        _EndPoint = original.EndPoint;

        _Vector = original.Vector;

        _Direction = original.Direction;
        _Magnitude = original.Magnitude;
    }

    public Vector2Complex(Vector2 startPoint, Vector2 endPoint)
    {
        _StartPoint = startPoint;
        _EndPoint = endPoint;

        _Vector = CalculateVector(startPoint, endPoint);

        _Direction = CalculateDirection(_Vector);
        _Magnitude = CalculateMagnitude(_Vector);
        //    _SquaredMagnitude = CalculateSquaredMagnitude(_Vector);
    }

    public Vector2Complex(Vector2 endPoint)
        : this(Vector2.zero, endPoint) { }

    private void CalculateVectorByPoints()
    { _Vector = CalculateVector(_StartPoint, _EndPoint); }

    private void CalculateVectorByValues()
    { _Vector = CalculateVector(_Direction, _Magnitude); }

    private void CalculateEndPoint()
    { _EndPoint = CalculateEndPoint(_StartPoint, _Vector); }

    private void CalculateDirection()
    { _Direction = CalculateDirection(_Vector); }

    private void CalculateMagnitude()
    { _Magnitude = CalculateMagnitude(_Vector); }

    /*
    private void CalculateSquaredMagnitude()
    { _SquaredMagnitude = CalculateSquaredMagnitude(_Vector); }
    */

    private static Vector2 CalculateVector(Vector2 startPoint, Vector2 endPoint)
    { return (endPoint - startPoint); }

    private static Vector2 CalculateVector(Vector2 direction, float magnitude)
    { return (direction * magnitude); }

    private static Vector2 CalculateEndPoint(Vector2 startPoint, Vector2 vector)
    { return (startPoint + vector); }

    private static Vector2 CalculateDirection(Vector2 vector)
    { return vector.normalized; }

    private static float CalculateMagnitude(Vector2 vector)
    { return vector.magnitude; }

    private static float CalculateSquaredMagnitude(Vector2 vector)
    { return vector.sqrMagnitude; }
}