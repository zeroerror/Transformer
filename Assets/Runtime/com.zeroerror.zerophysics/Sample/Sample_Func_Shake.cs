using UnityEngine;
using ZeroFrame.AllMath;

namespace ZeroPhysics.Sample
{

    public class Sample_Func_Shake : MonoBehaviour
    {

        public float amplitude;
        public float frequency;
        public float totalTime;
        public float amp_offset;
        public float t_duration = 0.001f;
        public bool useBezierOffset;

        public Transform start;
        public Transform end;
        public Transform control1;
        public Transform control2;

        void Update()
        {

        }

        void OnDrawGizmos()
        {
            t_duration = t_duration <= 0f ? 0.001f : t_duration;

            float t = 0f;
            float y = 0;
            float nextT = t;
            float nextY = y;
            var amp = amplitude;
            while (t < totalTime)
            {
                nextT += t_duration;

                if (useBezierOffset)
                {
                    BezierLineThree line;
                    line.start = start.position.y;
                    line.end = end.position.y;
                    line.c1 = control1.position.y;
                    line.c2 = control2.position.y;
                    nextY = AllFunctions.ShakeWithAmplitudeOffset(line, amp, frequency, t, totalTime);
                }
                else
                {
                    nextY = AllFunctions.Shake(amp, frequency, t);
                    amp -= amp_offset;
                    amp = amp < 0 ? 0 : amp;
                }

                Gizmos.color = Color.green;
                Gizmos.DrawLine(new Vector3(t, y, 0), new Vector3(nextT, nextY, 0));
                t = nextT;
                y = nextY;

            }

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(Vector3.down * 10f, Vector3.up * 10f);
            Gizmos.DrawLine(Vector3.left * 10f, Vector3.right * 10f);
            DrawBezier_Three();
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
            while (t <= 1f)
            {
                float t_percent = t;
                var px = Bezier.GetBezier_ThreeLevel(startPos.x, endPos.x, c1Pos.x, c2Pos.x, t_percent);
                var py = Bezier.GetBezier_ThreeLevel(startPos.y, endPos.y, c1Pos.y, c2Pos.y, t_percent);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(new Vector3(px, py, 0), 0.03f);
                t += 0.001f;
            }
        }

    }

}