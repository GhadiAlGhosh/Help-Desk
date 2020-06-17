using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.ViewModels
{
    public class BindableBase : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		// SetField (Name, value); // where there is a data member
		protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string property = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return false;
			field = value;
			RaisePropertyChanged(property);
			return true;
		}

		// SetField(()=> somewhere.Name = value; somewhere.Name, value) // Advanced case where you rely on another property
		protected void SetProperty<T>(T currentValue, T newValue, Action doSet, [CallerMemberName] string property = null)
		{
			if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return;
			doSet.Invoke();
			RaisePropertyChanged(property);
		}

		private void RaisePropertyChanged(string property)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
	public class BindableBase<T> : BindableBase where T : class, new()
	{
		protected readonly T This;

		public static implicit operator T(BindableBase<T> thing) { return thing.This; }

		protected BindableBase(T thing = null)
		{
			This = thing ?? new T();
		}
	}
}
