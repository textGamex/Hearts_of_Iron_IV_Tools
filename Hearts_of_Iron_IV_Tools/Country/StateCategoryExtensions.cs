using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts_of_Iron_IV_Tools.Country
{
    public static class StateCategoryExtensions
    {
        /// <summary>
        /// 得到地块初始建筑槽
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte GetStateCapacity(this StateCategory state)
        {
            switch (state)
            {
                case StateCategory.WASTELAND: return 0;
                case StateCategory.ENCLAVE: return 0;
                case StateCategory.TINY_ISLAND: return 0;
                case StateCategory.SMALL_ISLAND: return 1;
                case StateCategory.PASTORAL: return 1;
                case StateCategory.RURAL: return 2;
                case StateCategory.TOWN: return 4;
                case StateCategory.LARGE_TOWN: return 5;
                case StateCategory.CITY: return 6;
                case StateCategory.LARGE_CITY: return 8;
                case StateCategory.METROPOLIS: return 10;
                case StateCategory.MEGALOPOLIS: return 12;
                default: throw new ArgumentException();
            }
        }
    }
}
