using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Windows.Controls;
using System.Globalization;


namespace KyoeiSystem.Application.Windows.Views {
  
    public static class ValueConverterUtil{
        /// <summary>
        /// 引数がnullかどうか判定する。
        /// nullである、または、DBNull型のobjectである場合がtrue、そうでなければfalseを返す
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNull(object o) {

            if(o == null) {
                return true;
            }
            if(o is DBNull) {
                return true;
            }

            return false;
        }

        // public static readonly object NullObj = null;
        public static readonly object NullObj = DBNull.Value; // null

    }


    /// <summary>
    /// 20160408 sakurai add
    /// IValueConverterを実装したクラスで、NInt32型の値とHH:MM形式の文字列をやりとりするクラス
    /// </summary>
    /// 
    public class NInt32HHMMConverter : IValueConverter {
        public object Convert(object value, Type type, object parameter, CultureInfo culture) {

            if(ValueConverterUtil.IsNull (value)) {
                //** 2016-12-22 TOYAMA **//
                //** 空腹時には00:00を代入します **//
                //return string.Empty;
                return"00:00";
            }

            int i = 0;
            if(value is int?) {
                i = ((int?)value).Value;
            } else if(value is int) {
                i = (int)value;
            } else {
                if(int.TryParse(value.ToString(), out i)) {

                }
            }

            int i2 = i / 100;
            int i1 = i - i2 * 100;

            return i2.ToString("00") + ":" + i1.ToString("00");

        }

        public object ConvertBack(object value, Type type, object parameter, CultureInfo culture) {

            if(ValueConverterUtil.IsNull(value)) {
                return ValueConverterUtil.NullObj ;
            }

            string s = ""; //(string)value;

            if(value is TimeSpan) {
                TimeSpan ts = (TimeSpan)value;
                s = ts.ToString();
            } else if(value is string) {
                s = value as string;
            } else {
                s = value.ToString();
            }

            if(string.IsNullOrWhiteSpace(s)) {
                return ValueConverterUtil.NullObj;
            }

            try {
                var aaa = s.Split(':');
                if(aaa.Length >= 2) {
                    return int.Parse(aaa[0]) * 100 + int.Parse(aaa[1]);
                } else {
                    return int.Parse(s);
                }
            } catch(Exception) {
                return ValueConverterUtil.NullObj;
            }
         
        }
    }



    public class NDecimalConverter : IValueConverter {
        public object Convert(object value, Type type, object parameter, CultureInfo culture) {

            if(ValueConverterUtil.IsNull(value)) {
                return ValueConverterUtil.NullObj;
            }

            try {
                return value.ToString();
            } catch(Exception) {
                return ValueConverterUtil.NullObj;
            }
        }

        public object ConvertBack(object value, Type type, object parameter, CultureInfo culture) {
            try {
                if(ValueConverterUtil.IsNull(value)) {
                    return ValueConverterUtil.NullObj;
                }

                var typeee = value.GetType();
                var tc = Type.GetTypeCode(typeee);
                switch(tc) {
                    case TypeCode.Int32: {
                            return (decimal)((int)value);
                        }
                    case TypeCode.Int64: {
                            return (decimal)((long)value);
                        }
                    case TypeCode.Int16: {
                            return (decimal)((short)value);
                        }
                    case TypeCode.Decimal: {
                            return (decimal)((decimal)value);
                        }
                    case TypeCode.Double: {
                            return (decimal)((double)value);
                        }
                    case TypeCode.Single: {
                            return (decimal)((float)value);
                        }
                }


                string s = "";
                if(value is string) {
                    s = value as string;
                } else {
                    s = value.ToString();
                }

                //decimal? v;
                if(string.IsNullOrEmpty(s)) {
                    return ValueConverterUtil.NullObj;
                } else {
                    decimal v2 = 0.0m;
                    if(decimal.TryParse(s, out v2)) {
                        return v2;
                    }
                }
                return ValueConverterUtil.NullObj;
            } catch(Exception ex) {
                string s = ex.Message;
                return ValueConverterUtil.NullObj;
            }
        }
    }

    public class NInt32Converter : IValueConverter {
        public object Convert(object value, Type type, object parameter, CultureInfo culture) {

            if(ValueConverterUtil.IsNull(value)) {
                return ValueConverterUtil.NullObj;
            }

            try {
                return value.ToString();
            } catch(Exception) {
                return ValueConverterUtil.NullObj;
            }
        }

        public object ConvertBack(object value, Type type, object parameter, CultureInfo culture) {
            try {
                if(ValueConverterUtil.IsNull(value)) {
                    return ValueConverterUtil.NullObj;
                }

                var typeee = value.GetType();
                var tc = Type.GetTypeCode(typeee);
                switch(tc) {
                    case TypeCode.Int32: {
                            return (int)value;
                        }
                    case TypeCode.Int64: {
                            return (int)((long)value);
                        }
                    case TypeCode.Int16: {
                            return (int)((short)value);
                        }
                    case TypeCode.Decimal: {
                            return (int)((decimal)value);
                        }
                    case TypeCode.Double: {
                            return (int)((double)value);
                        }
                    case TypeCode.Single: {
                            return (int)((float)value);
                        }
                }


                string s = "";
                if(value is string) {
                    s = value as string;
                } else {
                    s = value.ToString();
                }

                //int? v;
                if(string.IsNullOrEmpty(s)) {
                    return ValueConverterUtil.NullObj;
                } else {
                    int v2 = 0;
                    if(int.TryParse(s, NumberStyles.AllowHexSpecifier, null, out v2)) {
                        return v2;
                    }
                }
                return ValueConverterUtil.NullObj;
            } catch(Exception ex) {
                string s = ex.Message;
                return ValueConverterUtil.NullObj;
            }
        }
    }
}
