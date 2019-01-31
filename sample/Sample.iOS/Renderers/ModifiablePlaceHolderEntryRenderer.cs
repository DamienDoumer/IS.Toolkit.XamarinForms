using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using IS.Toolkit.XamarinForms.Controls;
using Sample.IOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ModifiablePlaceHolderEntry), typeof(ModifiablePlaceHolderEntryRenderer))]
namespace Sample.IOS.Renderers
{
    public class ModifiablePlaceHolderEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Control != null)
                {
                    UIFont font = UIFont.ItalicSystemFontOfSize((float)Element.FontSize);
                }
            }
        }
    }
}