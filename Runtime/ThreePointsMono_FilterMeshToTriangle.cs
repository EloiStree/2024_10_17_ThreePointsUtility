using Eloi;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.ThreePoints { 
public class ThreePointsMono_FilterMeshToTriangle : MonoBehaviour

{
    public bool m_disableGameobjectWhenFound;
    public Transform m_xrRoot;
    public MeshFilter m_meshFilterGiven;
    public Mesh m_meshOfTheGuardian;

    [Header("Cube Zone from Mesh")]
    public Vector3 m_leftDownPointCubeLocal;
    public Vector3 m_rightUpPointCubeLocal;
    public int m_cubeWidthInMM;
    public int m_cubeHeightInMM;
    public int m_cubeDepthInMM;

    [Header("Triangle Zone from Mesh")]
    public Vector3 m_downLeftMiddleTriangleLocal;
    public Vector3 m_upRightStartTriangleLocal;
    public Vector3 m_upLeftFrontEndTriangleLocal;
    public int m_startMiddleMM;
    public int m_endMiddleMM;
    public int m_startEndMM;
    public int m_lengthInMillimeter;
    public UnityEvent<int> m_onLengthMillimeterTriangleAsId;
    public UnityEvent<Vector3, Vector3, Vector3> m_onMeshToTriangleId;
    public float m_meshScaleFactorToMeter = 100f;




    [Header("World Point Zone from Mesh")]
    public Vector3 m_leftDownPointCubeWorld;
    public Vector3 m_upRightPointCubeWorld;
    public Vector3 m_upRightStartTriangleWorld;
    public Vector3 m_downLeftMiddleTriangleWorld;
    public Vector3 m_upLeftFrontEndTriangleWorld;
    public bool m_useDebugDraw = true;


    [Header("Use of Transform")]
    public Transform m_startPoint;
    public Transform m_middlePoint;
    public Transform m_endPoint;

    [ContextMenu("Push from inspector")]
    public void PushInFromInsepector() {

        PushIn(m_meshFilterGiven);
    }

    public void PushIn(MeshFilter meshFilter)
    {
        m_meshFilterGiven = meshFilter;
        Mesh mesh = meshFilter.sharedMesh;

        if (mesh != null)
        {

            m_meshOfTheGuardian = mesh;
            Vector3[] vertices = mesh.vertices;
            Vector3 min = vertices[0];
            Vector3 max = vertices[0];
            foreach (Vector3 vertex in vertices)
            {

                min = Vector3.Min(min, vertex * m_meshScaleFactorToMeter);
                max = Vector3.Max(max, vertex * m_meshScaleFactorToMeter);
            }
            m_leftDownPointCubeLocal = min;
            m_rightUpPointCubeLocal = max;

            m_cubeWidthInMM = (int)((max.x - min.x) * 1000f);
            m_cubeHeightInMM = (int)((max.y - min.y) * 1000f);
            m_cubeDepthInMM = (int)((max.z - min.z) * 1000f);

            m_downLeftMiddleTriangleLocal = new Vector3(min.x, min.y, min.z);
            m_upRightStartTriangleLocal = new Vector3(max.x, max.y, min.z);
            m_upLeftFrontEndTriangleLocal = new Vector3(min.x, max.y, max.z);

            m_startMiddleMM = (int)(1000f * Vector3.Distance(m_upRightStartTriangleLocal, m_downLeftMiddleTriangleLocal));
            m_endMiddleMM = (int)(1000f * Vector3.Distance(m_upLeftFrontEndTriangleLocal, m_downLeftMiddleTriangleLocal));
            m_startEndMM = (int)(1000f * Vector3.Distance(m_upRightStartTriangleLocal, m_upLeftFrontEndTriangleLocal));
            m_lengthInMillimeter = m_startMiddleMM + m_endMiddleMM + m_startEndMM;

           
            m_onLengthMillimeterTriangleAsId.Invoke(m_lengthInMillimeter);
            m_onMeshToTriangleId.Invoke(m_upRightStartTriangleLocal, m_downLeftMiddleTriangleLocal, m_upLeftFrontEndTriangleLocal);


            Transform meshFilterTransform = m_meshFilterGiven.transform;
            RelocationUtility.GetLocalToWorld_Point(
                m_upRightStartTriangleLocal, meshFilterTransform, out m_upRightStartTriangleWorld);
            RelocationUtility.GetLocalToWorld_Point(
                m_downLeftMiddleTriangleLocal, meshFilterTransform, out m_downLeftMiddleTriangleWorld);
            RelocationUtility.GetLocalToWorld_Point(
                m_upLeftFrontEndTriangleLocal, meshFilterTransform, out m_upLeftFrontEndTriangleWorld);
            RelocationUtility.GetLocalToWorld_Point(
                m_leftDownPointCubeLocal, meshFilterTransform, out m_leftDownPointCubeWorld);
            RelocationUtility.GetLocalToWorld_Point(
                m_rightUpPointCubeLocal, meshFilterTransform, out m_upRightPointCubeWorld);


            if (m_startPoint != null)
                m_startPoint.position = m_upRightStartTriangleWorld;
            if (m_middlePoint != null)
                m_middlePoint.position = m_downLeftMiddleTriangleWorld;
            if (m_endPoint != null)
                m_endPoint.position = m_upLeftFrontEndTriangleWorld;


        }
    }
    public void Update()
    {
        if (m_useDebugDraw)
        {
            DrawLineWithTime(Time.deltaTime);

        }
    }

    private void DrawLineWithTime(float time)
    {
        Debug.DrawLine(m_downLeftMiddleTriangleWorld, m_upRightStartTriangleWorld, Color.red            , time);
        Debug.DrawLine(m_upRightStartTriangleWorld, m_upLeftFrontEndTriangleWorld, Color.red            , time);
        Debug.DrawLine(m_upLeftFrontEndTriangleWorld, m_downLeftMiddleTriangleWorld, Color.red          , time);
        Debug.DrawLine(m_xrRoot.transform.position, m_upRightStartTriangleWorld, Color.green    , time);
        Debug.DrawLine(m_xrRoot.transform.position, m_downLeftMiddleTriangleWorld, Color.green  , time);
        Debug.DrawLine(m_xrRoot.transform.position, m_upLeftFrontEndTriangleWorld, Color.green  , time);
        Debug.DrawLine(m_leftDownPointCubeWorld, m_upRightPointCubeWorld, Color.yellow  , time);
    }
}
}