using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

using KyoeiSystem.Framework.Windows;

namespace KyoeiSystem.Framework.Windows.Controls
{
	/// <summary>
	/// UcTextBox.xaml の相互作用ロジック
	/// </summary>
	public partial class UcPasswordBox : FrameworkControl
	{
		private Validator.ValidationTypes _validationType;
		private Brush _originBackground;
		private Brush _originBorderBrush;
		private Thickness _originBorderThickness;
		private object _originTooltip = string.Empty;
		private string _validationStatus = string.Empty;
		private string internalValidationStatus
		{
			get { return this._validationStatus; }
			set
			{
				this._validationStatus = value;
				if (value == string.Empty)
				{
					this.BorderBrush = this._originBorderBrush;
					this.BorderThickness = this._originBorderThickness;
					this.ToolTip = _originTooltip;
				}
				else
				{
					this.BorderBrush = new SolidColorBrush(System.Windows.Media.Colors.Red);
					this.BorderThickness = new Thickness(2);
					this.ToolTip = value;
				}
			}
		}

		public static readonly DependencyProperty PasswordTextProperty = DependencyProperty.Register(
				"PasswordText",
				typeof(string),
				typeof(UcPasswordBox),
				new UIPropertyMetadata(string.Empty)
		);

		[BindableAttribute(true)]
		public string PasswordText
		{
			get { return (string)GetValue(PasswordTextProperty); }
			set
			{
				SetPassword(value);
				SetValue(PasswordTextProperty, value);
			}
		}

		/// <summary>
		/// TextBox(UserControl)
		/// </summary>
		public UcPasswordBox()
			: base()
		{
			InitializeComponent();
		}

		public void SetPassword(string passwd)
		{
			if (this.cPasswordBox.Password != passwd)
				this.cPasswordBox.Password = passwd;
		}
		/// <summary>
		/// 値チェック
		/// </summary>
		[Category("動作")]
		public Validator.ValidationTypes ValidationType
		{
			set { _validationType = value; }
			get { return _validationType; }
		}

		/// <summary>
		/// 値チェックメッセージ
		/// </summary>
		[Category("動作")]
		public string ValidationStatus
		{
			get { return this._validationStatus; }
		}

		private bool _isRequired = false;
		/// <summary>
		/// 入力必須項目であるかどうかを指定します。
		/// </summary>
		[Category("動作")]
		public bool IsRequired
		{
			get
			{
				return this._isRequired;
			}
			set
			{
				this._isRequired = value;
				NotifyPropertyChanged();
			}
		}

		#region Event

		/// <summary>
		/// cTextChangeイベントの作成
		/// </summary>
		public static readonly RoutedEvent PasswordChangedEvent = EventManager.RegisterRoutedEvent("PasswordChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcPasswordBox));

		public event RoutedEventHandler PasswordChanged
		{
			add { AddHandler(PasswordChangedEvent, value); }
			remove { RemoveHandler(PasswordChangedEvent, value); }
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			this.PasswordText = this.cPasswordBox.Password;
		}


		/// <summary>
		/// 論理フォーカスを取得したときに発生します。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cPasswordBox_GotFocus(object sender, RoutedEventArgs e)
		{
            IMEChange();
			this.Background = new SolidColorBrush(System.Windows.Media.Colors.Orange);
			this.cPasswordBox.SelectAll();
		}

        /// <summary>
        /// IMEの切り替え
        /// </summary>
        private void IMEChange()
		{
            //初期化
            InputMethod im = InputMethod.Current;
            im.ImeState = InputMethodState.Off;
        }

		/// <summary>
		/// 論理フォーカスを失ったときに発生します。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cPasswordBox_LostFocus(object sender, RoutedEventArgs e)
		{
			this.Background = _originBackground;
		}

		#endregion

		/// <summary>
		/// フォーカス移動
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cPasswordBox_KeyDown(object sender, KeyEventArgs e)
		{
			TraversalRequest vector = null;
			switch (e.Key)
			{
			case Key.Enter:
				vector = new TraversalRequest(FocusNavigationDirection.Next);
				break;
			case Key.Down:
				vector = new TraversalRequest(FocusNavigationDirection.Next);
				break;
			case Key.Up:
				vector = new TraversalRequest(FocusNavigationDirection.Previous);
				break;
			}
			if (vector != null)
			{
				e.Handled = true;
				FocusControl.SetFocusWithOrder(vector);
			}
		}

		public override bool CheckValidation()
		{
			bool result = true;
			string status = string.Empty;
			if (IsRequired)
			{
				if (string.IsNullOrWhiteSpace(this.PasswordText))
				{
					status = Validator.ValidationMessage.ErrEmpty;
				}
			}

			internalValidationStatus = status;
			if (string.IsNullOrEmpty(status))
			{
				result = true;
			}
			else
			{
				result = false;
			}

			return result;
		}

		public override string GetValidationMessage()
		{
			return internalValidationStatus;
		}


		public void ResetValidation()
		{
			this.internalValidationStatus = string.Empty;
		}

	}
}
