using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonState{
    public bool value;
    public float holdTime = 0;
}

public enum Directions{
    Right = 1,
    Left = -1
}

public class InputState : MonoBehaviour {

    public Directions direction = Directions.Right;
    public float absVelR = 0f;
    public float absVelT = 0f;

    private Cylindrical cyln;
    private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();

    private void Awake(){
        cyln = GetComponent<Cylindrical>();
    }

    private void FixedUpdate(){
        absVelR = Mathf.Abs(cyln.velocity_r);
        absVelT = Mathf.Abs(cyln.velocity_t);
    }

    public void SetButtonValue(Buttons key, bool value){
        if(!buttonStates.ContainsKey(key))
            buttonStates.Add(key, new ButtonState());

        var state = buttonStates[key];

        if(state.value && !value){
            state.holdTime = 0;
        } else if(state.value && value){
            state.holdTime += Time.deltaTime;
        }

        state.value = value;
    }

    public bool GetButtonValue(Buttons key){
        return buttonStates.ContainsKey(key)? buttonStates[key].value : false;
    }

    public float GetButtonHoldTime(Buttons key){
        return buttonStates.ContainsKey(key)? buttonStates[key].holdTime : 0;
    }
}
