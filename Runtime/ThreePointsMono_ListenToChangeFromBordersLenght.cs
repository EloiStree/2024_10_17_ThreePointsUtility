using UnityEngine;
using UnityEngine.Events;

namespace Eloi.ThreePoints {
    public class ThreePointsMono_ListenToChangeFromBordersLenght : MonoBehaviour
{
    public ThreePointsTriangleDefault m_currentState;
    public UnityEvent<I_ThreePointsGet> m_onTriangleChangeHappened;
    public float m_currentLenght;
    public float m_previousLenght;
    public float m_changeLenght = 0.02f;

    public void PushInCurrentGuarianState(I_ThreePointsGet triangle)
    {
        m_currentState.SetThreePoints(triangle);
        m_currentState.GetTrianglesBorderDistance(out m_currentLenght);
        if (Mathf.Abs(m_currentLenght - m_previousLenght) > m_changeLenght)
        {
            m_previousLenght = m_currentLenght;
            m_onTriangleChangeHappened.Invoke(triangle);
        }
    }
}
}