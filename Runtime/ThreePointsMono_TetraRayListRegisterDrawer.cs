using UnityEngine;

namespace Eloi.ThreePoints {
    public class ThreePointsMono_TetraRayListRegisterDrawer : MonoBehaviour
    {
        public ThreePointsMono_TetraRayListRegister m_register;
        public bool m_debugDrawAll = true;
        public bool m_debugDrawSelected = true;
        public int m_index = 0;

        public void Update()
        {
            int lenght = m_register.m_register.m_listTetraRay.Count;

            
                for (int i = 0; i < lenght; i++)
                {
                    if (m_debugDrawSelected && i== m_index)
                        TetraRayDrawUtility.Draw(m_register.m_register.m_listTetraRay[i], 10);
                    if (m_debugDrawAll)
                        TetraRayDrawUtility.Draw(m_register.m_register.m_listTetraRay[i], 10);
                }
        }
    }
}

