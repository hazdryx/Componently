using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hazdryx.Componently
{
    /// <summary>
    ///     A special type of list which allows you to add and obtain
    ///     elements by type.
    /// </summary>
    /// <typeparam name="T">The base type which other types added are based on.</typeparam>
    public sealed class TypeFluidList<T> : IEnumerable<T>
    {
        // The core list which to work from.
        private List<T> _elements;

        /// <summary>
        ///     A blank contructor.
        /// </summary>
        public TypeFluidList() : this(new List<T>()) { }
        private TypeFluidList(List<T> elements)
        {
            this._elements = elements;
        }

        /// <summary>
        ///     Adds an instance of the element type to the list.
        /// </summary>
        /// <typeparam name="E">The type of element to instantiate.</typeparam>
        /// <param name="objs">The parameters for the contructor.</param>
        /// <returns>The element which was added to the list.</returns>
        public E Add<E>(params object[] objs) where E : T
        {
            E element = (E)Activator.CreateInstance(typeof(E), objs);
            _elements.Add(element);
            return element;
        }
        /// <summary>
        ///     Adds a pre created element to this list.
        /// </summary>
        /// <param name="element">The element being added.</param>
        public void Add(T element)
        {
            _elements.Add(element);
        }
        /// <summary>
        ///     Removes an element from this list.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        /// <returns>Whether the element was removed.</returns>
        public bool Remove(T element) => _elements.Remove(element);

        /// <summary>
        ///     Gets the first element in the list of the element type.
        /// </summary>
        /// <typeparam name="E">The type of element to get.</typeparam>
        /// <returns>The element obtained or the default value.</returns>
        public E First<E>() where E : T
        {
            foreach (T element in _elements)
            {
                if (element is E cast) return cast;
            }
            return default;
        }
        /// <summary>
        ///     Gets all the elements in the list of the element type.
        /// </summary>
        /// <typeparam name="E">The type of element to get.</typeparam>
        /// <returns>The array of elements obtained. This can be empty.</returns>
        public E[] All<E>() where E : T
        {
            List<E> casts = new List<E>();
            foreach (T element in _elements)
            {
                if (element is E cast) casts.Add(cast);
            }
            return casts.ToArray();
        }

        /// <summary>
        ///     Iterates over all elements in the list.
        /// </summary>
        /// <param name="action">The action performed on each element.</param>
        /// <param name="useSnapshot">Whether to iterate over the list or the snapshot.</param>
        public void ForEach(Action<T> action, bool useSnapshot)
        {
            List<T> list = useSnapshot ? _elements.ToList() : _elements;
            foreach (T element in list)
            {
                action(element);
            }
        }
        /// <summary>
        ///     Iterates over all elements in the actual list.
        /// </summary>
        /// <param name="action">The action performed on each element.</param>
        public void ForEach(Action<T> action) => ForEach(action, false);
        /// <summary>
        ///     Iterates over all elements which are the specified type in the list.
        /// </summary>
        /// <typeparam name="E">The type to iterate over.</typeparam>
        /// <param name="action">The action called on each element.</param>
        /// <param name="useSnapshot">Whether to iterate over the list or the snapshot.</param>
        public void ForEach<E>(Action<E> action, bool useSnapshot) where E : T
        {
            List<T> list = useSnapshot ? _elements.ToList() : _elements;
            foreach (T element in list)
            {
                if (element is E cast) action(cast);
            }
        }
        /// <summary>
        ///     Iterates over all elements which are the specified type in the actual list.
        /// </summary>
        /// <typeparam name="E">The type to iterate over.</typeparam>
        /// <param name="action">The action called on each element.</param>
        public void ForEach<E>(Action<E> action) where E : T => ForEach<E>(action, false);

        /// <summary>
        ///     Gets a snapshot of this list.
        /// </summary>
        public TypeFluidList<T> Snapshot => new TypeFluidList<T>(_elements.ToList());

        public IEnumerator<T> GetEnumerator() => _elements.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _elements.GetEnumerator();
    }
}
