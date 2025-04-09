using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Eloi.ThreePoints
{
    public class ThreePointsMono_MovableToGivenTriangle : MonoBehaviour {

        public Transform[] m_parentsWithMovable;
        public List<GameObject> m_shouldBeMovable;
        public List<ThreePointsMono_MovableTransform3> m_movableFound;
        public ThreePointsTriangleDefault m_receivedTriangle;

        public float m_tolerance = 0.07f;
        public bool m_loadOnAwake = true;

        private void Awake()
        {
            if (m_loadOnAwake)
            {
                LookForMovableInChildren();
            }
        }
        public void SetWith(I_ThreePointsGet triangle)
        {
            m_receivedTriangle = new ThreePointsTriangleDefault(triangle);
            RefreshList();

            foreach (var item in m_movableFound)
            {
                ThreePointsTriangleDefault computed = new ThreePointsTriangleDefault(
                    item.m_relatedTriangle.m_triangle);
                bool hasSameEdge= ThreePointsUtility.HasAlmostTheSameEdge(
                    m_receivedTriangle,
                    computed,
                    m_tolerance);

                if (hasSameEdge) { 
                
                    RelocateTriangleRootFromTo.MoveTo(
                        item.m_whatToMove,
                        item.m_relatedTriangle.m_triangle,
                        m_receivedTriangle
                        );
                }
            }
        }
 
        [ContextMenu("Look for Movable")]
        public void LookForMovableInChildren() {

            m_shouldBeMovable.Clear();
            m_movableFound.Clear();
            m_shouldBeMovable.AddRange(GetComponentsInChildren<ThreePointsMono_MovableTransform3>().Select(a => a.gameObject));

            foreach (var item in m_parentsWithMovable)
            {
                if (item == null) continue;
                ThreePointsMono_MovableTransform3[] found = item.GetComponentsInChildren<ThreePointsMono_MovableTransform3>();
                if (found != null)
                {
                    m_shouldBeMovable.AddRange(found.Select(a => a.gameObject));
                }
            }

            RefreshList();
        }

        [ContextMenu("Refresh List")]
        private void RefreshList()
        {
            m_movableFound.Clear();
            foreach (var item in m_shouldBeMovable)
            {
                ThreePointsMono_MovableTransform3 [] found = item.GetComponentsInChildren<ThreePointsMono_MovableTransform3>();
                if (found != null)
                {
                    m_movableFound.AddRange(found);
                }
            }
            m_shouldBeMovable=  m_shouldBeMovable.Distinct().ToList();

        }
    }

}