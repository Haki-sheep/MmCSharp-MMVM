using System;
using System.Collections.Generic;

namespace MiMieMVVM
{
    /// <summary>
    /// 跨模块业务服务标记
    /// </summary>
    public interface ICrossBusinessModuleService
    {
    }

    /// <summary>
    /// 业务模块 Cross 服务注册与查找
    /// </summary>
    public class BusinessModuleHub
    {
        private static BusinessModuleHub instance = new BusinessModuleHub();

        public static BusinessModuleHub Instance => instance ??= new BusinessModuleHub();

        private readonly Dictionary<Type, ICrossBusinessModuleService> bsDict = new Dictionary<Type, ICrossBusinessModuleService>();

        /// <summary>
        /// 注册业务 Cross 服务
        /// </summary>
        public void RegisterBusinessModule<T>(T service) where T : class, ICrossBusinessModuleService
        {
            bsDict[typeof(T)] = service;
        }

        /// <summary>
        /// 获取业务 Cross 服务
        /// </summary>
        public T GetBusinessModule<T>() where T : class, ICrossBusinessModuleService
        {
            return (T)bsDict[typeof(T)];
        }

        /// <summary>
        /// 注销业务 Cross 服务
        /// </summary>
        public void UnregisterBusinessModule<T>() where T : class, ICrossBusinessModuleService
        {
            bsDict.Remove(typeof(T));
        }
    }
}
