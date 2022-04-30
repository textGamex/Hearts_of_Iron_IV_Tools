using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts_of_Iron_IV_Tools.Economic
{
    public static class GameFactory
    {
        /// <summary>
        /// 建造速度修正
        /// </summary>
        public static double BuildSpeedFactor => _buildSpeedFactor;
        /// <summary>
        /// 建造速度修正
        /// </summary>
        private static double _buildSpeedFactor = 0.0;

        public static void AddBuildSpeedFactor(double value)
        {
            _buildSpeedFactor += value;
        }
    }
}
