using Minis;
using UnityEngine;

public class MidiDebugInput : MonoBehaviour
{
    public VisualizerManager VisualizerManager;
    public PianoManager PianoManager;
    
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                if (i == 0)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                        SimulateNoteDown(PianoManager.KeyCount - 9);
                    else    
                        SimulateNoteDown(9);
                    continue;
                }
                
                if (Input.GetKey(KeyCode.LeftShift))
                    SimulateNoteDown(PianoManager.KeyCount - i);
                else    
                    SimulateNoteDown(i-1);
            }
        }
        
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyUp(i.ToString()))
            {
                if (i == 0)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                        SimulateNoteUp(PianoManager.KeyCount - 9);
                    else    
                        SimulateNoteUp(9);
                    continue;
                }
                
                if (Input.GetKey(KeyCode.LeftShift))
                    SimulateNoteUp(PianoManager.KeyCount - i);
                else    
                    SimulateNoteUp(i-1);
            }
        }
    }

    private void SimulateNoteDown(int number)
    {
        VisualizerManager.StartUnit(number, 1f);
        Debug.Log("Note index " + number + " down.");
    }

    private void SimulateNoteUp(int number)
    {
        VisualizerManager.StopUnit(number);
        Debug.Log("Note index " + number + " up.");
    }
}
