using UnityEngine;
using UnityEngine.Events;
namespace Eloi.ThreePoints
{

public class ThreePointsMono_PushBetweenPointsCenter : MonoBehaviour
{

    public Transform m_fingerA;
    public Transform m_fingerB;
    public UnityEvent<Vector3> m_onPointPush;

   


    public bool IsTransformValide()
    {
        return m_fingerA != null && m_fingerB != null;
    }

    public bool IsPointsActive()
    {
        return m_fingerA.gameObject.activeInHierarchy && m_fingerB.gameObject.activeInHierarchy;
    }

    [ContextMenu("Push Tracked As Vector3")]
    public void PushTrackedAsVector3()
    {
        if (IsTransformValide() )
            m_onPointPush.Invoke(m_fingerA.position);
    }

    [ContextMenu("Push Tracked As Vector3 if active")]
    public void PushTrackedAsVector3IfActive()
    {
        if (IsTransformValide()  && IsPointsActive())
            m_onPointPush.Invoke(GetBetweenFingerPosition());
    }

    private Vector3 GetBetweenFingerPosition()
    {
        return (m_fingerA.position + m_fingerB.position) / 2;
    }

}

}