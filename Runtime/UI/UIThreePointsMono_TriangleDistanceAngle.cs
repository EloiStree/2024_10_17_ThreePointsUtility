using Eloi.ThreePoints;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.ThreePoints { 
public class UIThreePointsMono_TriangleDistanceAngle : MonoBehaviour
{

        public ThreePointsTriangleDefault m_triangle;

        public UnityEvent<string> m_maximumDistance;
        public UnityEvent<string> m_mediumDistance;
        public UnityEvent<string> m_minimumDistance;
        public UnityEvent<string> m_allAngles;
        public UnityEvent<string> m_distanceTriangle;
        public UnityEvent<string> m_distanceSuare;
        public UnityEvent<string> m_biggestAngle;
        public UnityEvent<string> m_airSurface;

        public float m_distanceTriangleValue;
        public float m_distanceSuareValue;
        public float m_biggestAngleValue;
        public float m_airSurfaceValue;
        public float m_angleMax;
        public float m_angleMiddle;
        public float m_angleMin;
        public float m_distanceMax;
        public float m_distanceMiddle;
        public float m_distanceMin;



        public void SetWith(I_ThreePointsGet threePoints)
        {
            m_triangle.SetThreePoints(threePoints);

            m_triangle.GetSquareBorderDistance(out m_distanceSuareValue);
            m_triangle.GetTrianglesBorderDistance(out m_distanceTriangleValue);
            m_triangle.GetAirSurface(out m_airSurfaceValue);
            m_triangle.GetOrderAngle(out m_angleMax, out m_angleMiddle, out m_angleMin);
            m_triangle.GetOrderDistance(out m_distanceMax, out m_distanceMiddle, out m_distanceMin);
            
            m_angleMax = (int)Mathf.Round(m_angleMax);
            m_angleMiddle = (int)Mathf.Round(m_angleMiddle);
            m_angleMin = (int)Mathf.Round(m_angleMin);
            m_distanceMax = (int)Mathf.Round(m_distanceMax*1000);
            m_distanceMiddle = (int)Mathf.Round(m_distanceMiddle * 1000);
            m_distanceMin = (int)Mathf.Round(m_distanceMin * 1000);
            m_biggestAngleValue = (int)m_angleMax;
            m_distanceSuareValue = (int)(m_distanceSuareValue * 1000f);
            m_distanceTriangleValue = (int)(m_distanceTriangleValue * 1000f);

            m_maximumDistance.Invoke(m_distanceMax.ToString());
            m_mediumDistance.Invoke(m_distanceMiddle.ToString());
            m_minimumDistance.Invoke(m_distanceMin.ToString());
            m_allAngles.Invoke(m_angleMax.ToString() + " " + m_angleMiddle.ToString() + " " + m_angleMin.ToString());
            m_distanceTriangle.Invoke(m_distanceTriangleValue.ToString());
            m_distanceSuare.Invoke(m_distanceSuareValue.ToString());
            m_biggestAngle.Invoke(m_biggestAngleValue.ToString());
            m_airSurface.Invoke(m_airSurfaceValue.ToString()+ "m²");

        }
    }

}