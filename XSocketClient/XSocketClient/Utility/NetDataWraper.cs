// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
namespace XSocketClient
{
    /// <summary>
    /// Net data wraper.
    /// </summary>
    public class NetDataWraper
    {
        /// <summary>
        /// Serialization the specified data and buf.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="buf">Buffer.</param>
        public static Int32 Serialization(IDataWraper data, ref byte[] buf)
        {
            if (data == null)
                return 0;

//            object[] objs = data.Serialization();
            CustomerArray<byte> objs = data.Serialization();
            if (objs == null || objs.CurrentLength < 1)
                return 0;

            int len = 0;

            for(int i = 0, max = objs.CurrentLength; i < max; i++)
            {
                if(objs[i].GetType() == typeof(byte))
                {
                    buf[len] = 3;
                    buf[len+1] = 1;
                    buf[len+2] = Convert.ToByte(objs[i]);
                    len += 3;
                }
            }

            return len;
        }

        /// <summary>
        /// Deserialization the specified buf.
        /// </summary>
        /// <param name="buf">Buffer.</param>
        public static IDataWraper Deserialization(byte[] buf)
        {
            return null;
        }
    }
}
