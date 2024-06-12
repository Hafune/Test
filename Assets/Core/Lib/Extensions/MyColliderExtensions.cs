using System.Linq;
using UnityEngine;
using UnityEngine.Internal;
using Random = UnityEngine.Random;

namespace Lib
{
    public static class MyColliderExtensions
    {
        public static bool ContainsPoint(this BoxCollider box, Vector3 point, Vector3? sizeMultiply = null)
        {
            var m = sizeMultiply ?? new Vector3(1, 1, 1);
            point = box.transform.InverseTransformPoint(point) - box.center;
            float halfX = box.size.x * 0.5f * m.x;
            float halfY = box.size.y * 0.5f * m.y;
            float halfZ = box.size.z * 0.5f * m.z;
            return point.x < halfX && point.x > -halfX &&
                   point.y < halfY && point.y > -halfY &&
                   point.z < halfZ && point.z > -halfZ;
        }

        public static (Vector3 point0, Vector3 point1, float rdaius) TransformCapsuleValues(this CapsuleCollider col)
        {
            var direction = new Vector3 { [col.direction] = 1 };
            var offset = col.height / 2 - col.radius;
            var localPoint0 = col.center - direction * offset;
            var localPoint1 = col.center + direction * offset;

            var point0 = col.transform.TransformPoint(localPoint0);
            var point1 = col.transform.TransformPoint(localPoint1);

            var rawRadius = col.transform.TransformVector(col.radius, col.radius, col.radius);
            var radius = Enumerable.Range(0, 3).Select(xyz => xyz == col.direction ? 0 : rawRadius[xyz])
                .Select(Mathf.Abs).Max();

            return (point0, point1, radius);
        }

        public static int OverlapCapsuleNonAlloc(this CapsuleCollider col, Collider[] _hitBuffer,
            [DefaultValue("AllLayers")] int layerMask)
        {
            var (point0, point1, radius) = col.TransformCapsuleValues();
            return Physics.OverlapCapsuleNonAlloc(point0, point1, radius, _hitBuffer, layerMask);
        }

        public static Vector3 GetRandomPointInside(this BoxCollider boxCollider)
        {
            Vector3 extents = boxCollider.size / 2f;
            Vector3 point = new Vector3(
                Random.Range(-extents.x, extents.x),
                Random.Range(-extents.y, extents.y),
                Random.Range(-extents.z, extents.z)
            ) + boxCollider.center;

            return boxCollider.transform.TransformPoint(point);
        }

