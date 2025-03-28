using UnityEngine;

public class PianoManager : MonoBehaviour
{
    public Transform PianoTransform;
    public int KeyCount = 88;
    public float KeySpacing = 0.05f;

    [Header("Endpoint Ellipse")] 
    public float EllipseWidth = 3f;
    public float EllipseDepth = .5f;
    public Vector3 EllipseOffset = new Vector3(0f, 0f, 0f);
    
    public Vector3[] KeyPositions { get; private set; }
    public Vector3[] EndPositions { get; private set; }

    void Start()
    {
        KeyPositions = new Vector3[KeyCount];
        EndPositions = new Vector3[KeyCount];
        
        SetupKeys();
        SetupEndPoints();
    }

    private void SetupKeys()
    {
        var leftPos = PianoTransform.position;
        leftPos -= new Vector3((KeyCount - 1) / 2f * KeySpacing, 0, 0);
        
        for (int i = 0; i < KeyCount; i++)
        {
            var pos = leftPos + new Vector3(i * KeySpacing, 0, 0);
            KeyPositions[i] = pos;
            
            //Debug.Log("i = " + i + " , KeyPosition = " + pos);
        }
    }

    private void SetupEndPoints()
    {
        var ellipseCenter = PianoTransform.position + EllipseOffset;

        for (int i = 0; i < KeyCount; i++)
        {
            float t = (float)i / (KeyCount - 1);
            float angle = Mathf.Lerp(0, Mathf.PI, 1f-t);

            float x = ellipseCenter.x + Mathf.Cos(angle) * EllipseWidth;
            float y = ellipseCenter.y;
            float z = ellipseCenter.z + Mathf.Sin(angle) * EllipseDepth;
            
            EndPositions[i] = new Vector3(x, y, z);
        }
    }
}
