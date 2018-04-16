namespace DistributedTrace.Pivot
{
    /// <summary>
    /// Тип измеряемой величины
    /// </summary>
    public enum MeasureType
    {
        /// <summary>
        /// Кол-во
        /// </summary>
        Count,
        /// <summary>
        /// Время наступления события
        /// </summary>
        BeginTime,
        /// <summary>
        /// Время завершения события
        /// </summary>
        EndTime,
        /// <summary>
        /// Длительность события
        /// </summary>
        Duration,
        /// <summary>
        /// Время не учтенного периода
        /// </summary>
        ExcludedTime
    }
}
