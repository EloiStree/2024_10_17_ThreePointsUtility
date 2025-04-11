using Eloi.ThreePoints;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.ThreePoints {

    public static class TetraRayDrawUtility
    { 

        public static void Draw(STRUCT_TetraRayWithWorld tetraRay, float timeDraw)
        {
            Draw(tetraRay, timeDraw, new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value,1));
        }
        public static void Draw(STRUCT_TetraRayWithWorld tetraRay, float timeDraw, Color color)
        {
            tetraRay.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
            Vector3 startGround, middleGround, endGround;
            startGround = new Vector3(start.x, 0, start.z);
            middleGround = new Vector3(middle.x, 0, middle.z);
            endGround = new Vector3(end.x, 0, end.z);
            Debug.DrawLine(startGround, middleGround, Color.yellow, timeDraw);
            Debug.DrawLine(middleGround, endGround, Color.yellow, timeDraw);
            Debug.DrawLine(startGround, endGround, Color.yellow, timeDraw);

            Debug.DrawLine(start, middle, color, timeDraw);
            Debug.DrawLine(middle, end, color, timeDraw);
            Debug.DrawLine(start, end, color, timeDraw);

            Debug.DrawLine(tetraRay.m_worldPointFoot, tetraRay.m_worldPointA, color, timeDraw);
            Debug.DrawLine(tetraRay.m_worldPointFoot, tetraRay.m_worldPointB, color, timeDraw);
            Debug.DrawLine(tetraRay.m_worldPointFoot, tetraRay.m_worldPointC, color, timeDraw);
            Debug.DrawLine(tetraRay.m_worldPointA, tetraRay.m_worldPointB, color, timeDraw);
            Debug.DrawLine(tetraRay.m_worldPointA, tetraRay.m_worldPointC, color, timeDraw);
            Debug.DrawLine(tetraRay.m_worldPointB, tetraRay.m_worldPointC, color, timeDraw);

        }

    }
    
    public class ThreePointsMono_TransformToWorldTetraRay : MonoBehaviour
    {

        public Transform m_targetToAffect;
        public ThreePointsTriangleDefault m_currentState;
        public STRUCT_ThreePointsSaveMillimeters m_saveState;
        public string m_currentSaveState;
        public STRUCT_TetraRayWithWorld m_tetraRay;
        public UnityEvent<STRUCT_TetraRayWithWorld> m_tetraRayEvent;

        public void PushInCurrentGuarianState(I_ThreePointsGet triangle)
        {
            m_currentState.SetThreePoints(triangle);
            m_saveState.SetWith(triangle);
            m_saveState.GetSaveAsOneLiner(out m_currentSaveState);

            TetraRayUtility.GetFrom(triangle, m_targetToAffect, out m_tetraRay, 10);
            m_tetraRayEvent.Invoke(m_tetraRay);

            TetraRayDrawUtility.Draw(m_tetraRay, 10);
        }
    }

    [System.Serializable]
    public class ThreePoints_TetraRayListRegister
    {
        public string m_dataBaseGuid;
        public List<STRUCT_TetraRayWithWorld> m_listTetraRay = new List<STRUCT_TetraRayWithWorld>();
        public float m_similarityThresholdMeter = 0.05f;
        public float m_similarityThresholdDegree = 5f;

        public void GenerateNewGuid()
        {
            m_dataBaseGuid = Guid.NewGuid().ToString();
        }
        public void SaveTetraRayWithDefaultThreshold(STRUCT_TetraRayWithWorld tetraRay) { 
        
            SaveTetraRay(tetraRay, m_similarityThresholdMeter, m_similarityThresholdDegree);
        }

       

        public void SaveTetraRay(STRUCT_TetraRayWithWorld tetraRay, float thresholdMeter, float thresholdDegeree)
        {
            int alreadyInIndex = -1;
            bool foundAlreadyExisting = false;
            for (int i = 0; i < m_listTetraRay.Count; i++)
            {
                ThreePointsUtility.AreSimilar(tetraRay, m_listTetraRay[i],
                    m_similarityThresholdMeter,
                    m_similarityThresholdDegree,
                    out bool areSimilar);
                if (areSimilar)
                {
                    foundAlreadyExisting = true;
                    alreadyInIndex = i;
                    break;
                }

            }
            if (foundAlreadyExisting)
            {
                m_listTetraRay[alreadyInIndex] = tetraRay;
                Debug.Log($"Update TetraRay {alreadyInIndex}");
            }
            else
            {
                m_listTetraRay.Add(tetraRay);
                Debug.Log($"Add TetraRay {m_listTetraRay.Count}");
            }

        }
        public void FlushDataBase()
        {
            m_listTetraRay.Clear();
        }

        public void TryToFindSimilarTo(I_ThreePointsGet triangle, out List<STRUCT_TetraRayWithWorld> listTetraRay)
        {

            if (triangle==null )
            {
                listTetraRay = new List<STRUCT_TetraRayWithWorld>();
                return;
            }


            listTetraRay = new List<STRUCT_TetraRayWithWorld>();
            STRUCT_TetraRayWithWorld tetraRay;
            for (int i = 0; i < m_listTetraRay.Count; i++)
            {
                I_ThreePointsGet ray = (I_ThreePointsGet)m_listTetraRay[i];
                ThreePointsUtility.AreSimilar(triangle, ray,
                    m_similarityThresholdMeter,
                    m_similarityThresholdDegree,
                    out bool areSimilar);
                if (areSimilar)
                {
                    listTetraRay.Add(m_listTetraRay[i]);
                }
            }
        }
    }

    [System.Serializable]
    public struct STRUCT_ThreePointsSaveMillimeters
    {
        public void SetWith(I_ThreePointsGet triangle) {

            triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
            m_startMiddleMM = (int)(Vector3.Distance(start, middle) * 1000);
            m_middleEndMM = (int)(Vector3.Distance(middle, end) * 1000);
            m_startEndMM = (int)(Vector3.Distance(start, end) * 1000);
            m_angleStartDegree100 = (int)(AngleBetween( middle, start, end) * 100);
            m_angleMiddleDegree100 = (int)(AngleBetween( start,middle, end) * 100);
            m_angleEndDegree100 = (int)(AngleBetween(start, end, middle) * 100);
        }

        private float AngleBetween(Vector3 start, Vector3 middle, Vector3 end)
        {
            Vector3 startToMiddle = middle - start;
            Vector3 endToMiddle = middle - end;

            float angle = Vector3.Angle(startToMiddle, endToMiddle);
            return angle;
        }
        public void GetSaveAsOneLiner(out string lineSave) {

            lineSave = $"{m_startMiddleMM}|{m_middleEndMM}|{m_startEndMM}|{m_angleStartDegree100}|{m_angleMiddleDegree100}|{m_angleEndDegree100}";
        }
        public void SetWithSaveAsOneLiner( string lineSave)
        {
            string[] values = lineSave.Split('|');
            if (values.Length >= 1) m_startMiddleMM = int.Parse(values[0]);
            if (values.Length >= 2) m_middleEndMM = int.Parse(values[1]);
            if (values.Length >= 3) m_startEndMM = int.Parse(values[2]);
            if (values.Length >= 4) m_angleStartDegree100 = int.Parse(values[3]);
            if (values.Length >= 5) m_angleMiddleDegree100 = int.Parse(values[4]);
            if (values.Length >= 6) m_angleEndDegree100 = int.Parse(values[5]);
        }

        public int m_startMiddleMM;
        public int m_middleEndMM;
        public int m_startEndMM;
        public int m_angleStartDegree100;
        public int m_angleMiddleDegree100;
        public int m_angleEndDegree100;
    }
}

