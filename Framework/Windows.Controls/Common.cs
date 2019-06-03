using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace KyoeiSystem.Framework.Windows.Controls
{

    /// <summary>
    /// IMEのタイプ
    /// </summary>
    public enum IMETypes {
        /// <summary>
        /// 未設定
        /// </summary>
        Off,
        /// <summary>
        /// 日本語文字列
        /// </summary>
        Native,
		/// <summary>
		/// 全角カタカナ
		/// </summary>
		Katakana,
		/// <summary>
		/// 半角カタカナ
		/// </summary>
		HankakuKatakana,
	}

	public enum OnOff
	{
		/// <summary>
		/// OFF
		/// </summary>
		On,
		/// <summary>
		/// ON
		/// </summary>
		Off,
	}

    public enum SearchModeTypes
    {
        /// <summary>
        /// 前方一致
        /// </summary>
        FORWARD_MATCH,
        /// <summary>
        /// 後方一致
        /// </summary>
        BACKWARD_MATCH,
        /// <summary>
        /// 部分一致
        /// </summary>
        PARTIAL_MATCH,
    }

	/// <summary>
	/// Booleanプロパティとint型の値の変換
	/// </summary>
	public class BooleanInverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return false;
			}
			if (value is int)
			{
				return ((int)value == 0) ? false : true;
			}
			if (value is bool)
			{
				return (bool)value;
			}
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return 0;
			}
			if (value is int)
			{
				return (int)value;
			}
			if (value is bool)
			{
				return (bool)value ? 1 : 0;
			}
			return 0;
		}
	}

	/// <summary>
	/// IsTabStopプロパティと他のBoolean型プロパティの変換
	///   主としてReadOnlyの値の反転値をIsTabStopに与える為の機能であり、DataGridCellのReadOnlyのセルを
	///   Tabキーでカーソル移動する際にスキップ対象とすることができる
	/// </summary>
	public class IsTabStopConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if ((bool)value == true)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}


	public static class FocusControl
	{
		public static void SetFocusWithOrder(TraversalRequest vector)
		{
			if (vector != null)
			{
				UIElement element = Keyboard.FocusedElement as UIElement;
				if (element != null)
				{
					FocusControl.SkipRadioButton(element, vector);
					element = Keyboard.FocusedElement as UIElement;
					if (element != null)
					{
						Type tp = element.GetType();
						if (tp.Name.StartsWith("Ribbon"))
						{
							// リボンコントロールに移動したら入力可能な先頭のフィールドに強制移動する
							FocusControl.SetFocusToTopControl(Window.GetWindow(element));
						}
					}
				}
			}
		}

		private static bool SkipRadioButton(UIElement element, TraversalRequest vector)
		{
			if (element != null)
			{
				element.MoveFocus(vector);
				element = Keyboard.FocusedElement as UIElement;
				//if ((element is RadioButton) || (element is Button) || (element is ComboBox))
				if ((element is RadioButton) || (element is Button))
				{
					return SkipRadioButton(element, vector);
				}
				else
				{
					return true;
				}
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 指定したオブジェクトの範囲内で移動可能な先頭のコントロールにフォーカスを移動する
		/// </summary>
		/// <param name="target">フォーカス移動の検索範囲となるオブジェクト</param>
		/// <returns>true:移動した、false:移動していない</returns>
		public static bool SetFocusToTopControl(DependencyObject target)
		{
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is TextBox)
					{
						if ((child as TextBox).IsEnabled)
						{
							Keyboard.Focus(child as TextBox);
							return true;
						}
					}
					else if (child is PasswordBox)
					{
						if ((child as PasswordBox).IsEnabled)
						{
							Keyboard.Focus(child as PasswordBox);
							return true;
						}
					}
					else if (child is DatePicker)
					{
						if ((child as DatePicker).IsEnabled)
						{
							Keyboard.Focus(child as DatePicker);
							return true;
						}
					}
					else if (child is ComboBox)
					{
						if ((child as ComboBox).IsEnabled)
						{
							Keyboard.Focus(child as ComboBox);
							return true;
						}
					}
					else if (child is CheckBox)
					{
						if ((child as CheckBox).IsEnabled)
						{
							Keyboard.Focus(child as CheckBox);
							return true;
						}
					}
					else if (SetFocusToTopControl((DependencyObject)child))
					{
						return true;
					}
				}
			}
			return false;
		}

	}

	public class PasswordBoxViewModel : INotifyPropertyChanged
	{
		private string password;
		public string Password
		{
			get { return this.password; }
			set
			{
				this.password = value;
				this.OnPropertyChanged("Password");
			}
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class PasswordBindBehavior : Behavior<PasswordBox>
	{
		static bool initialized = false;
		public string Password
		{
			get { return (string)GetValue(PasswordProperty); }
			set { SetValue(PasswordProperty, value); }
		}

		public static readonly DependencyProperty PasswordProperty =
			DependencyProperty.Register("Password", typeof(string)
			, typeof(PasswordBindBehavior), new UIPropertyMetadata(null));

		private void Init()
		{
			if (this.AssociatedObject is PasswordBox)
			{
				this.AssociatedObject.Password = (string)GetValue(PasswordProperty);
			}
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			if (!initialized)
			{
				this.Init();
				initialized = true;
			}
			this.AssociatedObject.PasswordChanged += PasswordChanged;
		}

		void PasswordChanged(object sender, RoutedEventArgs e)
		{
			SetValue(PasswordProperty, ((PasswordBox)sender).Password);
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.PasswordChanged -= PasswordChanged;
		}
	}
}
