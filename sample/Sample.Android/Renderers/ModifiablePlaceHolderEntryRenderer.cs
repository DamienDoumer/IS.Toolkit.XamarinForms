using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IS.Toolkit.XamarinForms.Controls;
using Sample.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ModifiablePlaceHolderEntry), typeof(ModifiablePlaceHolderEntryRenderer))]
namespace Sample.Droid.Renderers
{
    public class ModifiablePlaceHolderEntryRenderer : EntryRenderer
    {
        public ModifiablePlaceHolderEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Control != null)
                {
                    Control.SetTypeface(null, Android.Graphics.TypefaceStyle.Italic);
                }
            }
        }
    }
}