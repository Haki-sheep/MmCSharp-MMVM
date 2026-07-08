namespace MiMieMVVM.Demo.Internal
{
    /// <summary>
    /// 示例 VM 编排用例 对外 API
    /// </summary>
    public class CounterViewModel : IViewModel<CounterState>
    {
        private CounterState state = new CounterState();

        public CounterState Model => state;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            state = new CounterState();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Shutdown()
        {
            state = null!;
        }

        /// <summary>
        /// 点击加一
        /// </summary>
        public void OnAddClick()
        {
            state.Increment();
        }
    }
}
