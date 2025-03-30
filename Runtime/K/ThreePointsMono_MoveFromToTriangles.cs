using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Eloi.ThreePoints
{
    [ExecuteInEditMode]
    public class ThreePointsMono_MoveFromToTriangles : MonoBehaviour
    {

        public ThreePointsMono_MovableTransform3 m_origineAnchor;
        public ThreePointsMono_Transform3 m_whereToGoAnchor;

        public bool m_useDebug;

        [ContextMenu("Move and rotate")]
        public void MoveAndRotate()
        {
            RelocateTriangleRootFromTo.MoveTo(
                m_origineAnchor.m_whatToMove,
                m_origineAnchor.m_relatedTriangle.m_triangle,
                m_whereToGoAnchor.m_triangle);


        }


    }

}