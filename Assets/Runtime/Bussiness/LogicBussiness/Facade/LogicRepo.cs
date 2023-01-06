using Transformer.Bussiness.LogicBussiness.Repo;

namespace Transformer.Bussiness.LogicBussiness.Facade
{

    public class LogicRepo
    {

        public RoleRepo RoleRepo { get; private set; }

        public LogicRepo()
        {

            RoleRepo = new RoleRepo();

        }

    }

}