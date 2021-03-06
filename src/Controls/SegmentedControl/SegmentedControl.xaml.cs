﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS.Toolkit.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SegmentedControl : ContentView
    {
        internal ICommand ItemCommand { get; set; }
        public SegmentedControl()
        {
            InitializeComponent();
            ItemCommand = new Command<string>(ItemClicked);
        }

        #region BackgroundColor
        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
            propertyName: nameof(BackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(SegmentedControl),
            defaultValue: Color.White);

        public new Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(BackgroundColorProperty);
            }
            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }
        #endregion

        #region CornerRadius
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            propertyName: nameof(CornerRadius),
            returnType: typeof(double),
            declaringType: typeof(SegmentedControl),
            defaultValue: default(double));

        public double CornerRadius
        {
            get
            {
                return (double)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }
        #endregion

        #region BorderWidth
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
            propertyName: nameof(BorderWidth),
            returnType: typeof(double),
            declaringType: typeof(SegmentedControl),
            defaultValue: 1.0);

        public double BorderWidth
        {
            get
            {
                return (double)GetValue(BorderWidthProperty);
            }
            set
            {
                SetValue(BorderWidthProperty, value);
            }
        }
        #endregion

        #region BorderColor
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            propertyName: nameof(BorderColor),
            returnType: typeof(Color),
            declaringType: typeof(SegmentedControl),
            defaultValue: Color.Accent);

        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }
            set
            {
                SetValue(BorderColorProperty, value);
            }
        }
        #endregion

        #region SelectedItemBackgroundColor
        public static readonly BindableProperty SelectedItemBackgroundColorProperty = BindableProperty.Create(
            propertyName: nameof(SelectedItemBackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(SegmentedControl),
            defaultValue: Color.Accent);

        public Color SelectedItemBackgroundColor
        {
            get
            {
                return (Color)GetValue(SelectedItemBackgroundColorProperty);
            }
            set
            {
                SetValue(SelectedItemBackgroundColorProperty, value);
            }
        }
        #endregion

        #region Items
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(IEnumerable<string>),
            declaringType: typeof(SegmentedControl),
            defaultValue: default(IEnumerable<string>),
            propertyChanged: ItemsPropertyChanged);

        private static void ItemsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && newValue != null)
            {
                var control = (SegmentedControl)bindable;
                control.BuildLayout();
            }
        }

        public IEnumerable<string> Items
        {
            get
            {
                return (IEnumerable<string>)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }
        #endregion

        #region TextColor
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(SegmentedControl),
            defaultValue: Color.Accent);

        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }
        #endregion

        #region SelectedItem TextColor
        public static readonly BindableProperty SelectedItemTextColorProperty = BindableProperty.Create(
            propertyName: nameof(SelectedItemTextColor),
            returnType: typeof(Color),
            declaringType: typeof(SegmentedControl),
            defaultValue: Color.White);

        public Color SelectedItemTextColor
        {
            get
            {
                return (Color)GetValue(SelectedItemTextColorProperty);
            }
            set
            {
                SetValue(SelectedItemTextColorProperty, value);
            }
        }
        #endregion

        #region SelectedItem
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            propertyName: nameof(SelectedItem),
            returnType: typeof(string),
            declaringType: typeof(SegmentedControl),
            defaultValue: default(string),
            propertyChanged: SelectedItemChanged);

        public string SelectedItem
        {
            get
            {
                return (string)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as SegmentedControl)?.InvalidateSelection();
        }
        #endregion

        private void BuildLayout()
        {
            ItemsLayout.ColumnDefinitions.Clear();
            ItemsLayout.Children.Clear();

            for (int index = 0; index < Items.Count(); index++)
            {
                var currentValue = Items.ElementAt(index);

                var item = new SegmentItem
                {
                    BindingContext = currentValue
                };

                item.Build(SelectedItemBackgroundColor, SelectedItemTextColor, CornerRadius, TextColor, SelectedItem, currentValue, ItemCommand);

                ItemsLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                ItemsLayout.Children.Add(
                    item,
                    left: index,
                    top: 0);
            }
        }

        public void ItemClicked(string item)
        {
            SelectedItem = item;
        }

        private void InvalidateSelection()
        {
            foreach (var segment in ItemsLayout.Children.OfType<SegmentItem>())
            {
                segment.InvalidateSelection(SelectedItem, TextColor, SelectedItemBackgroundColor, SelectedItemTextColor);
            }
        }
    }
}