using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour{//, IRecycle {


    private void Awake(){

    }

    private void Restart(){
        // var collider = GetComponents<CircleCollider2D>();

        // collider.size = size;
        // collider.position = Vector3.zero;

    }

    public void Shutdown(){}

    private Mesh GenerateArcMesh(float holeWidth){
        var arc = 2*Mathf.PI - holeWidth;
        var pointCount = 20;
        var step = arc / pointCount;

        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, angleStep);
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