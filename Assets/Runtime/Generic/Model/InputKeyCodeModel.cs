using UnityEngine;
using ZeroFrame.Buffer;

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

        public byte[] ToBytes()
        {
            byte[] res = new byte[80];
            int offset = 0;
            BufferWriter.WriteUInt16(res, (ushort)jump_key, ref offset);
            BufferWriter.WriteUInt16(res, (ushort)moveForward_key, ref offset);
            BufferWriter.WriteUInt16(res, (ushort)moveBackward_key, ref offset);
            BufferWriter.WriteUInt16(res, (ushort)moveLeft_key, ref offset);
            BufferWriter.WriteUInt16(res, (ushort)moveRight_key, ref offset);
            return res;
        }

        public void FromBytes(byte[] bytes)
        {
            int offset = 0;
            jump_key = (KeyCode)BufferReader.ReadUInt16(bytes, ref offset);
            moveForward_key = (KeyCode)BufferReader.ReadUInt16(bytes, ref offset);
            moveBackward_key = (KeyCode)BufferReader.ReadUInt16(bytes, ref offset);
            moveLeft_key = (KeyCode)BufferReader.ReadUInt16(bytes, ref offset);
            moveRight_key = (KeyCode)BufferReader.ReadUInt16(bytes, ref offset);
        }

    }

}