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
using System.Collections.Generic;

namespace XUtility
{
    /// <summary>
    /// Message router.
    /// </summary>
    public class MessageRouter<T>
    {
        //private Dictionary<T,CustomerAction<T,Object>> listeners;

        private Dictionary<T,List<CustomerAction<T,Object>>> listeners;

        private LogWraper log;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public MessageRouter()
        {
            //listeners = new Dictionary<T, CustomerAction<T, Object>>();
            listeners = new Dictionary<T, List<CustomerAction<T, object>>>();
            log = LogWraper.GetLog<T>();
        }

        /// <summary>
        /// Subscribe the specified key and fun.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="fun">Fun.</param>
        public void Subscribe(T key, CustomerAction<T,Object> fun)
        {
            if (fun == null || listeners == null)
                return;
//            if (!listeners.ContainsKey(key))
//                listeners.Add(key, fun);
//            else
//                listeners [key] += fun;

            if (!listeners.ContainsKey(key))
            {
                listeners.Add(key, new List<CustomerAction<T, object>>());
            }

            if (listeners [key].Contains(fun))
                return;

            listeners [key].Add(fun);
        }

        /// <summary>
        /// Unsubscribe the specified key and fun.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="fun">Fun.</param>
        public void Unsubscribe(T key, CustomerAction<T,Object> fun)
        {
            if (fun == null || listeners == null)
                return;
            
//            if (listeners.ContainsKey(key))
//            {
//                listeners[key] -= fun;
//                if(listeners[key] == null)
//                    listeners.Remove(key);
//            }

            if (listeners.ContainsKey(key))
            {
                int i = listeners[key].IndexOf(fun);
                if(i != -1)
                    listeners[key].RemoveAt(i);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="param">Parameter.</param>
        public void Notify(T key, Object param)
        {
            if (listeners == null)
                return;

//            if (listeners.ContainsKey(key))
//            {
//                try
//                {
//                    listeners [key](key, param);
//                }
//                catch(Exception e)
//                {
//                    GlobalLog.Log(e.Message);
//                }
//            }

            if (listeners.ContainsKey(key))
            {
                try
                {
                    for(int i =0 , max = listeners [key].Count; i < max; i++)
                        listeners [key][i](key,param);
                } catch (Exception e)
                {
                    log.Log(e.Message);
                }
            }
        }
    }
}

