using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Core
{
    public class TracePath
    {
        public const string SEPARATOR = "/";

        private Stack<string> _stack = new Stack<string>();

        private string _value;

        private string _lastItem;

        public TracePath(string path = null)
        {
            if (path != null)
            {
                var items = path.Split(new string[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in items)
                    Push(item);
            }
        }

        public TracePath(TracePath path)
        {
            if (path == null) throw new ArgumentNullException("path");

            foreach (var item in path._stack.Reverse())
                Push(item);
        }

        public int Level { get { return _stack.Count - 1; } }

        public string LastItem { get { return _lastItem; } }

        public string Value
        {
            get
            {
                if (_value == null)
                {
                    _value = string.Join(SEPARATOR, _stack.Reverse().ToArray());
                }
                return _value;
            }
        }

        public void Push(string name)
        {
            _lastItem = name;
            _stack.Push(name);
            _value = null;
        }

        public void Pop(int cnt = 1)
        {
            while (cnt > 0)
            {
                _lastItem = _stack.Pop();
                cnt--;
            }
            _value = null;
        }

        public bool Equals(TracePath p)
        {
            if (p == null) return false;

            return p.Value == Value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TracePath);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
