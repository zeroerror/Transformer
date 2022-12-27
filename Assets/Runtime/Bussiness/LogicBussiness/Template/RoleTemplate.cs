using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Transformer.Bussiness.LogicBussiness.Generic;
using Transformer.Template;

namespace Transformer.Bussiness.LogicBussiness.Template
{

    public class RoleTemplate
    {

        List<RoleTemplateModel> all;

        public RoleTemplate()
        {
            all = new List<RoleTemplateModel>();
        }

        public async Task Init()
        {
            AssetLabelReference assetLabelReference = new AssetLabelReference();
            assetLabelReference.labelString = AssetLabelCollection.TEMPLE_MODEL_ROLE;
            var result = await Addressables.LoadAssetsAsync<RoleTemplateModel>(assetLabelReference, null).Task;
            foreach (var roleTM in result)
            {
                all.Add(roleTM);
            }
        }

        public RoleTemplateModel TryGet(int typeID)
        {
            return all.Find((r) => r.typeID == typeID);
        }

    }

}