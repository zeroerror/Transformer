using UnityEngine;
using FixMath.NET;

namespace Transformer.Bussiness.RendererAPI
{

    public interface IRendererSetter
    {

        public void SpawnRole(int typeID, int id, in FPVector3 spawnPos);
        public void SetRoleLocomotion_Center(int id, in FPVector3 center);

    }

}