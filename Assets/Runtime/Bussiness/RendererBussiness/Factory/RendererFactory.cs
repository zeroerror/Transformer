using Transformer.Bussiness.RendererBussiness.Facade;
using UnityEngine;

namespace Transformer.Bussiness.RendererBussiness.Factory
{

    public class RendererFactory
    {

        RendererFacade facade;

        public RendererFactory()
        {

        }

        public void Inject(RendererFacade facade)
        {
            this.facade = facade;
        }

        public RoleEntity SpawnRole(int typeID, int id, in Vector3 spawnPos)
        {
            var roleTemplate = facade.Template.RoleTemplate;
            var model = roleTemplate.TryGet(typeID);

            // - Spawn Mono
            // todo: load role prefab. load role mod
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            RoleEntity role = go.AddComponent<RoleEntity>();
            role.SetID(id);

            var size = model.ToSize();
            var tf = go.transform;
            tf.position = spawnPos;
            tf.rotation = Quaternion.identity;
            tf.localScale = Vector3.one;

            return role;
        }

    }

}