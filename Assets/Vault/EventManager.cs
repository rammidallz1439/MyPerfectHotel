using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Vault
{
    public class EventManager : IObserver, ITickable
    {
        public bool LimitQueueProcesing = false;
        public int QueueProcessTime = 20;

        private static EventManager _instance = null;
        private static bool isDestroyed = false;
        public delegate void EventDelegate<T>(T e) where T : GameEvent;
        private delegate void EventDelegate(GameEvent e);

        private readonly Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
        private readonly Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();
        private readonly Dictionary<System.Delegate, bool> onceLookups = new Dictionary<System.Delegate, bool>();
        private readonly Queue m_eventQueue = new Queue();

        public static EventManager Instance
        {
            get
            {
                if (!isDestroyed && _instance == null)
                {
                    _instance = new EventManager();
                }

                return _instance;
            }
        }

        public bool IsDestroyed
        {
            get
            {
                return isDestroyed;
            }

            set
            {
                isDestroyed = value;
            }
        }


        private EventDelegate AddDelegate<T>(EventDelegate<T> del) where T : GameEvent
        {
            // Early-out if we've already registered this delegate
            if (delegateLookup.ContainsKey(del))
            {
                return null;
            }

            // Create a new non-generic delegate which calls our generic one.
            // This is the delegate we actually invoke.
            EventDelegate internalDelegate = (e) => del((T)e);
            delegateLookup[del] = internalDelegate;

            EventDelegate tempDel = null;
            if (delegates.TryGetValue(typeof(T), out tempDel))
            {
                delegates[typeof(T)] = tempDel += internalDelegate;
            }
            else
            {
                delegates[typeof(T)] = internalDelegate;
            }

            return internalDelegate;
        }

        public void AddListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            AddDelegate<T>(del);
        }
        private void AddListenerOnce<T>(EventDelegate<T> del) where T : GameEvent
        {
            EventDelegate result = AddDelegate<T>(del);

            if (result != null)
            {
                // remember this is only called once
                onceLookups[result] = true;
            }
        }

        public void RemoveListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            if (delegateLookup.TryGetValue(del, out EventDelegate internalDelegate))
            {
                if (delegates.TryGetValue(typeof(T), out EventDelegate tempDel))
                {
                    tempDel -= internalDelegate;

                    if (tempDel == null)
                    {
                        delegates.Remove(typeof(T));
                    }
                    else
                    {
                        delegates[typeof(T)] = tempDel;
                    }
                }

                delegateLookup.Remove(del);
            }
        }

        private void RemoveAll()
        {
            delegates.Clear();
            delegateLookup.Clear();
            onceLookups.Clear();
        }

        public bool HasListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            return delegateLookup.ContainsKey(del);
        }
        public void TriggerEvent(GameEvent e)
        {
            if (delegates.TryGetValue(e.GetType(), out EventDelegate del))
            {
                del.Invoke(e);
            }
            else
            {
                // Debug.Log("Event: " + e.GetType() + " has no listeners");
            }
        }

        public bool QueueEvent(GameEvent evt)
        {
            if (!delegates.ContainsKey(evt.GetType()))
            {
                // Log.Warning("EventManager: QueueEvent failed due to no listeners for event: " + evt.GetType());
                return false;
            }

            m_eventQueue.Enqueue(evt);
            return true;
        }


        public void OnNotify()
        {
        }
        public void OnRelease()
        {
            RemoveAll();
            m_eventQueue.Clear();
            isDestroyed = true;
        }
        public void Tick()
        {
            DateTime startTime = DateTime.Now;

            while (m_eventQueue.Count > 0)
            {
                if (LimitQueueProcesing)
                {
                    if ((DateTime.Now - startTime).Milliseconds > QueueProcessTime)
                    {
                        return;
                    }
                }

                GameEvent evt = m_eventQueue.Dequeue() as GameEvent;
                TriggerEvent(evt);
            }
        }

        public void RegisterListeners()
        {
        }

        public void RemoveListeners()
        {
        }
        public void OnEnable()
        {

        }
        public void OnStart()
        {
        }
    }
}

