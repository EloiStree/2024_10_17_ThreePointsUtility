using UnityEngine;
using UnityEngine.Events;

namespace Eloi.ThreePoints {
    public class ThreePointsMono_RelayTransform3AsInterface : MonoBehaviour {

        public ThreePointsMono_Transform3 m_toRelay;
        public UnityEvent<I_ThreePointsGet> m_relayEvent;

        [ContextMenu("Relay Interface")]
        public void RelayTransform3Interface()
        {
            if (m_toRelay != null)
            {
                m_relayEvent.Invoke(m_toRelay);
            }
        }
    }
}

