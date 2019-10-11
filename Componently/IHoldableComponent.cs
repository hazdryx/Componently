namespace Hazdryx.Componently
{
    /// <summary>
    ///     The base interface for components which can have a holder.
    /// </summary>
    public interface IHoldableComponent : IComponent
    {
        /// <summary>
        ///     Gets or sets the component holder.
        /// </summary>
        IComponentHolder Holder { get; set; }
    }
}
