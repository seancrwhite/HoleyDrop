using UnityEngine;
using System.Collections;

public class Cylindrical : MonoBehaviour {
    public float radius = 5f;
    public float theta = 0f;
    public float velocity_r = 0f;
    public float velocity_t = 0f;
    public float drag = 0.05f;
    public float gravity = 5f;
    public static float rotationOffset = 0f;

    private float normalizer = 45f;
    private CollisionState collisionState;

    private void Awake(){
        collisionState = GetComponent<CollisionState>();
    }

    private void FixedUpdate(){
        var absVelT = Mathf.Abs(velocity_t);


        if(absVelT < 0.1f) velocity_t = 0f;
        else velocity_t -= velocity_t * drag;

        if(radius > 0)
            velocity_r -= gravity / 1000f;
        else
            radius = 0;

        if(!collisionState.standing) radius += velocity_r;
        else {
            velocity_r = 0;
            radius += 0.01f;
        }

        theta  += velocity_t / (normalizer * Mathf.PI);

        CorrectRotation();
        var coords = ToCartesian(new Vector2(theta, radius));
        transform.position = new Vector3(coords.x, coords.y, transform.position.z);
    }

    public static Vector2 ToCartesian(Vector2 cylindrical){
       return new Vector2( cylindrical.y * Mathf.Cos( cylindrical.x + (Mathf.PI / 2f)), cylindrical.y * Mathf.Sin( cylindrical.x + (Mathf.PI / 2f)));

    }

    private void CorrectRotation(){
      transform.eulerAngles = new Vector3(0, 0, 180 / Mathf.PI * theta + rotationOffset);
    }

    public static Vector2 ToCylinder(Vector2 cartesian){
        var radius = Mathf.Sqrt( cartesian.x * cartesian.x + cartesian.y * cartesian.y );

        var theta = 0f;
        if( cartesian.x == 0 && cartesian.y == 0 ){
            theta = 0f;
        }else if( cartesian.x >= 0 ){
            theta = Mathf.Asin( cartesian.y / radius );
        }else if( cartesian.x < 0 ){
            theta = -Mathf.Asin( cartesian.y / radius ) + Mathf.PI;
        }
        return new Vector2(theta, radius);
    }

}
