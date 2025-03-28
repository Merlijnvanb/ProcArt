using UnityEngine.Splines;
using UnityEngine;
using System.Collections.Generic;

public class SplineManager : MonoBehaviour
{
    public PianoManager PianoManager;
    public int SplineResolution = 5;
    public Vector3 RandomEndValue = new Vector3(0, 0, 0);

    public SplineContainer InitContainer(int keyNum)
    {
        var points = GetSplinePoints(keyNum);
        var spline = CreateSpline(points);
        
        var containerGO = new GameObject("Container" + keyNum);
        containerGO.transform.SetParent(gameObject.transform);
        
        var container = containerGO.AddComponent<SplineContainer>();
        container.RemoveSplineAt(0);
        container.AddSpline(spline);
        //Extruder.Rebuild();
        
        return container;
    }

    private Vector3[] GetSplinePoints(int keyNum)
    {
        var startPos = PianoManager.KeyPositions[keyNum];
        var endPoint = PianoManager.EndPositions[keyNum] + Vector3.Scale(Random.insideUnitSphere, RandomEndValue);
        
        var points = new Vector3[SplineResolution];
        
        var currentPos = startPos;
        points[0] = currentPos;

        for (int i = 1; i < SplineResolution; i++)
        {
            var randomX = Random.Range(-1.5f, 1.5f);
            var randomY = Random.Range(3.5f, 5.5f);
            var randomZ = Random.Range(-1.5f, 1.5f);
            var t = (float)i / (SplineResolution - 1);
            
            var newPos = currentPos + new Vector3(randomX, randomY, randomZ);
            currentPos = Vector3.Lerp(newPos, endPoint, t*t);
            points[i] = currentPos;
        }
        
        return points;
    }

    private Spline CreateSpline(Vector3[] points)
    {
        var spline = new Spline();

        for (int i = 0; i < points.Length; i++)
        {
            var knot = new BezierKnot();
            knot.Position = points[i];
            spline.Add(knot);
        }
        spline.SetTangentMode(TangentMode.AutoSmooth);
        
        return spline;
    }
}
