﻿using System;
using Godot;

namespace EventCallback
{
    public abstract class Event<T> where T : Event<T>
    {
        /*
         * The base Event,
         * might have some generic text
         * for doing Debug.Log?
         */
        public string EventName;
        private bool hasFired;
        public delegate void EventListener(T info);
        private static event EventListener listeners;

        public static void RegisterListener(EventListener listener)
        {
            listeners += listener;
        }
        public static void UnregisterListener(EventListener listener)
        {
            listeners -= listener;
        }
        public void FireEvent()
        {
            if (hasFired)
            {
                throw new Exception("The " + EventName + " has already fired, to prevent infinite loops you can't refire an event");
            }
            hasFired = true;
            if (listeners != null)
            {
                listeners(this as T);
            }
        }
    }
    public class DebugEvent : Event<DebugEvent>
    {
        public int VerbosityLevel;
    }
}