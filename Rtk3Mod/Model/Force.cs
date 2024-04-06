using System.Collections.Generic;
using System.Windows.Media;

namespace Rtk3Mod
{
    public class Force
    {
        public string ruler;
        public bool ended = false;
        public Color color;

        public List<City> spiedCities = new List<City>();

        public Force(string ruler)
        {
            this.ruler = ruler;
        }

        // level0_nothing,
        // level1_only_officers,
        // level2_officers_troops_and_city,
        // level3_officers_all_ability_except_loyalty,
        // level4_all
        public static string GetOfficerInfo(Force f, City c, Officer o, string info_name)
        {
            return "";
        }

        public List<City> GetCities()
        {
            List<City> ret = new List<City>();

            // Fisrt city is the location of ruler
            foreach (var c in Gv.g.cities)
                if (c.Value.governer == ruler)
                    ret.Add(c.Value);

            foreach (var c in Gv.g.cities)
                if (c.Value.ruler == ruler && c.Value.governer != ruler)
                    ret.Add(c.Value);

            return ret;
        }
    }
}
