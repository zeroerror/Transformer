using Transformer.LogicBussiness.Repo;

namespace Transformer.LogicBussiness.Facade
{

    public class AllLogicRepo
    {

        public RoleLogicRepo RoleRepo { get; private set; }

        public AllLogicRepo()
        {

            RoleRepo = new RoleLogicRepo();

        }

    }

}