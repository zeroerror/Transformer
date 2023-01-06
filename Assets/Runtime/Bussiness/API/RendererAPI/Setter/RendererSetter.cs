using UnityEngine;
using FixMath.NET;
using Transformer.Bussiness.RendererBussiness.Facade;
using Transformer.Extension;

namespace Transformer.Bussiness.RendererAPI
{

    public class RendererSetter : IRendererSetter
    {

        public RendererFacade rendererFacade;

        public void Inject(RendererFacade rendererFacade)
        {
            this.rendererFacade = rendererFacade;
        }

        void IRendererSetter.SpawnRole(int typeID, int id, in FPVector3 spawnPosFP)
        {
            var roleDomain = rendererFacade.Domain.RoleDomain;
            roleDomain.SpawnRole(typeID, id, spawnPosFP.ToVector3());
        }

        void IRendererSetter.SetRoleLocomotion_Center(int id, in FPVector3 center)
        {
            var roleDomain = rendererFacade.Domain.RoleDomain;
            roleDomain.SetRoleLocomotion_Center(id, center.ToVector3());
        }

    }

}