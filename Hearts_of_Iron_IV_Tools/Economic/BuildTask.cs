using System;
using Hearts_of_Iron_IV_Tools.Country;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts_of_Iron_IV_Tools.Economic
{
    public partial class NationalIndustry
    {
        private class BuildTask
        {
            public double BuildSpeed { get; set; }
            public BuildingType BuildTypd { get; }
            public double BuildingCost { get; }
            public uint UsingIndustrialComplex { get; set; }
            public uint BuildDays => _buildDays;
            public bool IsCompleted => FactoryOutput >= BuildingCost;
            public double FactoryOutput => BuildDays * BuildSpeed;
            public double RedundantOutput => FactoryOutput - BuildingCost;

            private uint _buildDays = 0;

            public BuildTask(BuildingType buildTypd, double buildSpeed, double buildingCost, 
                uint usingIndustrialComplex)
            {
                BuildSpeed = buildSpeed;
                BuildTypd = buildTypd;
                UsingIndustrialComplex = usingIndustrialComplex;
                BuildingCost = buildingCost;
            }

            public void NextDay()
            {
                ++_buildDays;
            }
        }
    }
}
