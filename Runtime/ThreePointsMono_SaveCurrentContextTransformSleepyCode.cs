using Eloi.ThreePoints;
using System;
using UnityEngine;
using UnityEngine.Events;
public class ThreePointsMono_SaveCurrentContextTransformSleepyCode : MonoBehaviour
{
    public ThreePointsMono_Transform3 m_context;
    public Transform m_targetTransform;
    public ThreePointsTriangleDefault m_lastTriangle;


    public Vector3 shortSidePoint;
    public Vector3 longSidePoint;
    public Vector3 cornerPoint;
    public Vector3 footPoint;
    public Vector3 upDirection;
    public Vector3 forwardDirection;
    public Vector3 rightDirection;
    public Quaternion localRotationOfPoint;
    public Vector3 localPositionOfPoint;

    public STRUCT_TetraRayWithWorld m_saved;
    public UnityEvent<STRUCT_TetraRayWithWorld> m_savedContext;

    public bool m_updateRefresh;

    private void Update()
    {
        if (m_updateRefresh)
        {
            Refresh(Time.deltaTime);
        }
    }



    [ContextMenu("Refresh")]
    public void RefreshInEditor() {

        Refresh(10);
    }


    public void Refresh(float timeDraw) {
        ThreePointsTriangleDefault triangle = new ThreePointsTriangleDefault(m_context);
        m_lastTriangle = triangle;
        triangle.GetThreePoints(out Vector3 worldPointA, out Vector3 worldPointB, out Vector3 worldPointC);
        m_lastTriangle.GetLongestSideWithFrontCorner(
            out shortSidePoint,
            out longSidePoint,
            out cornerPoint,
            out footPoint,
            out upDirection,
            out forwardDirection,
            out rightDirection
            );

        Debug.DrawLine(footPoint, footPoint + upDirection, Color.green, timeDraw);
        Debug.DrawLine(footPoint, footPoint + forwardDirection, Color.blue, timeDraw);
        Debug.DrawLine(footPoint, footPoint + rightDirection, Color.red, timeDraw);
        Quaternion forwardRotationSpace = Quaternion.LookRotation(forwardDirection, Vector3.up);
        Vector3 worldPointSpace = footPoint;

        Eloi.RelocationUtility.GetWorldToLocal_DirectionalPoint(
            m_targetTransform.position,
            m_targetTransform.rotation,
            worldPointSpace,
            forwardRotationSpace,
            out localPositionOfPoint,
            out localRotationOfPoint
            );

        Debug.DrawLine(Vector3.zero, localPositionOfPoint, Color.yellow, timeDraw);
        Debug.DrawLine(localPositionOfPoint, localPositionOfPoint+ localRotationOfPoint * Vector3.forward, Color.blue, timeDraw);

        Eloi.RelocationUtility.GetLocalToWorld_DirectionalPoint(
            localPositionOfPoint,
            localRotationOfPoint,
            worldPointSpace,
            forwardRotationSpace,
            out Vector3 worldPosition,
            out Quaternion worldRotation
            );

        Debug.DrawLine(worldPointSpace, worldPosition, Color.yellow, timeDraw);
        Debug.DrawLine(worldPosition, worldPosition + worldRotation * Vector3.forward, Color.blue, timeDraw);

        m_saved.m_worldPointA = worldPointA;
        m_saved.m_worldPointB = worldPointB;
        m_saved.m_worldPointC = worldPointC;
        m_saved.m_ray.m_localPositionFromFoot = localPositionOfPoint;
        m_saved.m_ray.m_localRotationFromFoot = localRotationOfPoint;
        m_saved.m_ray.m_longFrontPoint = Relocate(longSidePoint, worldPointSpace, worldRotation);
        m_saved.m_ray.m_cornerPoint = Relocate(cornerPoint, worldPointSpace, worldRotation);
        m_saved.m_ray.m_smallBackPoint = Relocate(shortSidePoint, worldPointSpace, worldRotation);
        m_savedContext.Invoke(m_saved);
    }

    private Vector3 Relocate(Vector3 worldPoint, Vector3 worldPointSpace, Quaternion worldRotation)
    {
        Eloi.RelocationUtility.GetWorldToLocal_Point(
            worldPoint,
            worldPointSpace,
            worldRotation,
            out Vector3 local);
        return local;

    }
}


/// <summary>
/// TETRAHEDRON LONG SIDE FOOT POSITION AND ROTATION OF A TRIANGLE
/// Make a line from the opposite corner to the longest line. From this line, you can create a front line on the bigest side of the triangle.
/// Now that you have a top and front line, you can create a plane. From the cross section of the line, You can create a 3D space to the "right".
/// Relocate the Unity3D world rotation and position in that space. 
/// </summary>
[System.Serializable]
public struct STRUCT_TetraRayLit
{
    /// <summary>
    /// Represent  the position from the boot a perpendicular line to the longest side of the triangle on the short side of the triangle to the right
    /// </summary>
    public Vector3 m_localPositionFromFoot;
    public Quaternion m_localRotationFromFoot;
}

