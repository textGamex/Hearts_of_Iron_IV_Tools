using Hearts_of_Iron_IV_Tools.Country;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Hearts_of_Iron_IV_Tools.Economic
{
    public partial class NationalIndustry
    {
        public const int ARMS_FACTORY_COST = 7200;
        public const int INDUSTRIAL_COMPLEX_COST = 10800;
        public const byte INDUSTRIAL_COMPLEX_OUTPUT = 5;
        public const byte MAX_INDUSTRIAL_COMPLEX_INVESTMENT = 15;

        public string Tag { get; }
        public uint Day { get; private set; } = 0;
        /// <summary>
        /// 民用工厂数量
        /// </summary>
        public uint IndustrialComplexNumber { get; private set; }
        /// <summary>
        /// 军用工厂数量
        /// </summary>
        public uint ArmsFactoryNumber { get; private set; }
        /// <summary>
        /// 建造速度修正
        /// </summary>
        private double _buildSpeedFactor = 0.0;
        /// <summary>
        /// 建造速度修正
        /// </summary>
        public double BuildSpeedFactor => _buildSpeedFactor;
        /// <summary>
        /// 工厂总数
        /// </summary>
        public uint TotalFactoryNumber => IndustrialComplexNumber + ArmsFactoryNumber;
        private double _buildIndustrialComplex = 0.0;
        private double _buildArmsFactory = 0.0;
        private readonly CountryInfo _countryInfo;
        private readonly DateTime _gameStartDateTime = new DateTime(1936, 1, 1, 12, 0, 0);
        /// <summary>
        /// 按基建等级从高到低排序的地块列表
        /// </summary>
        private readonly List<State> _states;
        private readonly Dictionary<State, (BuildingType type, double buildSpeed)> _buildTaskMap = 
            new Dictionary<State, (BuildingType type, double buildSpeed)>(8);

        public NationalIndustry(CountryInfo countryInfo)
        {
            _countryInfo = countryInfo;
            Tag = countryInfo.Tag;
            IndustrialComplexNumber = _countryInfo.GetTotalBuildingNumber(BuildingType.INDUSTRIAL_COMPLEX);
            ArmsFactoryNumber = countryInfo.GetTotalBuildingNumber(BuildingType.ARMS_FACTORY);
            _states = countryInfo
                .GetAllState()
                .OrderByDescending((x) => x.GetBuildingNumber(BuildingType.INFRASTRUCTURE))
                .ToList();
        }

        public void NextDay()
        {
        }

        public string GetDate()
        {
            return _gameStartDateTime.AddDays(Day).ToString();
        }

        public void AddBuildSpeedFactor(double value)
        {
            _buildSpeedFactor += value;
        }
    }
}
