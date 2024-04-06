using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Rtk3Mod
{
    public class SetTradeQuantityState : State
    {
        City city;
        SetValueUi setValueUi;
        JobType jobType;
        Officer officer;

        public void EnterState()
        {
            Util.Invoke(() =>
            {
                string status = "";
                int max = 1;
                switch (jobType)
                {
                    case JobType.buyFood:
                        max = city.gold * city.food_buying_price;
                        status = "要買多少米? (1 - " + max + ")";
                        break;
                    case JobType.sellFood:
                        max = city.food;
                        status = "要賣多少米? (1 - " + max + ")";
                        break;
                    case JobType.buyBow:
                        max = city.gold / city.crossbow_price;
                        status = "要買多少弓? (1 - " + max + ")";
                        break;
                    case JobType.buyHorse:
                        max = city.gold / city.horse_price;
                        status = "要買多少馬? (1 - " + max + ")";
                        break;
                    case JobType.buyStrongBow:
                        max = city.gold / city.strong_crossbow_price;
                        status = "要買多少強弓? (1 - " + max + ")";
                        break;
                }

                setValueUi = new SetValueUi();
                setValueUi.Init(max.ToString().Length, max, 1);
                setValueUi.SetDefaultPosition();
                setValueUi.Show();
                Gv.statusRectangleUi.SetText(status);
            });
        }

        public void ExitState()
        {
            Util.Invoke(() =>
            {
                setValueUi.RemoveAll();
            });
        }

        public void Init(City city, JobType jobType, Officer officer)
        {
            this.city = city;
            this.jobType = jobType;
            this.officer = officer;
        }

        public void Update(GameKey key)
        {
            setValueUi.Update(key);

            if (key == GameKey.Backward)
            {
                Gv.SetNextState(Gv.selectOfficerToTradeState);
            }
            else if (key == GameKey.Forward)
            {
                Util.Invoke(() =>
                {
                    officer.jobInfo = new JobInfo(city, 0);
                    officer.jobType = jobType;
                    officer.remaingJobMonth = 1;

                    int quantity = setValueUi.GetValue();
                    Gv.c.SetTrade(officer, city, jobType, quantity);

                    List<Tuple<string, Color, TextAlignment>> texts = new List<Tuple<string, Color, TextAlignment>>();

                    switch (jobType)
                    {
                        case JobType.buyFood:
                            texts.Add(Tuple.Create("已買", Colors.White, TextAlignment.Left));
                            texts.Add(Tuple.Create(quantity.ToString(), Color.FromRgb(255, 160, 85), TextAlignment.Left));
                            texts.Add(Tuple.Create("米", Colors.White, TextAlignment.Left));
                            break;
                        case JobType.sellFood:
                            texts.Add(Tuple.Create("已賣", Colors.White, TextAlignment.Left));
                            texts.Add(Tuple.Create(quantity.ToString(), Color.FromRgb(255, 160, 85), TextAlignment.Left));
                            texts.Add(Tuple.Create("米", Colors.White, TextAlignment.Left));
                            break;
                        case JobType.buyBow:
                            texts.Add(Tuple.Create("已買", Colors.White, TextAlignment.Left));
                            texts.Add(Tuple.Create(quantity.ToString(), Color.FromRgb(255, 160, 85), TextAlignment.Left));
                            texts.Add(Tuple.Create("弓", Colors.White, TextAlignment.Left));
                            break;
                        case JobType.buyHorse:
                            texts.Add(Tuple.Create("已買", Colors.White, TextAlignment.Left));
                            texts.Add(Tuple.Create(quantity.ToString(), Color.FromRgb(255, 160, 85), TextAlignment.Left));
                            texts.Add(Tuple.Create("馬", Colors.White, TextAlignment.Left));
                            break;
                        case JobType.buyStrongBow:
                            texts.Add(Tuple.Create("已買", Colors.White, TextAlignment.Left));
                            texts.Add(Tuple.Create(quantity.ToString(), Color.FromRgb(255, 160, 85), TextAlignment.Left));
                            texts.Add(Tuple.Create("強弓", Colors.White, TextAlignment.Left));
                            break;
                    }

                    Gv.statusRectangleUi.SetTextList(texts);
                    Gv.cityInfoSmallUi.Update(city);

                    Gv.idleState.Init(1000, () =>
                    {
                        Gv.SetNextState(Gv.tradeMenuState);
                    });
                    Gv.SetNextState(Gv.idleState);
                });
            }
        }
    }
}
