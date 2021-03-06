using UnityEngine;

public class BossVision : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public float viewDistance = 10f;
    public float fov = 45f;
    public int rayCount = 30;
    private Vector3 direction = Vector3.right;
    private Vector3 position;
    private Mesh mesh;
    

    public Color meshCol = Color.blue;

    void Start(){
        this.mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = this.mesh;
    }

    void Update()
    {
        float angleIncrease = fov / rayCount;

        Vector3 sweepingDir = Quaternion.Euler(0, 0, fov /2) *  this.direction ;
        sweepingDir = sweepingDir.normalized;
        
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles =  new int[rayCount * 3];

        Vector3 origin = position;
        vertices[0] = origin;
        
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i=0; i < rayCount; i++){
            Vector3 vertex;
            Vector3 destination = origin + sweepingDir * viewDistance;
            
            RaycastHit2D hit = Physics2D.Raycast(origin, sweepingDir, viewDistance, layerMask);
            if (hit.collider == null){
                vertex = destination;
            }
            else{
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
            sweepingDir = Quaternion.Euler(0, 0, -angleIncrease) * sweepingDir;
            sweepingDir = sweepingDir.normalized;
        }

       mesh.vertices = vertices;
       mesh.uv = uv;
       mesh.triangles = triangles;
    }

    // public static float findDegree(Vector3 dir){
    //     bool isDirLefty = Mathf.Acos(dir.x) > Mathf.PI / 2f;

    //     float value = (float)(Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg);
    //     if(isDirLefty) value += 180f;

    //     return value;
    // }

    private static Vector3 getDirectionVector(float angle){
        float rad = angle * Mathf.PI / 180f;
        Vector3 vec = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
        return vec.normalized;
    }

    public void SetPosition(Vector3 newPos){
        this.position = newPos;
    }

    public Vector3 GetPosition(){
        return this.position;
    }

    public void SetDirection(Vector3 newDir){
        this.direction = newDir.normalized;
    }

    public Mesh GetMesh(){
        return this.mesh;
    }
}
