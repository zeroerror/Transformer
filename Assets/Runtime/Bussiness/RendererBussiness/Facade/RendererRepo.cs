using Transformer.Bussiness.RendererBussiness.Repo;

namespace Transformer.Bussiness.RendererBussiness.Facade
{

    public class RendererRepo
    {

        public RoleRepo RoleRepo { get; private set; }

        public RendererRepo()
        {

            RoleRepo = new RoleRepo();

        }

    }

}