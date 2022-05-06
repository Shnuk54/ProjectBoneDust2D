using System.Collections.Generic;
using UnityEngine;

public class DrawTrajecroty : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] [Range(3, 30)] private int _lineSegmentCount = 20;

    private List<Vector3> _linePoints = new List<Vector3>();

    #region Singleton
    public static DrawTrajecroty singleton;

    private void Awake()
    {
         if(!singleton){
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    #endregion
    private void Start()
    {
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
        DontDestroyOnLoad(gameObject);
    }
    public void UpdateTrajectory(Vector2 forceVector, Rigidbody2D rigidbody, Vector3 startingPoint)
    {
        if(_lineRenderer == null){
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        Vector3 velocity = (forceVector / rigidbody.mass) * Time.fixedDeltaTime;


        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;

        if (velocity.y < 0)
        {
            //Типа пофиксил
            FlightDuration = -1;
        }

        float stepTime = FlightDuration / _lineSegmentCount;
        _linePoints.Clear();

        for (int i = 0; i < _lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;
            Vector3 MovementVector = new Vector2(
                velocity.x * stepTimePassed,
               (velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed)
            );
            _linePoints.Add(-MovementVector + startingPoint);
        }

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }
}
