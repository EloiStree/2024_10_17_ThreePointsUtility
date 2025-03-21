using UnityEngine;
using UnityEngine.Events;
namespace Eloi.ThreePoints
{
    public class ThreePointsMono_RelayAction : MonoBehaviour
    {
        public UnityEvent m_onAction;
        [ContextMenu("Invoke")]
        public void Invoke()
        {
            m_onAction.Invoke();
        }
    }

}