using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Transformer.Generic;

namespace Transformer.Template
{

    public class RoleTemplate
    {

        List<RoleSO> all;

        public RoleTemplate()
        {
            all = new List<RoleSO>();
        }

        public async Task Init()
        {
            AssetLabelReference assetLabelReference = new AssetLabelReference();
            assetLabelReference.labelString = AssetLabelCollection.TEMPLE_MODEL_ROLE;
            var result = await Addressables.LoadAssetsAsync<RoleSO>(assetLabelReference, null).Task;
            foreach (var so in result)
            {
                all.Add(so);
            }
        }

        public RoleSO TryGet(int typeID)
        {
            return all.Find((r) => r.typeID == typeID);
        }

    }

}