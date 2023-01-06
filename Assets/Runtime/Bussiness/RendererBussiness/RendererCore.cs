using GameArki.TripodCamera;
using UnityEngine;
using Transformer.Template;
using Transformer.Bussiness.RendererBussiness.Facade;

namespace Transformer.Bussiness.RendererBussiness
{

    public class RendererCore
    {

        RendererFacade rendererFacade;
        public RendererFacade RendererFacade => rendererFacade;

        public RendererCore()
        {
            rendererFacade = new RendererFacade();
        }

        public void Inject(TCCore camCore, AllTemplate allTemplate)
        {
            RendererFacade.Inject(camCore, allTemplate);
        }

        public void Update(float dt)
        {
            var roleRepo = rendererFacade.Repo.RoleRepo;
            roleRepo.ForeachAll((role) =>
            {
                var tf = role.transform;
                var tfPos = tf.position;
                var centerPos = role.CenterPos;
                tf.position = Vector3.Lerp(tfPos, centerPos, dt * 30);
            });
        }

    }

}