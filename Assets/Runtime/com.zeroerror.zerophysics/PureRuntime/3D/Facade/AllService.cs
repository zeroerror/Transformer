using ZeroPhysics.Physics3D.Domain;
using ZeroPhysics.Service;

namespace ZeroPhysics.Physics3D.Facade
{

    public class AllService
    {

        public IDService IDService { get; private set; }
        public CollisionService CollisionService { get; private set; }

        public AllService(int boxMax, int rbBoxMax, int sphereMax)
        {
            IDService = new IDService(boxMax, rbBoxMax, sphereMax);
            CollisionService = new CollisionService();
        }

    }

}