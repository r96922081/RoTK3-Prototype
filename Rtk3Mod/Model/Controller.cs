using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace Rtk3Mod
{
    public class Controller
    {
        GameData g = Gv.g;

        public void LoadData()
        {
            new FileLoader().LoadData();
        }

        public void AddPlayer(string ruler)
        {
            g.players.Add(ruler);
        }

        public Force GetForce(string ruler)
        {
            foreach (var force in g.forces)
                if (force.ruler == ruler)
                    return force;

            return null;
        }

        public void SetSpy(Officer officer, City c, int month)
        {
            officer.jobType = JobType.spy;
            officer.remaingJobMonth = month;
            officer.jobInfo = new JobInfo(c, 0);
        }

        public void SetDev(List<Officer> officers, JobType jobType, City c, int month, int gold)
        {
            for (int i = 0; i < officers.Count; i++)
            {
                Officer officer = officers[i];
                officer.jobType = jobType;
                officer.remaingJobMonth = month;
                officer.jobInfo = new JobInfo(c, gold);
            }
        }

        public void SetTrade(Officer officer, City city, JobType jobType, int quantity)
        {
            officer.jobType = jobType;
            officer.remaingJobMonth = 1;
            officer.jobInfo = new JobInfo(city, 0);

            switch (jobType)
            {
                case JobType.buyFood:
                    city.gold -= quantity / city.food_buying_price;
                    city.food += quantity;
                    break;
                case JobType.sellFood:
                    city.gold += quantity / city.food_selling_price;
                    city.food -= quantity;
                    break;
                case JobType.buyBow:
                    city.gold -= quantity * city.crossbow_price;
                    city.crossbow_count += quantity;
                    break;
                case JobType.buyHorse:
                    city.gold -= quantity * city.horse_price;
                    city.horse_count += quantity;
                    break;
                case JobType.buyStrongBow:
                    city.gold -= quantity * city.strong_crossbow_price;
                    city.strong_crossbow_count += quantity;
                    break;
            }
        }

        public void SetDoTaxation(City city)
        {
            city.has_taxed = true;
            city.gold += 1000;
            city.food += 10000;
            city.order -= 5;
            if (city.order < 0)
            {
                city.order = 0;
            }
            Util.Invoke(() =>
            {
                Gv.cityInfoSmallUi.Update(city);
            });
        }

        public void SetDoCharity(Officer officer, City city, int food)
        {
            officer.jobType = JobType.doCharity;
            officer.remaingJobMonth = 1;
            officer.jobInfo = new JobInfo(city, 0);

            city.has_charity = true;
            city.food -= food;
            city.order += 1;
            if (city.order > 100)
            {
                city.order = 100;
            }
            Util.Invoke(() =>
            {
                Gv.cityInfoSmallUi.Update(city);
            });
        }

        public void SetTaxRate(City city, int taxRate)
        {
            city.tax = taxRate;
            Util.Invoke(() =>
            {
                Gv.cityInfoSmallUi.Update(city);
            });
        }

        public void ForceAdvanceMonth(Force f)
        {
            f.spiedCities.Clear();

            List<City> cities = f.GetCities();

            foreach (var c in cities)
            {
                c.has_taxed = false;
                c.has_charity = false;

                for (int i = 0; i < c.GetOfficers().Count; i++)
                {
                    Officer o = c.GetOfficers()[i];

                    switch (o.jobType)
                    {
                        case JobType.spy:
                            f.spiedCities.Add(o.jobInfo.city);
                            break;
                        case JobType.land:
                            o.jobInfo.city.land++;
                            if (o.jobInfo.city.land >= 100)
                                o.jobInfo.city.land = 100;
                            break;
                        case JobType.cultivate:
                            o.jobInfo.city.cultivation++;
                            if (o.jobInfo.city.cultivation >= 100)
                                o.jobInfo.city.cultivation = 100;
                            break;
                        case JobType.economics:
                            o.jobInfo.city.economic++;
                            if (o.jobInfo.city.economic >= 9999)
                                o.jobInfo.city.economic = 9999;
                            break;
                        case JobType.floodControl:
                            o.jobInfo.city.flood_control++;
                            if (o.jobInfo.city.flood_control >= 100)
                                o.jobInfo.city.flood_control = 100;
                            break;
                    }

                    if (o.jobType != JobType.free)
                    {
                        o.remaingJobMonth--;
                        if (o.remaingJobMonth == 0)
                        {
                            o.jobType = JobType.free;
                            o.jobInfo = null;
                        }
                    }
                }
            }
        }

        public void update_gold(City c)
        {
            if (Gv.g.month == 1 || Gv.g.month == 4 || Gv.g.month == 7 || Gv.g.month == 10)
            {
                int gold_income = (int)((c.population * 0.01 * c.economic * 0.1 * c.tax) * 0.1);
                gold_income -= (int)((c.regular_officers.Count * 100 + c.troops * 0.1) * 0.1);
                c.gold += gold_income;

                if (c.gold > City.max_gold)
                    c.gold = City.max_gold;
                if (c.gold < City.min_gold)
                    c.gold = City.min_gold;
            }
        }

        public void update_food(City c)
        {
            if (Gv.g.month == 1 || Gv.g.month == 7)
            {
                int food_income = (int)((c.population * 0.01 * c.land * 0.1 * (3 + c.cultivation * 0.01)) + (c.population * 0.01 * c.flood_control * 0.05));
                food_income -= (int)(c.regular_officers.Count * 1000 + c.troops * 1);
                c.food += food_income;

                if (c.food > City.max_food)
                    c.food = City.max_food;
                if (c.food < City.min_food)
                    c.food = City.min_food;
            }
        }
    }

    public class FileLoader
    {
        GameData g = Gv.g;

        public void LoadData()
        {
            ReadCities(@"..\..\data\cities.txt");
            ReadGenerals(@"..\..\data\generals_ability.txt");
            ReadGeneralsLocation(@"..\..\data\generals_location.txt");
            UpdateAbilityForDifferentRanks();
            ReadForces(@"..\..\data\force.txt");

            g.remainingForce.Add(new Force("dummy")); // trigger month advance
        }


        private void ReadGenerals(string path)
        {
            string[] lines = File.ReadAllLines(path);

            for (int i = 2; i < lines.Length; i++)
            {
                string[] s = lines[i].Split(',');

                string name = s[0].Trim();
                Officer o = new Officer(name);
                o.army = Int32.Parse(s[1].Trim());
                o.navy = Int32.Parse(s[2].Trim());
                o.war_ability = Int32.Parse(s[3].Trim());
                o.intelligence = Int32.Parse(s[4].Trim());
                o.political_ability = Int32.Parse(s[5].Trim());
                o.charm = Int32.Parse(s[6].Trim());

                if (o.intelligence > o.war_ability || o.political_ability > o.war_ability)
                {
                    if (o.intelligence >= 80 || o.political_ability >= 80)
                    {
                        o.rank = RankEnum.Strategist;
                    }
                    else
                    {
                        o.rank = RankEnum.CivilOfficer;
                    }
                }
                else
                {
                    if (o.war_ability >= 80)
                    {
                        o.rank = RankEnum.General;
                    }
                    else
                    {
                        o.rank = RankEnum.MilitaryOfficer;
                    }
                }

                g.officers.Add(o);
                g.name_to_general_map.Add(o.name, o);
                g.officers.Sort((g1, g2) => g1.name.CompareTo(g2.name));
            }
        }

        private void ReadCities(string path)
        {
            string[] lines = File.ReadAllLines(path);

            for (int j = 2; j < lines.Length; j++)
            {
                string[] s = lines[j].Split(',');

                int i = 0;
                string name = s[i++].Trim();
                int index = Int32.Parse(s[i++].Trim());
                City c = new City(name, index);
                c.ruler = s[i++].Trim();
                c.governer = s[i++].Trim();
                c.stragetist = s[i++].Trim();
                c.population = Int32.Parse(s[i++].Trim());
                c.order = Int32.Parse(s[i++].Trim());
                c.gold = Int32.Parse(s[i++].Trim());
                c.food = Int32.Parse(s[i++].Trim());
                c.tax = Int32.Parse(s[i++].Trim());
                c.land = Int32.Parse(s[i++].Trim());
                c.cultivation = Int32.Parse(s[i++].Trim());
                c.irrigation = Int32.Parse(s[i++].Trim());
                c.flood_control = Int32.Parse(s[i++].Trim());
                c.economic = Int32.Parse(s[i++].Trim());
                c.pos_x = Int32.Parse(s[i++].Trim());
                c.pos_y = Int32.Parse(s[i++].Trim());

                string adjacency_cities_string = s[i++].Trim();
                string[] adjacency_cities_array = adjacency_cities_string.Split(' ');
                foreach (string s2 in adjacency_cities_array)
                {
                    string s3 = s2.Trim();
                    if (s3 == "")
                        continue;

                    c.adjacency_cities.Add(Int32.Parse(s3));
                }

                g.cities.Add(c.index, c);
                g.name_to_city_map.Add(c.name, c);

            }
        }

        private void ReadGeneralsLocation(string path)
        {
            string[] lines = File.ReadAllLines(path);

            for (int i = 2; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split(',');

                string name = tokens[0].Trim();
                string location = tokens[1].Trim();
                Officer o = g.name_to_general_map[name];
                City c = g.name_to_city_map[location];
                o.SetLocation(c);

                string position = tokens[2].Trim();
                if (position == "ruler")
                {
                    o.position = PositionEnum.Ruler;
                    o.rank = RankEnum.Ruler;
                }
                else if (position == "governer")
                {
                    o.position = PositionEnum.Govenor;
                    o.rank = RankEnum.Govenor;
                }
                else if (position == "strategist")
                {
                    o.position = PositionEnum.Strategist;
                }
                else if (position == "regular")
                    o.position = PositionEnum.Regular;
                else
                    o.position = PositionEnum.Free;

                if (o.rank == RankEnum.Ruler)
                    o.troops = 10000;
                else if (o.rank == RankEnum.Govenor)
                    o.troops = 8000;
                else if (o.rank == RankEnum.General)
                    o.troops = 5000;
                else if (o.rank != RankEnum.CivilOfficer)
                    o.troops = 3000;
            }
        }

        private void UpdateAbilityForDifferentRanks()
        {
            foreach (var o in g.officers)
            {
                switch (o.rank)
                {
                    case RankEnum.Ruler:
                        o.loyalty = -1;
                        o.seniority = -1;
                        o.troops = 10000;
                        break;
                    case RankEnum.Govenor:
                        o.troops = 5000;
                        break;
                    case RankEnum.General:
                        o.troops = 3000;
                        break;
                    case RankEnum.Strategist:
                        o.troops = 2000;
                        break;
                    case RankEnum.MilitaryOfficer:
                        o.troops = 1000;
                        break;
                    case RankEnum.CivilOfficer:
                        o.troops = -1;
                        o.training = -1;
                        o.morale = -1;
                        break;
                }
            }
        }

        private void ReadForces(string path)
        {
            string[] lines = File.ReadAllLines(path);

            for (int i = 2; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split(',');

                string name = tokens[0].Trim();
                Byte r = Byte.Parse(tokens[1].Trim());
                Byte g = Byte.Parse(tokens[2].Trim());
                Byte b = Byte.Parse(tokens[3].Trim());

                Force force = new Force(name);
                force.color = Color.FromRgb(r, g, b);
                this.g.forces.Add(force);
            }
        }
    }
}
