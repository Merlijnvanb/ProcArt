using UnityEngine;
using System.Collections.Generic;

public struct NoteData
{
    public int Number;
    public float Velocity;
    public int TotalNotes;
    public GameObject GO;
}

public class VisualizerManager : MonoBehaviour
{
    public GameObject VisualizerPrefab;
    public SplineManager SplineManager;
    public PianoManager PianoManager;
    
    private List<VisualizerUnit> visualizerUnits = new List<VisualizerUnit>();

    public void StartUnit(int number, float velocity)
    {
        var splineContainer = SplineManager.InitContainer(number);
        var unitInstance = Instantiate(VisualizerPrefab, gameObject.transform);
        var unitScript = unitInstance.GetComponent<VisualizerUnit>();

        var data = new NoteData()
        {
            Number = number,
            Velocity = velocity,
            TotalNotes = PianoManager.KeyCount,
            GO = unitInstance
        };
        
        unitScript.InitializeUnit(this, splineContainer, data);
        
        visualizerUnits.Add(unitScript);
    }

    public void StopUnit(int number)
    {
        for (int i = 0; i < visualizerUnits.Count; i++)
        {
            var unit = visualizerUnits[i];
            if (unit.Number == number)
            {
                unit.StoppedPress();
            }
        }
    }

    public void DestroyUnit(int number)
    {
        for (int i = 0; i < visualizerUnits.Count; i++)
        {
            var unit = visualizerUnits[i];
            if (unit.Number == number)
            {
                visualizerUnits.Remove(unit);
                Destroy(unit.SplineContainer.gameObject, 3f);
                Destroy(unit.gameObject, 3f);
                return;
            }
        }
    }
}
