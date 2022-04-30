using Hearts_of_Iron_IV_Tools.Country;

namespace Hearts_of_Iron_IV_Tools.Economic
{
    public partial class NationalIndustry
    {
        private class BuildTask
        {
            public State State { get; }
            public double BuildSpeed => GetBuildSpeed();
            public BuildingType BuildingType { get; }
            public uint UsingIndustrialComplex { get; set; }
            public uint BuildDays => _buildDays;
            public bool IsCompleted => FactoryOutput >= BuildingType.GetCost();
            public double FactoryOutput => BuildDays * BuildSpeed;
            public double RedundantOutput => FactoryOutput - BuildingType.GetCost();

            private uint _buildDays = 0;

            public BuildTask(BuildingType buildTypd, uint usingIndustrialComplex, State state)
            {
                BuildingType = buildTypd;
                UsingIndustrialComplex = usingIndustrialComplex;
                State = state;
            }

            public void NextDay()
            {
                ++_buildDays;
            }

            private double GetBuildSpeed()
            {
                double infrastructureFactor = 1.0;
                infrastructureFactor += State.GetBuildingNumber(BuildingType.INFRASTRUCTURE) * 
                    BuildingType.INFRASTRUCTURE.GetCost();
                return UsingIndustrialComplex * GameDefines.INDUSTRIAL_COMPLEX_OUTPUT * infrastructureFactor;
            }
        }
    }
}
