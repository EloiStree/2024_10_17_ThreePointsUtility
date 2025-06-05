using UnityEngine;

namespace Eloi.ThreePoints
{
    public class StaitcTriangleCompute

    {

        // Function to calculate the area using Heron's formula
        public static float CalculateArea(float a, float b, float c)
        {
            float s = (a + b + c) / 2f;
            return Mathf.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        // Function to calculate the circumradius (R)
        public static float CalculateCircumRadius(float a, float b, float c)
        {
            float area = CalculateArea(a, b, c);
            if (area == 0) return 0; 
            return (a * b * c) / (4f * area);
        }

        // Function to calculate the inradius (r)
        public static float CalculateInradius(float a, float b, float c)
        {
            float area = CalculateArea(a, b, c);
            float s = (a + b + c) / 2f;
            if (s == 0) return 0; 
            return area / s;

        }
    }
}