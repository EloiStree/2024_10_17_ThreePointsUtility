using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace Eloi.ThreePoints
{

    public class ThreePointsMono_MeshTriangleIndexTransform : MonoBehaviour
    {
        public ThreePointsMono_Transform3 m_toAffect;
        public MeshFilter m_meshFilter;
        public int m_index = 0;

        public int m_triangleCount = 0;
        public int m_indexModulo = 0;

        public void Reset()
        {
            m_meshFilter = GetComponentInChildren<MeshFilter>();
        }

        public void SetIndex(int index)
        {
            m_index = index;
            Refresh();
        }
        [ContextMenu("Next")]
        public void Next()
        {
            m_index++;
            Refresh();
        }
        [ContextMenu("Suivant")]
        public void Previous()
        {
            m_index--;
            Refresh();
        }
        public void Refresh()
        {

            if (m_meshFilter == null)
                return;
            if (m_meshFilter.sharedMesh == null)
                return;

            m_triangleCount = m_meshFilter.sharedMesh.triangles.Length / 3;
            m_indexModulo = m_index % m_triangleCount;
            if (m_indexModulo < 0)
                m_indexModulo += m_triangleCount;
            Mesh mesh = m_meshFilter.sharedMesh;
            Vector3 start = mesh.vertices[mesh.triangles[m_indexModulo * 3]];
            Vector3 middle = mesh.vertices[mesh.triangles[m_indexModulo * 3 + 1]];
            Vector3 end = mesh.vertices[mesh.triangles[m_indexModulo * 3 + 2]];
            Transform transform = m_meshFilter.transform;

            RotateAroundCenter(ref start, transform.rotation);
            RotateAroundCenter(ref middle, transform.rotation);
            RotateAroundCenter(ref end, transform.rotation);

            start += transform.position;
            middle += transform.position;
            end += transform.position;
            m_toAffect.SetWith(start, middle, end);

        }

        private void RotateAroundCenter(ref Vector3 start, Quaternion rotation)
        {
            start = rotation * start;
        }
    }

}