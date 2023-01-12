namespace ZeroPhysics.Physics3D.Facade
{

    public class Physics3DFacade
    {

        // 对于所有Box，都给一个id，id由service提供，id作为一个key便于查找，不需要使用字典，直接用数组下标代替，这样提升了遍历效率
        // 保证在创建时，必须确定是rigidbody还是static，其后不做更改，这样就不需要数组之间的数据转移
        // rb需要进行速度改变，和所有包括rb和static进行交叉恢复，static这2个都不需要

        public Box3DRigidbody[] boxRBs;
        public Box3D[] boxes;
        public Sphere3D[] spheres;

        public AllPhysicsDomain Domain { get; private set; }
        public Physics3DFactory Factory { get; private set; }
        public AllService Service { get; private set; }

        public Physics3DFacade(int boxMax, int rbBoxMax, int sphereMax)
        {
            boxes = new Box3D[boxMax];
            boxRBs = new Box3DRigidbody[rbBoxMax];
            spheres = new Sphere3D[sphereMax];

            Domain = new AllPhysicsDomain();
            Domain.Inject(this);

            Factory = new Physics3DFactory();
            Factory.Inject(this);

            Service = new AllService(boxMax, rbBoxMax, sphereMax);
        }

    }


}