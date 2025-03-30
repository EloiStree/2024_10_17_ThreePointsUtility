using UnityEngine;
namespace Eloi.ThreePoints
{
    public class RelocateTriangleRootFromTo
    {

        public static void MoveTo(
            Transform rootToMove,
            ThreePointsMono_Transform3 fromTransform,
            ThreePointsMono_Transform3 toTransform

            )
        {
            I_ThreePointsGet from = fromTransform.m_triangle;
            I_ThreePointsGet to = toTransform.m_triangle;

        }
        public static void MoveTo(
            Transform rootToMove,
            I_ThreePointsGet fromTri,
            I_ThreePointsGet toTri
            )
        {
            Vector3 rootToMoveStartPoint = rootToMove.position;
            Quaternion rootToMoveStartRotation = rootToMove.rotation;
            ThreePointsTriangleDefault fromRaw=new ThreePointsTriangleDefault(fromTri);
            ThreePointsTriangleDefault toRaw= new ThreePointsTriangleDefault(toTri);

            ThreePointsUtility.GetCentroid(fromTri, out Vector3 from);
            ThreePointsUtility.GetCentroid(toTri, out Vector3 to);
            Vector3 translateToDestination = to - from;
            Vector3 newPosition = rootToMoveStartPoint + translateToDestination;

            fromRaw.Translate(translateToDestination);
            GetCentroidAndClosestPoint(
                fromRaw, toRaw,
                out Vector3 centroide,
                out Vector3 cfromPoint,
                out Vector3 ctoPoint);
            GetCentroidAndFarestPoint(
                fromRaw, toRaw,
                out Vector3 fcentroide,
                out Vector3 ffromPoint,
                out Vector3 ftoPoint);
            Vector3 forwardFrom = Vector3.Cross(cfromPoint - centroide, ffromPoint - centroide);
            Vector3 forwardTo = Vector3.Cross(ctoPoint - centroide, ftoPoint - centroide);

            GetRotationFromTwoDirection(forwardFrom, cfromPoint - centroide, out Quaternion rotationFrom);
            GetRotationFromTwoDirection(forwardTo, ctoPoint - centroide, out Quaternion rotationTo);

            RelocationUtility.RotateTargetAroundPointMath(
                newPosition, rootToMoveStartRotation,
               centroide, rotationFrom, rotationTo,
               out Vector3 rotatedPosition, out Quaternion newRotation);

            rootToMove.position = rotatedPosition;
            rootToMove.rotation = newRotation;


        }
      
        public static void GetRotationFromTwoDirection(Vector3 forward, Vector3 upward, out Quaternion orientation)
        {
            orientation = Quaternion.LookRotation(forward, upward);
        }

        public static void GetCentroidAndClosestPoint(
            I_ThreePointsGet origine,
            I_ThreePointsGet whereToGo,
            out Vector3 centroide,
            out Vector3 fromPoint,
            out Vector3 toPoint)
        {
            ThreePointsUtility.GetCentroid(whereToGo, out centroide);
            ThreePointsUtility.GetClosestPoint(origine, centroide, out ThreePointCorner _, out fromPoint, out _);
            ThreePointsUtility.GetClosestPoint(whereToGo, centroide, out ThreePointCorner _, out toPoint, out _);
        }
        public static void GetCentroidAndFarestPoint(
            I_ThreePointsGet origine,
            I_ThreePointsGet whereToGo,
            out Vector3 centroide,
            out Vector3 fromPoint, out Vector3 toPoint)
        {
            ThreePointsUtility.GetCentroid(whereToGo, out centroide);
            ThreePointsUtility.GetFarestPoint(origine, centroide, out ThreePointCorner _, out fromPoint, out _);
            ThreePointsUtility.GetFarestPoint(whereToGo, centroide, out ThreePointCorner _, out toPoint, out _);
        }
    }

}