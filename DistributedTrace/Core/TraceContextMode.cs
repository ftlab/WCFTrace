namespace DistributedTrace.Core
{
    /// <summary>
    /// Режим трассировки
    /// </summary>
    public enum TraceContextMode
    {
        /// <summary>
        /// Используем NullTrace
        /// </summary>
        None = 0,
        /// <summary>
        /// Создание новой трассировки
        /// </summary>
        New = 1,
        /// <summary>
        /// Добавление в текущую трассировку
        /// </summary>
        Add = 2,
        /// <summary>
        /// Добавление в текущую трассировку или создание новой
        /// </summary>
        AddOrNew = 3,

        /// <summary>
        /// Создание новой и добавление в текущую трассировку
        /// </summary>
        NewAndAdd = 4,
    }
}
