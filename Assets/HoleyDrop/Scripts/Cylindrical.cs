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

    private void FixedUpdate(){
        // var absVelR = Mathf.Abs(velocity_r);
        var absVelT = Mathf.Abs(velocity_t);

        //if(absVelR < 0.1f) velocity_r = 0f;
        //else velocity_r -= velocity_r * drag;

        if(absVelT < 0.1f) velocity_t = 0f;
        else velocity_t -= velocity_t * drag;

        if(radius > 0)
            velocity_r -= gravity / 1000f;
        else
            radius = 0;

        radius += velocity_r;
        theta  += velocity_t / (normalizer * Mathf.PI);
        ToCartesian();
    }

    private void ToCartesian(){
       transform.position = new Vector3( radius * Mathf.Cos( theta + (Mathf.PI / 2f)), radius * Mathf.Sin( theta + (Mathf.PI / 2f)), transform.position.z );
       transform.eulerAngles = new Vector3(0, 0, 180 / Mathf.PI * theta + rotationOffset);
    }

    private void ToCylinder(){
       radius = Mathf.Sqrt( transform.position.x * transform.position.x + transform.position.y * transform.position.y );

       if( transform.position.x == 0 && transform.position.y == 0 ){
          theta = 0;
       }else if( transform.position.x >= 0 ){
          theta = Mathf.Asin( transform.position.y / radius );
       }else if( transform.position.x < 0 ){
          theta = -Mathf.Asin( transform.position.y / radius ) + Mathf.PI;
       }
    }

}
