using System;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.ThreePoints {
    public static class TetraRayUtility{


        public static void GetFrom(
            I_ThreePointsGet threePoints,
            Transform transform,
            out STRUCT_TetraRayWithWorld tetraRay,
            float timeDrawDebug)
        {
            GetFrom(threePoints, transform.position, transform.rotation, out tetraRay, timeDrawDebug);
        }
        public static void GetFrom(
            I_ThreePointsGet threePoints,
            Vector3 targetWorldPosition,
            Quaternion targetWorldRotation,
            out STRUCT_TetraRayWithWorld tetraRay,
            float timeDrawDebug)
        {

            Vector3 shortSidePoint;
            Vector3 longSidePoint;
            Vector3 cornerPoint;
            Vector3 footPoint;
            Vector3 upDirection;
            Vector3 forwardDirection;
            Vector3 rightDirection;
            Quaternion localRotationOfPoint;
            Vector3 localPositionOfPoint;

            STRUCT_TetraRayWithWorld m_saved;

            ThreePointsTriangleDefault triangle = new ThreePointsTriangleDefault(threePoints);

            tetraRay = new STRUCT_TetraRayWithWorld();
            triangle.GetThreePoints(out Vector3 worldPointA, out Vector3 worldPointB, out Vector3 worldPointC);
            triangle.GetLongestSideWithFrontCorner(
                out shortSidePoint,
                out longSidePoint,
                out cornerPoint,
                out footPoint,
                out upDirection,
                out forwardDirection,
                out rightDirection
                );

            Quaternion forwardRotationSpace =GetQuaternionFromDirections(rightDirection, upDirection, forwardDirection);
            Vector3 worldPointSpace = footPoint;

            Eloi.RelocationUtility.GetWorldToLocal_DirectionalPoint(
                targetWorldPosition,
                targetWorldRotation,
                worldPointSpace,
                forwardRotationSpace,
                out localPositionOfPoint,
                out localRotationOfPoint
                );

           

            Eloi.RelocationUtility.GetLocalToWorld_DirectionalPoint(
                localPositionOfPoint,
                localRotationOfPoint,
                worldPointSpace,
                forwardRotationSpace,
                out Vector3 worldPosition,
                out Quaternion worldRotation
                );




            if (timeDrawDebug > 0) {

                //Debug Draw direction at footPoint
                Debug.DrawLine(footPoint, footPoint + upDirection, Color.green, timeDrawDebug);
                Debug.DrawLine(footPoint, footPoint + forwardDirection, Color.blue, timeDrawDebug);
                Debug.DrawLine(footPoint, footPoint + rightDirection, Color.red, timeDrawDebug);
            
                //Local Debug
                Debug.DrawLine(Vector3.zero, localPositionOfPoint, Color.yellow, timeDrawDebug);
                Debug.DrawLine(localPositionOfPoint, localPositionOfPoint + localRotationOfPoint * Vector3.forward, Color.blue, timeDrawDebug);
              
                // World Debug
                Debug.DrawLine(worldPointSpace, worldPosition, Color.yellow, timeDrawDebug);
                Debug.DrawLine(worldPosition, worldPosition + worldRotation * Vector3.forward, Color.blue, timeDrawDebug);
            }

            m_saved.m_worldPointA = worldPointA;
            m_saved.m_worldPointB = worldPointB;
            m_saved.m_worldPointC = worldPointC;
            m_saved.m_ray.m_localPositionFromFoot = localPositionOfPoint;
            m_saved.m_ray.m_localRotationFromFoot = localRotationOfPoint;
            m_saved.m_ray.m_longFrontPoint = RelocateToLocal(longSidePoint, worldPointSpace, worldRotation);
            m_saved.m_ray.m_cornerPoint = RelocateToLocal(cornerPoint, worldPointSpace, worldRotation);
            m_saved.m_ray.m_smallBackPoint = RelocateToLocal(shortSidePoint, worldPointSpace, worldRotation);
            m_saved.m_worldPointFoot = footPoint;
            tetraRay = m_saved;

        }

        private static Vector3 RelocateToLocal(Vector3 worldPoint, Vector3 worldPointSpace, Quaternion worldRotation)
        {
            

            GetWorldToLocal_Point(
                worldPoint,
                worldPointSpace,
                worldRotation,
                out Vector3 local
                );

            return local;

        }
        public static void GetWorldToLocal_Point(in Vector3 worldPosition, in Vector3 positionReference, in Quaternion rotationReference, out Vector3 localPosition) =>
          localPosition = Quaternion.Inverse(rotationReference) * (worldPosition - positionReference);

        public static void GetFrom(in STRUCT_TetraRayWithWorld tetraRayWithWorld, out STRUCT_TetraRay tetraRay)
        {
            tetraRay = new STRUCT_TetraRay();
            tetraRay.m_localPositionFromFoot = tetraRayWithWorld.m_ray.m_localPositionFromFoot;
            tetraRay.m_localRotationFromFoot = tetraRayWithWorld.m_ray.m_localRotationFromFoot;
            tetraRay.m_longFrontPoint = tetraRayWithWorld.m_ray.m_longFrontPoint;
            tetraRay.m_cornerPoint = tetraRayWithWorld.m_ray.m_cornerPoint;
            tetraRay.m_smallBackPoint = tetraRayWithWorld.m_ray.m_smallBackPoint;
        }

        public static void GetFrom(STRUCT_TetraRay tetraRay, out STRUCT_TetraRayWithWorld tetraRayWithWorld)
        {
            tetraRayWithWorld = new STRUCT_TetraRayWithWorld();
            tetraRayWithWorld.m_ray.m_localPositionFromFoot = tetraRay.m_localPositionFromFoot;
            tetraRayWithWorld.m_ray.m_localRotationFromFoot = tetraRay.m_localRotationFromFoot;
            tetraRayWithWorld.m_ray.m_longFrontPoint = tetraRay.m_longFrontPoint;
            tetraRayWithWorld.m_ray.m_cornerPoint = tetraRay.m_cornerPoint;
            tetraRayWithWorld.m_ray.m_smallBackPoint = tetraRay.m_smallBackPoint;

            ThreePointsTriangleDefault triangle = new ThreePointsTriangleDefault(tetraRay);
            triangle.GetLongestSideWithFrontCorner(
                out Vector3 shortSidePoint,
                out Vector3 longSidePoint,
                out Vector3 cornerPoint,
                out Vector3 footPoint,
                out Vector3 upDirection,
                out Vector3 forwardDirection,
                out Vector3 rightDirection
            );
            tetraRayWithWorld.m_worldPointA = shortSidePoint;
            tetraRayWithWorld.m_worldPointB = longSidePoint;
            tetraRayWithWorld.m_worldPointC = cornerPoint;
            tetraRayWithWorld.m_worldPointFoot = footPoint;
        }
        public static void GetRelocationOfPointWithTetraRay(
            I_ThreePointsGet triangleGet,
           STRUCT_TetraRayWithWorld input,
           Transform whatToMove)
        {
            if (whatToMove == null)
            {
                return;
            }

            GetRelocationOfPointWithTetraRay(triangleGet, input, out Vector3 worldPosition, out Quaternion worldRotation);
            whatToMove.position = worldPosition;
            whatToMove.rotation = worldRotation;
        }
        public static void GetRelocationOfPointWithTetraRay(I_ThreePointsGet triangleGet,
            STRUCT_TetraRayWithWorld input,
            out Vector3 worldPosition,
            out Quaternion worldRotation)
        {


            ThreePointsTriangleDefault triangle = new ThreePointsTriangleDefault(triangleGet);
            triangle.GetLongestSideWithFrontCorner(
                out Vector3 shortSidePoint,
                out Vector3 longSidePoint,
                out Vector3 cornerPoint,
                out Vector3 footPoint,
                out Vector3 upDirection,
                out Vector3 forwardDirection,
                out Vector3 rightDirection
                );


            Quaternion forwardRotationSpace= GetQuaternionFromDirections(rightDirection, upDirection, forwardDirection);


            Debug.DrawLine(footPoint, footPoint + upDirection, Color.cyan, 0.5f);

            Vector3 worldPointSpace = footPoint;

            GetLocalToWorld_DirectionalPoint(
               input.m_ray.m_localPositionFromFoot,
               input.m_ray.m_localRotationFromFoot,
               worldPointSpace,
               forwardRotationSpace,
               out  worldPosition,
               out  worldRotation,
               false
               );


            Debug.DrawLine(footPoint, footPoint + upDirection, Color.green, Time.deltaTime);
            Debug.DrawLine(footPoint, footPoint + forwardDirection, Color.blue, Time.deltaTime);
            Debug.DrawLine(footPoint, footPoint + rightDirection, Color.red, Time.deltaTime);
            Debug.DrawLine(worldPointSpace, worldPosition, Color.yellow, Time.deltaTime);
            Debug.DrawLine(worldPosition, worldPosition + worldRotation * Vector3.forward, Color.blue, Time.deltaTime);


        }

        public static Quaternion GetQuaternionFromDirections(Vector3 rightDirection, Vector3 upDirection, Vector3 forwardDirection)
        {
            // Normalize the direction vectors
            rightDirection.Normalize();
            upDirection.Normalize();
            forwardDirection.Normalize();

            // Create the rotation matrix using the right, up, and forward directions
            Matrix4x4 rotationMatrix = new Matrix4x4();
            rotationMatrix.SetColumn(0, rightDirection);   // Right vector as the first column (X axis)
            rotationMatrix.SetColumn(1, upDirection);      // Up vector as the second column (Y axis)
            rotationMatrix.SetColumn(2, forwardDirection); // Forward vector as the third column (Z axis)

            // Convert the rotation matrix to a quaternion
            Quaternion rotationQuaternion = rotationMatrix.rotation;

            return rotationQuaternion;
        }
        public static void GetLocalToWorld_DirectionalPoint(
            in Vector3 localPosition,
            in Quaternion localRotation, 
            in Vector3 positionReference,
            in Quaternion rotationReference,
            out Vector3 worldPosition,
            out Quaternion worldRotation, bool inverse=false)
        {
            worldRotation =inverse? localRotation* rotationReference : rotationReference*localRotation;
            worldPosition = (rotationReference * localPosition) + (positionReference);
        }

    }
}

