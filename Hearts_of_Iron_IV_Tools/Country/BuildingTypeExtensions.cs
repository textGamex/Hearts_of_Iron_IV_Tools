using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hearts_of_Iron_IV_Tools.Economic.GameDefines;

namespace Hearts_of_Iron_IV_Tools.Country
{
    public static class BuildingTypeExtensions
    {
        public static int GetCost(this BuildingType type)
        {
            switch (type)
            {
                case BuildingType.INDUSTRIAL_COMPLEX: return INDUSTRIAL_COMPLEX_COST;
                case BuildingType.ARMS_FACTORY: return ARMS_FACTORY_COST;
                default: throw new ArgumentException();
            }
        }
    }
}
