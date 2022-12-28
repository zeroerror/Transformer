using System.Threading.Tasks;
using Transformer.LogicBussiness.Generic;
using Transformer.Template;
using UnityEngine.AddressableAssets;

namespace Transformer.LogicBussiness.Facade
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