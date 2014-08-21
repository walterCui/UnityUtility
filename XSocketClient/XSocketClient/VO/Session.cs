using System;

namespace XSocketClient
{
    /// <summary>
    /// Session.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// The uid.
        /// </summary>
        public static UInt64 Uid = 99999999;

        /// <summary>
        /// The dialog identifier.
        /// </summary>
        public static Int32 DialogId = -1;

        /// <summary>
        /// The player I.
        /// </summary>
        public static Int32 PlayerID = -1;

        /// <summary>
        /// The room identifier.
        /// </summary>
        public static Int32 RoomId;

        /// <summary>
        /// The table ID.
        /// </summary>
        public static Int32 TableID;

        /// <summary>
        /// The seat I.
        /// </summary>
        public static Int32 SeatID;

        /// <summary>
        /// The server Ip.
        /// </summary>
        public static string ServerIP;

        /// <summary>
        /// The server port.
        /// </summary>
        public static ushort ServerPort;

    }
}

