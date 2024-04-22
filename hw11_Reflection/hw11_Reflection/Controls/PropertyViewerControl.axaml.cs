using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Reflection;
using ReactiveUI;

namespace hw11_Reflection.Controls
{
    public partial class PropertyViewerControl : UserControl
    {
        public static readonly StyledProperty<object> TargetObjectProperty =
            AvaloniaProperty.Register<PropertyViewerControl, object>(nameof(TargetObject));

        public object TargetObject
        {
            get => GetValue(TargetObjectProperty);
            set => SetValue(TargetObjectProperty, value);
        }

        public PropertyViewerControl()
        {
            InitializeComponent();
            //this.AttachedToLogicalTree += OnAttached;
            this.WhenAnyValue(x => x.TargetObject)
                .Subscribe(_ => BuildTree());
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void BuildTree()
        {
            if (TargetObject == null)
                return;

            var stackPanel = this.FindControl<StackPanel>("StackPanel");
            stackPanel.Children.Clear();

            PopulateProperties(TargetObject, stackPanel);
        }

        private void PopulateProperties(object obj, StackPanel parentPanel)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;

                if (IsBasicType(propertyType))
                {
                    var textBlock = new TextBlock();
                    textBlock.Text = $"{property.Name}: {property.GetValue(obj)}";
                    textBlock.Margin = new Thickness(5,5,5,5);
                    parentPanel.Children.Add(textBlock);
                }
                else
                {
                    var expander = new Expander();
                    expander.Header = property.Name;
                    var innerPanel = new StackPanel();

                    var propertyValue = property.GetValue(obj);
                    if (propertyValue != null) 
                    {
                        PopulateProperties(propertyValue, innerPanel);
                        expander.Content = innerPanel;
                        parentPanel.Children.Add(expander);
                    }
                }
            }
        }

        private bool IsBasicType(Type type)
        {
            return type.IsPrimitive || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime);
        }

        //public void OnAttached(object sender, LogicalTreeAttachmentEventArgs e)
        //{
            
        //}
    }
}
