using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Rtk3Mod
{
    public class SpecialMenuState : MenuState
    {
        public SpecialMenuState() : base(new List<string> { "徵收", "稅率", "施捨" })
        {
        }

        public override void SetMenuIndex(int index)
        {
            this.index = index;
            for (int i = 0; i < menu.Count; i++)
            {
                if (i == index)
                {
                    GetTextBlockElement("menu" + i).Background = Gv.selected_background_brush;
                    GetTextBlockElement("menu" + i).Foreground = Gv.selected_foreground_brush;
                }
                else
                {
                    GetTextBlockElement("menu" + i).Background = Gv.unselected_background_brush;
                    GetTextBlockElement("menu" + i).Foreground = Gv.unselected_foreground_brush;

                    if (i == 0)
                    {
                        if (city.has_taxed)
                            GetTextBlockElement("menu" + i).Background = Gv.red;
                    }
                    else if (i == 2)
                    {
                        if (city.has_charity)
                            GetTextBlockElement("menu" + i).Background = Gv.red;
                    }
                }
            }
        }

        private void DoTaxation()
        {
            if (!city.has_taxed)
            {
                Action forwardAction = () =>
                {
                    Gv.c.SetDoTaxation(city);

                    Util.Invoke(() =>
                    {
                        List<Tuple<string, Color, TextAlignment>> texts2 = new List<Tuple<string, Color, TextAlignment>>();
                        texts2.Add(Tuple.Create("已完成對", Colors.White, TextAlignment.Left));
                        texts2.Add(Tuple.Create(city.name, Color.FromRgb(255, 160, 85), TextAlignment.Left));
                        texts2.Add(Tuple.Create("的徵收", Colors.White, TextAlignment.Left));
                        Gv.statusRectangleUi.SetTextList(texts2);
                        Gv.SetNextState(Gv.specialMenuState);
                    });

                    Gv.idleState.Init(1000,
                        () =>
                        {
                            Gv.SetNextState(Gv.specialMenuState);
                        }
                        );

                    Gv.SetNextState(Gv.idleState);
                };

                Action backwardAction = () =>
                {
                    Gv.SetNextState(Gv.specialMenuState);
                };

                List<Tuple<string, Color, TextAlignment>> texts = new List<Tuple<string, Color, TextAlignment>>();
                texts.Add(Tuple.Create("確定要對", Colors.White, TextAlignment.Left));
                texts.Add(Tuple.Create(city.name, Color.FromRgb(255, 160, 85), TextAlignment.Left));
                texts.Add(Tuple.Create("徵收嗎？", Colors.White, TextAlignment.Left));
                Gv.commonYesNoState.Init(city, texts, forwardAction, backwardAction);
                Gv.SetNextState(Gv.commonYesNoState);
            }
        }

        private void AdjustTax()
        {
            Gv.setTaxRateState.Init(3, 100, 0,
                () =>
                {
                    Util.Invoke(() =>
                    {
                        int tax = Gv.setTaxRateState.setValueUi.GetValue();
                        Gv.c.SetTaxRate(city, tax);

                        List<Tuple<string, Color, TextAlignment>> texts = new List<Tuple<string, Color, TextAlignment>>();
                        texts.Add(Tuple.Create("已將", Colors.White, TextAlignment.Left));
                        texts.Add(Tuple.Create(city.name, Color.FromRgb(255, 160, 85), TextAlignment.Left));
                        texts.Add(Tuple.Create("的稅率設成", Colors.White, TextAlignment.Left));
                        texts.Add(Tuple.Create(tax.ToString(), Color.FromRgb(255, 160, 85), TextAlignment.Left));
                        Gv.statusRectangleUi.SetTextList(texts);

                        Gv.idleState.Init(1000, () =>
                        {
                            Util.Invoke(() =>
                            {
                                Gv.SetNextState(Gv.specialMenuState);
                            });
                        });
                        Gv.SetNextState(Gv.idleState);
                    });
                },
                () =>
                {
                    Gv.SetNextState(Gv.specialMenuState);
                }, "請設定稅率(0 - 100)");
            Gv.SetNextState(Gv.setTaxRateState);
        }

        public override void Update(GameKey key)
        {
            base.Update(key);

            Util.Invoke(new Action(() =>
            {
                if (key == GameKey.Forward)
                {
                    base.Update(key);

                    Util.Invoke(new Action(() =>
                    {
                        if (key == GameKey.Forward)
                        {
                            if (index == 0)
                            {
                                if (!city.has_taxed)
                                {
                                    DoTaxation();
                                }
                            }
                            else if (index == 1)
                            {
                                AdjustTax();
                            }
                            else if (index == 2)
                            {
                                if (!city.has_charity)
                                {
                                    Gv.selectOfficerToDoCharityState.Init(city);
                                    Gv.SetNextState(Gv.selectOfficerToDoCharityState);
                                }
                            }
                        }
                        else if (key == GameKey.Backward)
                        {
                            Gv.SetNextState(Gv.specialMenuState);
                        }
                    }));
                }
                else if (key == GameKey.Backward)
                {
                    Gv.SetNextState(Gv.topMenuState);
                }
            }));
        }


        public override void EnterState()
        {
            Util.Invoke(() =>
            {
                Gv.statusRectangleUi.SetPleaseOrder(city);
                Gv.cityInfoSmallUi.Show();
                Show();
            });
        }

        public override void ExitState()
        {
            Util.Invoke(() =>
            {
                Hide();
            });
        }
    }
}
