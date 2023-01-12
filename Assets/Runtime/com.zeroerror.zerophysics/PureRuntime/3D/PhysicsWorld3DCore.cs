using FixMath.NET;
using ZeroPhysics.Physics3D.API;
using ZeroPhysics.Physics3D.Facade;

namespace ZeroPhysics.Physics3D {

    public class PhysicsWorld3DCore {

        FPVector3 gravity;

        // ====== Facade
        Physics3DFacade physicsFacade;

        // ====== Phase
        ForcePhase forcePhase;
        VelocityPhase velocityPhase;
        IntersectPhase intersectPhase;
        PenetrationPhase penetrationPhase;
        TransformPhase transformPhase;

        // ====== API
        GetterAPI getterAPI;
        public IGetterAPI GetterAPI => getterAPI;

        SetterAPI setterAPI;
        public ISetterAPI SetterAPI => setterAPI;

        public PhysicsWorld3DCore(FPVector3 gravity, int boxMax = 1000, int rbBoxMax = 1000, int sphereMax = 1000) {
            this.gravity = gravity;

            forcePhase = new ForcePhase();
            velocityPhase = new VelocityPhase();
            intersectPhase = new IntersectPhase();
            penetrationPhase = new PenetrationPhase();
            transformPhase = new TransformPhase();

            getterAPI = new GetterAPI();
            setterAPI = new SetterAPI();

            physicsFacade = new Physics3DFacade(boxMax, rbBoxMax, sphereMax);

            forcePhase.Inject(physicsFacade);
            velocityPhase.Inject(physicsFacade);
            intersectPhase.Inject(physicsFacade);
            penetrationPhase.Inject(physicsFacade);
            transformPhase.Inject(physicsFacade);

            getterAPI.Inject(physicsFacade);
            setterAPI.Inject(physicsFacade);
        }

        public void Tick(FP64 time) {
            forcePhase.Tick(time, gravity);
            velocityPhase.Tick(time);
            intersectPhase.Tick(time);
            penetrationPhase.Tick(time);
            transformPhase.Tick(time);
        }

    }

}
