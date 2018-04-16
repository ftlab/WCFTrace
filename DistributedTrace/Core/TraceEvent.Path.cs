using DistributedTrace.Utils;
using System.Collections.Generic;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Событие трассировки. Навигация
    /// </summary>
    public partial class TraceEvent
    {
        /// <summary>
        /// Извлечение событий по значению свойства
        /// </summary>
        /// <param name="name">имя свойства</param>
        /// <param name="value">значение свойства</param>
        /// <returns></returns>
        public IEnumerable<TraceEvent> ByProperty(string name, string value)
        {
            foreach (var node in Flatten())
            {
                if (node.Value[name] == value)
                    yield return node.Value;
            }
        }

        /// <summary>
        /// Извлечение событий по имени
        /// </summary>
        /// <param name="name">имя события</param>
        /// <returns></returns>
        public IEnumerable<TraceEvent> ByName(string name)
        {
            foreach (var node in Flatten())
            {
                if (node.Value.Name == name)
                    yield return node.Value;
            }
        }

        /// <summary>
        /// Извлечение событий по пути
        /// </summary>
        /// <param name="path">путь</param>
        /// <returns></returns>
        public IEnumerable<TraceEvent> ByPath(string path)
        {
            var tracePath = new TreePath();
            int prevLevel = -1;
            foreach (var node in Flatten())
            {
                if (node.Level > prevLevel)
                    tracePath.Push(node.Value.Name);
                else if (node.Level < prevLevel)
                    tracePath.Pop(prevLevel - node.Level);

                if (tracePath.Value == path)
                    yield return node.Value;

                prevLevel = node.Level;
            }
        }
    }
}
