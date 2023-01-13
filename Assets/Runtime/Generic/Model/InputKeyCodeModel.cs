using UnityEngine;

namespace Transformer.Generic
{

    public class InputKeyCodeModel
    {

        // KeyCode  ushort  16byte * 5 = 80 byte
        public KeyCode jump_key;
        public KeyCode moveForward_key;
        public KeyCode moveBackward_key;
        public KeyCode moveLeft_key;
        public KeyCode moveRight_key;

        public InputKeyCodeModel() { }

        public void DefaultInit()
        {
            jump_key = KeyCode.Space;
            moveForward_key = KeyCode.W;
            moveBackward_key = KeyCode.S;
            moveLeft_key = KeyCode.A;
            moveRight_key = KeyCode.D;
        }

    }

}