﻿using System.ComponentModel;

namespace PizzaPalace.Model
{
    public class Category : INotifyPropertyChanged
    {
        private int categoryID = 0;
        public int CategoryID
        {
            get { return this.categoryID; }
            set {
                this.categoryID = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoryID)));
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Exists)));
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
            }
        }

        private string name = "";
        public string Name
        {
            get { return this.name; }
            set {
                this.name = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid))); 
            }
        }
        public bool Exists { get { return this.CategoryID > 0; } }

        public bool IsValid
        {
            get
            {
                if (!(this.CategoryID >= 0))
                {
                    return false;
                }
                if (!(this.Name.Length > 0))
                {
                    return false;
                }
                return true;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Set default values to this object.
        /// </summary>
        public void SetDefaults()
        {
            this.CategoryID = 0;
            this.Name = "";
        }
        /// <summary>
        /// Updates this object to match parameter.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Category CopyFrom(Category category)
        {
            this.CategoryID = category.CategoryID;
            this.Name = category.Name;
            return this;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
