using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using YouCashApp.CustomRenderer;

using YouCashApp.Droid;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace YouCashApp.Droid
{
    public class CustomPickerRenderer : PickerRenderer
    {
        CustomPicker element;
        public CustomPickerRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            element = (CustomPicker)this.Element;
            if (Control != null && this.Element != null)

                Control.Background = AddPickerStyles();

        }
        public LayerDrawable AddPickerStyles()
        {
            ShapeDrawable border = new ShapeDrawable();
            border.Paint.Color = Android.Graphics.Color.Red;
            border.Paint.SetStyle(Paint.Style.Stroke);

            Drawable[] layers = { border};
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }

    }
}