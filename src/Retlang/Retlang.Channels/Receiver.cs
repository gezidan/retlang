using System;
using Retlang.Core;
using Retlang.Fibers;

namespace Retlang.Channels
{
    /// <summary>
    /// Simple receiver that enqueues messages to a fiber.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Receiver<T> : BaseReceiver<T>
    {
        private readonly Action<T> _receiver;

        /// <summary>
        /// Construct the subscription
        /// </summary>
        /// <param name="fiber"></param>
        /// <param name="receiver"></param>
        public Receiver(IFiber fiber, Action<T> receiver)
            : base(fiber)
        {
            _receiver = receiver;
        }

        /// <summary>
        /// Receives the action and queues the execution on the target fiber.
        /// </summary>
        /// <param name="msg"></param>
        protected override void OnMessageOnProducerThread(T msg)
        {
            _fiber.Enqueue(() => _receiver(msg));
        }
    }
}