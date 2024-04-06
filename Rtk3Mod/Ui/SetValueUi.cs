using System.Windows.Controls;

namespace Rtk3Mod
{
    public class SetValueUi : RectangleBaseUi
    {
        int currentPos = 0;
        int maxDigit = 0;
        int updateCount = 0;
        int maxValue;
        int minValue;

        public SetValueUi() : base(130, 40)
        {
            RemoveCorner();
        }

        public void Init(int maxDigit, int maxValue, int minValue)
        {
            Util.Invoke(() =>
            {
                Util.LoadUiDesign(Util.GetUiJson("set_value_ui_design.json"), this);
                Remove("back_rect");
                SetZIndex(Gv.z_index_level_5);
                this.maxDigit = maxDigit;
                for (int i = maxDigit; i < 8; i++)
                {
                    Remove("digit" + i);
                }
                SetToValue(minValue);
            });

            Hide();
            currentPos = 0;
            updateCount = 0;
            this.maxValue = maxValue;
            this.minValue = minValue;
        }

        public void SetDefaultPosition()
        {
            SetTopLeftPosition(Gv.screen_right - GetWidth(), Gv.status_top - GetHeight());
        }

        public override void Update(GameKey key)
        {
            updateCount++;

            if (updateCount % 10 == 0)
            {
                Util.Invoke(() =>
                {
                    TextBlock currentTextBlock = GetTextBlockElement("digit" + currentPos);
                    if (currentTextBlock.Visibility == System.Windows.Visibility.Visible)
                    {
                        currentTextBlock.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        currentTextBlock.Visibility = System.Windows.Visibility.Visible;
                    }
                });

            }

            if (key == GameKey.None)
                return;

            Util.Invoke(() =>
            {
                TextBlock currentTextBlock = GetTextBlockElement("digit" + currentPos);

                if (key == GameKey.Up)
                {
                    string prevText = currentTextBlock.Text;
                    currentTextBlock.Text = "" + (int.Parse(currentTextBlock.Text) + 1) % 10;

                    if (GetValue() > maxValue || GetValue() < minValue)
                        currentTextBlock.Text = prevText;
                }
                else if (key == GameKey.Down)
                {
                    string prevText = currentTextBlock.Text;
                    if (currentTextBlock.Text == "0")
                        currentTextBlock.Text = "9";
                    else
                        currentTextBlock.Text = (int.Parse(currentTextBlock.Text) - 1).ToString();

                    if (GetValue() > maxValue || GetValue() < minValue)
                        currentTextBlock.Text = prevText;
                }
                else if (key == GameKey.Left)
                {
                    if (currentPos + 1 != maxDigit)
                    {
                        currentTextBlock.Visibility = System.Windows.Visibility.Visible;
                        currentPos++;
                    }
                    else
                    {
                        SetToValue(maxValue);
                    }
                }
                else if (key == GameKey.Right)
                {
                    if (currentPos != 0)
                    {
                        currentTextBlock.Visibility = System.Windows.Visibility.Visible;
                        currentPos--;
                    }
                    else
                    {
                        SetToValue(minValue);
                    }
                }
            });
        }

        public int GetValue()
        {
            int value = 0;
            int mul = 1;
            for (int i = 0; i < maxDigit; i++)
            {
                value += int.Parse(GetTextBlockElement("digit" + i).Text) * mul;
                mul *= 10;
            }

            return value;
        }

        private void SetToValue(int value)
        {
            for (int i = 0; i < maxDigit; i++)
            {
                GetTextBlockElement("digit" + i).Text = (value % 10).ToString();
                value /= 10;
            }
        }
    }
}
