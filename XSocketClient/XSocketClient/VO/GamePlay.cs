using System;

namespace XSocketClient
{
    public class GameOperationBase:NetworkVOBase
    {
        protected CustomerArray<byte> subOperation;

        /// <summary>
        /// Serialization this instance.
        /// </summary>
        public override CustomerArray<byte> Serialization()
        {
            CustomerArray<byte> b = base.Serialization();
            if(b == null)
                return null;

            int offset = b.CurrentLength;
            offset += BitConvert.MemoryCopy((UInt64)Session.Uid, ref b, offset);
            offset += BitConvert.MemoryCopy(0, ref b, offset); //time.
            offset += BitConvert.MemoryCopy((ushort)subOperation.CurrentLength, ref b, offset);
            for (int i = 0, max = subOperation.CurrentLength,j = b.CurrentLength; i < max; i++,j++)
            {
                b[j] = subOperation[i];
            }

            return b;
        }

        public GameOperationBase()
        {
            MsgID = GlobalConstantNet.CodePlayGame;
            DestType = NetObjectType.FE_TABLE;
            DestId = Session.TableID;
            subOperation = new CustomerArray<byte>(512);
        }

    }

    public class GamePlay:GameOperationBase
    {

    }
}

