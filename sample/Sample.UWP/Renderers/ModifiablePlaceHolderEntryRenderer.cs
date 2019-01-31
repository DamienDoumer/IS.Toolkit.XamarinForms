using IS.Toolkit.XamarinForms.Controls;
using Sample.UWP.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ModifiablePlaceHolderEntry), typeof(ModifiablePlaceHolderEntryRenderer))]
namespace Sample.UWP.Renderers
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
                    ////Control.Pla
                }
            }
        }
    }
}
