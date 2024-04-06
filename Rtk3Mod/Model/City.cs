using System.Collections.Generic;
using System.Windows.Media;

namespace Rtk3Mod
{
    public enum CityInfoLevelEnum
    {
        level0_nothing,
        level1_only_officers,
        level2_officers_troops_and_city,
        level3_officers_all_ability_except_loyalty,
        level4_all,
    }

    public class City
    {
        public bool ended = false;

        public City(string name, int index)
        {
            this.index = index;
            this.name = name;
            population = 0;
            troops = 0;
            gold = 0;
            food = 0;
            order = 0;
            tax = 40;
            food_selling_price = 50;
            food_buying_price = 60;
            crossbow_price = 40;
            strong_crossbow_price = 50;
            horse_price = 45;
            crossbow_count = 30;
            strong_crossbow_count = 20;
            horse_count = 40;
            ruler = "";
            governer = "";
            stragetist = "";
            has_taxed = false;
            has_charity = false;
            has_raised_troop = false;

            adding_officer = false;
        }

        public List<Officer> GetOfficers()
        {
            List<Officer> officers = new List<Officer>();
            foreach (Officer officer in Gv.g.officers)
            {
                if (officer.location != null && officer.location == this && officer.location.ruler == ruler)
                    officers.Add(officer);
            }

            return officers;
        }

        public void RemoveOfficer(Officer officer)
        {
            foreach (var g in regular_officers)
            {
                if (g == officer)
                {
                    regular_officers.Remove(g);
                    return;
                }
            }
        }

        public void AddRegularOfficer(Officer officer)
        {
            if (adding_officer)
                return;

            try
            {
                adding_officer = true;
                foreach (Officer g in regular_officers)
                {
                    if (g == officer)
                        return;
                }

                regular_officers.Add(officer);
                officer.SetLocation(this);
            }
            finally
            {
                adding_officer = false;
            }
        }

        public string name;
        public int index;
        public string ruler;
        public string governer;
        public string stragetist;
        public int population;
        public int gold;
        public int food;
        public int troops;
        public int land;
        public int cultivation;
        public int irrigation;
        public int flood_control;
        public int economic;
        public int order;
        public int tax;
        public int crossbow_count;
        public int strong_crossbow_count;
        public int horse_count;

        public int food_buying_price;
        public int food_selling_price;
        public int crossbow_price;
        public int strong_crossbow_price;
        public int horse_price;

        public bool has_taxed;
        public bool has_charity;
        public bool has_raised_troop;

        public int pos_x;
        public int pos_y;
        public List<Officer> regular_officers = new List<Officer>();
        public List<int> adjacency_cities = new List<int>();

        public static int max_gold = 100000;
        public static int min_gold = 0;
        public static int max_food = 10000000;
        public static int min_food = 0;

        private bool adding_officer;


        public int Troops
        {
            get
            {
                int soldier_count = 0;
                foreach (var g in regular_officers)
                {
                    if (g.troops != -1)
                        soldier_count += g.troops;
                }

                return soldier_count;
            }
        }

        public Brush GetBrush()
        {
            if (ruler == "")
                return new SolidColorBrush(Colors.White);
            else
                return new SolidColorBrush(Gv.c.GetForce(ruler).color);
        }

        public bool HasJob(JobType type)
        {
            foreach (var g in regular_officers)
                if (g.jobType == type)
                    return true;

            return false;
        }
    }
}
