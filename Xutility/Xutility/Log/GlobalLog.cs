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
namespace XUtility
{
    /// <summary>
    /// Global log.
    /// </summary>
    public class GlobalLog
    {
        private static GlobalLog instance;

        private LogWraper log;

        /// <summary>
        /// Log the specified message.
        /// </summary>
        /// <param name="message">Message.</param>
        public static void Log(string message)
        {
            instance.log.Log(message);
        }
        
        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="message">Message.</param>
        public static void LogWarning(string message)
        {
            instance.log.LogWarning(message);
        }
        
        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">Message.</param>
        public static void LogError(string message)
        {
            instance.log.LogError(message);
        }

        static GlobalLog()
        {
            instance = new GlobalLog();
            instance.log = LogWraper.GetLog<GlobalLog>();
        }
    }
}

