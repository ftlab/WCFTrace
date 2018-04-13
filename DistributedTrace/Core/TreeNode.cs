namespace DistributedTrace.Core
{
    /// <summary>
    /// Узел дерева
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNode<T>
    {
        /// <summary>
        /// Уровень
        /// </summary>
        public int Level;
        /// <summary>
        /// Позиция
        /// </summary>
        public int Position;
        /// <summary>
        /// Узел
        /// </summary>
        public T Node;
    }
}
