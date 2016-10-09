using UnityEngine;
using System.Collections;

public class CollisionState : MonoBehaviour {

    public LayerMask collisionLayer;
    public bool standing;
    public bool fisting;

    public float bottomOffset = 0f;
    public float bottomRadius = 0.1f;

    public float middleOffset = -0.11f;
    public float middleRadius = 0.6f;

    private InputState inputState;
    private Cylindrical cyln;


    private void Awake(){
        inputState = GetComponent<InputState>();
        cyln = GetComponent<Cylindrical>();
    }

    private void FixedUpdate(){
        standing = Physics2D.OverlapCircle(
            Cylindrical.ToCartesian(
                new Vector2(cyln.theta, cyln.radius + bottomOffset)
            ),
            bottomRadius,
            collisionLayer
        );

        fisting = Physics2D.OverlapCircle(
            Cylindrical.ToCartesian(
                new Vector2(cyln.theta, cyln.radius + middleOffset)
            ),
            middleRadius,
            collisionLayer
        );

    }

}