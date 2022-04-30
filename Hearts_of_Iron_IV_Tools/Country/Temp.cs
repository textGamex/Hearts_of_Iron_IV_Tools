using MathNet.Numerics.Random;
using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Hearts_of_Iron_IV_Tools.Country
{
    public class Temp
    {
        private const string TEST = 
            "state= {\n\tid=2\n\tname=\"STATE_2\" # Latium\n\tmanpower = 5781971\n\t\n\tstate_category = megalopolis\n\n\thistory=\n\t{\n\t\towner = LAT\n\t\tvictory_points = { 9904 40 }\n\t\tvictory_points = { 11846 5 }\n\t\tbuildings = {\n\t\t\tinfrastructure = 4\n\t\t\tair_base = 8\n\t\t\tarms_factory = 1\n\t\t\tindustrial_complex = 4\n\t\t\t11751 = {\n\t\t\t\tnaval_base = 5\n\t\t\t}\n\t\t\t11846 = {\n\t\t\t\tnaval_base = 2\n\t\t\t}\n\t\t}\n\t\tadd_core_of = LAT  tadd_core_of = GER\n\t}\n\n\tprovinces= {\n\t\t923 6862 9794 9904 11751 11846    11882\n \t}\n\t\n\tlocal_supplies=0.0\n}";

        private readonly static MersenneTwister _random = new MersenneTwister(RandomSeed.Robust(), false);

        public static void Maina(string[] args)
        {
            var matchs = Regex.Matches(TEST, @"(?<=add_core_of\s*=\s*)\w+");
            foreach (Match match in matchs)
            {
                Console.WriteLine(match.Value);
            }    
            //string path = @"D:\STEAM\steamapps\common\Hearts of Iron IV\history\states";

            //var files = new DirectoryInfo(path).GetFiles();

            //var states = new Dictionary<string, State>();
            //var map = new Dictionary<string, CountryInfo>(256);

            //foreach (var file in files)
            //{
            //    var state = new State(file.FullName);
            //    states.Add(file.FullName, state);
            //}
            //List<int> all = new List<int>();
            //foreach (var state in states.Values)
            //{
            //    all.AddRange(state.GetAllProvince());
            //}
            //Console.WriteLine(all.Count);
            
            //Console.WriteLine(all.OrderByDescending((x) => x).ToArray()[0]);
        }

        private static void Re()
        {
            //string path = @"C:\Users\Programmer\Desktop\states";
            Console.WriteLine("请输入地块路径");
            string path = Console.ReadLine();

            Console.WriteLine("请输入全球总人口");
            long inputTotalManpower = long.Parse(Console.ReadLine());
            Console.WriteLine("请输入屏蔽的国家Tag，用,分隔开");
            var tagString = Console.ReadLine();
            string[] tagList = tagString == string.Empty ? Array.Empty<string>() : tagString.Split(',');

            //string path = @"D:\STEAM\steamapps\common\Hearts of Iron IV\history\states";
            long totalManpower = 0;

            var files = new DirectoryInfo(path).GetFiles();
            var manpowerList = GetRandomInt(inputTotalManpower, files.Length);
            manpowerList.Sort();
            int count = 0;
            uint passCount = 0;

            var states = new Dictionary<string, State>();
            var map = new Dictionary<string, CountryInfo>(256);

            foreach (var file in files)
            {
                var state = new State(file.FullName);
                states.Add(file.FullName, state);
            }
            foreach (var data in states.OrderBy(x => (int)x.Value.Type))
            {
                bool isPass = false;
                foreach (var tag in tagList)
                {
                    if (data.Value.Owner == tag.ToUpper())
                    {
                        ++count;
                        ++passCount;
                        Console.WriteLine($"{data.Key} 已跳过");
                        isPass = true;
                        break;
                    }
                }
                if (isPass)
                {
                    continue;
                }
                var newText = Regex.Replace(File.ReadAllText(data.Key),
                    @"(?<=manpower\s*=\s*)\d+", manpowerList[count++].ToString(), RegexOptions.Compiled);
                using (var stream = new FileStream(data.Key, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(newText);
                    Console.WriteLine($"{data.Key} 写入成功");
                }
            }
            foreach (var file in files)
            {
                var state = new State(file.FullName);
                if (map.ContainsKey(state.Owner))
                {
                    map[state.Owner].AddState(state);
                }
                else
                {
                    var info = new CountryInfo(state.Owner);
                    info.AddState(state);
                    map.Add(state.Owner, info);
                }
            }
            foreach (var data in map)
            {
                totalManpower += data.Value.GetTotalManpower();
            }

            Console.WriteLine($"一共 {totalManpower} 人口");
            Console.WriteLine($"一共写入{files.Length - passCount}个文件，跳过 {passCount} 个文件, 共{files.Length}个文件");
        }

        /// <summary>
        /// 获得指定数量的随机数，这些随机数的和为sum
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<long> GetRandomInt(long sum, int count)
        {
            if (count == 0)
            {
                throw new ArgumentException($"参数错误：{nameof(count)}");
            }
            if (count == 1)
            {
                return new List<long>(1) { sum };
            }

            var list = new List<long>(count);
            var randomNumbers = new List<long>(count - 1);
            for (int i = 0, max = count - 1; i < max; ++i)
            {
                randomNumbers.Add(_random.NextInt64() % (sum + 1));
            }
            randomNumbers.Sort();

            list.Add(randomNumbers[0]);
            for (int i = 1; i < randomNumbers.Count; ++i)
            {
                list.Add(randomNumbers[i] - randomNumbers[i - 1]);
            }
            list.Add(sum - randomNumbers[randomNumbers.Count - 1]);
            return list;
        }

        private static Dictionary<string, State> GetStateByFiles(string stateFolderPath)
        {
            var files = new DirectoryInfo(stateFolderPath).GetFiles();
            var states = new Dictionary<string, State>();
            foreach (var file in files)
            {
                var state = new State(file.FullName);
                states.Add(file.FullName, state);
            }
            return states;
        }
    }
}