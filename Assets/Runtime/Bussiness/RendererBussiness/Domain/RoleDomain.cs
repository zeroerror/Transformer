using Transformer.Bussiness.RendererBussiness.Facade;
using UnityEngine;

namespace Transformer.Bussiness.RendererBussiness.Domain
{

    public class RoleDomain
    {

        RendererFacade facade;

        public RoleDomain()
        {

        }

        public void Inject(RendererFacade facade)
        {
            this.facade = facade;
        }

        public RoleEntity SpawnRole(int typeID, int id, in Vector3 spawnPos)
        {
            var factory = facade.Factory;
            var roleRepo = facade.Repo.RoleRepo;

            // - Spawn Entity
            var role = factory.SpawnRole(typeID, id, spawnPos);

            // - Set Entity
            role.SetID(id);

            // - Add
            roleRepo.Add(role);

            return role;
        }

        public void SetRoleLocomotion_Center(int id, Vector3 center)
        {
            var roleRepo = facade.Repo.RoleRepo;
            var role = roleRepo.Get(id);
            role.SetCenterPos(center);
        }

    }

}