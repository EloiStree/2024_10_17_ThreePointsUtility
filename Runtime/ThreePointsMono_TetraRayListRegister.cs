using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Eloi.ThreePoints {
    public class ThreePointsMono_TetraRayListRegister : MonoBehaviour {
        public ThreePoints_TetraRayListRegister m_register;
        public void SaveTetraRay(I_ThreePointsGet triangle, Transform target)
        {
            TetraRayUtility.GetFrom(triangle, target, out STRUCT_TetraRayWithWorld tetraRayWithWorld, 10);
            m_register.SaveTetraRayWithDefaultThreshold(tetraRayWithWorld);
        }

        public void SaveTetraRay(STRUCT_TetraRayWithWorld tetraRayWithWorld)
        {
            m_register.SaveTetraRayWithDefaultThreshold(tetraRayWithWorld);
        }
        public void SaveTetraRay(STRUCT_TetraRay tetraRay) { 
            TetraRayUtility.GetFrom(tetraRay, out STRUCT_TetraRayWithWorld tetraRayWithWorld);
            m_register.SaveTetraRayWithDefaultThreshold(tetraRayWithWorld);
        }

        [ContextMenu("Generate New GUID")]
        public void GenerateNewGuid()
        {
            m_register.GenerateNewGuid();
        }

        public void TryToFindSimilarTo(I_ThreePointsGet triangle, out List<STRUCT_TetraRayWithWorld> listTetraRay)
        {

            m_register.TryToFindSimilarTo(triangle, out listTetraRay);
           

        }
    }
}

