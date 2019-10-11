using System;
using System.Collections;
using System.Collections.Generic;

namespace Hazdryx.Componently
{
    /// <summary>
    ///     A component holder which can add, obtain, and iterate over it's components.
    /// </summary>
    /// <typeparam name="T">The base type of components allowed.</typeparam>
    public class ComponentHolder<T> : IComponentHolder where T : IHoldableComponent
    {
        private TypeFluidList<IComponent> _components = new TypeFluidList<IComponent>();

        /// <summary>
        ///     Adds a new component to the holder.
        /// </summary>
        /// <typeparam name="U">The type of component to add.</typeparam>
        /// <returns>The newly created component.</returns>
        public U AddComponent<U>() where U : T
        {
            U component = _components.Add<U>();
            component.Holder = this;
            return component;
        }

        /// <summary>
        ///     Gets the first component with a specific type.
        /// </summary>
        /// <typeparam name="U">The type of component you want.</typeparam>
        /// <returns>The component found of the specific type or null if none found.</returns>
        public U GetComponent<U>() where U : IComponent => _components.First<U>();
        /// <summary>
        ///     Gets all the components with a specific type.
        /// </summary>
        /// <typeparam name="U">The type of component you want.</typeparam>
        /// <returns>An array of components with the specified type. Can be empty.</returns>
        public U[] GetComponents<U>() where U : IComponent => _components.All<U>();

        /// <summary>
        ///     Calls <code>action</code> for each component.
        /// </summary>
        /// <param name="action"></param>
        public void EachComponent(Action<IComponent> action) => _components.ForEach(action);
        /// <summary>
        ///     Calls <code>action</code> for each component of the specified type.
        /// </summary>
        /// <typeparam name="U">The type of component to iterate.</typeparam>
        /// <param name="action"></param>
        public void EachComponent<U>(Action<U> action) where U : IComponent => _components.ForEach(action);

        public IEnumerator<IComponent> GetEnumerator() => _components.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _components.GetEnumerator();
    }
}
