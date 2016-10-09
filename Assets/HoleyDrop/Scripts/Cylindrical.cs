using UnityEngine;
using System.Collections;

public class Cylindrical : MonoBehaviour {
    public float radius = 5f;
    public float theta = 0f;
    public float velocity_r = 0f;
    public float velocity_t = 0f;
    public float drag = 0.05f;
    public float gravity = 5f;
    public float boundryLimit = 4f;

    private float normalizer = 45f;
    private StandingState standingState;

    private void Awake(){
        standingState = GetComponent<StandingState>();
    }

    private void FixedUpdate(){
        var absVelT = Mathf.Abs(velocity_t);

        if(absVelT < 0.1f) velocity_t = 0f;
        else velocity_t -= velocity_t * drag;

        if(radius > 0)
            velocity_r -= gravity / 1000f;
            if(radius > boundryLimit)
                // Load lose scene
        else{
            theta = 0f;
            velocity_r = 0;
            // Load lose scene
        }

        theta  += velocity_t / (normalizer * Mathf.PI);

        CorrectRotation();
        var coords = ToCartesian(new Vector2(theta, radius));
        transform.position = new Vector3(coords.x, coords.y, transform.position.z);
    }

    private void Update(){
        if(!standingState.standing) radius += velocity_r;
        else {
            velocity_r = 0;
            radius += 0.01f;
        }

        // Bound theta
        if(theta > 2 * Mathf.PI) theta -= 2 * Mathf.PI;
        else if(theta <= 0f) theta += 2 * Mathf.PI;
    }

    public static Vector2 ToCartesian(Vector2 cylindrical){
       return new Vector2( cylindrical.y * Mathf.Cos( cylindrical.x + (Mathf.PI / 2f)), cylindrical.y * Mathf.Sin( cylindrical.x + (Mathf.PI / 2f)));

    }

    private void CorrectRotation(){
      transform.eulerAngles = new Vector3(0, 0, 180 / Mathf.PI * theta);
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
