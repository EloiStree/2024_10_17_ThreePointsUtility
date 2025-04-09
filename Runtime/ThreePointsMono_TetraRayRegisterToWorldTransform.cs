using System.Collections.Generic;
using UnityEngine;

namespace Eloi.ThreePoints {
    public class ThreePointsMono_TetraRayRegisterToWorldTransform : MonoBehaviour { 
    

        public Transform m_targetToAffect;
        public ThreePointsMono_TetraRayListRegister m_register;
        public STRUCT_TetraRayWithWorld m_lastReceived;


      
        public void SetWith(I_ThreePointsGet triangle)
        {
            m_register.TryToFindSimilarTo(triangle, out List<STRUCT_TetraRayWithWorld> listTetraRay);
            
            if (listTetraRay.Count > 0)
            {
                m_lastReceived = listTetraRay[0];
                //ThreePointsTriangleDefault t = new ThreePointsTriangleDefault(triangle);
                //t.GetLongestSideWithFrontCorner(
                //    out Vector3 shortSidePoint,
                //    out Vector3 longSidePoint,
                //    out Vector3 cornerPoint,
                //    out Vector3 footPoint,
                //    out Vector3 upDirection,
                //    out Vector3 forwardDirection,
                //    out Vector3 rightDirection
                //);
                TetraRayUtility.GetRelocationOfPointWithTetraRay(triangle, listTetraRay[0], m_targetToAffect);
                TetraRayDrawUtility.Draw(listTetraRay[0], 10);

            }
            else
            {
                m_lastReceived = new STRUCT_TetraRayWithWorld();
            }
        }

    }
}

