using FixMath.NET;
using ZeroPhysics.Physics3D.Facade;

namespace ZeroPhysics.Physics3D.Domain
{

    public class SpawnDomain
    {

        Physics3DFacade physicsFacade;

        public SpawnDomain() { }

        public void Inject(Physics3DFacade physicsFacade)
        {
            this.physicsFacade = physicsFacade;
        }

        public Box3D SpawnBox(in FPVector3 center, in FPQuaternion rotation, in FPVector3 scale, in FPVector3 size)
        {
            var factory = physicsFacade.Factory;
            var box = factory.SpawnBox3D(center, rotation, scale, size);

            var idService = physicsFacade.Service.IDService;
            var id = idService.FetchID_Box();
            box.SetInstanceID(id);
            // UnityEngine.Debug.Log($"SpawnBox {id}");
            physicsFacade.boxes[id] = box;
            return box;
        }

        public Box3DRigidbody SpawnRBBox(in FPVector3 center, in FPQuaternion rotation, in FPVector3 scale, in FPVector3 size, in FP64 mass)
        {
            var factory = physicsFacade.Factory;
            var box = factory.SpawnBox3D(center, rotation, scale, size);

            Box3DRigidbody rb = new Box3DRigidbody(box);
            rb.SetMass(mass);

            var idService = physicsFacade.Service.IDService;
            var id = idService.FetchID_BoxRB();
            rb.SetInstanceID(id);
            // UnityEngine.Debug.Log($"SpawnRBBox {id}");

            physicsFacade.boxRBs[id] = rb;
            return rb;
        }

        public Sphere3D SpawnSphere(in FPVector3 center, in FPQuaternion rotation, in FPVector3 scale, in FPVector3 size)
        {
            Sphere3D sphere = new Sphere3D();
            sphere.SetCenter(center);
            sphere.SetRotation(rotation);
            sphere.SetScale(scale);
            sphere.SetRadius(size.x);
            sphere.UpdateScaledRadius();

            var idService = physicsFacade.Service.IDService;
            var id = idService.FetchID_Sphere();
            sphere.SetInstanceID(id);

            physicsFacade.spheres[id] = sphere;
            return sphere;
        }


    }

}