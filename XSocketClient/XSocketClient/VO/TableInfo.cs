using System;

namespace XSocketClient
{
    public class TableInfo
    {
        public int ID;

        public ushort Status;
    }

    /// <summary>
    /// Sit down V.
    /// </summary>
    public class SitDownVO:NetworkVOBase
    {

        private int tableId;

        private int SeatId;

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
            offset += BitConvert.MemoryCopy(tableId, ref b, offset); 
            offset += BitConvert.MemoryCopy(SeatId, ref b, offset); 

            return b;
        }
        /// <summary>
        /// Deserialization the specified val and offset.
        /// </summary>
        /// <param name="val">Value.</param>
        /// <param name="offset">Offset.</param>
        public override int Deserialization(byte[] val, int offset)
        {
            int index = base.Deserialization(val, offset);

            tableId = BitConvert.GetInt32(val, ref index);
            SeatId = BitConvert.GetInt32(val, ref index);
            if (ReturnCode == 0)
            {
                Session.TableID = tableId;
                Session.SeatID = SeatId;
            }
            return index;
        }
        public SitDownVO()
        {
            MsgID = GlobalConstantNet.CodeSitdown;
            DestType = NetObjectType.FE_ROOM;
            DestId = Session.RoomId;
        }

        public SitDownVO(int table, int seat)
        {
            tableId = table;
            SeatId = seat;
            MsgID = GlobalConstantNet.CodeSitdown;
            DestType = NetObjectType.FE_ROOM;
            DestId = Session.RoomId;
        }

    }

    /// <summary>
    /// Standup V.
    /// </summary>
    public class StandupVO: NetworkVOBase
    {
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

            return b;
        }

        public StandupVO()
        {
            MsgID = GlobalConstantNet.CodeStandup;
            DestType = NetObjectType.FE_ROOM;
            DestId = Session.RoomId;
        }
    }
}

