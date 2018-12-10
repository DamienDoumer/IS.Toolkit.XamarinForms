﻿using IS.Toolkit.XamarinForms.Controls;
using Sample.UWP.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ISCheckbox), typeof(CheckboxRenderer))]
namespace Sample.UWP.Renderers
{
    public class CheckboxRenderer : ViewRenderer<ISCheckbox, CheckBox>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ISCheckbox> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.IsChecked = Element.IsChecked;
                    checkBox.Checked += CheckBox_Checked;
                    checkBox.Content = Element.Text;
                    SetNativeControl(checkBox);
                    UpdateTextColor(checkBox);
                    UpdateAccentColor(checkBox);
                }
            }
        }

        private void CheckBox_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Element.IsChecked = (bool)Control.IsChecked;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(ISCheckbox.IsChecked)))
            {
                Control.IsChecked = Element.IsChecked;
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.AccentColor)))
            {
                UpdateAccentColor(Control as CheckBox);
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.Text)))
            {
                Control.Content = Element.Text;
            }
            else if (e.PropertyName.Equals(nameof(ISCheckbox.TextColor)))
            {
                UpdateTextColor(Control as CheckBox);
            }
        }

        private void UpdateAccentColor(CheckBox checkBox)
        {
            checkBox.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(
                                        (byte)Element.AccentColor.A,
                                        (byte)Element.AccentColor.R,
                                        (byte)Element.AccentColor.G,
                                        (byte)Element.AccentColor.B));
        }

        private void UpdateTextColor(CheckBox checkBox)
        {
        }
    }
}