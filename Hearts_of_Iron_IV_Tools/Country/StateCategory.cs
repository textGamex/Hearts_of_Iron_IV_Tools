using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts_of_Iron_IV_Tools.Country
{
    public enum StateCategory : byte
    {
        /// <summary>
        /// 荒地
        /// </summary>
        WASTELAND,
        /// <summary>
        /// 飞地
        /// </summary>
        ENCLAVE,
        /// <summary>
        /// 小岛
        /// </summary>
        TINY_ISLAND,
        /// <summary>
        /// 小岛
        /// </summary>
        SMALL_ISLAND,
        /// <summary>
        /// 田园
        /// </summary>
        PASTORAL,
        /// <summary>
        /// 乡村
        /// </summary>
        RURAL,
        /// <summary>
        /// 城镇
        /// </summary>
        TOWN,
        /// <summary>
        /// 发达城镇
        /// </summary>
        LARGE_TOWN,
        /// <summary>
        /// 城市
        /// </summary>
        CITY,
        /// <summary>
        /// 大城市
        /// </summary>
        LARGE_CITY,
        /// <summary>
        /// 大都市
        /// </summary>
        METROPOLIS,
        /// <summary>
        /// 特大城市
        /// </summary>
        MEGALOPOLIS
    }
}
