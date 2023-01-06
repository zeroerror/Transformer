namespace Transformer.Bussiness.LogicBussiness
{

    public class RoleEntity
    {

        public InputComponent InputComponent { get; private set; }
        public IDComponent IDComponent { get; private set; }
        public LocomotionComponent LocomotionComponent { get; private set; }

        public RoleEntity()
        {
            InputComponent = new InputComponent();
            IDComponent = new IDComponent();
            LocomotionComponent = new LocomotionComponent();
        }

    }

}