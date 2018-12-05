using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;
using System;

[RequireComponent(typeof(LineRenderer))]
public class BallTrajectory : MonoBehaviour
{

    public event Action<Vector3> OnBallFire = delegate {};
    private LineRenderer _lineRenderer;
    [SerializeField] private List<Vector3> linePoints = new List<Vector3>();

    public float threshold = 0.001f;
    private int lineCount = 0;

    private Vector3 _startPos;
    public Vector3 startPos
    {
        set
        {
            linePoints.Clear();
            linePoints.Add(value);
            _startPos = value;
        }

    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        Assert.IsNotNull(_lineRenderer, "Error: line renderer is null");
    }

    private Vector3 MousePosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;

    }

    private void ResetLine()
    {
        linePoints = new List<Vector3>();
        linePoints.Add(_startPos);
        _lineRenderer.positionCount = 1;
    }
    private void Update()
    {

        // Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            linePoints.Add(MousePosition());
        }
        else if (Input.GetMouseButton(0))
        {
            lineCount = 0;
            linePoints[1] = MousePosition();

        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnBallFire(linePoints[1]);
            ResetLine();
        }

        UpdateLine();

    }

    void UpdateLine()
    {
        _lineRenderer.positionCount = linePoints.Count;

        for (int i = lineCount; i < linePoints.Count; i++)
        {
            _lineRenderer.SetPosition(i, linePoints[i]);
        }
        lineCount = linePoints.Count;
    }


}
