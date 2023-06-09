﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Fijicare.Utility.Controls
{

    public class ItemsStack : StackLayout
    {
        #region BindAble
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<ItemsStack, IEnumerable>(p => p.ItemsSource, default(IEnumerable<object>), BindingMode.TwoWay, null, ItemsSourceChanged);
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create<ItemsStack, object>(p => p.SelectedItem, default(object), BindingMode.TwoWay, null, OnSelectedItemChanged);
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create<ItemsStack, DataTemplate>(p => p.ItemTemplate, default(DataTemplate));

        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        private static void ItemsSourceChanged(BindableObject bindable, IEnumerable oldValue, IEnumerable newValue)
        {
            ItemsStack itemsLayout = (ItemsStack)bindable;
            itemsLayout.SetItems();
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ItemsStack itemsView = (ItemsStack)bindable;
            if (newValue == oldValue)
                return;

            itemsView.SetSelectedItem(newValue);
        }
        #endregion

        #region item rendering
        protected readonly ICommand ItemSelectedCommand;

        protected virtual void SetItems()
        {
            Children.Clear();

            if (ItemsSource == null)
            {
                ObservableSource = null;
                return;
            }

            foreach (object item in ItemsSource)
                Children.Add(GetItemView(item));

            bool isObs = ItemsSource.GetType().IsGenericType && ItemsSource.GetType().GetGenericTypeDefinition() == typeof(ObservableCollection<>);
            if (isObs)
            {
                ObservableSource = new ObservableCollection<object>(ItemsSource.Cast<object>());
            }
        }

        protected virtual View GetItemView(object item)
        {
            object content = ItemTemplate.CreateContent();

            View view = content as View;
            if (view == null)
                return null;

            view.BindingContext = item;

            TapGestureRecognizer gesture = new TapGestureRecognizer
            {
                Command = ItemSelectedCommand,
                CommandParameter = item
            };

            AddGesture(view, gesture);

            return view;
        }

        protected void AddGesture(View view, TapGestureRecognizer gesture)
        {
            view.GestureRecognizers.Add(gesture);

            Layout<View> layout = view as Layout<View>;

            if (layout == null)
                return;

            foreach (View child in layout.Children)
                AddGesture(child, gesture);
        }

        protected virtual void SetSelectedItem(object selectedItem)
        {
            EventHandler<SelectedItemChangedEventArgs> handler = SelectedItemChanged;
            if (handler != null)
                handler(this, new SelectedItemChangedEventArgs(selectedItem));
        }

        ObservableCollection<object> _observableSource;
        protected ObservableCollection<object> ObservableSource
        {
            get { return _observableSource; }
            set
            {
                if (_observableSource != null)
                {
                    _observableSource.CollectionChanged -= CollectionChanged;
                }
                _observableSource = value;

                if (_observableSource != null)
                {
                    _observableSource.CollectionChanged += CollectionChanged;
                }
            }
        }

        private async void   CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int index = e.NewStartingIndex;
                        foreach (object item in e.NewItems)
                            Children.Insert(index++, GetItemView(item));
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    {
                        object item = ObservableSource[e.OldStartingIndex];
                        Children.RemoveAt(e.OldStartingIndex);
                        Children.Insert(e.NewStartingIndex, GetItemView(item));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        Children.RemoveAt(e.OldStartingIndex);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        Children.RemoveAt(e.OldStartingIndex);
                        Children.Insert(e.NewStartingIndex, GetItemView(ObservableSource[e.NewStartingIndex]));
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Children.Clear();
                    foreach (object item in ItemsSource)
                        Children.Add(GetItemView(item));
                    break;
            }
        }
        #endregion

        public ItemsStack()
        {

            ItemSelectedCommand = new Command<object>(item =>
            {
                SelectedItem = item;
            });
        }
    }
}
