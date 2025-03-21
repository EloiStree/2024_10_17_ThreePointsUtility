using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Eloi.ThreePoints
{
    [System.Serializable]
    public class ThreePoints_PrefabRegister
    {

        public GameObject[] m_prefabs;
        public List<PrefabWithThreePoints> m_prefabWithThreePoints;

        public void RefreshList()
        {
            m_prefabWithThreePoints = new List<PrefabWithThreePoints>();
            for (int i = 0; i < m_prefabs.Length; i++)
            {
                ThreePointsMono_Transform3  t = m_prefabs[i].GetComponentInChildren<ThreePointsMono_Transform3>(); 
                if (t == null)
                    continue;
                m_prefabWithThreePoints.Add(new PrefabWithThreePoints(m_prefabs[i], t));
             }
        }
        [System.Serializable]
        public class PrefabWithThreePoints
        {
            public GameObject m_prefab;
            public STRUCT_ThreePointTriangleDistanceAndAngle m_triangleInfo;

            public PrefabWithThreePoints(GameObject prefab, ThreePointsMono_Transform3 t)
            {
                m_prefab = prefab;
                m_triangleInfo = t.m_triangle.m_distanceAndAngle;
            }
            public void SetWith(GameObject target)
            {

                m_prefab = target;
                ThreePointsMono_Transform3 t  = target.GetComponentInChildren<ThreePointsMono_Transform3>();
                if (t == null)
                    return;
                m_triangleInfo = t.m_triangle.m_distanceAndAngle;
            }
        }
    }

    public class ThreePointsMono_PrefabInvocationSleepy : MonoBehaviour
{
    public GameObject m_prefab;
    public ThreePointsTriangleDefault m_triangle;
    public bool m_invokeOnStart = false;
    public void InvokeWith(I_ThreePointsDistanceAngleGet info) {

        m_triangle.SetThreePoints(info);

    }
    [ContextMenu("Invoke")]
    public void Invoke() {
        m_triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
        Vector3 centroid = new Vector3(
    (start.x + middle.x + end.x) / 3,
    (start.y + middle.y + end.y) / 3,
    (start.z + middle.z + end.z) / 3
);
        ThreePointUtility.GetCrossDirection(m_triangle, out Vector3 crossDirection, m_invokeOnStart);
        GameObject go = Instantiate(m_prefab, centroid, Quaternion.identity);
        go.transform.up = crossDirection;


    }
}

}