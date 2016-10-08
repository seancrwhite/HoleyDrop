using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public int animState;

    private InputState inputState;
    private Animator animator;
    private CollisionState collisionState;

    private void Awake(){
        inputState = GetComponent<InputState>();
        // animator = GetComponent<Animator>();
        collisionState = GetComponent<CollisionState>();

    }

	// Update is called once per frame
	private void Update () {
        if (collisionState.standing){
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
