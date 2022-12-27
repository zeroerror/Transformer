using System.Threading.Tasks;
using Transformer.Bussiness.LogicBussiness.Generic;
using Transformer.Bussiness.LogicBussiness.Template;
using UnityEngine.AddressableAssets;

namespace Transformer.Bussiness.LogicBussiness.Facade
{

    public class AllTemplate
    {

        public RoleTemplate RoleTemplate { get; private set; }

        public AllTemplate()
        {

            RoleTemplate = new RoleTemplate();

        }

        public async Task Init()
        {
            await RoleTemplate.Init();
        }

    }

}