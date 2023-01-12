using UnityEngine;

// Attach this to a GameObject that has a Collider component attached
namespace ZeroPhysics.Sample
{

    public class Sample_ShowClosestPoint : MonoBehaviour
    {
        public Transform location;

        public void OnDrawGizmos()
        {
            var collider = GetComponent<Collider>();

            if (!collider)
                return; // nothing to do without a collider

            Vector3 closestPoint = collider.ClosestPoint(location.position);
            if (location == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(location.position, 0.1f);
            Gizmos.DrawWireSphere(closestPoint, 0.1f);
        }
    }

}