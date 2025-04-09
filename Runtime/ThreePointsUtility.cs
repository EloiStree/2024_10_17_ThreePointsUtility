using System;
using UnityEngine;

namespace Eloi.ThreePoints
{
    public static class ThreePointsUtility

    {

        public static string GetStringOfAnglesInDegree(I_ThreePointsDistanceAngleGet triangle)
        {
            triangle.GetCornerAngle(ThreePointCorner.Start, out float angle);
            triangle.GetCornerAngle(ThreePointCorner.Middle, out float angle2);
            triangle.GetCornerAngle(ThreePointCorner.End, out float angle3);
            return string.Format("A(°): {0:0.0} {1:0.0} {2:0.0}", angle, angle2, angle3);
        }
        public static string GetStringOfDistancesInCm(I_ThreePointsDistanceAngleGet triangle)
        {
            triangle.GetSegmentDistance(ThreePointSegment.StartMiddle, out float distance0);
            triangle.GetSegmentDistance(ThreePointSegment.MiddleEnd, out float distance1);
            triangle.GetSegmentDistance(ThreePointSegment.StartEnd, out float distance2);

            return string.Format("D(cm): {0:0.0} {1:0.0} {2:0.0}",
                distance0 * 100f,
                distance1 * 100f,
                distance2 * 100f);
        }

        public static bool IsPerpendicularAtMiddlePoint(I_ThreePointsDistanceAngleGet triangle, int errorAllowed = 5)
        {
            return IsPerpendicular(triangle, ThreePointCorner.Middle, errorAllowed);
        }
        public static bool IsPerpendicular(I_ThreePointsDistanceAngleGet triangle, ThreePointCorner corner, int errorAllowed = 5)
        {
            triangle.GetCornerAngle(corner, out float angle);
            return Math.Abs(angle - 90f) < errorAllowed;
        }

        public static bool IsDistanceAlmostEqualsTo(
            I_ThreePointsDistanceAngleGet triangle,
            ThreePointSegment segment,
            float distance,
            int errorAllowed = 5)
        {
            triangle.GetSegmentDistance(segment, out float segmentDistance);
            return Math.Abs(distance - segmentDistance) < errorAllowed;





        }


        public static bool IsGround(I_ThreePointsGet triangle,
            Vector3 groundPosition, float errorAllowed = 0.1f)
        {
            triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
            return
                 IsAtPosition(groundPosition.y, start.y, errorAllowed)
                 && IsAtPosition(groundPosition.y, middle.y, errorAllowed)
                && IsAtPosition(groundPosition.y, end.y, errorAllowed);
        }

        private static bool IsAtPosition(float wantedPositionAxis, float positionAxis, float errorAllowed)
        {
            return Math.Abs(wantedPositionAxis - positionAxis) < errorAllowed;
        }

        public static bool IsVertical(I_ThreePointsGet t, float angleError = 5)
        {
            GetCrossDirection(t, out Vector3 directionForward, false);
            float angle = Vector3.Angle(directionForward, Vector3.up);
            if (angle > 90)
            {
                angle = 180 - angle;
            }

            return angle < angleError;

        }


        public static bool IsHorizontal(I_ThreePointsGet t, float angleError = 5)
        {

            GetCrossDirection(t, out Vector3 directionForward, false);
            float angle = Vector3.Angle(directionForward, Vector3.up) - 90;
            if (angle < 0)
            {
                angle = -angle;
            }
            return angle < angleError;
        }

        public static void GetCrossDirection(I_ThreePointsGet t, out Vector3 directionForward, bool inverse)
        {
            t.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
            directionForward = Vector3.Cross(
                middle - start
                , middle - end);
            if (inverse)
            {
                directionForward = -directionForward;
            }
        }

        public static void GetCentroid(I_ThreePointsGet triangle, out Vector3 centroid)
        {
            triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
            centroid = new Vector3(
        (start.x + middle.x + end.x) / 3,
        (start.y + middle.y + end.y) / 3,
        (start.z + middle.z + end.z) / 3
    );
        }

