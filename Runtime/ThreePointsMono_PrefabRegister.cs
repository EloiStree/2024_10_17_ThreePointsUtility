using UnityEngine;
namespace Eloi.ThreePoints
{
    public class ThreePointsMono_PrefabRegister : MonoBehaviour
    {

        public ThreePoints_PrefabRegister m_data;

        [ContextMenu("Refresh List")]
        public void RefreshList()
        {
            m_data.RefreshList();
        }
        public void Start()
        {
            m_data.RefreshList();
        
        }
        public void OnEnable()
        {
            m_data.RefreshList();
        }

    }

}