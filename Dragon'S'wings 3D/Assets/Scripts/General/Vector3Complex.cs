using UnityEngine;

[System.Serializable]
public struct Vector3Complex
{
    [SerializeField] private Vector3 _Vector;
    public Vector3 Vector
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

    [SerializeField] private Vector3 _StartPoint;
    public Vector3 StartPoint
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

    [SerializeField] private Vector3 _EndPoint;
    public Vector3 EndPoint
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

    [SerializeField] private Vector3 _Direction;
    public Vector3 Direction
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

    public Vector3Complex(Vector3Complex original)
    {
        _StartPoint = original.StartPoint;
        _EndPoint = original.EndPoint;

        _Vector = original.Vector;

        _Direction = original.Direction;
        _Magnitude = original.Magnitude;
    }

    public Vector3Complex(Vector3 startPoint, Vector3 vector)
    {
        _StartPoint = startPoint;
        _Vector = vector;

        _EndPoint = CalculateEndPoint(startPoint, vector);

        _Direction = CalculateDirection(_Vector);
        _Magnitude = CalculateMagnitude(_Vector);
        //    _SquaredMagnitude = CalculateSquaredMagnitude(_Vector);
    }

    public Vector3Complex(Vector3 vector)
        : this(Vector3.zero, vector) { }

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

    private static Vector3 CalculateVector(Vector3 startPoint, Vector3 endPoint)
    { return (endPoint - startPoint); }

    private static Vector3 CalculateVector(Vector3 direction, float magnitude)
    { return (direction * magnitude); }

    private static Vector3 CalculateEndPoint(Vector3 startPoint, Vector3 vector)
    { return (startPoint + vector); }

    private static Vector3 CalculateDirection(Vector3 vector)
    { return vector.normalized; }

    private static float CalculateMagnitude(Vector3 vector)
    { return vector.magnitude; }

    private static float CalculateSquaredMagnitude(Vector3 vector)
    { return vector.sqrMagnitude; }

    public override string ToString()
    {
        return
            "Startpoint: " + _StartPoint + "\n" +
            "Endpoint: " + _EndPoint + "\n" +
            "Vector: " + _Vector + "\n" +
            "Direction: " + _Direction + "\n" +
            "Magnitude: " + _Magnitude;
    }
}