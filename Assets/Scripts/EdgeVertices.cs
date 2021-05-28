
using System;
using UnityEngine;

public struct EdgeVertices
{
    public Vector3 v1, v2, v3, v4;

    public EdgeVertices(Vector3 corner1, Vector3 corner2)
    {
        v1 = corner1;
        v2 = Vector3.Lerp(corner1, corner2, 1f / 3f);
        v3 = Vector3.Lerp(corner1, corner2, 2f / 3f);
        v4 = corner2;
    }

    public static EdgeVertices TerraceLerp(EdgeVertices begin, EdgeVertices end, int v)
    {
        EdgeVertices result;
        result.v1 = HexMetrics.TerraceLerp(begin.v1, end.v1, v);
        result.v2 = HexMetrics.TerraceLerp(begin.v2, end.v2, v);
        result.v3 = HexMetrics.TerraceLerp(begin.v3, end.v3, v);
        result.v4 = HexMetrics.TerraceLerp(begin.v4, end.v4, v);
        return result;
    }
}
