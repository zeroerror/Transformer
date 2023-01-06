using FixMath.NET;

namespace Transformer.Bussiness.LogicBussiness
{

    public class InputComponent
    {

        public FPVector3 moveDir;
        public bool jump;

        public InputComponent() { }

        public void Reset()
        {
            moveDir = FPVector3.Zero;
            jump = false;
        }

    }

}