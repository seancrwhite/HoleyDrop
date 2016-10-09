using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ground : MonoBehaviour, IRecycle {

    // Degrees!
    public float holeMax = 90f;
    public float holeMin = 15f;

    private MeshFilter meshFilter;
    private CircleCollider2D groundCollider;
    private CircleCollider2D holeCollider;

    private CollisionState collisionState;

    public void Awake(){
        collisionState = GameObject.Find("Player").GetComponent<CollisionState>();
        Restart();
    }

    public void Restart(){
        meshFilter = GetComponent<MeshFilter>();

        groundCollider = gameObject.AddComponent<CircleCollider2D>();
        holeCollider = gameObject.AddComponent<CircleCollider2D>();

        meshFilter.mesh = GenerateArcMesh(Random.Range(holeMin, holeMax));

        transform.localScale += new Vector3(1f, 1f, 1f);
        transform.eulerAngles = new Vector3(0, 0, Random.Range(1f, 360f));

        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void Update(){

        if(collisionState.fisting){
            Destroy(groundCollider);
            Destroy(holeCollider);
        }

    }

    private void FixedUpdate(){
        transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void Shutdown(){
        // Make transparent?
    }

    private Mesh GenerateArcMesh(float holeWidth){
        var arc = 360 - holeWidth;
        var pointCount = 360;
        var step = arc / pointCount;

        holeCollider.radius = Mathf.PI * (holeWidth / 360) * 0.95f;
        holeCollider.offset = Cylindrical.ToCartesian(
            new Vector2((Mathf.PI / 180f) * (-holeWidth / 2f), 1f)
        );

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