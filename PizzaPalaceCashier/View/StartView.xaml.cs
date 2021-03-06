﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaPalace.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartView : Page
    {
        public readonly string OrdersListLabel = "Orders";
        public readonly string ItemsListLabel = "Items";
        public readonly string CategoriesListLabel = "Categories";
        public Frame AppFrame => frame;
        public StartView()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// Handles selection in navigation view when GoBack() is requested.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                if (e.SourcePageType == typeof(OrderView))
                {
                    this.NavigationView.SelectedItem = OrdersMenuItem;
                    frame.Navigate(typeof(OrderView));
                }
                else if (e.SourcePageType == typeof(ItemView))
                {
                    this.NavigationView.SelectedItem = ItemsMenuItem;
                    frame.Navigate(typeof(ItemView));
                }
                else if (e.SourcePageType == typeof(CategoryView))
                {
                    this.NavigationView.SelectedItem = CategoriesMenuItem;
                    frame.Navigate(typeof(CategoryView));
                }
            }
        }
        /// <summary>
        /// Handles navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var label = args.InvokedItem as string;
            var pageType =
                label == OrdersListLabel ? typeof(OrderView) :
                label == ItemsListLabel ? typeof(ItemView) : 
                label == CategoriesListLabel ? typeof(CategoryView) : null;
            if (pageType != null && pageType != AppFrame.CurrentSourcePageType)
            {
                AppFrame.Navigate(pageType);
            }
        }
        /// <summary>
        /// Returns user to most recent page if possible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (AppFrame.CanGoBack)
            {
                AppFrame.GoBack();
            }
        }
    }
}
