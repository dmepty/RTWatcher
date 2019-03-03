using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Core.Client.Controls
{
    public class HighlightableTextBlock
    {
        // Highlight text color.
        public static Brush GetHighlightTextBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HighlightTextBrushProperty);
        }

        public static void SetHighlightTextBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(HighlightTextBrushProperty, value);
        }

        public static readonly DependencyProperty HighlightTextBrushProperty = DependencyProperty.RegisterAttached(
        "HighlightTextBrush",
        typeof(Brush),
        typeof(HighlightableTextBlock),
        new PropertyMetadata(SystemColors.HighlightTextBrush, Refresh));

        // Hightlight color.
        public static Brush GetHighlightBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HighlightBrushProperty);
        }

        public static void SetHighlightBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(HighlightBrushProperty, value);
        }

        public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.RegisterAttached(
        "HighlightBrush",
        typeof(Brush),
        typeof(HighlightableTextBlock),
        new PropertyMetadata(SystemColors.HighlightBrush, Refresh));

        // Hightlight text.
        public static string GetHightlightText(DependencyObject obj)
        {
            return (string)obj.GetValue(HightlightTextProperty);
        }

        public static void SetHightlightText(DependencyObject obj, string value)
        {
            obj.SetValue(HightlightTextProperty, value);
        }

        public static readonly DependencyProperty HightlightTextProperty = DependencyProperty.RegisterAttached(
        "HightlightText",
        typeof(string),
        typeof(HighlightableTextBlock),
        new PropertyMetadata(string.Empty, Refresh));

        private static bool GetIsBusy(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsBusyProperty);
        }

        private static void SetIsBusy(DependencyObject obj, bool value)
        {
            obj.SetValue(IsBusyProperty, value);
        }

        private static readonly DependencyProperty IsBusyProperty = DependencyProperty.RegisterAttached(
        "IsBusy",
        typeof(bool),
        typeof(HighlightableTextBlock),
        new PropertyMetadata(false));

        private static void Refresh(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            Highlight(dependencyObject as TextBlock);
        }

        private static void Highlight(TextBlock textblock)
        {
            if (textblock == null)
            {
                return;
            }

            string text = textblock.Text;

            if (!String.IsNullOrEmpty(text))
            {
                SetIsBusy(textblock, true);

                // Получаем подсвечиваемый текст для TextBlock'a
                var toHighlight = GetHightlightText(textblock);

                if (!String.IsNullOrEmpty(toHighlight))
                {
                    var matches = Regex.Split(
                    text,
                    $"({Regex.Escape(toHighlight)})",
                    RegexOptions.IgnoreCase);

                    textblock.Inlines.Clear();

                    var highlightBrush = GetHighlightBrush(textblock);
                    var highlightTextBrush = GetHighlightTextBrush(textblock);

                    foreach (var subString in matches)
                    {
                        if (String.Compare(subString, toHighlight, true) == 0)
                        {
                            var formattedText = new Run(subString)
                            {
                                Background = highlightBrush,
                                Foreground = highlightTextBrush,
                            };

                            textblock.Inlines.Add(formattedText);
                        }
                        else
                        {
                            textblock.Inlines.Add(subString);
                        }
                    }
                }
                else
                {
                    textblock.Inlines.Clear();
                    textblock.SetCurrentValue(TextBlock.TextProperty, text);
                }

                SetIsBusy(textblock, false);
            }
        }
    }
}
