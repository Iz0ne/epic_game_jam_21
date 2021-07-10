using UnityEngine;

public class BossVision : MonoBehaviour
{
    public float viewDistance = 2;
    public float fov = 20f;
    public int rayCount = 8;
    public float direction = 0f;
    


    public Color meshCol = Color.blue;

    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles =  new int[rayCount * 3];

        Vector3 origin = transform.localPosition;

        vertices[0] = origin;

        float angleIncrease = fov / rayCount;
        float angle = this.direction + (fov / 2f);
        
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i=0; i < rayCount; i++){
            Vector3 vertex;
            Vector3 destination = origin + getDirectionVector(angle) * viewDistance;
            
            RaycastHit2D hit = Physics2D.Linecast(origin, destination);
            if (hit.collider == null){
                vertex = destination;
            }
            else {
                vertex = hit.point;
            }
            
            
            vertices[vertexIndex] = vertex;
            
            if (i > 0){
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }


       mesh.vertices = vertices;
       mesh.uv = uv;
       mesh.triangles = triangles;

    }


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Entered NPC CollideBox");
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Exited NPC CollideBox");

		}
	}
    
    void OnCollisionEnter(Collision collision)
    {
		Debug.Log ("Player Entered cone of vision");
	}

    void OnCollisionExit(Collision collision)
    {
		Debug.Log ("Player Exited cone of vision");
	}

    private static Vector3 getDirectionVector(float angle){
        float rad = angle * Mathf.PI / 180f;
        Vector3 vec = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
        return vec.normalized;
    }
}
