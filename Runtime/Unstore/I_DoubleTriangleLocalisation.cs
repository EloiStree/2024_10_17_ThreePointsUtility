using Eloi.ThreePoints;

public interface I_DoubleTriangleLocalisation { 

    void GetTriangleOne(out I_ThreePointsGet squareOne);
    void GetTriangleTwo(out I_ThreePointsGet squareTwo);
    void GetTheDoubleTriangle(out I_ThreePointsGet squareOne, out I_ThreePointsGet squareTwo);
    void GetTheDoubleTriangle(out I_ThreePointsDistanceAngleGet squareOne, out I_ThreePointsDistanceAngleGet squareTwo);
}

