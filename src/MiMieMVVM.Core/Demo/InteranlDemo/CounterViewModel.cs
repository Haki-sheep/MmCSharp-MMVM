namespace MiMieMVVM.Demo.Internal
{
    /// <summary>
    /// 示例 VM 一个类一个接口 Model 私有
    /// </summary>
    public class CounterViewModel : IViewModel
    {
        private CounterState state = new CounterState();

        /// <summary>
        /// UI 展示计数
        /// </summary>
        public int DisplayCount => state.Count;

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
