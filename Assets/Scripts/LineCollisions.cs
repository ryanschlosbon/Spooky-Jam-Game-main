using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]

public class LineCollisions : MonoBehaviour
{

    public EdgeCollider2D edgeCollider;
    public LineRenderer line;

    void Start()
    {
        edgeCollider = this.GetComponent<EdgeCollider2D>();
        line = this.GetComponent<LineRenderer>();
        
    }

    void Update()
    {
        SetEdgeCollider(line);
    }

    void SetEdgeCollider(LineRenderer lineRenderer)
    {
        List<Vector2> edges = new List<Vector2>();
        for (int point = 0; point < lineRenderer.positionCount; point++)
        {
            Vector3 lineRendererPoint = lineRenderer.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }

        edgeCollider.SetPoints(edges);
    }

}
