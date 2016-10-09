using UnityEngine;
using System.Collections;

public class StandingState : MonoBehaviour {

    public LayerMask collisionLayer;
    public bool standing;

    public float bottomOffset = 0f;
    public float bottomRadius = 0.1f;

    private Cylindrical cyln;


    private void Awake(){
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
    }
}