namespace Rtk3Mod
{
    public enum JobType
    {
        free,
        land,
        cultivate,
        economics,
        floodControl,
        buyFood,
        sellFood,
        buyBow,
        buyHorse,
        buyStrongBow,
        doCharity,
        spy
    }

    public class JobInfo
    {
        public City city;
        public int gold;

        public JobInfo(City city, int gold)
        {
            this.city = city;
            this.gold = gold;
        }
    }
}