        public static void GetClosestPoint(
            I_ThreePointsGet triangle,
            Vector3 toPoint,
            out ThreePointCorner closestCorner,
            out Vector3 closestPosition,
            out float distance)
        {
            distance = float.MaxValue;
            triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
            closestCorner = ThreePointCorner.Start;
            closestPosition = start;
            float distanceStart = Vector3.Distance(start, toPoint);
            float distanceMiddle = Vector3.Distance(middle, toPoint);
            float distanceEnd = Vector3.Distance(end, toPoint);
            if (distanceStart < distance)
            { closestCorner = ThreePointCorner.Start; distance = distanceStart; closestPosition = start; }
            if (distanceMiddle < distance)
            { closestCorner = ThreePointCorner.Middle; distance = distanceMiddle; closestPosition = middle; }
            if (distanceEnd < distance)
            { closestCorner = ThreePointCorner.End; distance = distanceEnd; closestPosition = end; }

        }

        public static void GetFarestPoint(
            I_ThreePointsGet triangle,
            Vector3 toPoint,
            out ThreePointCorner closestCorner,
            out Vector3 closestPosition,
            out float distance)
        {
            distance = 0;
            triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
            closestCorner = ThreePointCorner.Start;
            closestPosition = start;
            float distanceStart = Vector3.Distance(start, toPoint);
            float distanceMiddle = Vector3.Distance(middle, toPoint);
            float distanceEnd = Vector3.Distance(end, toPoint);
            if (distanceStart > distance)
            { closestCorner = ThreePointCorner.Start; distance = distanceStart; closestPosition = start; }
            if (distanceMiddle > distance)
            { closestCorner = ThreePointCorner.Middle; distance = distanceMiddle; closestPosition = middle; }
            if (distanceEnd > distance)
            { closestCorner = ThreePointCorner.End; distance = distanceEnd; closestPosition = end; }
        }

        public static void GetCrossProductMiddle(I_ThreePointsGet triangle, out Vector3 cross)
        {
            triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
            cross = Vector3.Cross(middle - start, middle - end);
        }

        public static float GetBiggestAngle(I_ThreePointsDistanceAngleGet computed)
        {
            GetOrderedAngle(computed, out float maxAngle, out float middleAngle, out float minAngle);
            return maxAngle;
        }

        public static void GetOrderedEdgeDistance(I_ThreePointsDistanceAngleGet computed, out float max, out float middle, out float min)
        {
            computed.GetSegmentDistance(ThreePointSegment.StartMiddle, out float distance0);
            computed.GetSegmentDistance(ThreePointSegment.MiddleEnd, out float distance1);
            computed.GetSegmentDistance(ThreePointSegment.StartEnd, out float distance2);
            float[] distances = new float[3] { distance0, distance1, distance2 };
            Array.Sort(distances);
            min = distances[0];
            middle = distances[1];
            max = distances[2];
        }

        public static void GetOrderedAngle(I_ThreePointsDistanceAngleGet computed, out float maxAngle, out float middleAngle, out float minAngle)
        {
            computed.GetCornerAngle(ThreePointCorner.Start, out float angle);
            computed.GetCornerAngle(ThreePointCorner.Middle, out float angle2);
            computed.GetCornerAngle(ThreePointCorner.End, out float angle3);
            float[] angles = new float[3] { angle, angle2, angle3 };
            Array.Sort(angles);
            minAngle = angles[0];
            middleAngle = angles[1];
            maxAngle = angles[2];


        }
        public static void GetOrderedAngle(I_ThreePointsGet triangle, out float maxAngle, out float middleAngle, out float minAngle)
        {
            ThreePointsTriangleDefault tri = new ThreePointsTriangleDefault(triangle);
            GetOrderedAngle(tri, out maxAngle, out middleAngle, out minAngle);

        }


        public static bool IsAlmostAPaperInMeter(I_ThreePointsDistanceAngleGet computed, APaperEnum paperType, float tolerance)
        {


            APaper.GetDimensionMeter(paperType, out float widthMeter, out float heightMeter, out float hypothenus);
            return HasAlmostTheSameEdge(computed, widthMeter, heightMeter, hypothenus, tolerance);
        }


