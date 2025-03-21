using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Eloi.ThreePoints
{
    public class ThreePointsMono_RelayTrackedPoint : MonoBehaviour
{

    public Transform m_trackedObject;
    public UnityEvent<Vector3> m_onPointDiffused;


    private void Reset()
    {
        m_trackedObject = transform;
    }
    [ContextMenu("Push Tracked As Vector3")]
    public void PushTrackedAsVector3()
    {
        if (m_trackedObject != null)
            m_onPointDiffused.Invoke(m_trackedObject.position);
    }

    [ContextMenu("Push Tracked As Vector3 if active")]
    public void PushTrackedAsVector3IfActive()
    {
        if (m_trackedObject != null && m_trackedObject.gameObject.activeInHierarchy)
            m_onPointDiffused.Invoke(m_trackedObject.position);
    }
}

}