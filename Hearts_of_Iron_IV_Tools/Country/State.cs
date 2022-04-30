using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace Hearts_of_Iron_IV_Tools.Country
{
    /// <summary>
    /// 地块
    /// </summary>
    [Serializable]
    public partial class State : IEquatable<State>
    {
        [NonSerialized]
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public int Id { get; }
        /// <summary>
        /// 本地化Key
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 人口
        /// </summary>
        public int Manpower { get; }
        /// <summary>
        /// 地块类型
        /// </summary>
        public StateCategory Type { get; }
        /// <summary>
        /// 控制此地块的国家Tag
        /// </summary>
        public string Owner { get; }
        /// <summary>
        /// 小地块
        /// </summary>
        private readonly List<int> _provincesId;
        private readonly List<string> _hasCoreCountryTags;
        private readonly Dictionary<BuildingType, byte> _buildingMap = new Dictionary<BuildingType, byte>();
        private readonly Dictionary<ResourcesType, ushort> _resourcesMap = new Dictionary<ResourcesType, ushort>();

        /// <summary>
        /// 构建省份
        /// </summary>
        /// <param name="path">省份文件路径</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static State ByPath(string path)
        {
            if (path == null)
            {
                var ex = new ArgumentNullException(nameof(path));
                _logger.Error(ex);
                throw ex;
            }
            return new State(File.ReadAllText(path));
        }

        /// <summary>
        /// 构建省份
        /// </summary>
        /// <param name="path">省份文件路径</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public State(string text)
        {
            if (text == null)
            {
                var ex = new ArgumentNullException(nameof(text));
                _logger.Error(ex);
                throw ex;
            }
            var parser = new StateFileParser(text);

            Name = parser.GetName();
            Owner = parser.GetOwner();
            Manpower = parser.GetManpower();
            Id = parser.GetId();

            Type = parser.GetStateType();
            _provincesId = parser.GetStateProvinces();
            _hasCoreCountryTags = parser.GetHasCoreCountryTags();

            foreach (BuildingType buildingType in Enum.GetValues(typeof(BuildingType)))
            {
                AddBuildingLevel(parser, buildingType);
            }
            foreach (ResourcesType resourcesType in Enum.GetValues(typeof(ResourcesType)))
            {
                AddResourcesNumber(parser, resourcesType);
            }
        }

        private void AddBuildingLevel(StateFileParser parser, BuildingType buildingType)
        {
            var buildingLevel = parser.GetBuildingLevel(buildingType);
            _buildingMap.Add(buildingType, buildingLevel);
        }

        private void AddResourcesNumber(StateFileParser parser, ResourcesType resourcesType)
        {
            var resourcesNumber = parser.GetResourcesNumber(resourcesType);
            _resourcesMap.Add(resourcesType, resourcesNumber);
        }

        public List<int> GetAllProvinceId()
        {
            return _provincesId.ToList();
        }

        public List<string> GetHasCoreTags()
        {
            return _hasCoreCountryTags.ToList();
        }
        
        public byte GetBuildingNumber(BuildingType buildingType)
        {
            return _buildingMap.TryGetValue(buildingType, out byte level) ? level : (byte)0;
        }

        public Dictionary<BuildingType, byte> GetBuildingLevelMap()
        {
            return _buildingMap.ToDictionary(x => x.Key, x => x.Value);
        }

        public ushort GetResourcesNumber(ResourcesType type)
        {
            return _resourcesMap.TryGetValue(type, out ushort number) ? number : (ushort)0;
        }

        /// <summary>
        /// 获得可用建筑槽数量
        /// </summary>
        /// <returns></returns>
        public byte GetAvailableSlot()
        {
            byte alreadyUsed = 0;
            alreadyUsed += GetBuildingNumber(BuildingType.INDUSTRIAL_COMPLEX);
            alreadyUsed += GetBuildingNumber(BuildingType.ARMS_FACTORY);
            alreadyUsed += GetBuildingNumber(BuildingType.NUCLEAR_REACTOR);
            alreadyUsed += GetBuildingNumber(BuildingType.DOCKYARD);
            alreadyUsed += GetBuildingNumber(BuildingType.ROCKET_SITE);
            return (byte) (GetStateCapacity(Type) - alreadyUsed);
        }

        private byte GetStateCapacity(StateCategory state)
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
        #region 杂
        public override int GetHashCode()
        {
            int result = Id.GetHashCode();
            result = 31 * result + Name?.GetHashCode() ?? 0;
            result = 31 * result + Manpower.GetHashCode();
            result = 31 * result + Type.GetHashCode();
            result = 31 * result + Owner?.GetHashCode() ?? 0;             
            return result;
        }

        private int HashCode<TKet, TValue>(Dictionary<TKet, TValue> map)
        {
            int hashCode = 0;
            foreach (var key in map.Keys)
            {
                hashCode += (key == null ? 0 : key.GetHashCode()) ^
                (map[key] == null ? 0 : map[key].GetHashCode());
            }
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as State);
        }

        public bool Equals(State other)
        {
            return other != null &&
                   Id == other.Id &&
                   Name == other.Name &&
                   Manpower == other.Manpower &&
                   Type == other.Type &&
                   Owner == other.Owner;
        }

        public static bool operator ==(State left, State right)
        {
            return EqualityComparer<State>.Default.Equals(left, right);
        }

        public static bool operator !=(State left, State right)
        {
            return !(left == right);
        }
        #endregion
    }
}