        public static bool HasAlmostTheSameEdge(
     I_ThreePointsDistanceAngleGet source,
     I_ThreePointsDistanceAngleGet wanted,
     float toleranceLenght)
        {
            if (source == null)
                return false;
            if (wanted == null)
                return false;

            GetOrderedEdgeDistance(source, out float maxSource, out float middleSource, out float minSource);
            GetOrderedEdgeDistance(wanted, out float maxWanted, out float middleWanted, out float minWanted);

            return HasAlmostTheSameEdge(
                maxSource, middleSource, minSource,
                maxWanted, middleWanted, minWanted,
                toleranceLenght);
        }

        public static bool HasAlmostTheSameEdge(I_ThreePointsDistanceAngleGet source,
            float edgeA, float edgeB, float edgeC, float tolerance)
        {
            GetOrderedEdgeDistance(source, out float maxS, out float middleS, out float minS);
            GetOrderedThreeFloatValue(edgeA, edgeB, edgeC,
                out float max, out float middle, out float min);
            return HasAlmostTheSameEdge(
                maxS, middleS, minS,
                max, middle, min, tolerance);


        }
        public static bool HasAlmostTheSameAngle(I_ThreePointsDistanceAngleGet source, float angleA, float angleB, float angleC)
        {
            GetOrderedAngle(source, out float maxS, out float middleS, out float minS);
            GetOrderedThreeFloatValue(angleA, angleB, angleC,
                out float max, out float middle, out float min);
            return HasAlmostTheSameEdge(
                maxS, middleS, minS,
                max, middle, min);
        }

        private static void GetOrderedThreeFloatValue(float edgeA, float edgeB, float edgeC, out float max, out float middle, out float min)
        {
            float[] distances = new float[3] { edgeA, edgeB, edgeC };
            Array.Sort(distances);
            min = distances[0];
            middle = distances[1];
            max = distances[2];
        }

        public static bool HasAlmostTheSameEdge(
            float edgeMaxA, float edgeMiddleA, float edgeMinA,
            float edgeMaxB, float edgeMiddleB, float edgeMinB,
            float tolerance = 0.05f)
        {

            return Mathf.Abs(edgeMaxA - edgeMaxB) < tolerance
                && Mathf.Abs(edgeMiddleA - edgeMiddleB) < tolerance
                && Mathf.Abs(edgeMinA - edgeMinB) < tolerance;
        }

        public static bool IsOneAngleAlmostOf90Degree(I_ThreePointsDistanceAngleGet source, float toleranceAngle)
        {
            return IsOneAngleAlmostOf(source, 90, toleranceAngle);
        }
        public static bool IsOneAngleAlmostOf0Degree(I_ThreePointsDistanceAngleGet source, float toleranceAngle)
        {
            return IsOneAngleAlmostOf(source, 0, toleranceAngle);
        }



        public static bool IsTrianglesAlmostTheSame(
            I_ThreePointsDistanceAngleGet source,
            I_ThreePointsDistanceAngleGet wanted,
            float toleranceLenght, float toleranceAngle)
        {
            return HasAlmostTheSameEdge(source, wanted, toleranceLenght)
                && HasAlmostTheSameAngle(source, wanted, toleranceAngle);
        }
        public static bool IsAllAngleAlmostOf33Degree(I_ThreePointsDistanceAngleGet source,
            float toleranceAngle)
        {
            return IsAllAngleAlmostOf(source, 33f, toleranceAngle);
        }

        public static bool IsAllAngleAlmostOf(I_ThreePointsDistanceAngleGet soruce,
            float wantedAngle, float toleranceAngle)
        {
            if (soruce == null)
                return false;
            soruce.GetCornerAngle(ThreePointCorner.End, out float end);
            if (Mathf.Abs(end - wantedAngle) > toleranceAngle)
                return false;
            soruce.GetCornerAngle(ThreePointCorner.Middle, out float middle);
            if (Mathf.Abs(middle - wantedAngle) > toleranceAngle)
                return false;
            soruce.GetCornerAngle(ThreePointCorner.Start, out float start);
            if (Mathf.Abs(start - wantedAngle) > toleranceAngle)
                return false;
            return true;
        }

