using Rtk3Mod.New;
using Rtk3Mod.States;
using System.Windows.Media;

namespace Rtk3Mod
{
    public class Gv
    {
        public static GameData g = new GameData();
        public static Controller c = new Controller();

        // some static object's new depends on this, so it must be newed first >>>
        public static int screen_left = 15;
        public static int screen_top = 2;
        public static int screen_right = 480;
        public static int screen_bottom = 436;
        public static int status_top = 339;

        public static FontFamily menu_font = new FontFamily("標楷體");
        public static int menu_font_size = 25;
        public static int order_menu_bottom = status_top - 2;
        public static int order_menu_left = 28;

        public static int z_index_level_n1 = z_index_level_0 - 1000;
        public static int z_index_level_0 = 30000;
        public static int z_index_level_1 = z_index_level_0 + 1000;
        public static int z_index_level_2 = z_index_level_0 + 2000;
        public static int z_index_level_3 = z_index_level_0 + 3000;
        public static int z_index_level_4 = z_index_level_0 + 4000;
        public static int z_index_level_5 = z_index_level_0 + 5000;
        public static int z_index_level_6 = z_index_level_0 + 6000;
        public static int z_index_level_7 = z_index_level_0 + 7000;
        // <<< some static object's new depends on this, so it must be newed first

        public static TurnControllerState turnControllerState = new TurnControllerState();
        public static PlayerPlottingState playerPlottingState = new PlayerPlottingState();
        public static NpcPlottingState npcPlottingState = new NpcPlottingState();
        public static TopMenuState topMenuState = new TopMenuState();
        public static IntelligenceMenuState intelligenceMenuState = new IntelligenceMenuState();
        public static HomeCityMenuState homeCityMenuState = new HomeCityMenuState();
        public static HomeCityMenuState otherCityMenuState = new HomeCityMenuState();
        public static DevMenuState devMenuState = new DevMenuState();
        public static EndCityYesNoState endCityYesNoState = new EndCityYesNoState();
        public static TerritoryListInfoState territoryListInfoState = new TerritoryListInfoState();
        public static SelectCityToSpyPromptState selectCityToSpyPromptState = new SelectCityToSpyPromptState();
        public static SelectCityToSpyState selectCityToSpyState = new SelectCityToSpyState();
        public static SelectCityToWatchPromptState selectCityToWatchPromptState = new SelectCityToWatchPromptState();
        public static SelectCityToWatchState selectCityToWatchState = new SelectCityToWatchState();
        public static ShowCityInfoState showCityInfoState = new ShowCityInfoState();
        public static ShowOfficerListIntelligenceState officerListIntelligenceState = new ShowOfficerListIntelligenceState();
        public static SelectOfficerIntelligenceState selectIntelligenceOfficerState = new SelectOfficerIntelligenceState();
        public static ShowOfficerIntelligenceState showIntelligenceOfficerState = new ShowOfficerIntelligenceState();
        public static SelectOfficerToSpyState selectOfficerToSpyState = new SelectOfficerToSpyState();
        public static SetValueState setSpyMonthState = new SetValueState();
        public static SetValueState setDevGoldState = new SetValueState();
        public static SetValueState setTaxRateState = new SetValueState();
        public static SetValueState setCharityValueState = new SetValueState();
        public static SelectOfficerToDevState selectOfficerToDevState = new SelectOfficerToDevState();
        public static TradeMenuState tradeMenuState = new TradeMenuState();
        public static SelectOfficerToTradeState selectOfficerToTradeState = new SelectOfficerToTradeState();
        public static SetTradeQuantityState setTradeQuantityState = new SetTradeQuantityState();
        public static SpecialMenuState specialMenuState = new SpecialMenuState();
        public static SelectOfficerToDoCharityState selectOfficerToDoCharityState = new SelectOfficerToDoCharityState();
        public static CommonYesNoState commonYesNoState = new CommonYesNoState();
        public static IdleState idleState = new IdleState();

        public static TerritoryListInfoUi territoryListInfoUi;
        public static YesNoUi yesNoUi;
        public static StatusRectangleUi statusRectangleUi;
        public static CityInfoSmallUi cityInfoSmallUi;
        public static DateRectangle dateRectangle;
        public static BackgroundUi backgroundUi;
        public static CityMapUi cityMapUi;
        public static OfficerInfoUi officerInfoUi;
        public static SetValueUi setValueUi;

        public static SolidColorBrush unselected_background_brush = new SolidColorBrush(Color.FromRgb(0x40, 0x00, 0x80));
        public static SolidColorBrush unselected_foreground_brush = new SolidColorBrush(System.Windows.Media.Colors.White);
        public static SolidColorBrush selected_background_brush = new SolidColorBrush(Color.FromRgb(0xFF, 0xA0, 0x55));
        public static SolidColorBrush selected_foreground_brush = new SolidColorBrush(Color.FromRgb(0x40, 0x00, 0x80));
        public static SolidColorBrush white = unselected_foreground_brush;
        public static SolidColorBrush green = new SolidColorBrush(Color.FromRgb(0x4e, 0x98, 0x5e));
        public static SolidColorBrush yellow = new SolidColorBrush(Color.FromRgb(236, 204, 114));
        public static SolidColorBrush red = new SolidColorBrush(Color.FromRgb(222, 81, 66));
        public static SolidColorBrush blue = new SolidColorBrush(Color.FromRgb(140, 190, 222));

        public static SolidColorBrush spring = new SolidColorBrush(Color.FromRgb(50, 168, 129));
        public static SolidColorBrush summer = new SolidColorBrush(Color.FromRgb(104, 206, 116));
        public static SolidColorBrush autum = new SolidColorBrush(Color.FromRgb(50, 145, 168));
        public static SolidColorBrush winter = new SolidColorBrush(Color.FromRgb(50, 53, 94));

        public static Color warn_color = Color.FromRgb(0xdc, 0x70, 0xcc);

        public static void SetNextState(State state)
        {
            GameWindow.nextState = state;
        }

        public static void Init()
        {
            Util.Invoke(() =>
            {
                backgroundUi = new BackgroundUi();
                backgroundUi.Show();

                statusRectangleUi = new StatusRectangleUi();
                statusRectangleUi.Show();

                dateRectangle = new DateRectangle();
                dateRectangle.Show();

                cityMapUi = new CityMapUi();
                cityMapUi.Show();

                territoryListInfoUi = new TerritoryListInfoUi();
                yesNoUi = new YesNoUi();


                cityInfoSmallUi = new CityInfoSmallUi();

                officerInfoUi = new OfficerInfoUi();

                setValueUi = new SetValueUi();
            });
        }
    }
}