[System.Serializable]
/// <summary>
/// TETRAHEDRON LONG SIDE FOOT POSITION AND ROTATION OF A TRIANGLE WITH SOURCE
/// </summary>
public struct STRUCT_TetraRayLitWithWorld: I_ThreePointsGet
{
    public Vector3 m_worldPointA;
    public Vector3 m_worldPointB;
    public Vector3 m_worldPointC;
    public STRUCT_TetraRayLit m_ray;

    public void GetPoint(ThreePointCorner corner, out Vector3 point)
    {
        switch (corner)
        {
            case ThreePointCorner.End:
                point = m_worldPointA;
                break;
            case ThreePointCorner.Middle:
                point = m_worldPointB;
                break;
            case ThreePointCorner.Start:
                point = m_worldPointC;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(corner), corner, null);
        }
    }

    public void GetPoints(out Vector3[] arrayOf3)
    {
        arrayOf3 = new Vector3[3];
        arrayOf3[0] = m_worldPointA;
        arrayOf3[1] = m_worldPointB;
        arrayOf3[2] = m_worldPointC;
    }

    public void GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end)
    {
        start = m_worldPointA;
        middle = m_worldPointB;
        end = m_worldPointC;
    }
}

/// <summary>
/// TETRAHEDRON LONG SIDE FOOT POSITION AND ROTATION OF A TRIANGLE
/// I am a class that allows the reconstruction of a tetrahedron from a perpenddiculatr line on the longest edge of a base.
/// It allows to store the position and quaternion rotatoin of a point located in a triangular space.
/// </summary>
/// 
[System.Serializable]
public struct STRUCT_TetraRay : I_ThreePointsGet
{

    //Represent the front point that was with a biggest lenght use to generate the tetrafoot
    public Vector3 m_longFrontPoint;
    //Represent the "top" point opposed to the longest edge
    public Vector3 m_cornerPoint;
    // Represent the back point that was with a smallest lenght use to generate the tetrafoot
    public Vector3 m_smallBackPoint;

    /// <summary>
    /// Represent  the position from the boot a perpendicular line to the longest side of the triangle on the short side of the triangle to the right
    /// </summary>
    public Vector3 m_localPositionFromFoot;
    public Quaternion m_localRotationFromFoot;

    public void GetPoint(ThreePointCorner corner, out Vector3 point)
    {
        switch (corner)
        {
            case ThreePointCorner.End:
                point = m_longFrontPoint;
                break;
            case ThreePointCorner.Middle:
                point = m_cornerPoint;
                break;
            case ThreePointCorner.Start:
                point = m_smallBackPoint;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(corner), corner, null);
        }
    }

    public void GetPoints(out Vector3[] arrayOf3)
    {
        arrayOf3 = new Vector3[3];

        arrayOf3[0] = 
            m_smallBackPoint;
        arrayOf3[1] =
            m_cornerPoint;
        arrayOf3[2] = 
            m_longFrontPoint;
    }

    public void GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end)
    {
        start = m_smallBackPoint;
        middle = m_cornerPoint;
        end = m_longFrontPoint;
    }
}

/// <summary>
/// TETRAHEDRON LONG SIDE FOOT POSITION AND ROTATION OF A TRIANGLE WITH SOURCE
/// </summary>
/// 
[System.Serializable]
public struct STRUCT_TetraRayWithWorld : I_ThreePointsGet {
    public Vector3 m_worldPointA;
    public Vector3 m_worldPointB;
    public Vector3 m_worldPointC;
    public Vector3 m_worldPointFoot;
    public STRUCT_TetraRay m_ray;

    public void GetPoint(ThreePointCorner corner, out Vector3 point)
    {
       m_ray.GetPoint(corner, out point);
    }

    public void GetPoints(out Vector3[] arrayOf3)
    {
        m_ray.GetPoints(out arrayOf3);
    }

    public void GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end)
    {
        m_ray.GetThreePoints(out start, out middle, out end);

    }
}




public class ThreePointsMono_DebugDrawTetraRayWithSource : MonoBehaviour { 

    public STRUCT_TetraRayWithWorld m_tetraRay;
    public Color m_edgeColor = Color.red;
    private void Update()
    {
        float deltaTime = Time.deltaTime;
        Debug.DrawLine(m_tetraRay.m_worldPointA, m_tetraRay.m_worldPointB, m_edgeColor, deltaTime);
        Debug.DrawLine(m_tetraRay.m_worldPointB, m_tetraRay.m_worldPointC, m_edgeColor, deltaTime);
        Debug.DrawLine(m_tetraRay.m_worldPointC, m_tetraRay.m_worldPointA, m_edgeColor, deltaTime);
        Debug.DrawLine(m_tetraRay.m_ray.m_longFrontPoint, m_tetraRay.m_ray.m_cornerPoint, m_edgeColor, deltaTime);
        Debug.DrawLine(m_tetraRay.m_ray.m_longFrontPoint, m_tetraRay.m_ray.m_smallBackPoint, m_edgeColor, deltaTime);
        Debug.DrawLine(m_tetraRay.m_ray.m_cornerPoint, m_tetraRay.m_ray.m_smallBackPoint, m_edgeColor, deltaTime);
    }
}
