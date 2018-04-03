﻿using System;

namespace DistributedTrace.Core
{
    /// <summary>
    /// Окружение контекста трассировки
    /// </summary>
    public class TraceContextScope : IDisposable
    {
        /// <summary>
        /// Текущее окружение
        /// </summary>
        static AsyncLocal<TraceContextScope> _current = new AsyncLocal<TraceContextScope>();

        /// <summary>
        /// флаг очистки
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// предыдущая область
        /// </summary>
        private TraceContextScope _saved;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mode"></param>
        public TraceContextScope(TraceId id
            , TraceEvent root
            , TraceContextMode mode)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (root == null) throw new ArgumentNullException("root");

            Id = id;
            Root = root;
            Mode = mode;

            PushScope();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        public TraceContextScope(string eventName
            , TraceContextMode mode = TraceContextMode.Add
            , string source = null
            , string type = null)
        {
            if (Current != null)
                Id = Current.Id;
            else
                Id = TraceId.Create(eventName);

            Root = TraceEvent.Create(Id, eventName, source, type);
            Mode = mode;

            PushScope();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public TraceId Id { get; private set; }

        /// <summary>
        /// Событие трассировки
        /// </summary>
        public TraceEvent Root { get; private set; }

        /// <summary>
        /// Режим трассировки
        /// </summary>
        public TraceContextMode Mode { get; private set; }

        /// <summary>
        /// Требуется запись
        /// </summary>
        public bool RequiredWrite
        {
            get
            {
                if (Mode == TraceContextMode.None)
                    return false;
                else if (Mode == TraceContextMode.New)
                    return true;
                else if (Mode == TraceContextMode.NewAndAdd)
                    return true;
                else if (Mode == TraceContextMode.Add)
                    return false;
                else if (Mode == TraceContextMode.AddOrNew)
                    return _saved == null;
                else throw new ArgumentOutOfRangeException(Mode.ToString());
            }
        }

        /// <summary>
        /// Требуется передать событие выше
        /// </summary>
        public bool RequiredAdd
        {
            get
            {
                if (Mode == TraceContextMode.None)
                    return false;
                else if (Mode == TraceContextMode.New)
                    return false;
                else if (Mode == TraceContextMode.NewAndAdd)
                    return _saved != null;
                else if (Mode == TraceContextMode.Add)
                    return _saved != null;
                else if (Mode == TraceContextMode.AddOrNew)
                    return _saved != null;
                else throw new ArgumentOutOfRangeException(Mode.ToString());
            }
        }

        /// <summary>
        /// Контекст трассировки
        /// </summary>
        public TraceContext Context { get; private set; }

        /// <summary>
        /// Текущее окружение
        /// </summary>
        public static TraceContextScope Current
        {
            get
            {
                return _current.Value;
            }
            private set
            {
                _current.Value = value;
            }
        }

        /// <summary>
        /// Положить окружение
        /// </summary>
        private void PushScope()
        {
            Context = new TraceContext(Id, Root);

            _saved = Current;
            Current = this;
        }

        /// <summary>
        /// Извлечь окружение
        /// </summary>
        private void PopScope()
        {
            Current = _saved;
        }

        /// <summary>
        /// Очистить
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;

            Context.Root.SetEnd(Id);

            if (RequiredWrite)
                TraceWriter.Default.Write(Id, Context.Root);

            if (RequiredAdd)
                _saved.Context.AppendEvent(Context.Root);

            PopScope();
            _disposed = true;
        }
    }
}
