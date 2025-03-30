
using System.Collections.Generic;
using UnityEngine;

namespace Eloi.ThreePoints { 
public class ThreePointsMono_WorldPointsToTriangle : MonoBehaviour
{

    public Vector3[] m_worldPointsGiven;

    public Vector3 m_centerOfPoints;
    public Vector3 m_nearestPointOfCenter;
    public Vector3 m_farestPointOfCenter;
    public Transform m_start;
    public Transform m_middle;
    public Transform m_end;

        public bool m_useDebugDraw;
        public Color m_color = Color.red;

        public void PushIn(Vector3[] points) { 
        
            PushIn(new List<Vector3>(points));
        }

    public void PushIn(List<Vector3> points)
    {



        if (points.Count > 1)
        {
            m_worldPointsGiven = points.ToArray();

            long x = 0, y = 0, z = 0;
            foreach (Vector3 point in m_worldPointsGiven)
            {
                x += (long)(point.x * 1000.0);
                y += (long)(point.y * 1000.0);
                z += (long)(point.z * 1000.0);
            }
                m_centerOfPoints = new Vector3(x / m_worldPointsGiven.Length, y / m_worldPointsGiven.Length, z / m_worldPointsGiven.Length);
                m_centerOfPoints /= 1000.0f;
                m_farestPointOfCenter = m_centerOfPoints;
                m_nearestPointOfCenter = Vector3.one * float.MaxValue;
                foreach (Vector3 point in m_worldPointsGiven)
            {
                if (Vector3.Distance(m_centerOfPoints, m_nearestPointOfCenter) > Vector3.Distance(m_centerOfPoints, point))
                {
                    m_nearestPointOfCenter = point;
                }
                if (Vector3.Distance(m_centerOfPoints, m_farestPointOfCenter) < Vector3.Distance(m_centerOfPoints, point))
                {
                    m_farestPointOfCenter = point;
                }
            }


            if (m_middle)
                m_middle.position = m_centerOfPoints;
            if (m_start)
                m_start.position = m_nearestPointOfCenter;
            if (m_end)
                m_end.position = m_farestPointOfCenter;
        }


    }
        public void Update()
        {
            if (m_useDebugDraw)
            {
               if (m_worldPointsGiven.Length > 1)
                {
                    Vector3 previous;
                    Vector3 current;
                    for (int i = 0; i < m_worldPointsGiven.Length; i++)
                    {
                        previous = m_worldPointsGiven[i];
                        current = m_worldPointsGiven[(i + 1) % m_worldPointsGiven.Length];
                        Debug.DrawLine(previous, current, m_color);
                    }
                }
            }
        }
    }
}