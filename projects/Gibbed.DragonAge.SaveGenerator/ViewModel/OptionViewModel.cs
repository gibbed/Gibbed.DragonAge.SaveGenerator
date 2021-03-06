﻿/* Copyright (c) 2017 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Gibbed.DragonAge.SaveGenerator.ViewModel
{
    /// <summary>
    /// Represents a value with a user-friendly name that can be selected by the user.
    /// </summary>
    /// <typeparam name="TValue">The type of value represented by the option.</typeparam>
    public class OptionViewModel<TValue>
        : INotifyPropertyChanged, IComparable<OptionViewModel<TValue>>
    {
        private const int _UnsetSortValue = Int32.MinValue;

        private readonly string _DisplayName;
        private readonly ImageSource _DisplayImage;
        private readonly int _SortValue;
        private readonly TValue _Value;
        private readonly string _GroupName;
        private bool _IsSelected;

        public OptionViewModel(string displayName, ImageSource displayImage, TValue value)
            : this(displayName, displayImage, value, _UnsetSortValue, null)
        {
        }

        public OptionViewModel(string displayName, ImageSource displayImage, TValue value, int sortValue)
            : this(displayName, displayImage, value, sortValue, null)
        {
        }

        public OptionViewModel(string displayName, ImageSource displayImage, TValue value, string groupName)
            : this(displayName, displayImage, value, _UnsetSortValue, groupName)
        {
        }

        public OptionViewModel(string displayName, ImageSource displayImage, TValue value, int sortValue, string groupName)
        {
            this._DisplayName = displayName;
            this._DisplayImage = displayImage;
            this._Value = value;
            this._SortValue = sortValue;
            this._GroupName = groupName;
        }

        public string DisplayName
        {
            get { return this._DisplayName; }
        }

        public ImageSource DisplayImage
        {
            get { return this._DisplayImage; }
        }

        public bool IsSelected
        {
            get { return this._IsSelected; }
            set
            {
                if (value == this._IsSelected)
                {
                    return;
                }

                this._IsSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }

        public int SortValue
        {
            get { return this._SortValue; }
        }

        internal TValue GetValue()
        {
            return this._Value;
        }

        public string GroupName
        {
            get { return this._GroupName; }
        }

        public int CompareTo(OptionViewModel<TValue> other)
        {
            if (other == null)
            {
                return -1;
            }

            if (this.SortValue == _UnsetSortValue && other.SortValue == _UnsetSortValue)
            {
                return string.Compare(this.DisplayName, other.DisplayName, StringComparison.Ordinal);
            }

            if (this.SortValue != _UnsetSortValue && other.SortValue != _UnsetSortValue)
            {
                return this.SortValue.CompareTo(other.SortValue);
            }

            if (this.SortValue != _UnsetSortValue && other.SortValue == _UnsetSortValue)
            {
                return -1;
            }

            return 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
