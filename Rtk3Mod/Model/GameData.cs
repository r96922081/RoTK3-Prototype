using System.Collections.Generic;

namespace Rtk3Mod
{
    public class GameData
    {
        public Dictionary<int, City> cities = new Dictionary<int, City>();
        public List<string> players = new List<string>();
        public Dictionary<string, City> name_to_city_map = new Dictionary<string, City>();
        public List<Officer> officers = new List<Officer>();
        public Dictionary<string, Officer> name_to_general_map = new Dictionary<string, Officer>();
        public List<Force> forces = new List<Force>();

        public int year = 188;
        public int month = 12;
        public List<Force> remainingForce = new List<Force>();
        public List<City> remainingCity = new List<City>();
    }
}
