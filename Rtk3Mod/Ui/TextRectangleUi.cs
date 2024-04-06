using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Rtk3Mod
{
    public class TextRectangleUi : RectangleBaseUi
    {
        public TextRectangleUi(int width, int height) : base(width, height)
        {
        }

        public void RemoveAllText()
        {
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                FrameworkElement e = elements[i];
                if (e is TextBlock)
                {
                    Remove(e);
                }
            }
        }

        public void SetWhiteText(string s)
        {
            SetText(Tuple.Create(s, Colors.White, TextAlignment.Left));
        }

        public void SetWarnText(string s)
        {
            SetText(Tuple.Create(s, Gv.warn_color, TextAlignment.Left));
        }

        public void SetText(string s)
        {
            SetText(Tuple.Create(s, Colors.White, TextAlignment.Left));
        }

        public void SetText(Tuple<string, Color, TextAlignment> text)
        {
            List<Tuple<string, Color, TextAlignment>> list = new List<Tuple<string, Color, TextAlignment>>();
            list.Add(text);
            SetTextList(list);
        }

        public void SetTextList(List<Tuple<string, Color, TextAlignment>> texts)
        {
            RemoveAllText();

            for (int i = 0; i < texts.Count; i++)
            {
                Tuple<string, Color, TextAlignment> text_tuple = texts[i];
                TextBlock text_block = new TextBlock();
                string prev_string = "";

                if (texts[i].Item3 == TextAlignment.Left)
                {
                    for (int j = 0; j < i; j++)
                    {
                        for (int k = 0; k < texts[j].Item1.Length; k++)
                        {
                            char c = texts[j].Item1[k];
                            if (('0' <= c && c <= '9') || c == ' ')
                                prev_string += " ";
                            else
                                prev_string += "　";
                        }
                    }

                    text_block.Text = prev_string + text_tuple.Item1;
                }
                else
                {
                    for (int j = i; j < texts.Count; j++)
                    {
                        for (int k = 0; k < texts[j].Item1.Length; k++)
                        {
                            char c = texts[j].Item1[k];
                            if (('0' <= c && c <= '9') || c == ' ')
                                prev_string += " ";
                            else
                                prev_string += "　";
                        }
                    }

                    text_block.Text = text_tuple.Item1 + prev_string;
                }

                int padding = 10;
                text_block.Foreground = new SolidColorBrush(text_tuple.Item2);

                text_block.Height = GetHeight() - padding * 2;
                text_block.Width = GetWidth() - padding * 2;
                text_block.TextWrapping = TextWrapping.Wrap;
                text_block.FontWeight = FontWeights.Bold;
                text_block.FontFamily = new FontFamily("標楷體");
                text_block.FontSize = 25;
                text_block.TextAlignment = texts[i].Item3;

                Canvas.SetLeft(text_block, GetLeft() + padding);
                Canvas.SetTop(text_block, GetTop() + padding);
                Add(text_block, GetZIndex() + 1);

                Show();
            }
        }
    }
}
