﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManager.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
    {
		public BaseViewModel()
		{
		}


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected bool SetProperty<T>(ref T backingStore, T value,[CallerMemberName] string propertyName = "",Action onChanged = null, bool checkEquality = true)
        {
            if (checkEquality && EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

