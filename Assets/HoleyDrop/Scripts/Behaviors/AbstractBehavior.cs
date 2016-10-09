using UnityEngine;
using System.Collections;

public abstract class AbstractBehavior : MonoBehaviour {

    public Buttons[] inputButtons;
    public MonoBehaviour[] disableScripts;

    protected InputState inputState;
    protected Cylindrical cyln;

    protected virtual void Awake(){
        inputState = GetComponent<InputState>() ;
        cyln = GetComponent<Cylindrical>() ;
    }

    protected virtual void ToggleScripts(bool value){
        foreach(var script in disableScripts) {
            script.enabled = value;
        }
    }
}
