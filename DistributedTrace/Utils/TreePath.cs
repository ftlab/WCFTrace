using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedTrace.Utils
{
    /// <summary>
    /// ПУть
    /// </summary>
    public class TreePath
    {
        /// <summary>
        ///Разделитель
        /// </summary>
        public const string SEPARATOR = "/";

        /// <summary>
        /// стэк элементов
        /// </summary>
        private Stack<string> _stack = new Stack<string>();

        /// <summary>
        /// значение
        /// </summary>
        private string _value;

        /// <summary>
        /// последний элемент пути
        /// </summary>
        private string _lastItem;

        /// <summary>
        /// Путь
        /// </summary>
        /// <param name="path"></param>
        public TreePath(string path = null)
        {
            if (path != null)
            {
                var items = path.Split(new string[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in items)
                    Push(item);
            }
        }

        /// <summary>
        /// Путь
        /// </summary>
        /// <param name="path"></param>
        public TreePath(TreePath path)
        {
            if (path == null) throw new ArgumentNullException("path");

            foreach (var item in path._stack.Reverse())
                Push(item);
        }

        /// <summary>
        /// Уровень
        /// </summary>
        public int Level { get { return _stack.Count - 1; } }

        /// <summary>
        /// Последний элемент
        /// </summary>
        public string LastItem { get { return _lastItem; } }

        /// <summary>
        /// Значение
        /// </summary>
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

        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="name"></param>
        public void Push(string name)
        {
            _lastItem = name;
            _stack.Push(name);
            _value = null;
        }

        /// <summary>
        /// Извлечь
        /// </summary>
        /// <param name="cnt"></param>
        public void Pop(int cnt = 1)
        {
            while (cnt > 0)
            {
                _lastItem = _stack.Pop();
                cnt--;
            }
            _value = null;
        }

        /// <summary>
        /// Сравнение
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Equals(TreePath p)
        {
            if (p == null) return false;

            return p.Value == Value;
        }

        /// <summary>
        /// Сравнение
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as TreePath);
        }

        /// <summary>
        /// Получить хэш код
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Отображаемое значение
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
