using System.Threading.Tasks;
using Transformer.Template;

namespace Transformer.LogicBussiness.Facade
{

    public class AllTemplate
    {

        public RoleTemplate RoleTemplate { get; private set; }
        public FieldTempate FieldTempate { get; private set; }

        public AllTemplate()
        {
            RoleTemplate = new RoleTemplate();
            FieldTempate = new FieldTempate();
        }

        public async Task Init()
        {
            await RoleTemplate.Init();
            await FieldTempate.Init();
        }

    }

}