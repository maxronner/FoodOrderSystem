﻿using PizzaPalace.ViewModel;
using System;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.UI.Core;
using PizzaPalace.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaPalace.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryView : Page
    {
        CategoryViewModel categoryViewModel = new CategoryViewModel();
        private bool destroyed;
        public CategoryView()
        {
            this.InitializeComponent();
            //Fetches from backend while page is active
            Task.Run(async () => {
                while (!destroyed)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        await categoryViewModel.FetchCategories();
                    });
                    Thread.Sleep(2000);
                }
            });
            this.Unloaded += CategoryView_Unloaded;
        }
        /// <summary>
        /// Disables server call when page is not active.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryView_Unloaded(object sender, RoutedEventArgs e)
        {
            destroyed = true;
        }
        /// <summary>
        /// Saves category if valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Focus(FocusState.Programmatic); // To unfocus form controls allowing notify to fire text changed
            await Task.Run(() => Thread.Sleep(20)); // So notify can do its thing before we rely on our bindings

            if (!categoryViewModel.FormCategory.IsValid)
            {
                return;
            }

            if (categoryViewModel.FormCategory.CategoryID == 0)
            {
                await categoryViewModel.AddCategory(new Category().CopyFrom(categoryViewModel.FormCategory));
                categoryViewModel.FormCategory.SetDefaults();
            }
            else
            {
                await categoryViewModel.UpdateCategory(new Category().CopyFrom(categoryViewModel.FormCategory));
                categoryViewModel.FormCategory.SetDefaults();
            }
        }
        /// <summary>
        /// Deletes category in backend, sets default values on FormCategory in frontend
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await categoryViewModel.DeleteCategory(new Category().CopyFrom(categoryViewModel.FormCategory));
            categoryViewModel.FormCategory.SetDefaults();
        }
        /// <summary>
        /// Used for selecting and storing a category object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                var category = (sender as ListView).SelectedItem as Category;
                categoryViewModel.FormCategory.CopyFrom(category);
            }
            else
            {
                categoryViewModel.FormCategory.SetDefaults();
            }
        }
    }
}
