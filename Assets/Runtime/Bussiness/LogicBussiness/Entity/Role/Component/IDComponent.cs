using FixMath.NET;
using Transformer.Bussiness.LogicBussiness.Generic;

namespace Transformer.Bussiness.LogicBussiness
{

    public class IDComponent
    {

        EntityType entityType;
        public EntityType EntityType => entityType;
        public void SetEntityType(EntityType v) => entityType = v;

        int typeID;
        public int TypeID => typeID;
        public void SetTypeID(int v) => typeID = v;

        int id;
        public int ID => id;
        public void SetID(int v) => id = v;

        ControlType controlType;
        public ControlType ControlType => controlType;
        public void SetControlType(ControlType v) => controlType = v;

        public IDComponent() { }

    }

}