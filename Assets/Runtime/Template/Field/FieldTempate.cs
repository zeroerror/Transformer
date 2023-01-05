using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Transformer.Generic;

namespace Transformer.Template
{

    public class FieldTempate
    {

        List<FieldSO> all;

        public FieldTempate()
        {
            all = new List<FieldSO>();
        }

        public async Task Init()
        {
            AssetLabelReference assetLabelReference = new AssetLabelReference();
            assetLabelReference.labelString = AssetLabelCollection.TEMPLE_MODEL_FIELD;
            var result = await Addressables.LoadAssetsAsync<FieldSO>(assetLabelReference, null).Task;
            foreach (var so in result)
            {
                all.Add(so);
            }
        }

        public FieldSO TryGet(int typeID)
        {
            return all.Find((r) => r.typeID == typeID);
        }

    }

}