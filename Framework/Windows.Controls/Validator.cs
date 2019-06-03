using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Windows.Controls
{
	/// <summary>
	/// 値チェック機能
	/// </summary>
	public static class Validator
	{
		/// <summary>
		/// 値チェック結果メッセージ定数
		/// </summary>
		public static class ValidationMessage
		{
			/// <summary>
			/// 
			/// </summary>
			public const string ErrInteger = "整数値ではありません。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrDecimal = "数値ではありません。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrDecimalFormat = "整数または小数の桁数が不正です。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrDate = "日付の値ではありません。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrTime = "時刻の値ではありません。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrDateTime = "日付時刻の値ではありません。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrASCII = "英数字ではありません。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrNumber = "数値ではありません。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrNihongo = "日本語ではありません。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrString = "入力可能な文字ではありません。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrRange = "範囲外です。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrRangeMax = "最大値 {0} を超えています。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrRangeMin = "最小値 {0} を下回っています。";

			/// <summary>
			/// 
			/// </summary>
			public const string ErrOverLength = "内容が長すぎます。";
			/// <summary>
			/// 
			/// </summary>
			public const string ErrEmpty = "入力必須の項目です。";

			/// <summary>
			/// 
			/// </summary>
			public const string ErrNotFound = "指定されたキーに該当するデータがありません。";
		}

		/// <summary>
		/// 値チェックのタイプ
		/// </summary>
		public enum ValidationTypes
		{
			/// <summary>
			/// チェックなし
			/// </summary>
			None,
			/// <summary>
			/// 数値（整数）
			/// </summary>
			Integer,
            /// <summary>
            /// 数値（マイナス許可）
            /// </summary>
            SignedInteger,
			/// <summary>
			/// 数値（小数）
			/// </summary>
			Decimal,
			/// <summary>
			/// 数値（小数：マイナス許可）
			/// </summary>
			SignedDecimal,
			/// <summary>
			/// 日付
			/// </summary>
			Date,
			/// <summary>
			/// 日付（年月のみ）
			/// </summary>
			DateYYYYMM,
			/// <summary>
			/// 日付（月日のみ）
			/// </summary>
			DateMMDD,
			/// <summary>
			/// 時刻
			/// </summary>
			Time,
			/// <summary>
			/// 日付および時刻
			/// </summary>
			DateTime,
			/// <summary>
			/// 数字文字
			/// </summary>
			Number,
			/// <summary>
			/// 数字文字（マイナス許可）
			/// </summary>
			SignedNumber,
			/// <summary>
			/// 英数字文字（ASCII）
			/// </summary>
			ASCII,
			/// <summary>
			/// 日本語文字列
			/// </summary>
			Nihongo,
			/// <summary>
			/// 全ての文字（ASCII文字日本語混在）
			/// </summary>
			String,
			/// <summary>
			/// 文字のリスト
			/// </summary>
			StringList,
			/// <summary>
			/// AutoComplete数値入力専用（ComboBoxのアイテムからの選択）
			/// </summary>
			NumberAutoComplete,
			/// <summary>
			/// AutoComplete文字入力専用（ComboBoxのアイテムからの選択）
			/// </summary>
			StringAutoComplete,
			/// <summary>
			/// AutoComplete正規表現による入力専用（別途CustomCharsプロパティの設定が必要）
			/// </summary>
			CustomAutoComplete,
			/// <summary>
			/// 編集可能かつAutoComplete（リストになくても正常とする）
			/// </summary>
			EditableAutoComplete,
		}

		public static bool InputCheck(ValidationTypes vtype, string text, string custom = null, List<string> valuelist = null)
		{
			bool ret = false;

			switch (vtype)
			{
			case ValidationTypes.None:
				return true;
			case Validator.ValidationTypes.Integer:
				if ((Regex.IsMatch(text, @"^[0-9]*$")))
				{
					return true;
				}
				break;
            case Validator.ValidationTypes.SignedInteger:
				if ((Regex.IsMatch(text, @"^[-+]?[0-9]*$")))
                {
                    return true;
                }
                break;
			case Validator.ValidationTypes.Decimal:
			case Validator.ValidationTypes.Number:
				if ((Regex.IsMatch(text, @"^[-+]?[0-9\.\,]*$")))
				{
					return true;
				}
				break;
			case Validator.ValidationTypes.SignedDecimal:
			case Validator.ValidationTypes.SignedNumber:
				if ((Regex.IsMatch(text, @"^[-+]?[0-9\.\,\-]*$")))
				{
					return true;
				}
				break;
			case Validator.ValidationTypes.Date:
			case Validator.ValidationTypes.DateYYYYMM:
			case Validator.ValidationTypes.DateMMDD:
				if ((Regex.IsMatch(text, @"^[0-9/]*$")))
				{
					return true;
				}
				break;
			case Validator.ValidationTypes.Time:
				if ((Regex.IsMatch(text, @"^[0-9:]*$")))
				{
					return true;
				}
				break;
			case Validator.ValidationTypes.DateTime:
				if ((Regex.IsMatch(text, @"^[0-9/:]*$")))
				{
					return true;
				}
				break;
			case Validator.ValidationTypes.ASCII:
				if ((Regex.IsMatch(text, "^[\x20-\x7F]*$")))
				{
					return true;
				}
				break;
			case Validator.ValidationTypes.Nihongo:
				if ((Regex.IsMatch(text, "^[\u0100-\uFFFF]*$")))
				{
					return true;
				}
				break;
			case Validator.ValidationTypes.String:
				break;
			case Validator.ValidationTypes.StringList:
				if (valuelist == null)
				{
					return true;
				}
				if (valuelist.Contains(text))
				{
					return true;
				}
				break;
			case ValidationTypes.CustomAutoComplete:
			case ValidationTypes.EditableAutoComplete:
				if (string.IsNullOrWhiteSpace(custom) == true)
				{
					return true;
				}
				if ((Regex.IsMatch(text, custom)))
				{
					return true;
				}
				break;
			case ValidationTypes.NumberAutoComplete:
				if ((Regex.IsMatch(text, @"^[0-9]*$")))
				{
					return true;
				}
				break;
			case ValidationTypes.StringAutoComplete:
				return true;
			}

			return ret;
		}

		/// <summary>
		/// 各タイプに応じて値チェックを行う。
		/// </summary>
		/// <param name="ValidationType">値タイプ</param>
		/// <param name="value">入力値</param>
		/// <returns>
		/// チェック結果
		/// ・true：OK
		/// ・false：NG
		/// </returns>
		public static string Check(ValidationTypes ValidationType, string value, string mask = null, List<string> valuelist = null)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			DateTime date;
			switch (ValidationType)
			{
			case Validator.ValidationTypes.Integer:
				long integer;
				if (long.TryParse(value, System.Globalization.NumberStyles.AllowThousands, null, out integer) == false)
				{
					return ValidationMessage.ErrInteger;
				}
				break;
            case Validator.ValidationTypes.SignedInteger:
                long integer2;
				if (long.TryParse(value, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign, null, out integer2) == false)
                {
                    return ValidationMessage.ErrInteger;
                }
                break;
			case Validator.ValidationTypes.Decimal:
			case Validator.ValidationTypes.Number:
				if (!(Regex.IsMatch(value, @"^[-+]?[0-9,\,\.]+$")))
				{
					return ValidationMessage.ErrNumber;
				}
				break;
			case Validator.ValidationTypes.SignedDecimal:
			case Validator.ValidationTypes.SignedNumber:
				if (!(Regex.IsMatch(value, @"^[-+]?[0-9,\,\.]+$")))
				{
					return ValidationMessage.ErrNumber;
				}
				break;
			case Validator.ValidationTypes.Date:
				if (DateTime.TryParse(value, out date) == false)
				{
					return ValidationMessage.ErrDate;
				}
				break;
			case Validator.ValidationTypes.DateYYYYMM:
				string ymd = value + "/1";
				if (DateTime.TryParse(ymd, out date) == false)
				{
					return ValidationMessage.ErrDate;
				}
				break;
			case Validator.ValidationTypes.DateMMDD:
				if (DateTime.TryParse(DateTime.Today.ToString("yyyy/") + value, out date) == false)
				{
					return ValidationMessage.ErrDate;
				}
				break;
			case Validator.ValidationTypes.DateTime:
			case Validator.ValidationTypes.Time:
				if (DateTime.TryParse(value, out date) == false)
				{
					return ValidationMessage.ErrTime;
				}
				break;
			case Validator.ValidationTypes.ASCII:
				if (!(Regex.IsMatch(value, "^[\x20-\x7F]+$")))
				{
					return ValidationMessage.ErrASCII;
				}
				break;
			case Validator.ValidationTypes.Nihongo:
				if (!(Regex.IsMatch(value, "^[\u0100-\uFFFF]+$")))
				{
					return ValidationMessage.ErrNihongo;
				}
				break;
			case Validator.ValidationTypes.String:
				if (!(Regex.IsMatch(value, "^[\x20-\x7F,\u0100-\uFFFF]+$")))
				{
					return ValidationMessage.ErrNihongo;
				}
				break;
			case Validator.ValidationTypes.StringList:
				if (valuelist == null)
				{
					return string.Empty;
				}
				foreach (string chkstr in valuelist)
				{
					if (value == chkstr)
					{
						return string.Empty;
					}
				}
				return ValidationMessage.ErrRange;
			case ValidationTypes.CustomAutoComplete:
			case ValidationTypes.EditableAutoComplete:
				if (string.IsNullOrWhiteSpace(mask) != true)
				{
					if (!(Regex.Match(value, mask)).Success)
					{
						return ValidationMessage.ErrString;
					}
				}
				break;
			case ValidationTypes.NumberAutoComplete:
				if ((Regex.IsMatch(value, @"^[0-9]$")))
				{
					return string.Empty;
				}
				break;
			case ValidationTypes.StringAutoComplete:
				return string.Empty;
			}

			return string.Empty;
		}

		public static string CheckRange(ValidationTypes ValidationType, string value, string mask, string minval, string maxval)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			DateTime date;
			switch (ValidationType)
			{
			case Validator.ValidationTypes.Integer:
			case Validator.ValidationTypes.SignedInteger:
				long integer;
				if (long.TryParse(value, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign, null, out integer) == false)
                {
                    return ValidationMessage.ErrInteger;
                }
				// 値の範囲指定がある場合、追加のチェックを行う。
				if (string.IsNullOrWhiteSpace(minval) != true)
				{
					int ival;
					if (int.TryParse(minval, out ival) != true)
					{
						ival = int.MinValue;
					}
					if (integer < ival)
					{
						return string.Format(ValidationMessage.ErrRangeMin, minval);
					}
				}
				if (string.IsNullOrWhiteSpace(maxval) != true)
				{
					int ival;
					if (int.TryParse(maxval, out ival) != true)
					{
						ival = int.MaxValue;
					}
					if (integer > ival)
					{
						return string.Format(ValidationMessage.ErrRangeMax, maxval);
					}
				}
				break;
			case Validator.ValidationTypes.Decimal:
			case Validator.ValidationTypes.SignedDecimal:
			case Validator.ValidationTypes.Number:
			case Validator.ValidationTypes.SignedNumber:
				decimal deValue;
				if (decimal.TryParse(value, System.Globalization.NumberStyles.Number, null, out deValue) == false)
                {
                    return ValidationMessage.ErrDecimal;
                }
				// 値の範囲指定がある場合、追加のチェックを行う。
				if (string.IsNullOrWhiteSpace(minval) != true)
				{
                    var val = Decimal.Parse(minval);
                    if (deValue < val)
					{
						return string.Format(ValidationMessage.ErrRangeMin, minval);
					}
				}
				if (string.IsNullOrWhiteSpace(maxval) != true)
				{
                    var val = Decimal.Parse(maxval);
                    if (deValue > val)
					{
						return string.Format(ValidationMessage.ErrRangeMax, maxval);
					}
				}
				break;
			case Validator.ValidationTypes.Date:
				break;
			case Validator.ValidationTypes.DateYYYYMM:
				break;
			case Validator.ValidationTypes.DateMMDD:
				break;
			case Validator.ValidationTypes.DateTime:
			case Validator.ValidationTypes.Time:
				break;
			case Validator.ValidationTypes.ASCII:
				break;
			case Validator.ValidationTypes.Nihongo:
				break;
			case Validator.ValidationTypes.String:
				break;
			}

			return string.Empty;
		}

		public static string CheckLength(int maxlength, string value)
		{
			if (maxlength <= 0)
			{
				// 最大長を指定されていない場合は長さチェックしない
				return string.Empty;
			}

			Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
			int num = sjisEnc.GetByteCount(value);
			if (num > maxlength)
			{
				return ValidationMessage.ErrOverLength;
			}

			return string.Empty;
		}

	}
}
