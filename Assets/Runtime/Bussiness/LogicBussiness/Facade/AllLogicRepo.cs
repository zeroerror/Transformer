using Transformer.Bussiness.LogicBussiness.Repo;

namespace Transformer.Bussiness.LogicBussiness.Facade
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