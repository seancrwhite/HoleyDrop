using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ground : MonoBehaviour, IRecycle {

    // Degrees!
    public float holeMax = 90f;
    public float holeMin = 15f;

    public float rotation = 0f;

    public bool active = true;

    private MeshFilter meshFilter;
    private CircleCollider2D groundCollider;
    private Vector3 hole;

    private Cylindrical cyln;

    // private GameObject A;
    // private GameObject B;

    public void Awake(){
        cyln = GameObject.Find("Player").GetComponent<Cylindrical>();

        Restart();
    }

    public void Restart(){

        // A = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // B = GameObject.CreatePrimitive(PrimitiveType.Quad);

        meshFilter = GetComponent<MeshFilter>();

        groundCollider = GetComponent<CircleCollider2D>();

        hole.y = Random.Range(holeMin, holeMax);
        meshFilter.mesh = GenerateArcMesh(hole.y);
        // Debug.Log("H: "+hole);

        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        // transform.eulerAngles = new Vector3(0, 0, Random.Range(cyln.theta + (180f / Mathf.PI) + 90f, cyln.theta + (180f / Mathf.PI) - 90f));
        rotation = Random.Range(1f, 360f);
        transform.eulerAngles = new Vector3(0, 0, rotation);

        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void Update(){

        var thetaDegrees = (cyln.theta * 180 / Mathf.PI) + 90f;

        // var tempA = Cylindrical.ToCartesian(new Vector2(hole.x+rotation, transform.localScale.x));
        // var tempB = Cylindrical.ToCartesian(new Vector2(hole.y+rotation, transform.localScale.x));

        // A.transform.position = new Vector3(tempA.x, tempA.y, -2f);
        // A.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // B.transform.position = new Vector3(tempB.x, tempB.y, -2f);
        // B.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);


        // Debug.Log(tempA);
        // Debug.Log(tempB);

        if(thetaDegrees > hole.x+rotation && thetaDegrees < hole.y+rotation){
            Destroy(groundCollider);
        } else if ((thetaDegrees > hole.x+rotation || thetaDegrees < hole.y+rotation) && hole.x+rotation > hole.y+rotation) {
            Destroy(groundCollider);
        }

    }

    private void FixedUpdate(){
        if(active){
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            if(groundCollider)
                groundCollider.transform.localScale = transform.localScale;
        }

        transform.position += new Vector3(0f, 0f, 0.01f);
    }

    public void Shutdown(){
        // Make transparent?
    }

    private Mesh GenerateArcMesh(float holeWidth){
        var arc = 360 - holeWidth;
        var pointCount = 360;
        var step = arc / pointCount;

        // holeCollider.radius = Mathf.PI * (holeWidth / 360) * 0.95f;
        // holeCollider.offset = Cylindrical.ToCartesian(
        //     new Vector2((Mathf.PI / 180f) * (-holeWidth / 2f), 1f)
        // );

        Quaternion quaternion = Quaternion.Euler(0f, 0f, step);
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangleList = new List<int>();

        vertices.Add(new Vector3(0f, 0f, 0f));
        vertices.Add(new Vector3(0f, 1f, 0f));
        vertices.Add(quaternion * vertices[1]);

        triangleList.Add(0);
        triangleList.Add(1);
        triangleList.Add(2);

        for(var i = 0; i < pointCount; i++){
            triangleList.Add(0);
            triangleList.Add(vertices.Count - 1);
            triangleList.Add(vertices.Count);
            vertices.Add(quaternion * vertices[vertices.Count - 1]);
        }

        Mesh mesh = new Mesh();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangleList.ToArray();

        return mesh;
    }

}