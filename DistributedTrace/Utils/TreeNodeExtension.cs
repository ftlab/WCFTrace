using System;
using System.Collections.Generic;

namespace DistributedTrace.Utils
{
    /// <summary>
    /// Расширение для иерархических структур
    /// </summary>
    public static class TreeNodeExtension
    {
        /// <summary>
        /// Обойти дерево
        /// </summary>
        /// <typeparam name="T">тип узла</typeparam>
        /// <param name="root">узел</param>
        /// <param name="getChilds">получить дочерние узлы</param>
        /// <param name="observer">наблюдатель</param>
        public static void Visit<T>(
            this T root
            , Func<T, IEnumerable<T>> getChilds
            , Action<TreeNode<T>> observer)
        {
            if (observer == null) throw new ArgumentNullException("observer");

            foreach (var node in root.Flatten(getChilds))
                observer(node);
        }

        /// <summary>
        /// Развернуть дерево
        /// </summary>
        /// <typeparam name="T">тип узла</typeparam>
        /// <param name="root">узел</param>
        /// <param name="getChilds">получить дочерние узлы</param>
        /// <returns></returns>
        public static IEnumerable<TreeNode<T>> Flatten<T>(
            this T root
            , Func<T, IEnumerable<T>> getChilds)
        {
            return root.Flatten(getChilds, parent: null, prev: null);
        }

        /// <summary>
        /// развернуть дерево
        /// </summary>
        /// <typeparam name="T">тип значения</typeparam>
        /// <param name="value">значение</param>
        /// <param name="getChilds">получить дочерние узлы</param>
        /// <param name="parent">родитель</param>
        /// <param name="prev">предыдущий</param>
        /// <returns></returns>
        private static IEnumerable<TreeNode<T>> Flatten<T>(
            this T value
            , Func<T, IEnumerable<T>> getChilds
            , TreeNode<T> parent
            , TreeNode<T> prev)
        {
            if (value == null) throw new ArgumentNullException("node");
            if (getChilds == null) throw new ArgumentNullException("getChilds");

            int level = 0;
            if (parent != null)
                level = parent.Level + 1;
            int position = 0;
            if (prev != null)
                position = prev.Position + 1;


            var next = new TreeNode<T>()
            {
                Value = value,
                Level = level,
                Position = position,
                Parent = parent,
                Prev = prev,
            };

            yield return next;

            var childs = getChilds(value);
            if (childs == null) yield break;

            parent = next;
            prev = null;
            foreach (var child in childs)
            {
                foreach (var rc in Flatten(child, getChilds, parent, prev))
                {
                    yield return rc;
                    prev = rc;
                }
            }
        }
    }
}
