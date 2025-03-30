using System;
using UnityEngine;

namespace Eloi.ThreePoints {


    public static class TennisTableSize
    {
        public static readonly float TableWidth = 1.525f;
        public static readonly float TableDepth = 2.74f;
        public static readonly float GroundTableHeight = 0.76f;
        public static readonly float NetHeight = 0.1525f;
        public static readonly float NetWidth = 1.83f;
        public static readonly float NetDepth = 0.1525f;
        public static readonly ThreePointsTriangleDefault TableTriPoints = new ThreePointsTriangleDefault(new Vector3(TableWidth, GroundTableHeight, 0), new Vector3(0, GroundTableHeight, 0), new Vector3(TableDepth, GroundTableHeight, 0));
    }
    public enum APaperEnum {
    A0, A1, A2, A3, A4, A5, A6, A7, A8, A9, A10
    }
    public static class APaper {

        public static void GetDimensionMM(APaperEnum type, out int widthMM, out int heightMM)
        {
            widthMM = 0;
            heightMM = 0;
            switch (type)
            {
                case APaperEnum.A0:
                    widthMM = A0WidthMM;
                    heightMM = A0HeightMM;
                    break;

                case APaperEnum.A1:
                    widthMM = A1WidthMM;
                    heightMM = A1HeightMM;
                    break;
                case APaperEnum.A2:
                    widthMM = A2WidthMM;
                    heightMM = A2HeightMM;
                    break;

                case APaperEnum.A3:
                    widthMM = A3WidthMM;
                    heightMM = A3HeightMM;
                    break;
                case APaperEnum.A4:
                    widthMM = A4WidthMM;
                    heightMM = A4HeightMM;
                    break;
                case APaperEnum.A5:
                    widthMM = A5WidthMM;
                    heightMM = A5HeightMM;
                    break;
                case APaperEnum.A6:
                    widthMM = A6WidthMM;
                    heightMM = A6HeightMM;
                    break;
                case APaperEnum.A7:
                    widthMM = A7WidthMM;
                    heightMM = A7HeightMM;
                    break;
                case APaperEnum.A8:
                    widthMM = A8WidthMM;
                    heightMM = A8HeightMM;
                    break;
                case APaperEnum.A9:
                    widthMM = A9WidthMM;
                    heightMM = A9HeightMM;
                    break;
                case APaperEnum.A10:
                    widthMM = A10WidthMM;
                    heightMM = A10HeightMM;
                    break;
                default:
                    widthMM = 0;
                    heightMM = 0;
                    break;

            }
        }
        public static void GetDimensionCM(APaperEnum type, out float widthCM, out float heightCM)
        {
            int widthMM;
            int heightMM;
            GetDimensionMM(type, out widthMM, out heightMM);
            widthCM = widthMM / 10;
            heightCM = heightMM / 10;
        }
        public static void GetDimensionCM(APaperEnum type, out float widthCM, out float heightCM, out float hypotenus) {

            GetDimensionCM(type, out widthCM, out heightCM);
            hypotenus = Mathf.Sqrt(widthCM * widthCM + heightCM * heightCM);
        }
        public static void GetDimensionMM(APaperEnum type, out int widthMM, out int heightMM, out int hypotenus)
        {
            GetDimensionMM(type, out widthMM, out heightMM);
            hypotenus = Mathf.RoundToInt(Mathf.Sqrt(widthMM * widthMM + heightMM * heightMM));
        }

        public static void GetDimensionMeter(APaperEnum paperType, out float widthMeter, out float heightMeter)
        {
            GetDimensionMM(paperType, out int widthMM, out int heightMM);
            widthMeter = widthMM / 1000f;
            heightMeter = heightMM / 1000f;
        }
        public static void GetDimensionMeter(APaperEnum paperType, out float widthMeter, out float heightMeter, out float hypotenusMeter)
        {
            GetDimensionMeter(paperType, out widthMeter, out heightMeter);
            hypotenusMeter = Mathf.Sqrt(widthMeter * widthMeter + heightMeter * heightMeter);
        }

        public static int Hypothenus(int widthMM, int heightMM)
        {
           return  Mathf.RoundToInt(Mathf.Sqrt(widthMM * widthMM + heightMM * heightMM));
        }
        public static readonly int A0WidthMM= 841;
        public static readonly int A0HeightMM = 1189;
        public static readonly int A0HypotenuseMM = Hypothenus(A0WidthMM, A0HeightMM);
        public static readonly int A1WidthMM = 594;
        public static readonly int A1HeightMM = 841;
        public static readonly int A1HypotenuseMM = Hypothenus(A1WidthMM, A1HeightMM);

        public static readonly int A2WidthMM = 420;
        public static readonly int A2HeightMM = 594;
        public static readonly int A2HypotenuseMM = Hypothenus(A2WidthMM, A2HeightMM);

        public static readonly int A3WidthMM = 297;
        public static readonly int A3HeightMM = 420;
        public static readonly int A3HypotenuseMM = Hypothenus(A3WidthMM, A3HeightMM);

