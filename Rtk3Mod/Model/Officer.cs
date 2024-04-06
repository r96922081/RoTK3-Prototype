namespace Rtk3Mod
{
    public enum OfficerSelectionTypeEnum
    {
        info,
        job,
        none
    }

    public enum OfficerSelectionAbilityEnum
    {
        position,
        political_ability,
        charm,
        none,
    }

    public enum RankEnum
    {
        Ruler,
        Govenor,
        General,
        Strategist,
        MilitaryOfficer,
        CivilOfficer
    }

    public enum PositionEnum
    {
        Ruler,
        Govenor,
        Strategist,
        Regular,
        Free
    }

    public class Officer
    {
        public string name;
        public RankEnum rank;
        public PositionEnum position;
        public int loyalty;
        public int seniority;
        public int age;
        public int troops;
        public int war_ability;
        public int intelligence;
        public int political_ability;
        public int charm;
        public int army;
        public int navy;
        public int training;
        public int morale;
        public City location;

        public JobType jobType;
        public int remaingJobMonth;
        public JobInfo jobInfo;

        private bool adding_location;

        public Officer(string name)
        {
            this.name = name;
            war_ability = 80;
            intelligence = 80;
            political_ability = 80;
            charm = 80;
            rank = RankEnum.MilitaryOfficer;
            location = null;
            loyalty = 80;
            seniority = 1;
            age = 30;
            troops = 80;
            training = 80;
            morale = 80;
            remaingJobMonth = 0;
            jobType = JobType.free;

            adding_location = false;
        }

        public void SetLocation(City location)
        {
            if (adding_location)
                return;

            adding_location = true;

            if (this.location != null)
            {
                this.location.RemoveOfficer(this);
            }

            this.location = location;

            if (this.location != null)
            {
                location.AddRegularOfficer(this);
            }

            adding_location = false;
        }

        public static RankEnum GetRankEnumFromString(string rank)
        {
            if (rank == "Ruler")
                return RankEnum.Ruler;
            else if (rank == "Govenor")
                return RankEnum.Govenor;
            else if (rank == "General")
                return RankEnum.General;
            else if (rank == "Advisor")
                return RankEnum.Strategist;
            else if (rank == "MilitaryOfficer")
                return RankEnum.MilitaryOfficer;
            else
                return RankEnum.CivilOfficer;
        }

        public static string GetStringFromRankEnum(RankEnum rank)
        {
            if (rank == RankEnum.Ruler)
                return "君主";
            else if (rank == RankEnum.Govenor)
                return "太守";
            else if (rank == RankEnum.General)
                return "將軍";
            else if (rank == RankEnum.Strategist)
                return "軍師";
            else if (rank == RankEnum.MilitaryOfficer)
                return "武官";
            else
                return "文官";
        }

        public static bool GetAbilityVisibility(string name, CityInfoLevelEnum info_level)
        {
            if (info_level == CityInfoLevelEnum.level0_nothing || info_level == CityInfoLevelEnum.level1_only_officers)
                return false;
            else if (info_level == CityInfoLevelEnum.level4_all)
                return true;
            else if (info_level == CityInfoLevelEnum.level2_officers_troops_and_city)
            {
                if (name == "Troops")
                    return true;
                else
                    return false;
            }
            else if (info_level == CityInfoLevelEnum.level3_officers_all_ability_except_loyalty)
            {
                if (name == "Loyalty")
                    return false;
                else
                    return true;
            }

            return false;
        }
    }
}