        public static bool IsOneAngleAlmostOf(I_ThreePointsDistanceAngleGet soruce,
            float wantedAngle, float toleranceAngle)
        {
            if (soruce == null)
                return false;
            soruce.GetCornerAngle(ThreePointCorner.End, out float end);
            if (Mathf.Abs(end - wantedAngle) < toleranceAngle)
                return true;
            soruce.GetCornerAngle(ThreePointCorner.Middle, out float middle);
            if (Mathf.Abs(middle - wantedAngle) < toleranceAngle)
                return true;
            soruce.GetCornerAngle(ThreePointCorner.Start, out float start);
            if (Mathf.Abs(start - wantedAngle) < toleranceAngle)
                return true;
            return false;

        }
        public static bool HasAlmostTheSameAngle(
            I_ThreePointsDistanceAngleGet source,
            I_ThreePointsDistanceAngleGet wanted,
            float toleranceAngle)
        {
            if (source == null)
                return false;
            if (wanted == null)
                return false;
            source.GetCornerAngle(ThreePointCorner.End, out float sourceEnd);
            wanted.GetCornerAngle(ThreePointCorner.End, out float wantedEnd);
            source.GetCornerAngle(ThreePointCorner.Middle, out float sourceMiddle);
            wanted.GetCornerAngle(ThreePointCorner.Middle, out float wantedMiddle);
            source.GetCornerAngle(ThreePointCorner.Start, out float sourceStart);
            wanted.GetCornerAngle(ThreePointCorner.Start, out float wantedStart);

            return HasAlmostTheSameEdge(
                sourceEnd, sourceMiddle, sourceStart,
                wantedEnd, wantedMiddle, wantedStart,
                toleranceAngle);
        }

        public static void GetHypothenus(I_ThreePointsDistanceAngleGet computed, out float hypothenusDistance)
        {
            GetOrderedEdgeDistance(computed, out hypothenusDistance, out _, out _);
        }
        public static void GetBordersEdgeOfHypotenus(I_ThreePointsDistanceAngleGet computed, out float biggestEdge, out float smallestEdge)
        {
            GetOrderedEdgeDistance(computed, out float _, out float middle, out float min);
            biggestEdge = middle;
            smallestEdge = min;
        }

        public static bool IsMaxRadisuSmallerThat(I_ThreePointsDistanceAngleGet computed, float radiusDistanceValue)
        {
            GetMaxMinRadius(computed, out float maxRadius, out float minRadius);
            return maxRadius < radiusDistanceValue;
        }
        public static void GetMaxMinRadius(I_ThreePointsDistanceAngleGet computed, out float maxRadius, out float minRadius)
        {

            GetOrderedEdgeDistance(computed, out float max, out float middle, out float min);
            minRadius = StaitcTriangleCompute.CalculateInradius(max, middle, min);
            maxRadius = StaitcTriangleCompute.CalculateCircumradius(max, middle, min);
        }

        public static void AreSimilar(I_ThreePointsGet a, I_ThreePointsGet b,
            float similarityThresholdMillimeter,
            float similarityThresholdDegree, 
            out bool areSimilar)
        {
            areSimilar = false;
            if (a == null)
                return;
            if (b == null)
                return;
            GetBordersTotalLenght(a, out float totalLenghtA);
            GetBordersTotalLenght(b, out float totalLenghtB);
            if (Mathf.Abs(totalLenghtA - totalLenghtB) > similarityThresholdMillimeter)
                return;

            GetOrderedAngle(a, out float maxAngleA, out float middleAngleA, out float minAngleA);
            GetOrderedAngle(b, out float maxAngleB, out float middleAngleB, out float minAngleB);
            if (Mathf.Abs(maxAngleA - maxAngleB) > similarityThresholdDegree)
                return;
            if (Mathf.Abs(middleAngleA - middleAngleB) > similarityThresholdDegree)
                return;
            if (Mathf.Abs(minAngleA - minAngleB) > similarityThresholdDegree)
                return;
            areSimilar = true;
        }


        private static void GetBordersTotalLenght(I_ThreePointsGet a, out float totalLenght)
        {
            a.GetThreePoints(out Vector3 startA, out Vector3 middleA, out Vector3 endA);
            totalLenght = Vector3.Distance(startA, middleA) + Vector3.Distance(middleA, endA) + Vector3.Distance(startA, endA);
        }
    }
}