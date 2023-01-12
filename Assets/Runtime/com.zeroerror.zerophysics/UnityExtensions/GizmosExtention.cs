using UnityEngine;
using ZeroPhysics.Physics3D;

namespace ZeroPhysics.Extensions
{

    public static class GizmosExtention
    {

        public static void DrawBoxBorder(this Box3D box)
        {
            var model = box.GetModel();
            var vertices = model.vertices.ToVector3Array();
            Gizmos.DrawLine(vertices[0], vertices[1]);
            Gizmos.DrawLine(vertices[0], vertices[2]);
            Gizmos.DrawLine(vertices[1], vertices[3]);
            Gizmos.DrawLine(vertices[2], vertices[3]);

            Gizmos.DrawLine(vertices[4], vertices[5]);
            Gizmos.DrawLine(vertices[4], vertices[6]);
            Gizmos.DrawLine(vertices[5], vertices[7]);
            Gizmos.DrawLine(vertices[6], vertices[7]);

            Gizmos.DrawLine(vertices[0], vertices[4]);
            Gizmos.DrawLine(vertices[1], vertices[5]);
            Gizmos.DrawLine(vertices[2], vertices[6]);
            Gizmos.DrawLine(vertices[3], vertices[7]);
        }

        public static void DrawBoxPoint(this Box3D box, float size = 0.1f)
        {
            var model = box.GetModel();
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(model.Min.ToVector3(), size);
            Gizmos.DrawSphere(model.Max.ToVector3(), size);
            Gizmos.color = Color.yellow;
        }

    }

}