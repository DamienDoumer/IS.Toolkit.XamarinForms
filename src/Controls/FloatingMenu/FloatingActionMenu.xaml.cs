﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingActionMenu : Grid
    {
        private int _animationSpeed = 500;
        private bool _canClose = true;
        private bool _firstClose = false;

        public FloatingActionMenu()
        {
            InitializeComponent();
        }

        #region Items
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(IEnumerable<FloatingActionMenuItem>),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: default(IEnumerable<FloatingActionMenuItem>));

        public IEnumerable<FloatingActionMenuItem> Items
        {
            get
            {
                return (IEnumerable<FloatingActionMenuItem>)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }
        #endregion

        #region Image
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            propertyName: nameof(Image),
            returnType: typeof(ImageSource),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: default(ImageSource));

        public ImageSource Image
        {
            get
            {
                return (ImageSource)GetValue(ImageProperty);
            }
            set
            {
                SetValue(ImageProperty, value);
            }
        }
        #endregion

        #region Color
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            propertyName: nameof(Color),
            returnType: typeof(Color),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: Color.Accent);

        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }
        #endregion

        public static readonly BindableProperty IsRotateAnimationEnabledProperty = BindableProperty.Create(nameof(IsRotateAnimationEnabled), typeof(bool), typeof(FloatingActionButton), default(bool));
        public bool IsRotateAnimationEnabled
        {
            get { return (bool)GetValue(IsRotateAnimationEnabledProperty); }
            set { SetValue(IsRotateAnimationEnabledProperty, value); }
        }

        #region FilterBackgroundColor
        public static readonly BindableProperty FilterBackgroundColorProperty = BindableProperty.Create(
            propertyName: nameof(FilterBackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: Color.FromHex("#B3ffffff"));

        public Color FilterBackgroundColor
        {
            get
            {
                return (Color)GetValue(FilterBackgroundColorProperty);
            }
            set
            {
                SetValue(FilterBackgroundColorProperty, value);
            }
        }
        #endregion

        #region Padding
        public static new readonly BindableProperty PaddingProperty = BindableProperty.Create(
            propertyName: nameof(Padding),
            returnType: typeof(Thickness),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: default(Thickness));

        public new Thickness Padding
        {
            get
            {
                return (Thickness)GetValue(PaddingProperty);
            }
            set
            {
                SetValue(PaddingProperty, value);
            }
        }
        #endregion

        #region IsOpen
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
            propertyName: nameof(IsOpen),
            returnType: typeof(bool),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: false,
            propertyChanged: IsOpenPropertyChanged);

        private static void IsOpenPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && newValue != null)
            {
                var control = (FloatingActionMenu)bindable;
                var isOpen = (bool)newValue;
                if (!control.IsOpen)
                {
                    control.CloseAnimationOnFab();
                }
                else
                {
                    control.OpenFABAnimation();
                }

                control.OpacityFilter.InputTransparent = !control.IsOpen;
            }
        }

        private async void CloseAnimationOnFab()
        {
            if (IsRotateAnimationEnabled)
            {
                FAB.RestoreRotationAnimation();
            }

            if (ItemsLayout != null)
            {
                List<Task> tasks = new List<Task>();
                if (ItemsLayout.ViewItems != null)
                {
                    tasks.Add(OpacityFilter.FadeTo(0, 500));
                    int nAnimationCount = 0;
                    for (int i = ItemsLayout.ViewItems.Count - 1; i >= 0; i--)
                    {
                        for (int j = ItemsLayout.ViewItems.Count - i; j > 0; j--)
                        {
                            nAnimationCount++;
                        }

                        tasks.Add(ItemsLayout.ViewItems[i].TranslateTo(0, (ItemsLayout.ViewItems[i].Height + 10) * (ItemsLayout.ViewItems.Count - i)));
                    }

                    _firstClose = true;
                    await Task.WhenAll(tasks.ToArray());
                    ItemsLayout.IsVisible = false;
                    OpacityFilter.IsVisible = false;
                }
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (!_firstClose)
            {
                CloseAnimationOnFab();
            }
        }

        private void OpenFABAnimation()
        {
            if (IsRotateAnimationEnabled)
            {
                FAB.RotateAnimation();
            }

            OpacityFilter.FadeTo(0.5, 500);
            OpacityFilter.IsVisible = true;
            ItemsLayout.IsVisible = true;
            foreach (var item in ItemsLayout.ViewItems)
            {
                item.TranslateTo(0, 0);
            }
        }

        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }
        #endregion

        #region Size
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
            propertyName: nameof(Size),
            returnType: typeof(double),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: 70.0);

        public double Size
        {
            get
            {
                return (double)GetValue(SizeProperty);
            }
            set
            {
                SetValue(SizeProperty, value);
            }
        }

        public double ItemSize { get; set; } = 50;
        public Thickness ItemsMargin { get; set; } = new Thickness(0, 0, 10, 0);
        #endregion

        #region ItemsPadding
        public static readonly BindableProperty ItemsPaddingProperty = BindableProperty.Create(
            propertyName: nameof(ItemsPadding),
            returnType: typeof(Thickness),
            declaringType: typeof(FloatingActionMenu),
            defaultValue: default(Thickness));

        public Thickness ItemsPadding
        {
            get
            {
                return (Thickness)GetValue(ItemsPaddingProperty);
            }
            set
            {
                SetValue(ItemsPaddingProperty, value);
            }
        }
        #endregion

        #region MainButtonToItemMargin

        public static readonly BindableProperty MainButtonToItemMarginProperty = BindableProperty.Create(nameof(MainButtonToItemMargin), typeof(Thickness), typeof(FloatingActionMenu), new Thickness(0, 0, 0, 0));

        public Thickness MainButtonToItemMargin
        {
            get { return (Thickness)GetValue(MainButtonToItemMarginProperty); }
            set { SetValue(MainButtonToItemMarginProperty, value); }
        }

        #endregion

        private void FloatingActionButton_Clicked(object sender, EventArgs e)
        {
            IsOpen = !IsOpen;
        }

        private void OpacityFilter_Tapped(object sender, EventArgs e)
        {
            IsOpen = false;
        }

        private void ItemClicked(object sender, EventArgs e)
        {
            IsOpen = false;
        }
    }
}