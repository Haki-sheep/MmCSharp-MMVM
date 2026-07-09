using System;
using MiMieMVVM.Demos.CrossModule;
using MiMieMVVM.Demos.InternalFlow;

namespace MiMieMVVM.Demos
{
    /// <summary>
    /// 控制台 Demo 入口
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// 主入口
        /// </summary>
        public static void Main(string[] args)
        {
            Demo1InternalFlow.Run();
            Demo2CrossModule.Run();

            Console.WriteLine("=== Demo3 Unity 引擎调用 ===");
            Console.WriteLine("见 samples/MiMieMVVM.Demos/Unity/README.md");
            Console.WriteLine("源码 Demo3UnityBagView.cs 需在 Unity 工程定义 MIMIE_UNITY 后使用");
        }
    }
}
