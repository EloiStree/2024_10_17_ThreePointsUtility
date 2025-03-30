using UnityEngine;
namespace Eloi.ThreePoints
{
    public class ThreePointsMono_InverseMeshAxis : MonoBehaviour
    {
        public MeshFilter m_meshFilter;


        public void Reset()
        {
            m_meshFilter = GetComponentInChildren<MeshFilter>();
        }
        [ContextMenu("Inverse X")]
        public void InverseX()
        {

            Mesh mesh = m_meshFilter.sharedMesh;
            Vector3[] vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].x = -vertices[i].x;
            }
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            m_meshFilter.sharedMesh = mesh;
        }

        [ContextMenu("Inverse XY")]
        public void InverseXY()
        {

            InverseX();
            InverseY();
        }

        [ContextMenu("Inverse Y")]
        public void InverseY()
        {

            Mesh mesh = m_meshFilter.sharedMesh;
            Vector3[] vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].y = -vertices[i].y;
            }
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            m_meshFilter.sharedMesh = mesh;
        }
        [ContextMenu("Inverse Z")]
        public void InverseZ()
        {

            Mesh mesh = m_meshFilter.sharedMesh;
            Vector3[] vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].z = -vertices[i].z;
            }
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            m_meshFilter.sharedMesh = mesh;
        }

     
        
    }

}