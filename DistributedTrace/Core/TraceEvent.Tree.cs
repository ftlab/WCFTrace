using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Разбор дерева событий
    /// </summary>
    public partial class TraceEvent
    {
        /// <summary>
        /// Развернуть событие
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreeNode<TraceEvent>> Flatten()
        {
            return this.Flatten(e => e.Events);
        }

        /// <summary>
        /// Обойти событие
        /// </summary>
        /// <param name="observer"></param>
        public void Visit(Action<TreeNode<TraceEvent>> observer)
        {
            this.Visit(e => e.Events, observer);
        }
    }
}
