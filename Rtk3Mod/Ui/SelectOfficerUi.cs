using System.Collections.Generic;
using System.Windows.Controls;

namespace Rtk3Mod
{
    public class SelectOfficerUi : RectangleBaseUi
    {
        public City city;
        public TextBlock[] indexes;
        public TextBlock[] names;
        public TextBlock[] col2;
        public int current_page = 0;
        public int current_row = 0;
        public int maxCount = 0;
        public const int max_row_count_in_one_page = 8;
        public OfficerSelectionTypeEnum selectionType = OfficerSelectionTypeEnum.none;
        public OfficerSelectionAbilityEnum ability_type = OfficerSelectionAbilityEnum.none;
        public List<Officer> selectedOfficers = new List<Officer>();

        public SelectOfficerUi() : base(260, 315)
        {
            Util.LoadUiDesign(Util.GetUiJson("officer_selection_rectangle_dialog_ui_design.json"), this);

            indexes = new TextBlock[]{ GetTextBlockElement("index_0"), GetTextBlockElement("index_1"), GetTextBlockElement("index_2")
            , GetTextBlockElement("index_3"), GetTextBlockElement("index_4"), GetTextBlockElement("index_5"), GetTextBlockElement("index_6"), GetTextBlockElement("index_7")};

            names = new TextBlock[]{ GetTextBlockElement("name_0"), GetTextBlockElement("name_1"), GetTextBlockElement("name_2")
            , GetTextBlockElement("name_3"), GetTextBlockElement("name_4"), GetTextBlockElement("name_5"), GetTextBlockElement("name_6"), GetTextBlockElement("name_7")};

            col2 = new TextBlock[9];

            for (int i = 0; i < 9; i++)
                col2[i] = GetTextBlockElement("r" + i + "c2");

            Hide();
        }

        public void Init(City city, OfficerSelectionTypeEnum selectionType, OfficerSelectionAbilityEnum ability_type, int maxCount)
        {
            this.city = city;
            this.selectionType = selectionType;
            this.ability_type = ability_type;

            SetZIndex(Gv.z_index_level_5);
            Canvas.SetZIndex(GetTextBlockElement("highlight_bar"), Gv.z_index_level_4);
            Canvas.SetZIndex(GetElement("base_rect_back"), Gv.z_index_level_3);

            SetTopLeftPosition(Gv.screen_right - 260, Gv.screen_top + 15);
            current_page = 0;
            current_row = 0;
            this.maxCount = maxCount;
            selectedOfficers.Clear();
            UpdateCurrentPage();
        }

        public int GetTotalPageCount()
        {
            return (city.regular_officers.Count + max_row_count_in_one_page - 1) / max_row_count_in_one_page;
        }

        public void UpdateHighlightBar()
        {
            TextBlock bar = GetTextBlockElement("highlight_bar");
            Canvas.SetTop(bar, Canvas.GetTop(indexes[current_row]));
        }

        public int GetEndIndex()
        {
            int start_index = current_page * max_row_count_in_one_page;
            int end_index = start_index + max_row_count_in_one_page - 1;

            if (end_index >= city.regular_officers.Count)
                end_index = city.regular_officers.Count - 1;

            return end_index;
        }

