using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public int animState;

    private InputState inputState;
    private Animator animator;
    private StandingState standingState;

    private void Awake(){
        inputState = GetComponent<InputState>();
        // animator = GetComponent<Animator>();
        standingState = GetComponent<StandingState>();

    }

	// Update is called once per frame
	private void Update () {
        if (standingState.standing){
            animState = 0;
        }

        if (inputState.absVelT > 0){
            animState = 1;
        }

        if (inputState.absVelT > 0){
            animState = 2;
        }

        ChangeAnimationState(animState);
	}

    private void ChangeAnimationState(int value){
        // animator.SetInteger("AnimState", value);
    }

}
