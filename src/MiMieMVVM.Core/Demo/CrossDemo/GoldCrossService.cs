using System.Collections.Generic;

namespace MiMieMVVM.Demo.Cross
{
    /// <summary>
    /// 跨模块金币服务实现
    /// </summary>
    public class GoldCrossService : IGoldCrossService
    {
        private readonly Dictionary<string, int> balanceDict = new Dictionary<string, int>();

        /// <summary>
        /// 模块间转移金币
        /// </summary>
        public bool TryTransfer(string fromModule, string toModule, int amount)
        {
            if (amount <= 0)
                return false;

            if (!balanceDict.TryGetValue(fromModule, out int fromBalance))
                return false;

            if (fromBalance < amount)
                return false;

            balanceDict[fromModule] = fromBalance - amount;

            if (!balanceDict.TryGetValue(toModule, out int toBalance))
                toBalance = 0;

            balanceDict[toModule] = toBalance + amount;
            return true;
        }

        /// <summary>
        /// 查询模块余额
        /// </summary>
        public int GetBalance(string moduleName)
        {
            return balanceDict.TryGetValue(moduleName, out int balance) ? balance : 0;
        }

        /// <summary>
        /// 设置模块初始余额 演示用
        /// </summary>
        public void SetBalance(string moduleName, int balance)
        {
            balanceDict[moduleName] = balance;
        }
    }
}