        public void UpdateCurrentPage()
        {
            for (int i = 0; i < max_row_count_in_one_page; i++)
            {
                indexes[i].Text = "";
                names[i].Text = "";
                col2[i].Text = "";
            }

            int start_index = current_page * max_row_count_in_one_page;
            int end_index = GetEndIndex();

            if (ability_type == OfficerSelectionAbilityEnum.position)
            {
                GetTextBlockElement("ability_type").Text = "身份";
            }
            else if (ability_type == OfficerSelectionAbilityEnum.political_ability)
            {
                GetTextBlockElement("ability_type").Text = "政治";
            }
            else if (ability_type == OfficerSelectionAbilityEnum.charm)
            {
                GetTextBlockElement("ability_type").Text = "魅力";
            }

            for (int i = start_index, j = 0; i <= end_index; i++, j++)
            {
                indexes[j].Foreground = Gv.white;
                indexes[j].Text = (i + 1).ToString();

                Officer o = city.regular_officers[i];
                names[j].Text = o.name;

                if (selectionType == OfficerSelectionTypeEnum.job)
                {
                    if (o.jobType != JobType.free)
                    {
                        names[j].Foreground = Gv.red;
                        col2[j].Foreground = Gv.red;
                    }
                    else if (selectedOfficers.Contains(o))
                    {
                        names[j].Foreground = Gv.yellow;
                        col2[j].Foreground = Gv.yellow;
                    }
                    else if (city.regular_officers[i].rank == RankEnum.CivilOfficer || city.regular_officers[i].rank == RankEnum.MilitaryOfficer)
                    {
                        names[j].Foreground = Gv.white;
                        col2[j].Foreground = Gv.white;
                    }
                    else
                    {
                        names[j].Foreground = Gv.green;
                        col2[j].Foreground = Gv.green;
                    }
                }
                else
                {
                    if (city.regular_officers[i].rank == RankEnum.CivilOfficer || city.regular_officers[i].rank == RankEnum.MilitaryOfficer)
                    {
                        names[j].Foreground = Gv.white;
                        col2[j].Foreground = Gv.white;
                    }
                    else
                    {
                        names[j].Foreground = Gv.green;
                        col2[j].Foreground = Gv.green;
                    }
                }

                if (ability_type == OfficerSelectionAbilityEnum.position)
                {
                    col2[j].Text = Officer.GetStringFromRankEnum(city.regular_officers[i].rank);
                }
                else if (ability_type == OfficerSelectionAbilityEnum.political_ability)
                {
                    col2[j].Text = city.regular_officers[i].political_ability.ToString();
                }
                else if (ability_type == OfficerSelectionAbilityEnum.charm)
                {
                    col2[j].Text = city.regular_officers[i].charm.ToString();
                }

                if (j == current_row)
                {
                    indexes[j].Foreground = Gv.selected_foreground_brush;
                    names[j].Foreground = Gv.selected_foreground_brush;
                    col2[j].Foreground = Gv.selected_foreground_brush;
                }
            }

            UpdateHighlightBar();

        }

        public override void Update(GameKey key)
        {
            Util.Invoke(() =>
            {
                if (key == GameKey.Up)
                {
                    current_row--;
                    if (current_row == -1)
                        current_row = GetEndIndex() % max_row_count_in_one_page;

                    UpdateCurrentPage();
                }
                else if (key == GameKey.Down)
                {
                    current_row++;
                    if (current_row == (GetEndIndex() % max_row_count_in_one_page) + 1)
                        current_row = 0;

                    UpdateCurrentPage();
                }
                else if (key == GameKey.Left)
                {
                    current_page--;
                    if (current_page == -1)
                        current_page = GetTotalPageCount() - 1;

                    current_row = 0;
                    UpdateCurrentPage();
                }
                else if (key == GameKey.Right)
                {
                    current_page++;
                    if (current_page == GetTotalPageCount())
                        current_page = 0;

                    current_row = 0;
                    UpdateCurrentPage();
                }
                else if (key == GameKey.Forward)
                {
                    Officer o = city.regular_officers[current_page * max_row_count_in_one_page + current_row];
                    if (selectionType == OfficerSelectionTypeEnum.job)
                    {
                        if (o.jobType == JobType.free)
                        {
                            if (selectedOfficers.Contains(o))
                                selectedOfficers.Remove(o);
                            else
                            {
                                if (selectedOfficers.Count < maxCount)
                                    selectedOfficers.Add(o);
                            }

                        }
                    }
                    else
                    {
                        if (selectedOfficers.Contains(o))
                            selectedOfficers.Remove(o);
                        else
                        {
                            if (selectedOfficers.Count < maxCount)
                                selectedOfficers.Add(o);
                        }
                    }
                }
            });
        }
    }
}
