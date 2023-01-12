using ZeroPhysics.Physics3D.Domain;

namespace ZeroPhysics.Physics3D.Facade
{

    public class AllPhysicsDomain
    {

        public SpawnDomain SpawnDomain { get; private set; }
        public DataDomain DataDomain { get; private set; }

        public AllPhysicsDomain()
        {
            SpawnDomain = new SpawnDomain();
            DataDomain = new DataDomain();
        }

        public void Inject(Physics3DFacade physicsFacade)
        {
            SpawnDomain.Inject(physicsFacade);
            DataDomain.Inject(physicsFacade);
        }

    }

}