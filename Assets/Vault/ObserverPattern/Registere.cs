using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Vault
{

    public abstract class Registere : MonoBehaviour, ISubject
    {
        public List<IObserver> observers = new List<IObserver>();
        public List<ITickable> ticks = new List<ITickable>();
        public List<IFixedTickable> fixedTicks = new List<IFixedTickable>();
        public List<ILateTickable> lateTickables = new List<ILateTickable>();
    
        private void Awake()
        {
            Initial();
            RegisterListeners();
            NotifyObservers();

        }
        private void OnEnable()
        {
            Enable();

            foreach (IObserver observer in observers)
            {
                observer.OnEnable();


            }
        }

        private void Start()
        {
            OnShow();
            CallStart();
        }

        void Update()
        {
            NotifyTicks();
        }
        private void FixedUpdate()
        {
            NotifyFixedTicks();
        }
        private void LateUpdate()
        {
            NotifyLateTicks();
        }
        private void OnDestroy()
        {
            DeNotifyObservers();
            /*foreach (IObserver item in observers)
            {
                RemoveObserver(item);
            }*/
            observers.Clear();
            MEC.Timing.KillCoroutines();
        }
        public abstract void Initial();
        public abstract void Enable();
        public abstract void OnShow();
        public void AddObserver(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                AddRespectiveTick(observer);
            }
            else
            {
                Debug.LogError("Observer not added or not the right type of observer");
            }

        }
        public void AddRespectiveTick(IObserver observer)
        {

            if (observer is ITickable)
            {
                ticks.Add((ITickable)observer);
            }
            if (observer is IFixedTickable)
            {
                fixedTicks.Add((IFixedTickable)observer);
            }
            if (observer is ILateTickable)
            {
                lateTickables.Add((ILateTickable)observer);
            }


        }


        public void RemoveObserver(IObserver observer)
        {
            observers.Clear();
            /* if (observers.Contains(observer))
             {
                 observers.Remove(observer);
             }*/
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.OnNotify();


            }
        }


        public void CallStart()
        {
            foreach (IObserver observer in observers)
            {
                observer.OnStart();


            }
        }
        public void RegisterListeners()
        {
            foreach (IObserver observer in observers)
            {
                observer.RegisterListeners();

            }
        }
        public void DeNotifyObservers()
        {
            foreach (IObserver observer in observers)
            {

                observer.OnRelease();
                observer.RemoveListeners();

            }

        }
        public void NotifyTicks()
        {

            foreach (ITickable tick in ticks)
            {
                tick.Tick();
            }
        }
        public void NotifyFixedTicks()
        {
            foreach (IFixedTickable tick in fixedTicks)
            {
                tick.FixedTick();
            }
        }
        public void NotifyLateTicks()
        {
            foreach (ILateTickable tick in lateTickables)
            {
                tick.LateTick();
            }
        }

    }
}