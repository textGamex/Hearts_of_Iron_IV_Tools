using NLog;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Hearts_of_Iron_IV_Tools.Country
{
    public partial class State
    {
        /// <summary>
        /// 省份文件解析器
        /// </summary>
        private class StateFileParser
        {
            private const string _MANPOWER = @"(?<=manpower\s*=\s*)\d+";
            private const string _ID = @"(?<=id\s*=\s*)\d+";
            private const string _NAME = @"(?<=name\s*=\s*)\S+";
            private const string _OWNER = @"(?<=owner\s*=\s*)\w+";
            private const string _TYPE = @"(?<=state_category\s*=\s*)\S+";
            private const string _PROVINCES = @"(?<=provinces\s*=\s*{\s*)[^}]*";
            private const string _CORE_COUNTRY_TAG = @"(?<=add_core_of\s*=\s*)\w+";
            private const string _INFRASTRUCTURE = @"(?<=infrastructure\s*=\s*)\d+";
            private const string _INDUSTRIAL_COMPLEX = @"(?<=industrial_complex\s*=\s*)\d+";
            private const string _AIR_BASE = @"(?<=air_base\s*=\s*)\d+";
            private const string _ARMS_FACTORY = @"(?<=arms_factory\s*=\s*)\d+";
            private const string _ANTI_AIR_BUILDING = @"(?<=anti_air_building\s*=\s*)\d+";
            private const string _DOCKYARD = @"(?<=dockyard\s*=\s*)\d+";
            private const string _SYNTHETIC_REFINERY = @"(?<=synthetic_refinery\s*=\s*)\d+";
            private const string _FUEL_SILO = @"(?<=fuel_silo\s*=\s*)\d+";
            private const string _ROCKET_SITE = @"(?<=rocket_site\s*=\s*)\d+";
            private const string _NUCLEAR_REACTOR = @"(?<=nuclear_reactor\s*=\s*)\d+";
            private const string _RADAR_STATION = @"(?<=radar_station\s*=\s*)\d+";

            /* 资源 */
            private const string _ALUMINIUM = @"(?<=aluminium\s*=\s*)\d+";
            private const string _OIL = @"(?<=oil\s*=\s*)\d+";
            private const string _RUBBER = @"(?<=rubber\s*=\s*)\d+";
            private const string _TUNGSTEN = @"(?<=tungsten\s*=\s*)\d+";
            private const string _STEEL = @"(?<=steel\s*=\s*)\d+";
            private const string _CHROMIUM = @"(?<=chromium\s*=\s*)\d+";

            private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
            private readonly string _text;

            /// <summary>
            /// 构造省份文件解析器
            /// </summary>
            /// <param name="text"></param>
            /// <exception cref="ArgumentNullException"></exception>
            public StateFileParser(string text)
            {
                _text = text ?? throw new ArgumentNullException(nameof(text));
            }

            public List<int> GetStateProvinces()
            {
                List<int> provincesList = new List<int>();
                var provincesText = Regex.Match(_text, _PROVINCES, RegexOptions.Compiled).Value.Trim();
                foreach (string id in provincesText.Split(' '))
                {
                    if (id == string.Empty)
                        continue;
                    if (int.TryParse(id, out int value))
                    {
                        provincesList.Add(value);
                    }
                    else
                    {
                        _logger.Warn($"{provincesText} 有数值未转换成功, input：{id}");
                    }
                }
                provincesList.TrimExcess();
                return provincesList;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="buildingRegex"></param>
            /// <returns></returns>
            /// <exception cref="FormatException"></exception>
            /// <exception cref="ArgumentException"></exception>
            public byte GetBuildingLevel(BuildingType buildingType)
            {
                var buildingRegex = GetBuildingRegexString(buildingType);
                var match = Regex.Match(_text, buildingRegex, RegexOptions.Compiled);
                if (!match.Success)
                {
                    return 0;
                }
                try
                {
                    return byte.Parse(match.Value);
                }
                catch (FormatException ex)
                {
                    _logger.Error(ex, "转换错误, 进行转换的值:{0}, 转换类型: {1}", match.Value, buildingType);
                    throw ex;
                }
            }

            private string GetBuildingRegexString(BuildingType buildingType)
            {
                switch (buildingType)
                {
                    case BuildingType.INFRASTRUCTURE: return _INFRASTRUCTURE;
                    case BuildingType.AIR_BASE: return _AIR_BASE;
                    case BuildingType.INDUSTRIAL_COMPLEX: return _INDUSTRIAL_COMPLEX;
                    case BuildingType.ARMS_FACTORY: return _ARMS_FACTORY;
                    case BuildingType.ANTI_AIR_BUILDING: return _ANTI_AIR_BUILDING;
                    case BuildingType.DOCKYARD: return _DOCKYARD;
                    case BuildingType.SYNTHETIC_REFINERY: return _SYNTHETIC_REFINERY;
                    case BuildingType.FUEL_SILO: return _FUEL_SILO;
                    case BuildingType.RADAR_STATION: return _RADAR_STATION;
                    case BuildingType.ROCKET_SITE: return _ROCKET_SITE;
                    case BuildingType.NUCLEAR_REACTOR: return _NUCLEAR_REACTOR;
                    default: throw new ArgumentException();
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="resourcesType"></param>
            /// <returns></returns>
            /// <exception cref="FormatException"></exception>
            /// <exception cref="ArgumentException"></exception>
            public ushort GetResourcesNumber(ResourcesType resourcesType)
            {
                var resourcesRegex = GetResourcesRegexString(resourcesType);
                var match = Regex.Match(_text, resourcesRegex, RegexOptions.Compiled);
                if (!match.Success)
                {
                    return 0;
                }
                try
                {
                    return ushort.Parse(match.Value);
                }
                catch (FormatException ex)
                {
                    _logger.Error(ex, "转换错误, 进行转换的值:{0}, 转换类型: {1}", match.Value, resourcesType);
                    throw ex;
                }
            }

            private string GetResourcesRegexString(ResourcesType resourcesType)
            {
                switch (resourcesType)
                {
                    case ResourcesType.OIL: return _OIL;
                    case ResourcesType.ALUMINIUM: return _ALUMINIUM;
                    case ResourcesType.STEEL: return _STEEL;
                    case ResourcesType.CHROMIUM: return _CHROMIUM;
                    case ResourcesType.RUBBER: return _RUBBER;
                    case ResourcesType.TUNGSTEN: return _TUNGSTEN;
                    default: throw new ArgumentException("找不到资源类型");
                }
            }

            public List<string> GetHasCoreCountryTags()
            {
                var tags = new List<string>();
                foreach (Match match in Regex.Matches(_text, _CORE_COUNTRY_TAG, RegexOptions.Compiled))
                {
                    tags.Add(match.Value);
                }
                tags.TrimExcess();
                return tags;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public string GetName()
            {
                var match = Regex.Match(_text, _NAME, RegexOptions.Compiled);
                //去除 “”
                return match.Value.Replace("\"", string.Empty);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public string GetOwner()
            {
                return Regex.Match(_text, _OWNER, RegexOptions.Compiled).Value;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            /// <exception cref="FormatException"></exception>
            public int GetId()
            {
                var id = Regex.Match(_text, _ID, RegexOptions.Compiled).Value;
                try
                {
                    return int.Parse(id);
                }
                catch (FormatException ex)
                {
                    _logger.Error(ex, "进行转换的值: {0}, text: {1}", id, _text);
                    throw ex;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            /// <exception cref="FormatException"></exception>
            public int GetManpower()
            {
                var manpower = Regex.Match(_text, _MANPOWER, RegexOptions.Compiled).Value;
                try
                {
                    return int.Parse(manpower);
                }
                catch (FormatException ex)
                {
                    _logger.Error(ex, "进行转换的值: {0}, text: {1}", manpower, _text);
                    throw ex;
                }
            }

            /// <summary>
            /// 获得地块类型
            /// </summary>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public StateCategory GetStateType()
            {
                try
                {
                    //有些state_category带 “”
                    string type = Regex.Match(_text, _TYPE, RegexOptions.Compiled).Value.Replace("\"", string.Empty);
                    return GetStateType(type);
                }
                catch (ArgumentException ex)
                {
                    _logger.Error(ex, "解析错误, text: {0}", _text);
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "未知错误");
                    throw ex;
                }
            }

            private static StateCategory GetStateType(string text)
            {
                switch (text ?? throw new ArgumentNullException(nameof(text)))
                {
                    case "wasteland": return StateCategory.WASTELAND;
                    case "enclave": return StateCategory.ENCLAVE;
                    case "tiny_island": return StateCategory.TINY_ISLAND;
                    case "small_island": return StateCategory.SMALL_ISLAND;
                    case "pastoral": return StateCategory.PASTORAL;
                    case "rural": return StateCategory.RURAL;
                    case "town": return StateCategory.TOWN;
                    case "large_town": return StateCategory.LARGE_TOWN;
                    case "city": return StateCategory.CITY;
                    case "large_city": return StateCategory.LARGE_CITY;
                    case "metropolis": return StateCategory.METROPOLIS;
                    case "megalopolis": return StateCategory.MEGALOPOLIS;
                    default: throw new ArgumentException($"未知类型, 文本：{text}");
                }
            }
        }
    }
}
