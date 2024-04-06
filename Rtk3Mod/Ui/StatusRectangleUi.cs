using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Rtk3Mod.New
{
    public class StatusRectangleUi : TextRectangleUi
    {
        public StatusRectangleUi() : base(Gv.screen_right - Gv.screen_left, 87)
        {
            SetZIndex(Gv.z_index_level_3);
            SetTopLeftPosition(Gv.screen_left, Gv.status_top);
        }

        public void SetPleaseOrder(City c)
        {
            List<Tuple<string, Color, TextAlignment>> texts = new List<Tuple<string, Color, TextAlignment>>();
            texts.Add(Tuple.Create("請執行", Colors.White, TextAlignment.Left));
            texts.Add(Tuple.Create(c.name, Color.FromRgb(255, 160, 85), TextAlignment.Left));
            texts.Add(Tuple.Create("的命令。", Colors.White, TextAlignment.Left));
            SetTextList(texts);
        }

        public void SetPlotting(string ruler)
        {
            List<Tuple<string, Color, TextAlignment>> texts = new List<Tuple<string, Color, TextAlignment>>();
            texts.Add(Tuple.Create(ruler, Color.FromRgb(255, 160, 85), TextAlignment.Left));
            texts.Add(Tuple.Create("戰略中", Colors.White, TextAlignment.Left));
            SetTextList(texts);
        }

        public void SetSureToEnd(City c)
        {
            List<Tuple<string, Color, TextAlignment>> texts = new List<Tuple<string, Color, TextAlignment>>();
            texts.Add(Tuple.Create("確定要結束", Colors.White, TextAlignment.Left));
            texts.Add(Tuple.Create(c.name, Color.FromRgb(255, 160, 85), TextAlignment.Left));
            texts.Add(Tuple.Create("的命令嗎？", Colors.White, TextAlignment.Left));
            SetTextList(texts);
        }

        public void StartSpy(Officer officer, City city, int month)
        {
            List<Tuple<string, Color, TextAlignment>> texts = new List<Tuple<string, Color, TextAlignment>>();
            texts.Add(Tuple.Create(officer.name, Color.FromRgb(255, 160, 85), TextAlignment.Left));
            texts.Add(Tuple.Create("已開始在", Colors.White, TextAlignment.Left));
            texts.Add(Tuple.Create(city.name, Color.FromRgb(255, 160, 85), TextAlignment.Left));
            texts.Add(Tuple.Create("共", Colors.White, TextAlignment.Left));
            texts.Add(Tuple.Create("" + month, Color.FromRgb(255, 160, 85), TextAlignment.Left));
            texts.Add(Tuple.Create("個月", Colors.White, TextAlignment.Left));
            texts.Add(Tuple.Create("的間諜", Colors.White, TextAlignment.Left));

            Gv.statusRectangleUi.SetTextList(texts);
        }

        public void StartDev(JobType jobType, List<Officer> officers, int month, int money)
        {
            List<Tuple<string, Color, TextAlignment>> texts = new List<Tuple<string, Color, TextAlignment>>();
            texts.Add(Tuple.Create(officers[0].name, Color.FromRgb(255, 160, 85), TextAlignment.Left));
            for (int i = 1; i < officers.Count; i++)
            {
                texts.Add(Tuple.Create(",", Colors.White, TextAlignment.Left));
                texts.Add(Tuple.Create(officers[i].name, Color.FromRgb(255, 160, 85), TextAlignment.Left));
            }
            texts.Add(Tuple.Create("開始", Colors.White, TextAlignment.Left));
            texts.Add(Tuple.Create("" + month, Color.FromRgb(255, 160, 85), TextAlignment.Left));
            texts.Add(Tuple.Create("個月", Colors.White, TextAlignment.Left));
            texts.Add(Tuple.Create("" + money, Color.FromRgb(255, 160, 85), TextAlignment.Left));
            texts.Add(Tuple.Create("元的", Colors.White, TextAlignment.Left));
            if (jobType == JobType.land)
                texts.Add(Tuple.Create("土地", Colors.White, TextAlignment.Left));
            else if (jobType == JobType.cultivate)
                texts.Add(Tuple.Create("農業", Colors.White, TextAlignment.Left));
            else if (jobType == JobType.floodControl)
                texts.Add(Tuple.Create("防災", Colors.White, TextAlignment.Left));
            else if (jobType == JobType.economics)
                texts.Add(Tuple.Create("經濟", Colors.White, TextAlignment.Left));
            texts.Add(Tuple.Create("開發", Colors.White, TextAlignment.Left));

            Gv.statusRectangleUi.SetTextList(texts);
        }

        public void SetPlainText(string s)
        {
            SetWhiteText(s);
        }
    }
}
