using UnityEngine;
namespace Eloi.ThreePoints
{

public class ThreePointsMono_QuickFollowIt : MonoBehaviour
{
        public Transform m_whatToMove;
        public Transform m_whatToFollow;

        public bool m_useUpdate=true;
        public bool m_useLateUpdate = true;
        private void Reset()
        {
            m_whatToMove = transform;
        }
        void Update()
        {
            if (m_useUpdate)
            MoveToTarget();
        }
        void LateUpdate()
        {
            if (m_useLateUpdate)
                MoveToTarget();
        }

        [ContextMenu("Move To Target")]
        private void MoveToTarget()
        {
            if (m_whatToMove == null || m_whatToFollow == null)
                return;
            m_whatToMove.position = m_whatToFollow.position;
            m_whatToMove.rotation = m_whatToFollow.rotation;
        }
    }

}