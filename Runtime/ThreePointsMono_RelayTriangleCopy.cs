using UnityEngine;
using UnityEngine.Events;
namespace Eloi.ThreePoints
{

    public class ThreePointsMono_RelayTriangleCopy : MonoBehaviour
    {

        public ThreePointsTriangleDefault m_triangle;
        public UnityEvent<I_ThreePointsDistanceAngleGet> m_onTriangleReceived;

        public void NotifyNewPoint(Vector3 start, Vector3 middle, Vector3 end)
        {
            m_triangle = new ThreePointsTriangleDefault(start, middle, end);
            m_onTriangleReceived.Invoke(m_triangle);

        }
        public void NotifyNewPoint(I_ThreePointsGet triangle)
        {
            triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
            m_triangle = new ThreePointsTriangleDefault(start, middle, end);
            m_onTriangleReceived.Invoke(m_triangle);
        }

    }
}