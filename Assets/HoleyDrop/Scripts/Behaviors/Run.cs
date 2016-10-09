using UnityEngine;
using System.Collections;

public class Run : AbstractBehavior {

    public bool canRun = true;
    public float currentSpeed = 0f;
    public float maxSpeed = 40f;
    public float acceleration = 5f;
    public float deceleration = 5f;

    private Directions previousDirection;

    private void FixedUpdate() {

        var right = inputState.GetButtonValue(inputButtons[0]);
        var left = inputState.GetButtonValue(inputButtons[1]);

        var direction = (float)inputState.direction;

        if(left || right){
            if(Mathf.Abs(currentSpeed) < maxSpeed){
                currentSpeed = currentSpeed + (direction * acceleration);
            }

            if(previousDirection != inputState.direction) {
                currentSpeed *= -1;
            }

        }else{
            if(Mathf.Abs(currentSpeed) - (direction * deceleration) < 0){
                currentSpeed = 0;
            }else if(Mathf.Abs(currentSpeed) > 0){
                currentSpeed -= (direction * deceleration);
            }
        }

        if(Mathf.Abs(currentSpeed) > maxSpeed){
            currentSpeed = direction * maxSpeed;
        }

        previousDirection = inputState.direction;

        cyln.velocity_t = currentSpeed;
    }

}