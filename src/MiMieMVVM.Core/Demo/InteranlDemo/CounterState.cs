namespace MiMieMVVM.Demo.Internal
{
    /// <summary>
    /// 示例 State 运行时计数
    /// </summary>
    public class CounterState : IModelState
    {
        /// <summary> 当前计数 </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 增加计数
        /// </summary>
        public void Increment()
        {
            Count++;
        }
    }
}
