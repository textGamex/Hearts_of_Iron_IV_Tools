using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts_of_Iron_IV_Tools.Country
{
    /// <summary>
    /// 国家信息
    /// </summary>
    [Serializable]
    public class CountryInfo
    {
        /// <summary>
        /// 国家Tag
        /// </summary>
        public string Tag { get; }
        public int StatesCount => _states.Count;

        private readonly List<State> _states;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CountryInfo(string tag)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
            _states = new List<State>();
        }

        public void AddState(State state)
        {
            _states.Add(state);
        }

        /// <summary>
        /// 获得国家总人口
        /// </summary>
        /// <returns>国家总人口</returns>
        public long GetTotalManpower()
        {
            long totalManpower = 0;
            foreach (var state in _states)
            {
                totalManpower += state.Manpower;
            }
            return totalManpower;
        }

        public uint GetTotalBuildingNumber(BuildingType type)
        {
            uint totalBuildingNumber = 0;
            foreach (var state in _states)
            {
                totalBuildingNumber += state.GetBuildingNumber(type);
            }
            return totalBuildingNumber;
        }

        public uint GetTotalResourcesNumber(ResourcesType type)
        {
            uint totalResourcesNumber = 0;
            foreach (var state in _states)
            {
                totalResourcesNumber += state.GetResourcesNumber(type);
            }
            return totalResourcesNumber;
        }

        public List<State> GetAllState()
        {
            return _states.ToList();
        }

        public static List<CountryInfo> GetCountryInfoList(IEnumerable<State> states)
        {
            var countryInfos = new Dictionary<string, CountryInfo>();
            foreach (var state in states)
            {
                if (countryInfos.ContainsKey(state.Owner))
                {
                    countryInfos[state.Owner].AddState(state);
                }
                else
                {
                    var info = new CountryInfo(state.Owner);
                    countryInfos.Add(state.Owner, info);
                    info.AddState(state);
                }
            }
            var list = new List<CountryInfo>(countryInfos.Count);
            foreach (var countryInfo in countryInfos.Values)
            {
                list.Add(countryInfo);
            }
            return list;
        }
    }
}
