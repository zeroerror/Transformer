using UnityEngine;

namespace Transformer.Bussiness.RendererBussiness
{

    public class RoleEntity : MonoBehaviour
    {

        int id;
        public int ID => id;
        public void SetID(int v) => id = v;

        Vector3 centerPos;
        public Vector3 CenterPos => centerPos;
        public void SetCenterPos(Vector3 v) => centerPos = v;

    }

}