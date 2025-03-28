using UnityEngine;
using UnityEngine.Splines;

public class VisualizerUnit : MonoBehaviour
{
    [System.Serializable]
    public struct VisualData
    {
        public Material TrailMaterial;
        public Color LowerColor;
        public Color UpperColor;
    }

    public VisualData VisData;
    
    public VisualizerManager Manager { get; private set; }
    public SplineContainer SplineContainer { get; private set; }
    public int Number { get; private set; }
    public float Velocity { get; private set; }

    private SplineAnimate animator;
    private float progress = 0f;
    private float friction = 0.9995f;
    private float accelerationFactor = 0.0015f;
    private float minVelocityThreshold = 0.001f;

    private float targetVelocity;
    private bool reachedEnd = false;

    public void InitializeUnit(VisualizerManager manager, SplineContainer splineContainer, NoteData noteData)
    {
        Manager = manager;
        SplineContainer = splineContainer;
        Number = noteData.Number;
        targetVelocity = noteData.Velocity / 10f;
        Velocity = targetVelocity;
        
        SetupVisuals(noteData);

        SetupAnimator();
        animator.Play();
    }

    public void StoppedPress()
    {
        targetVelocity = 1.5f;
    }

    private void SetupVisuals(NoteData noteData)
    {
        var pointLight = noteData.GO.GetComponentInChildren<Light>();
        var trail = noteData.GO.GetComponentInChildren<TrailRenderer>();

        trail.material = VisData.TrailMaterial;

        var t = noteData.Number / ((float)noteData.TotalNotes - 1);
        var color = Color.Lerp(VisData.LowerColor, VisData.UpperColor, t);
        
        trail.material.SetColor("_Emission", color);
        pointLight.color = color;
    }

    private void SetupAnimator()
    {
        animator = GetComponent<SplineAnimate>();
        animator.Container = SplineContainer;
        animator.NormalizedTime = 0;
    }

    void Update()
    {
        ApplyFrictionAndAcceleration();
    }

    private void ApplyFrictionAndAcceleration()
    {
        if (reachedEnd)
            return;
        
        if (Velocity > minVelocityThreshold || targetVelocity > 0)
        {
            progress += Velocity * Time.deltaTime;

            if (Velocity < targetVelocity)
            {
                Velocity += (targetVelocity - Velocity) * accelerationFactor;
            }
            else
            {
                Velocity *= friction;
            }
        }
        else
        {
            Velocity = 0;
        }

        if (progress >= 1)
        {
            Manager.DestroyUnit(Number);
            reachedEnd = true;
            progress = 1f;
        }
        
        animator.NormalizedTime = progress;
    }
}
