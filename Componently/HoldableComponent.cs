using System;
using System.Collections.Generic;
using System.Text;

namespace Hazdryx.Componently
{
    /// <summary>
    ///     A component which can have a component holder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HoldableComponent<T> : IHoldableComponent where T : IComponentHolder
    {
        public T Holder
        {
            get => _holder;
            set => ((IHoldableComponent)this).Holder = value;
        }

        IComponentHolder IHoldableComponent.Holder
        {
            get => _holder;
            set
            {
                if (value is T cast)
                {
                    if (_holder == null) _holder = cast;
                    else throw new ArgumentException("A holdable component can only have one holder.");
                }
                else throw new ArgumentException("Holder must be castable from " + typeof(T).FullName + ".");
            }
        }
        private T _holder;
    }
}