        public static readonly int A4WidthMM = 210;
        public static readonly int A4HeightMM = 297;
        public static readonly int A4HypotenuseMM = Hypothenus(A4WidthMM, A4HeightMM);


        public static readonly int A5WidthMM = 148;
        public static readonly int A5HeightMM = 210;
        public static readonly int A5HypotenuseMM = Hypothenus(A5WidthMM, A5HeightMM);

        public static readonly int A6WidthMM = 105;
        public static readonly int A6HeightMM = 148;
        public static readonly int A6HypotenuseMM = Hypothenus(A6WidthMM, A6HeightMM);

        public static readonly int A7WidthMM = 74;
        public static readonly int A7HeightMM = 105;
        public static readonly int A7HypotenuseMM = Hypothenus(A7WidthMM, A7HeightMM);

        public static readonly int A8WidthMM = 52;
        public static readonly int A8HeightMM = 74;
        public static readonly int A8HypotenuseMM = Hypothenus(A8WidthMM, A8HeightMM);

        public static readonly int A9WidthMM = 37;
        public static readonly int A9HeightMM = 52;
        public static readonly int A9HypotenuseMM = Hypothenus(A9WidthMM, A9HeightMM);

        public static readonly int A10WidthMM = 26;
        public static readonly int A10HeightMM = 37;
        public static readonly int A10HypotenuseMM = Hypothenus(A10WidthMM, A10HeightMM);
    }
public class TDD_StaticThreePointsFilter : MonoBehaviour
{
        public ThreePointsMono_Transform3 m_source;
        public bool m_isSquare;
        public float m_bigAngle;
        public float m_minSquareEdge;
        public float m_maxSquareEdge;
        public float m_hypotenus;
        public bool m_isA0;
        public bool m_isA1;
        public bool m_isA2;
        public bool m_isA3;
        public bool m_isA4;
        public bool m_isA5;
        public bool m_isTennisTable;
        private bool m_isAll33Degree;
        public float m_tolerenceAngle=5;
        public float m_tolerenceEdge=0.05f;
        public bool m_isPoint = false;
        public float m_maxSizeToBePoint = 0.06f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            RefreshInfo();

        }

        [ContextMenu("Refresh Info")]
        private void RefreshInfo()
        {
            if (m_source == null)
                return;
            m_source.GetDistanceAngle(out I_ThreePointsDistanceAngleGet computed);
            m_isSquare = ThreePointsUtility.IsOneAngleAlmostOf90Degree(computed, m_tolerenceAngle);
            m_bigAngle = ThreePointsUtility.GetBiggestAngle(computed);
            ThreePointsUtility.GetOrderedEdgeDistance(computed, out float max, out float middle, out float min);
            ThreePointsUtility.GetOrderedAngle(computed, out float maxAngle, out float middleAngle, out float minAngle);
            ThreePointsUtility.GetHypothenus(computed, out float hypothenusDistance);
            m_hypotenus = hypothenusDistance;
            ThreePointsUtility.GetBordersEdgeOfHypotenus(computed, out m_maxSquareEdge, out m_minSquareEdge);
            m_isA0 = ThreePointsUtility.IsAlmostAPaperInMeter(computed, APaperEnum.A0, m_tolerenceEdge);
            m_isA1 = ThreePointsUtility.IsAlmostAPaperInMeter(computed, APaperEnum.A1, m_tolerenceEdge);
            m_isA2 = ThreePointsUtility.IsAlmostAPaperInMeter(computed, APaperEnum.A2, m_tolerenceEdge);
            m_isA3 = ThreePointsUtility.IsAlmostAPaperInMeter(computed, APaperEnum.A3, m_tolerenceEdge);
            m_isA4 = ThreePointsUtility.IsAlmostAPaperInMeter(computed, APaperEnum.A4, m_tolerenceEdge);
            m_isA5 = ThreePointsUtility.IsAlmostAPaperInMeter(computed, APaperEnum.A5, m_tolerenceEdge);
            m_isTennisTable = ThreePointsUtility.HasAlmostTheSameEdge(computed, TennisTableSize.TableTriPoints, m_tolerenceEdge);

            m_isAll33Degree = ThreePointsUtility.IsAllAngleAlmostOf33Degree(computed, m_tolerenceAngle);
            ThreePointsUtility.GetMaxMinRadius(computed, out float maxRadius, out float minRadius);
            m_isPoint= ThreePointsUtility.IsMaxRadisuSmallerThat(computed, m_maxSizeToBePoint);

            // Check if in a group There is A4, A3 paper
            // Check in a list if there a tennis table
            // Check in  a list if it has almost the angle wanted
            // Check in a list if it has almost the same edge lenght
            // Check in a list if it has almost a 90 angle
            // Check in a list the triangle distance size
            // Check in a list the square distance size
            // Check if smaller that a certain size
            // Check if almost perfect 33 angle.
        }



        // Update is called once per frame
        void Update()
    {

            RefreshInfo();        
    }
}
}


