using System;
using System.Collections.Generic;

namespace DistributedTrace.Core
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
            return root.Flatten(getChilds, level: 0, position: 0);
        }

        /// <summary>
        /// развернуть дерево
        /// </summary>
        /// <typeparam name="T">тип узла</typeparam>
        /// <param name="root">узел</param>
        /// <param name="getChilds">получить дочерние узлы</param>
        /// <param name="level">уровень</param>
        /// <param name="position">позиция</param>
        /// <returns></returns>
        private static IEnumerable<TreeNode<T>> Flatten<T>(
            this T root
            , Func<T, IEnumerable<T>> getChilds
            , int level
            , int position)
        {
            if (root == null) throw new ArgumentNullException("root");
            if (getChilds == null) throw new ArgumentNullException("getChilds");

            yield return new TreeNode<T>()
            {
                Node = root,
                Level = level,
                Position = position,
            };

            var childs = getChilds(root);
            if (childs == null) yield break;

            int i = 0;
            foreach (var child in childs)
            {
                foreach (var rc in Flatten(child, getChilds, level + 1, i))
                    yield return rc;
                i++;
            }
        }
    }
}