        //Бред нейросети не проверен
        /*
        using UnityEngine;

public static class ColliderIntersection
{
    public static Vector2 CalculateIntersectionCenter(Collider2D collider1, Collider2D collider2)
    {
        const int maxPoints = 10;
        var points = new Vector2[maxPoints];
        var pointCount = 0;

        // Получение всех точек пересечения
        if (FindIntersectionPoints(collider1, collider2, points, ref pointCount))
        {
            // Найти центр пересечения
            return CalculateCentroid(points, pointCount);
        }

        return Vector2.zero;
    }

    private static bool FindIntersectionPoints(Collider2D collider1, Collider2D collider2, Vector2[] points, ref int pointCount) =>
        (collider1, collider2) switch
        {
            (CircleCollider2D circle1, CircleCollider2D circle2) => CircleCircleIntersection(circle1, circle2, points, ref pointCount),
            (CircleCollider2D circle, PolygonCollider2D poly) => CirclePolygonIntersection(circle, poly, points, ref pointCount),
            (PolygonCollider2D poly1, PolygonCollider2D poly2) => PolygonPolygonIntersection(poly1, poly2, points, ref pointCount),
            (BoxCollider2D box1, BoxCollider2D box2) => BoxBoxIntersection(box1, box2, points, ref pointCount),
            (CircleCollider2D circle, BoxCollider2D box) => CirclePolygonIntersection(circle, BoxToPolygonCollider(box), points, ref pointCount),
            (BoxCollider2D box, CircleCollider2D circle) => CirclePolygonIntersection(circle, BoxToPolygonCollider(box), points, ref pointCount),
            (PolygonCollider2D poly, BoxCollider2D box) => PolygonPolygonIntersection(poly, BoxToPolygonCollider(box), points, ref pointCount),
            (BoxCollider2D box, PolygonCollider2D poly) => PolygonPolygonIntersection(BoxToPolygonCollider(box), poly, points, ref pointCount),
            _ => false
        };

    private static PolygonCollider2D BoxToPolygonCollider(BoxCollider2D box)
    {
        var poly = new GameObject("TempPolyCollider").AddComponent<PolygonCollider2D>();
        Vector2 size = box.size;
        Vector2 offset = box.offset;
        Vector2[] points = {
            new Vector2(offset.x - size.x / 2, offset.y - size.y / 2),
            new Vector2(offset.x + size.x / 2, offset.y - size.y / 2),
            new Vector2(offset.x + size.x / 2, offset.y + size.y / 2),
            new Vector2(offset.x - size.x / 2, offset.y + size.y / 2)
        };
        poly.SetPath(0, points);
        return poly;
    }

    private static bool CircleCircleIntersection(CircleCollider2D circle1, CircleCollider2D circle2, Vector2[] points, ref int pointCount)
    {
        var pos1 = circle1.transform.TransformPoint(circle1.offset);
        var pos2 = circle2.transform.TransformPoint(circle2.offset);

        if ((pos1 - pos2).sqrMagnitude <= Mathf.Pow(circle1.radius + circle2.radius, 2))
        {
            if (pointCount < points.Length)
                points[pointCount++] = pos1 + (pos2 - pos1) / 2;

            return true;
        }

        return false;
    }

    private static bool CirclePolygonIntersection(CircleCollider2D circle, PolygonCollider2D poly, Vector2[] points, ref int pointCount)
    {
        var circlePos = circle.transform.TransformPoint(circle.offset);
        var path = poly.GetPath(0);
        RaycastHit2D[] results = new RaycastHit2D[1];
        for (int i = 0; i < path.Length; i++)
        {
            var point1 = poly.transform.TransformPoint(path[i]);
            var point2 = poly.transform.TransformPoint(path[(i + 1) % path.Length]);
            if (Physics2D.LinecastNonAlloc(circlePos, circlePos + (point2 - point1).normalized * circle.radius, results) > 0)
            {
                if (pointCount < points.Length)
                    points[pointCount++] = (point1 + point2) / 2;

                return true;
            }
        }

        return false;
    }

    private static bool PolygonPolygonIntersection(PolygonCollider2D poly1, PolygonCollider2D poly2, Vector2[] points, ref int pointCount)
    {
        for (int i = 0; i < poly1.pathCount; i++)
        {
            var path1 = poly1.GetPath(i);
            for (int j = 0; j < path1.Length; j++)
            {
                var point1A = poly1.transform.TransformPoint(path1[j]);
                var point1B = poly1.transform.TransformPoint(path1[(j + 1) % path1.Length]);

                for (int k = 0; k < poly2.pathCount; k++)
                {
                    var path2 = poly2.GetPath(k);
                    for (int l = 0; l < path2.Length; l++)
                    {
                        var point2A = poly2.transform.TransformPoint(path2[l]);
                        var point2B = poly2.transform.TransformPoint(path2[(l + 1) % path2.Length]);

                        if (LineIntersection(point1A, point1B, point2A, point2B, out var intersection))
                        {
                            if (pointCount < points.Length)
                                points[pointCount++] = intersection;

                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    private static bool BoxBoxIntersection(BoxCollider2D box1, BoxCollider2D box2, Vector2[] points, ref int pointCount)
    {
        var poly1 = BoxToPolygonCollider(box1);
        var poly2 = BoxToPolygonCollider(box2);

        return PolygonPolygonIntersection(poly1, poly2, points, ref pointCount);
    }

    private static bool LineIntersection(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2 intersection)
    {
        intersection = Vector2.zero;
        var b = a2 - a1;
        var d = b2 - b1;
        var bDotDPerp = b.x * d.y - b.y * d.x;

        if (bDotDPerp == 0)
            return false; // Линии параллельны

        var c = b1 - a1;
        var t = (c.x * d.y - c.y * d.x) / bDotDPerp;

        if (t < 0 || t > 1)
            return false;

        var u = (c.x * b.y - c.y * b.x) / bDotDPerp;

        if (u < 0 || u > 1)
            return false;

        intersection = a1 + t * b;
        return true;
    }

    private static Vector2 CalculateCentroid(Vector2[] points, int pointCount)
    {
        var centroid = Vector2.zero;
        for (int i = 0; i < pointCount; i++)
            centroid += points[i];

        centroid /= pointCount;
        return centroid;
    }
}

        */
    }
}