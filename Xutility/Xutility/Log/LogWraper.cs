// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using UnityEngine;
using System;

namespace XUtility
{
    /// <summary>
    /// Log wraper.
    /// </summary>
    public class LogWraper
    {
        /// <summary>
        /// The level.
        /// 0=all 1 = warning and error 2 == error.
        /// </summary>
        public static int Level;

        private string owner;

        private string hint;

        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <returns>The log.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static LogWraper GetLog<T>()
        {
            LogWraper log = new LogWraper();
            log.owner = typeof(T).ToString();
            log.hint = "[" + log.owner + "]";
            return log;
        }

        /// <summary>
        /// Log the specified message.
        /// </summary>
        /// <param name="message">Message.</param>
        public void Log(string message)
        {
            if (LogConsole.Write != null)
                LogConsole.Write("[log]"+hint + message);
            else
                Debug.Log("[log]"+hint + message);
        }

        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="message">Message.</param>
        public void LogWarning(string message)
        {
            Debug.LogWarning(hint + message);
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">Message.</param>
        public void LogError(string message)
        {
            if (LogConsole.Write != null)
                LogConsole.Write("[Err]"+hint + message);
            else
                Debug.LogError("[Err]"+hint + message);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public LogWraper()
        {
        }
    }
}

