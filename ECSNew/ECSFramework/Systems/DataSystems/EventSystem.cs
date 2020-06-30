using ECSFramework.Args;
using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.DataSystems
{
    public class EventSystem : IEventSystem, IECSUpdateable
    {
        private EventHandler<EventArgs> _handlers;

        private Queue<EventArgs> _eventQueue;
        /// <summary>
        /// CONSTRUCTOR for EventSystem
        /// </summary>
        public EventSystem() 
        {
            _eventQueue = new Queue<EventArgs>();
        }

        #region IECSUpdateable
        public void Update(GameTime gameTime) 
        {
            FireQueueEvents();
        }
        #endregion

        #region IEventSystem
        /// <summary>
        /// METHOD: AddToQueue a method which will be passed as a delegate for event publishers to 
        /// queue an event which has occured
        /// </summary>
        /// <param name="args"></param>
        public void AddToQueue(EventArgs args) 
        {
            _eventQueue.Enqueue(args);
        }
        /// <summary>
        /// METHOD: AddHandler a method which adds a handler to an EventHandler
        /// </summary>
        /// <param name="handler"></param>
        public void AddHandler(EventHandler<EventArgs> handler)
        {
            _handlers += handler;
        }

        /// <summary>
        /// METHOD: RemoveHandler, a method which removes a handler from the EventHandler
        /// </summary>
        /// <param name="handler"></param>
        public void RemoveHandler(EventHandler<EventArgs> handler)
        {
            _handlers -= handler;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// METHOD: FireQueueEvents, a method which fires the events in a queue
        /// starting with the first added
        /// </summary>
        private void FireQueueEvents() 
        {
            if (_eventQueue != null)
                if (_eventQueue.Count > 0)
                {
                    for (int i = _eventQueue.Count - 1; i > 0; i--)
                    {
                        if (_eventQueue.ElementAt(i) is RemoveEntityArgs)
                            _handlers(this, _eventQueue.Dequeue());
                    }
                    _handlers(this, _eventQueue.Dequeue());
                }
            

            //_eventQueue.Clear();
        }

        #endregion 
    }
}
