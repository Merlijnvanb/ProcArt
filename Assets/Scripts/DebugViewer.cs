using UnityEngine;

public class DebugViewer : MonoBehaviour
{
    public bool Enabled;
    public PianoManager PianoManager;
    public SplineManager SplineManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled)
            return;
        
        DrawKeysEndPoints();
    }

    private void DrawKeysEndPoints()
    {
        for (int i = 0; i < PianoManager.KeyCount; i++)
        {
            Debug.DrawLine(
                PianoManager.KeyPositions[i],
                PianoManager.EndPositions[i],
                Color.white);
        }
    }
}
