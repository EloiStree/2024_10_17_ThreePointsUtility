using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.ThreePoints {
    /// <summary>
    /// I am a class to stack info about submit three points in aim to check for multi triangle loading structure
    /// </summary>
    public class ThreePointsMono_SubmitStackFIFO : MonoBehaviour
    {
        public ThreePoints_SubmitStackFIFO m_data = new ThreePoints_SubmitStackFIFO();
        public void PushIn(I_ThreePointsGet triangleGet)
        {
            m_data.PushIn(triangleGet);
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            m_data.Clear();
        }
    }
    [System.Serializable]
    public class ThreePoints_SubmitStackFIFO 
    {
        public List<ThreePointsTriangleDefault> m_stackNewToAll = new List<ThreePointsTriangleDefault>();

        public UnityEvent<I_ThreePointsGet> m_onChangedOneTriangle;
        public UnityEvent<I_ThreePointsGet, I_ThreePointsGet> m_onChangedTwoTriangle;
        public UnityEvent<I_ThreePointsGet, I_ThreePointsGet, I_ThreePointsGet> m_onChangedThreeTriangle;

        [ContextMenu("Clear")]
        public void Clear()
        {
            m_stackNewToAll.Clear();
        }

        public int m_maxSize = 5;
        public void PushIn(I_ThreePointsGet triangleGet)
        {

            ThreePointsTriangleDefault triangle = new ThreePointsTriangleDefault();
            triangle.SetThreePoints(triangleGet);
            m_stackNewToAll.Insert(0, triangle);
            if (m_stackNewToAll.Count > m_maxSize)
            {
                m_stackNewToAll.RemoveAt(m_stackNewToAll.Count - 1);
            }
            if (m_stackNewToAll.Count >= 1)
            {
                m_onChangedOneTriangle.Invoke(triangleGet);
            }
            else if (m_stackNewToAll.Count >= 2)
            {
                m_onChangedTwoTriangle.Invoke(triangleGet, m_stackNewToAll[1]);
            }
            else if (m_stackNewToAll.Count >= 3)
            {
                m_onChangedThreeTriangle.Invoke(triangleGet, m_stackNewToAll[1], m_stackNewToAll[2]);
            }

        }
    }

}