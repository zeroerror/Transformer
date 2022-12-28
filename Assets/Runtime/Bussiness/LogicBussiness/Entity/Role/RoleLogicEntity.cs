namespace Transformer.LogicBussiness
{

    public class RoleLogicEntity
    {

        public InputComponent InputComponent { get; private set; }
        public IDComponent IDComponent { get; private set; }
        public LocomotionComponent LocomotionComponent { get; private set; }

        public RoleLogicEntity()
        {
            InputComponent = new InputComponent();
            IDComponent = new IDComponent();
            LocomotionComponent = new LocomotionComponent();
        }

    }

}