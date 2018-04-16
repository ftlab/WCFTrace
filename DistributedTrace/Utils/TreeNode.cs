namespace DistributedTrace.Utils
{
    /// <summary>
    /// Узел дерева
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNode<T>
    {
        /// <summary>
        /// Предыдущий узел
        /// </summary>
        public TreeNode<T> Prev;
        /// <summary>
        /// Родительский узел
        /// </summary>
        public TreeNode<T> Parent;
        /// <summary>
        /// Уровень
        /// </summary>
        public int Level;
        /// <summary>
        /// Позиция
        /// </summary>
        public int Position;
        /// <summary>
        /// Значение
        /// </summary>
        public T Value;
    }
}
