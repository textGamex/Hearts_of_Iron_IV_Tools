using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts_of_Iron_IV_Tools.Economic
{
    /// <summary>
    /// 游戏常量定义
    /// </summary>
    public static class GameDefines
    {
        /// <summary>
        /// 军用工厂造价
        /// </summary>
        public const int ARMS_FACTORY_COST = 7200;
        /// <summary>
        /// 民用工厂造价
        /// </summary>
        public const int INDUSTRIAL_COMPLEX_COST = 10800;
        /// <summary>
        /// 每个民用工厂的产出
        /// </summary>
        public const byte INDUSTRIAL_COMPLEX_OUTPUT = 5;
        /// <summary>
        /// 可投入的民用工厂最大数量
        /// </summary>
        public const byte MAX_INDUSTRIAL_COMPLEX_INVESTMENT = 15;
        /// <summary>
        /// 每级基础设施加成
        /// </summary>
        public const double INFRASTRUCTURE_BONUS_PER_LEVEL = 0.2;
    }
}
