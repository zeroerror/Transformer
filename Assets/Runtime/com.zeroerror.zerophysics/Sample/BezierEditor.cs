using UnityEngine;
using ZeroFrame.AllMath;

namespace ZeroPhysics.Sample
{

    public class BezierEditor : MonoBehaviour
    {

        public Transform start;
        public Transform end;
        public Transform control1;
        public Transform control2;

        public bool showBezier_TwoLevel;
        public bool showBezier_ThreeLevel;
        public bool showBezier_ThreeLevel_Vector3;

        void OnDrawGizmos()
        {
            if (showBezier_TwoLevel) DrawBezier_Two();
            if (showBezier_ThreeLevel) DrawBezier_Three();
            if (showBezier_ThreeLevel_Vector3) DrawBezier_Three_Vector3();
        }

        void DrawBezier_Two()
        {
            if (start == null || end == null || control1 == null)
            {
                return;
            }

            var startPos = start.position;
            var endPos = end.position;
            var c1Pos = control1.position;
            float t = 0f;
            while (t < 1)
            {
                var px = Bezier.GetBezier_TwoLevel(startPos.x, endPos.x, c1Pos.x, t);
                var py = Bezier.GetBezier_TwoLevel(startPos.y, endPos.y, c1Pos.y, t);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(new Vector3(px, py, 0), 0.03f);
                t += 0.001f;
            }
        }

        void DrawBezier_Three()
        {
            if (start == null || end == null || control1 == null || control2 == null)
            {
                return;
            }

            var startPos = start.position;
            var endPos = end.position;
            var c1Pos = control1.position;
            var c2Pos = control2.position;
            float t = 0f;
            while (t < 1)
            {
                var px = Bezier.GetBezier_ThreeLevel(startPos.x, endPos.x, c1Pos.x, c2Pos.x, t);
                var py = Bezier.GetBezier_ThreeLevel(startPos.y, endPos.y, c1Pos.y, c2Pos.y, t);
                Gizmos.color = new Color(0, 1, 0, t / 1);
                Gizmos.DrawSphere(new Vector3(px, py, 0), 0.03f);
                t += 0.001f;
            }
        }

        void DrawBezier_Three_Vector3()
        {
            var startPos = start.position;
            var endPos = end.position;
            var c1Pos = control1.position;
            var c2Pos = control2.position;
            float t = 0f;
            while (t < 1)
            {
                var p = GetBezier_ThreeLevelVector3(startPos, endPos, c1Pos, c2Pos, t);
                Gizmos.color = new Color(0, 0, 1, t / 1);
                Gizmos.DrawSphere(p, 0.03f);
                t += 0.001f;
            }
        }

        Vector3 GetBezier_ThreeLevelVector3(Vector3 start, Vector3 end, Vector3 c1, Vector3 c2, float t)
        {
            return AllDigit.Pow((1 - t), 3) * start
             + 3 * t * AllDigit.Pow((1 - t), 2) * c1
             + 3 * AllDigit.Pow(t, 2) * (1 - t) * c2
             + AllDigit.Pow(t, 3) * end;
        }

        [ContextMenu("Save")]
        void s()
        {

        }

    }

}