using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;
using GrapeCity.Windows.SpreadGrid;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.EntityClient;
using System.IO;
using System.Windows.Data;

namespace KyoeiSystem.Application.Windows.Views
{
    #region 範囲バリデーションクラス
    // Validator例（最大・最小）
    public class VaridatorRange : ValidationRule, INotifyPropertyChanged
    {
        private int _min;
        private int _max;

        public VaridatorRange()
        {
        }

        public int Min
        {
            get { return _min; }
            set { _min = value; NotifyPropertyChanged(); }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; NotifyPropertyChanged(); }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((age < Min) || (age > Max))
            {
                return new ValidationResult(false,
                  "入力された値が範囲を超えています。：" + Min + " ～ " + Max);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }

        #region INotifyPropertyChanged メンバー
        /// <summary>
        /// Binding機能対応（プロパティの変更通知イベント）
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Binding機能対応（プロパティの変更通知イベント送信）
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
    #endregion

    #region AppConst
    public static class AppConst
    {
        #region 定数定義

        public static string MAINTENANCEMODE_EDIT = "編 集";
        public static string MAINTENANCEMODE_ADD = "新 規";
        public static string CONFIRM_UPDATE = "登録してもよろしいですか？";
        public static string CONFIRM_DELETE = "削除してもよろしいですか？";
        public static string CONFIRM_CANCEL = "入力を取り消してもよろしいですか？";
        public static string CONFIRM_CLOSE = "画面を閉じてよろしいですか？";
        public static string CONFIRM_PRINT = "帳票の印刷をおこなってもよろしいですか？";
        public static string COMPLETE_UPDATE = "登録が完了しました。";
        public static string COMPLETE_DELETE = "削除が完了しました。";
        public static string RESTORE_DATA = "復元したデータを登録しますか？";


        public static string SUCCESS_UPDATE = "登録処理が完了しました。";
        public static string SUCCESS_DELETE = "削除処理が完了しました。";

        public static string FAILED_UPDATE = "登録処理に失敗しました。";
        public static string FAILED_DELETE = "削除処理に失敗しました。";

        public static string CONFIRM_DELETE_ROW = "選択されている行を削除してもよろしいですか？";

        public static int KEIHI_CODE_KEIYUZEI = 401;

        // 請求書
        public static string BillReportFileBase = @"Files\TKS\";
        public static string BillReportFileOriginalBase = @"Files\Original\";
        public static string rptBill_A = BillReportFileBase + @"BillReportA.rpt";
        public static string rptBill_B = BillReportFileBase + @"BillReportB.rpt";
        public static string rptBill_T = BillReportFileBase + @"BillReportT.rpt";
        public static string rptBill_K = BillReportFileBase + @"BillReportK.rpt";
        public static string rptBill_C = BillReportFileBase + @"BillReportC.rpt";

        // ログインチェックしないユーザー（デバッグ時想定）
        // ※公開時にはstring.Emptyに書き換える。
        static public string CommonDB_DebugUser = "lU9A9P/GnOAPbMaQEkyujQ==";

        #endregion

        #region 列挙型定義

        /// <summary>
        /// ページングボタン用
        /// パラメータ定義
        /// </summary>
        public enum PagingOption : int
        {
            /// <summary>先頭データ取得</summary>
            Paging_Top = -2,
            /// <summary>前データ取得</summary>
            Paging_Before = -1,
            /// <summary>指定コード取得</summary>
            Paging_Code = 0,
            /// <summary>次データ取得</summary>
            Paging_After = 1,
            /// <summary>最終データ取得</summary>
            Paging_End = 2
        }

        #endregion

    }
    #endregion

    #region データクラス定義

    public class DateFromTo
    {
        public bool Result;
        public DateTime DATEFrom;
        public DateTime DATETo;
        public int Kikan;
    }

    public class DateList
    {
        public bool Result;
        public DateTime[] dDATEList;
        public int Kikan;
    }

    public class PrinterDriver
    {
        public bool Result;
        public string PrinterName;
    }

    public class DaysList
    {
        public bool Result;
        public DateTime DateFrom;
        public int ErrorCode;

    }

    public class NNGDays
    {
        public bool Result;
        public string DateFrom;
        public int ErrorCode;
        public string DateYear;
        public string DateMonth;
    }

    public class DataFromTo
    {
        public bool Result;
        public string DataFrom;
        public string DataTo;
    }

    public class CsvData
    {
        public bool Result;
        public string Date;
        public string No;
    }

    #endregion

    #region AppCommon

    /// <summary>
    /// アプリケーション共通クラス
    /// （システム固有の機能）
    /// </summary>
    public static class AppCommon
    {
        #region  画面の基底クラスが所有する連携用オブジェクトを取得
        /// <summary>
        /// 画面の基底クラスが所有する連携用オブジェクトを取得する
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static ViewsCommon GetViewsCommonData(Window view)
        {
            ViewsCommon viewcom = null;
            if (view is WindowViewBase)
            {
                viewcom = (view as WindowViewBase).viewsCommData;
            }
            else if (view is RibbonWindowViewBase)
            {
                viewcom = (view as RibbonWindowViewBase).viewsCommData;
            }
            return viewcom;
        }
        #endregion

        #region 年、月、締日で開始日付、終了日付を返す
        /// <summary>　中村作成
        /// 年、月、締日で開始日付、終了日付を返す
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static DateFromTo GetDateFromTo(int iYear, int iMonth, int iSime)
        {
            var ret = new DateFromTo();

            try
            {
                if (iYear < 2 || (iMonth < 1 || iMonth > 12) || (iSime < 1 || iSime > 31))
                {
                    ret.Result = false;
                    return ret;
                }

                if (iSime < 31)
                {

                    //string Date = iYear.ToString() + "/" + iMonth.ToString() + "/" + 01;
                    DateTime dYMD = Convert.ToDateTime(iYear.ToString() + "/" + iMonth.ToString() + "/" + 01);
                    dYMD = dYMD.AddDays(-1);
                    int iSimechk = dYMD.Day;
                    if (iSimechk < iSime + 1)
                    {
                        dYMD = dYMD.AddDays(1);
                        ret.DATEFrom = dYMD;
                    }
                    else
                    {
                        ret.DATEFrom = Convert.ToDateTime(dYMD.Year.ToString() + "/" + dYMD.Month.ToString() + "/" + (iSime + 1).ToString());
                    }

                    dYMD = Convert.ToDateTime(iYear.ToString() + "/" + iMonth.ToString() + "/" + 01);
                    dYMD = dYMD.AddMonths(1).AddDays(-1);
                    iSimechk = dYMD.Day;
                    if (iSimechk < iSime)
                    {
                        ret.DATETo = dYMD;
                    }
                    else
                    {
                        ret.DATETo = Convert.ToDateTime(iYear.ToString() + "/" + iMonth.ToString() + "/" + iSime.ToString());
                    }
                    TimeSpan span = ret.DATETo - ret.DATEFrom;
                    ret.Kikan = span.Days;
                    ret.Result = true;
                }
                else if (iSime == 31)
                {
                    DateTime dYMD = Convert.ToDateTime(iYear.ToString() + "/" + iMonth.ToString() + "/" + 01);
                    ret.DATEFrom = dYMD;
                    ret.DATETo = dYMD.AddMonths(1).AddDays(-1);
                    ret.Result = true;

                }
                else
                {
                    ret.Result = false;
                }

                return ret;
            }
            //catch (Exception ex)
            catch
            {
                ret.Result = false;
                return ret;
            }
        }
        #endregion

        #region 開始、終了日付から、日のリストを返す。
        /// <summary>　中村作成
        /// 開始、終了日付から、日のリストを返す。
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static DateList GetDateList(DateTime dDateFrom, DateTime dDateTo)
        {
            var ret = new DateList();

            try
            {
                if (dDateFrom > dDateTo)
                {
                    ret.Result = false;
                    return ret;
                }

                int cnt = 0;
                for (DateTime dDate = dDateFrom; dDate <= dDateTo; cnt += 1)
                {
                    ret.dDATEList[cnt] = dDate;
                    dDate = dDate.AddDays(1);
                }
                TimeSpan span = dDateTo - dDateFrom;
                ret.Kikan = span.Days;
                ret.Result = true;
                return ret;
            }
            //catch (Exception ex)
            catch
            {
                ret.Result = false;
                return ret;
            }
        }
        #endregion

        #region 月の入力ルール作成
        /// <summary>
        /// 中村作成　2014/12/6
        /// ValidationRule
        /// 月の入力ルール作成
        /// </summary>
        public class MonthValidationRule : ValidationRule
        {
            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                var inputValue = value as string;

                int parsedValue = 0;
                if (!int.TryParse(inputValue, out parsedValue))
                {
                    return new ValidationResult(false, "月を正しく入力して下さい。");
                }
                else
                {
                    if (parsedValue <= 0 || parsedValue > 12)
                    {
                        return new ValidationResult(false, "月を正しく入力して下さい。");
                    }
                }

                return ValidationResult.ValidResult;
            }
        }
        #endregion

        #region 印刷するプリンタがプリンタの一覧にあるかチェック
        /// <summary>　中村作成
        /// プログラムから印刷するプリンタがプリンタの一覧にあるかチェック
        /// 無ければ通常使うプリンタを返す。
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static PrinterDriver GetPrinter(string sPrinterName)
        {
            //PrintDocumentの作成
            System.Drawing.Printing.PrintDocument pd =
            new System.Drawing.Printing.PrintDocument();


            var ret = new PrinterDriver();
            ret.Result = false;
            ret.PrinterName = sPrinterName;

            foreach (string s in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if (s == sPrinterName)
                {
                    ret.Result = true;
                }
            }
            if (ret.Result == false)
            {
                ret.PrinterName = pd.PrinterSettings.PrinterName;
            }
            if (string.IsNullOrEmpty(ret.PrinterName))
            {
                ret.Result = false;
            }
            else
            {
                ret.Result = true;
            }
            return ret;
        }
        #endregion

        #region 日付処理　【遠山作成】

        /// <summary>  遠山作成
        /// Int型で送られてきた【年　月】をDatetime型に変換し返す
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static DaysList GetDaysList(int? iYear, int? iMonth, int? iDay)
        {
            var ret = new DaysList();

            //*** Null値チェック ***//
            if (iYear == null)
            {
                iYear = DateTime.Now.Year;
            }
            if (iMonth == null)
            {
                iMonth = DateTime.Now.Month;
            }
            if (iDay == null)
            {
                iDay = 1;
            }

            //*** 変数定義 ***//
            string sYear, sMonth, sDay;
            sYear = iYear.ToString();
            sMonth = iMonth.ToString();
            sDay = iDay.ToString();

            ///<<<ErrorCode>>> 【ErrorCode = 0】:例外エラー 【ErrorCode = 1】:作成月の不正エラー　【ErrorCode = 2】:作成日の不正エラー

            try
            {
                if (iMonth > 0 && iMonth < 13)
                {
                    if (iDay > 0 && iDay < 32)
                    {
                        //Resultの初期値を【True】に設定
                        ret.Result = true;

                        //作成月が一桁だった場合0を代入
                        if (sMonth.Length == 1)
                        {
                            sMonth = "0" + sMonth;
                        }
                        else if (sMonth.Length > 2)
                        {
                            ret.Result = false;
                            ret.ErrorCode = 1;
                            return ret;
                        }

                        //作成日が一桁だった場合0を代入
                        if (sDay.Length == 1)
                        {
                            sDay = "0" + sDay;
                        }
                        else if (sDay.Length > 2)
                        {
                            ret.Result = false;
                            ret.ErrorCode = 2;
                            return ret;
                        }

                        if (!(string.IsNullOrEmpty(sYear) && string.IsNullOrEmpty(sMonth)) && ret.Result != false)
                        {
                            ret.DateFrom = Convert.ToDateTime(sYear + "/" + sMonth + "/" + sDay);
                            return ret;
                        }
                    }
                    else
                    {
                        ret.Result = false;
                        ret.ErrorCode = 2;
                        return ret;
                    }
                }
                else
                {
                    ret.Result = false;
                    ret.ErrorCode = 1;
                    return ret;
                }
                return ret;
            }
            //catch (Exception ex)
            catch
            {
                ret.Result = false;
                ret.ErrorCode = 0;
                return ret;
            }
        }

        #endregion

        #region NNG画面の日付 【遠山作成】

        /// <summary> 遠山作成
        /// 月別合計表の日付処理
        /// </summary>
        public static NNGDays GetNNGDays(string Days)
        {

            var ret = new NNGDays();
            ret.Result = false;
            //送られてきたデータがNullの場合
            if (string.IsNullOrEmpty(Days))
            {
                ret.DateFrom = string.Empty;
                ret.ErrorCode = 1;
                return ret;
            }

            //文字列の長さが 5 or 6 or 7　以外はエラーで返す
            if (Days.Length == 5 || Days.Length == 6 || Days.Length == 7)
            {
                #region 文字列の長さが5文字

                if (Days.Length == 5)
                {
                    string 変換作成年 = Days.Substring(0, 4);
                    string 変換作成月 = Days.Substring(4, 1);
                    int i = 0;
                    if (変換作成月.Contains("+") || 変換作成月.Contains("-") == true || 変換作成年.Contains("+") || 変換作成年.Contains("-") == true)
                    {
                        ret.DateFrom = string.Empty;
                        return ret;
                    }
                    bool result = int.TryParse(変換作成年, out i) && int.TryParse(変換作成月, out i);
                    if (result == false)
                    {
                        ret.DateFrom = string.Empty;
                        return ret;
                    }
                    int? i変換作成月 = AppCommon.IntParse(変換作成月);
                    if (i変換作成月 != 0)
                    {
                        string 変換作成年月 = 変換作成年 + "/0" + 変換作成月;
                        ret.DateFrom = 変換作成年月;  //XAML表示用
                        ret.DateYear = 変換作成年;
                        ret.DateMonth = 変換作成月;
                        ret.Result = true;
                        return ret;
                    }
                    else
                    {
                        ret.DateFrom = string.Empty;
                        ret.ErrorCode = 1;
                        return ret;
                    }

                }

                #endregion

                #region 文字列の長さが6文字

                //文字列の長さが6の時
                if (Days.Length == 6)
                {

                    string 変換作成年 = Days.Substring(0, 4);
                    string 変換作成月 = Days.Substring(4, 2);
                    int i = 0;
                    if (変換作成月.Contains("+") || 変換作成月.Contains("-") == true || 変換作成年.Contains("+") || 変換作成年.Contains("-") == true)
                    {
                        ret.DateFrom = string.Empty;
                        ret.ErrorCode = 1;
                        return ret;
                    }
                    bool result = int.TryParse(変換作成年, out i) && int.TryParse(変換作成月, out i);
                    if (result == false)
                    {
                        string 変換月 = 変換作成月.Substring(0, 1);
                        string 変換月1 = 変換作成月.Substring(1, 1);
                        if (変換月 == "/")
                        {
                            if (変換月1 != "/" && 変換月1 != "0")
                            {
                                string 変換作成年月 = 変換作成年 + "/0" + 変換作成月.Substring(1, 1);
                                ret.DateFrom = 変換作成年月;  //XAML表示用
                                ret.DateYear = 変換作成年;
                                ret.DateMonth = 変換作成月;
                                ret.Result = true;
                                return ret;
                            }
                            else
                            {
                                ret.DateFrom = string.Empty;
                                ret.ErrorCode = 1;
                                return ret;
                            }
                        }
                        else
                        {
                            ret.DateFrom = string.Empty;
                            ret.ErrorCode = 1;
                            return ret;
                        }
                    }
                    int? i変換作成月 = AppCommon.IntParse(変換作成月);
                    if (i変換作成月 <= 12 && i変換作成月 != 00 && i変換作成月 > 0)
                    {
                        string 変換作成年月 = 変換作成年 + "/" + 変換作成月;
                        ret.DateFrom = 変換作成年月;  //XAML表示用
                        ret.DateYear = 変換作成年;
                        ret.DateMonth = 変換作成月;
                        ret.Result = true;
                        return ret;
                    }
                    else
                    {
                        ret.DateFrom = string.Empty;
                        ret.ErrorCode = 1;
                        return ret;
                    }
                }

                #endregion

                #region 文字列の長さが7文字

                //文字列の長さが7文字の時
                if (Days.Length == 7)
                {
                    string 変換作成年 = Days.Substring(0, 4);
                    string 変換作成月 = Days.Substring(5, 2);
                    if (変換作成月.Contains("+") || 変換作成月.Contains("-") == true || 変換作成年.Contains("+") || 変換作成年.Contains("-") == true)
                    {
                        ret.DateFrom = string.Empty;
                        ret.ErrorCode = 1;
                        return ret;
                    }
                    int i = 0;
                    bool result = int.TryParse(変換作成年, out i) && int.TryParse(変換作成月, out i);
                    if (result == false)
                    {
                        ret.DateFrom = string.Empty;
                        ret.ErrorCode = 1;
                        return ret;
                    }
                    int i変換作成月 = AppCommon.IntParse(変換作成月);
                    if (i変換作成月 <= 12 && i変換作成月 != 00 && i変換作成月 > 0)
                    {
                        if (Days.Substring(4, 1) == "/")
                        {
                            ret.DateYear = 変換作成年;
                            ret.DateMonth = 変換作成月;
                            ret.DateFrom = Days;
                            ret.Result = true;
                            return ret;
                        }
                        else
                        {
                            ret.DateFrom = string.Empty;
                            ret.ErrorCode = 1;
                            return ret;
                        }
                    }
                    else
                    {
                        ret.DateFrom = string.Empty;
                        ret.ErrorCode = 1;
                        return ret;
                    }
                }

                #endregion

            }
            else
            {
                ret.DateFrom = string.Empty;
                ret.ErrorCode = 1;
                return ret;
            }
            return ret;
        }

        #endregion

        #region CSV取り込み
        /// <summary> 遠山作成
        /// CSV取り込み Datetime型とInt型の変換チェック
        /// </summary>
        public static CsvData GetCsvData(string Days, string no)
        {
            var ret = new CsvData();
            ret.Result = false;
            if (Days == null)
            {
                int i;
                //Int型に変更出来たらTrueを返す
                if (int.TryParse(no, out i))
                {
                    ret.Result = true;
                }
            }
            else
            {
                DateTime day;
                //Datetime型に変更出来たらTrueを返す
                if (DateTime.TryParse(Days, out day))
                {
                    ret.Result = true;
                }
            }
            return ret;
        }
        #endregion

        #region コード検索の数値チェック

        /// <summary>
        /// コード検索で数字以外の文字が入っていないかチェック 遠山作成
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static DataFromTo GetDataFromTo(string From, string To)
        {
            var ret = new DataFromTo();
            ret.Result = true;

            int iFrom, iTo;
            if (!(int.TryParse(From, out iFrom)))
            {
                if (!string.IsNullOrEmpty(From))
                {
                    ret.Result = false;
                }
            }

            if (!(int.TryParse(To, out iTo)))
            {
                if (!string.IsNullOrEmpty(To))
                {
                    ret.Result = false;
                }
            }

            if (From.Contains("+") || From.Contains("-") == true || To.Contains("+") || To.Contains("-") == true)
            {
                ret.Result = false;
            }

            return ret;
        }

        #endregion

        #region アプリケーション関連

        /// <summary>
        /// アプリケーション固有の画面間連携オブジェクトを基底クラスの連携オブジェクトにリンクする
        /// </summary>
        /// <param name="view"></param>
        /// <param name="appdata"></param>
        public static void SetAppCommonData(Window view, AppCommonData appdata)
        {
            ViewsCommon viewcom = GetViewsCommonData(view);
            if (viewcom != null)
            {
                viewcom.AppData = appdata;
            }
        }

        /// <summary>
        /// アプリケーション固有の画面間連携オブジェクトを取得する
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static AppCommonData GetAppCommonData(Window view)
        {
            ViewsCommon viewcom = GetViewsCommonData(view);
            AppCommonData appdata = null;
            if (viewcom != null)
            {
                appdata = viewcom.AppData as AppCommonData;
            }
            return appdata;
        }

        /// <summary>
        /// ログインした際に取得するユーザ設定項目を画面間連携オブジェクトにセットする
        /// </summary>
        /// <param name="view"></param>
        /// <param name="table"></param>
        public static void SetupConfig(Window view, UserConfig cfg)
        {
            AppCommonData appdata = GetAppCommonData(view);
            if (appdata == null)
            {
                throw new Exception("Application data is not ready.");
            }
            appdata.UserConfig = cfg;
        }

        /// <summary>
        /// ユーザ設定項目を取得する
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static UserConfig GetConfig(Window view)
        {
            AppCommonData appdata = GetAppCommonData(view);
            if (appdata == null)
            {
                throw new Exception("Application data is not ready.");
            }
            return appdata.UserConfig;
        }

        #endregion

        #region コンボデータ設定
        public static List<CodeData> GetComboboxCodeList(Window view, string 分類, string 機能, string カテゴリ, bool defaultvalue = false)
        {
            AppCommonData appdata = GetAppCommonData(view);
            if (appdata == null)
            {
                throw new Exception("Application data is not ready.");
            }
            List<CodeData> list = new List<CodeData>();
            if (defaultvalue)
            {
                list.Add(
                        new CodeData()
                        {
                            namearea = 分類,
                            category = 機能,
                            function = カテゴリ,
                            コード = -1,
                            表示順 = -1,
                            表示名 = string.Empty,
                        }
                    );
            }
            foreach (DataRow row in appdata.codedatacollection.Select(string.Format("分類 = '{0}' and 機能 = '{1}' and カテゴリ = '{2}'", 分類, 機能, カテゴリ), "表示順 ASC, コード ASC"))
            {
                list.Add(
                        new CodeData()
                        {
                            namearea = (string)row["分類"],
                            category = (string)row["機能"],
                            function = (string)row["カテゴリ"],
                            コード = (int)row["コード"],
                            表示順 = (int)row["表示順"],
                            表示名 = (string)row["表示名"],
                        }
                    );
            }
            return list;
        }

        public static void SetutpComboboxList(UcLabelComboBox uccombobox, bool defaultvalue = false)
        {
            string[] listparams = uccombobox.ComboListingParams.Split(new char[] { ',' });
            if (listparams.Length != 3)
            {
                return;
            }
            Window view = System.Windows.Window.GetWindow(uccombobox);
            uccombobox.Combo_DisplayMemberPath = "表示名";
            uccombobox.Combo_SelectedValuePath = "コード";
            uccombobox.ComboboxItems = AppCommon.GetComboboxCodeList(view, listparams[0], listparams[1], listparams[2], defaultvalue);
        }

        public static void SetutpComboboxListToCell(GcSpreadGrid spgrid, string columnname, string 分類, string 機能, string カテゴリ, bool defaultvalue = false)
        {
            foreach (var col in spgrid.Columns)
            {
                if (col.Name == columnname)
                {
                    Window view = System.Windows.Window.GetWindow(spgrid);
                    ComboBoxCellType c1 = new ComboBoxCellType();
                    c1.ItemsSource = AppCommon.GetComboboxCodeList(view, 分類, 機能, カテゴリ, defaultvalue);
                    c1.ContentPath = "表示名";
                    c1.SelectedValuePath = "コード";
                    c1.ValueType = ComboBoxValueType.SelectedValue;
                    if (spgrid.Columns[col.Name].Locked == true || spgrid.Columns[col.Name].Focusable != true)
                    {
                        c1.DropDownButtonVisibility = CellButtonVisibility.NotShow;
                    }
                    spgrid.Columns[col.Name].CellType = c1;
                    break;
                }
            }
        }

        #endregion

        #region 業務系メソッド

        /// <summary>
        /// 取得した管理情報からSPREADのヘッダの割増料金名を置き換える
        /// </summary>
        /// <param name="spgrid"></param>
        /// <param name="tbl"></param>
        public static void SetupSpreadHeaderWarimasiName(GcSpreadGrid spgrid, DataTable tbl)
        {
            string header1 = GetWarimasiName1(tbl);
            string header2 = GetWarimasiName2(tbl);
            if (spgrid.Columns["請求割増１"] != null)
            {
                spgrid.ColumnHeader.Rows[0].Cells[spgrid.Columns["請求割増１"].Index].Text = header1;
            }
            if (spgrid.Columns["請求割増２"] != null)
            {
                spgrid.ColumnHeader.Rows[0].Cells[spgrid.Columns["請求割増２"].Index].Text = header2;
            }
        }

        /// <summary>
        /// 取得した管理情報から割増名１を取得
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public static string GetWarimasiName1(DataTable tbl)
        {
            string header1 = "請求割増１";
            if (tbl != null)
            {
                if (tbl.Rows.Count > 0)
                {
                    // SPREADのヘッダを更新
                    string wk = (string)tbl.Rows[0]["割増料金名１"];
                    if (string.IsNullOrWhiteSpace(wk) != true)
                    {
                        header1 = wk;
                    }
                }
            }
            return header1;
        }

        /// <summary>
        /// 取得した管理情報から割増名１を取得
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public static string GetWarimasiName2(DataTable tbl)
        {
            string header2 = "請求割増２";
            if (tbl != null)
            {
                if (tbl.Rows.Count > 0)
                {
                    // SPREADのヘッダを更新
                    string wk = (string)tbl.Rows[0]["割増料金名２"];
                    if (string.IsNullOrWhiteSpace(wk) != true)
                    {
                        header2 = wk;
                    }
                }
            }
            return header2;
        }

        #endregion

        #region コンフィグ関連

        public static byte[] SaveSpConfig(GcSpreadGrid spgrid)
        {
            try
            {
                byte[] barray;
                using (var strm = new System.IO.MemoryStream())
                {
                    spgrid.Save(strm);
                    barray = strm.ToArray();
                    strm.Close();
                }
                return barray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LoadSpConfig(GcSpreadGrid spgrid
                                        , byte[] barray
                                        , string bname = null
                                        , BindingMode bmode = BindingMode.TwoWay
                                        , bool notifyforSource = true
                                        , bool notifyforTarget = true
                                        , UpdateSourceTrigger updateTrriger = UpdateSourceTrigger.PropertyChanged)
        {
            try
            {
                using (var strm = new System.IO.MemoryStream(barray))
                {
                    spgrid.Open(strm);
                    strm.Close();
                }
                if (string.IsNullOrWhiteSpace(bname) != true)
                {
                    SetSpreadBind(spgrid, GcSpreadGrid.ItemsSourceProperty, bname);
                }
                spgrid.Rows.Clear();
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region スプレッドシート操作

        public static void SetSpreadBind(GcSpreadGrid spgrid
                                        , DependencyProperty poperty
                                        , string bname
                                        , BindingMode bmode = BindingMode.TwoWay
                                        , bool notifyforSource = true
                                        , bool notifyforTarget = true
                                        , UpdateSourceTrigger updateTrriger = UpdateSourceTrigger.PropertyChanged)
        {
            Binding bind = new Binding(bname)
            {
                Mode = bmode,
                NotifyOnSourceUpdated = notifyforSource,
                NotifyOnTargetUpdated = notifyforTarget,
                UpdateSourceTrigger = updateTrriger,
            };
            spgrid.SetBinding(poperty, bind);
        }

        public static void SpreadYMDCellCheck(object sender, SpreadCellEditEndedEventArgs e, string _originalText)
        {
            var grid = sender as GcSpreadGrid;
            if (grid == null) return;
            if (e.EditAction == SpreadEditAction.Cancel) return;
            if (grid.RowCount == 0) return;
            //if (e.CellPosition.ColumnName.Contains("年月日") != true) return;

            var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
            if (string.IsNullOrWhiteSpace(text) == true) return;

            if (text.Contains("/") != true)
            {
                // 区切り文字を含まず数字のみが入力された場合
                if (text.Length > 8 || text.Length == 0)
                {
                    return;
                }
                if (text.Length < 8)
                {
                    if (text.Length == 1 || text.Length == 3)
                    {
                        text = "0" + text;
                    }
                    // 8桁に満たない部分(左側)を補完する
                    if (string.IsNullOrWhiteSpace(_originalText))
                    {
                        // 当日の日付で補完
                        string today = DateTime.Today.ToString("yyyyMMdd");
                        text = today.Substring(0, 8 - text.Length) + text;
                    }
                    else
                    {
                        // 既に入力されていた内容で補完
                        string work = _originalText.Replace("/", "");
                        if (work.Length == 8)
                        {
                            text = work.Substring(0, 8 - text.Length) + text;
                        }
                    }
                }
                text = text.Insert(6, "/").Insert(4, "/");
                grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text = text;
            }

            DateTime dtM = new DateTime();
            if (DateTime.TryParse(text, out dtM) == true)
            {
                grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text = dtM.ToString("yyyy/MM/dd");
            }
            else
            {
                grid.Cells[e.CellPosition.Row, e.CellPosition.Column].ValidationErrors.Add(new SpreadValidationError("正しい日付ではありません", null));
            }
        }

        public static void SpreadYMDCellChecks(object sender, SpreadCellEditEndedEventArgs e, string _originalText)
        {
            var grid = sender as GcSpreadGrid;
            if (grid == null) return;
            if (e.EditAction == SpreadEditAction.Cancel) return;
            if (grid.RowCount == 0) return;
            if (e.CellPosition.ColumnName.Contains("手形") != true && e.CellPosition.ColumnName.Contains("str経費発生日") != true) return;// {}
            //else if (e.CellPosition.ColumnName.Contains("str経費発生日") != true){}
            //else {return;}

            var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
            if (string.IsNullOrWhiteSpace(text) == true) return;

            if (text.Contains("/") != true)
            {
                // 区切り文字を含まず数字のみが入力された場合
                if (text.Length > 8 || text.Length == 0)
                {
                    return;
                }
                if (text.Length < 8)
                {
                    if (text.Length == 1 || text.Length == 3)
                    {
                        text = "0" + text;
                    }
                    // 8桁に満たない部分(左側)を補完する
                    if (string.IsNullOrWhiteSpace(_originalText))
                    {
                        // 当日の日付で補完
                        string today = DateTime.Today.ToString("yyyyMMdd");
                        text = today.Substring(0, 8 - text.Length) + text;
                    }
                    else
                    {
                        // 既に入力されていた内容で補完
                        string work = _originalText.Replace("/", "");
                        if (work.Length == 8)
                        {
                            text = work.Substring(0, 8 - text.Length) + text;
                        }
                    }
                }
                text = text.Insert(6, "/").Insert(4, "/");
                grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text = text;
            }

            DateTime dtM = new DateTime();
            if (DateTime.TryParse(text, out dtM) == true)
            {
                grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text = dtM.ToString("yyyy/MM/dd");
            }
            else
            {
                grid.Cells[e.CellPosition.Row, e.CellPosition.Column].ValidationErrors.Add(new SpreadValidationError("正しい日付ではありません", null));
            }
        }

        /// <summary>
        /// 編集中のCellの値を確定させる
        /// </summary>
        /// <param name="spread"></param>
        public static void FixSpreadActiveCell(GcSpreadGrid spread)
        {
            if (spread.ActiveCell != null && spread.ActiveCell.IsEditing)
            {
                if (spread.EditElement is TextBox)
                {
                    spread.ActiveRow.Cells[spread.ActiveCellPosition.Column].Value = (spread.EditElement as TextBox).Text;
                }
                else if (spread.EditElement is GrapeCity.Windows.SpreadGrid.Editors.TextEditElement)
                {
                    spread.ActiveRow.Cells[spread.ActiveCellPosition.Column].Text = (spread.EditElement as GrapeCity.Windows.SpreadGrid.Editors.TextEditElement).Text;
                }
                else if (spread.EditElement is GrapeCity.Windows.SpreadGrid.Editors.ComboBoxEditElement)
                {
                    var val = (spread.EditElement as GrapeCity.Windows.SpreadGrid.Editors.ComboBoxEditElement).SelectedValue;
                    if (val != null)
                    {
                        spread.ActiveRow.Cells[spread.ActiveCellPosition.Column].Value = val;
                    }
                }
            }
        }

        #endregion

        #region Table to List
        /// <summary>
        /// DataTableからList型または配列型に変換する
        /// </summary>
        /// <param name="ptype"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ConvertFromDataTable(Type ptype, object param)
        {
            try
            {
                if (param == null)
                {
                    return param;
                }

                if ((param is DataRow) != true && (param is DataTable) != true)
                {
                    return param;
                }

                if (ptype.IsArray)
                {
                    // 単純配列型（XXX[]）の場合
                    List<object> data = new List<object>();
                    if (param is DataRow)
                    {
                        if ((param as DataRow).RowState != DataRowState.Deleted)
                        {
                            data.Add(SetValues(ptype, param as DataRow));
                        }
                    }
                    else if (param is DataTable)
                    {
                        foreach (DataRow row in (param as DataTable).Rows)
                        {
                            if (row.RowState != DataRowState.Deleted)
                            {
                                data.Add(SetValues(ptype.GetProperty("Item").PropertyType, row));
                            }
                        }
                    }
                    return data.ToArray();
                }
                else
                {
                    if (ptype.IsGenericType && typeof(List<>).IsAssignableFrom(ptype.GetGenericTypeDefinition()))
                    {
                        object dat = Activator.CreateInstance(ptype);
                        MethodInfo mAdd = dat.GetType().GetMethod("Add");
                        // ジェネリックコレクション List<XXX>の場合
                        if (param is DataRow)
                        {
                            if ((param as DataRow).RowState != DataRowState.Deleted)
                            {
                                mAdd.Invoke(dat, new object[] { SetValues(ptype.GetProperty("Item").PropertyType, param as DataRow) });
                            }
                        }
                        else if (param is DataTable)
                        {
                            foreach (DataRow row in (param as DataTable).Rows)
                            {
                                if (row.RowState != DataRowState.Deleted)
                                {
                                    mAdd.Invoke(dat, new object[] { SetValues(ptype.GetProperty("Item").PropertyType, row) });
                                }
                            }
                        }
                        return dat;
                    }
                    else
                    {
                        if (param is DataRow)
                        {
                            if ((param as DataRow).RowState != DataRowState.Deleted)
                            {
                                return SetValues(ptype, (param as DataRow));
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else if (param is DataTable)
                        {
                            if ((param as DataTable).Rows.Count > 0)
                            {
                                if ((param as DataTable).Rows[0].RowState != DataRowState.Deleted)
                                {
                                    // 配列ではないので、Rowsの1件目のみを非コレクション型として渡す
                                    return SetValues(ptype, (param as DataTable).Rows[0]);
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return param;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 
        private static object SetValues(Type arraytp, DataRow row)
        {
            object item = Activator.CreateInstance(arraytp);
            //メンバを取得する
            try
            {
                PropertyInfo[] props = arraytp.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo pi in props)
                {
                    Type prptype;
                    object col;
                    prptype = pi.PropertyType;
                    if (row.Table.Columns.Contains(pi.Name))
                    {
                        if (row.IsNull(pi.Name))
                        {
                            col = null;
                        }
                        else
                        {
                            col = row[pi.Name];
                        }
                        pi.SetValue(item, col);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

        public static void SetupColumnsByProperties(Type tp, DataTable tbl)
        {
            //メンバを取得する
            PropertyInfo[] props = tp.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pi in props)
            {
                Type t = pi.PropertyType;
                bool isnullable = false;
                if (t.IsGenericType)
                {
                    isnullable = t.GetGenericTypeDefinition() == typeof(Nullable<>);
                    if (isnullable)
                    {
                        t = Nullable.GetUnderlyingType(t);
                    }
                }
                DataColumn col = new DataColumn(pi.Name, t);
                col.AllowDBNull = true;
                tbl.Columns.Add(col);
            }
        }
        #endregion

        #region object to Table
        public static bool ConvertToDataTable(object data, DataTable tbl)
        {
            try
            {
                BindingFlags bindf = BindingFlags.Public | BindingFlags.Instance;
                Type tp = data.GetType();
                if (tp.IsArray)
                {
                    // 単純配列型（XXX[]）の場合
                    AppCommon.SetupColumnsByProperties(tp.GetElementType(), tbl);
                    PropertyInfo arrayp = tp.GetProperty("Item");
                    var ary = (object[])data;
                    for (int ix = 0; ix < ary.Length; ix++)
                    {
                        GetValues(ary[ix].GetType().GetProperties(bindf), ary[ix], tbl);
                    }
                }
                else
                {
                    if (tp.IsGenericType && typeof(List<>).IsAssignableFrom(tp.GetGenericTypeDefinition()))
                    {
                        Type[] tpp = data.GetType().GetGenericArguments();
                        AppCommon.SetupColumnsByProperties(tpp[0], tbl);

                        // ジェネリックコレクション List<XXX>の場合
                        Type ts = tp.GetProperty("Item").PropertyType;
                        PropertyInfo arrayc = tp.GetProperty("Count");
                        int cnt = (int)arrayc.GetValue(data, null);
                        for (int ix = 0; ix < cnt; ix++)
                        {
                            PropertyInfo arrayp = tp.GetProperty("Item");
                            var ary = arrayp.GetValue(data, new object[] { ix });
                            GetValues(ary.GetType().GetProperties(bindf), ary, tbl);
                        }
                    }
                    else
                    {
                        // ジェネリックコレクション List<XXX>ではない場合
                        AppCommon.SetupColumnsByProperties(tp, tbl);
                        GetValues(tp.GetProperties(bindf), data, tbl);
                    }
                }

                return true;
            }
            //catch (Exception ex)
            catch
            {
                return false;
            }
        }
        #endregion

        #region SpreadData to Table
        public static bool ConvertSpreadDataToTable<T>(GcSpreadGrid grid, DataTable tbl, Dictionary<string, string> changecols)
        {
            try
            {
                // テーブルに列名をセットアップする
                AppCommon.SetupColumnsByProperties(typeof(T), tbl);

                //// "d"のついた項目のリストを初期化する
                //var deccols = (from r in grid.Columns where string.IsNullOrWhiteSpace(r.Name) != true && r.Name.StartsWith("d") == true select r.Name.Remove(0, 1)).ToArray();
                foreach (var row in grid.Rows)
                {
                    if (!row.IsVisible)
                    {
                        continue;
                    }
                    DataRow data = tbl.NewRow();
                    var cells = row.Cells;
                    foreach (var col in (from r in grid.Columns where string.IsNullOrWhiteSpace(r.Name) != true select r).ToArray())
                    {
                        if (changecols.Values.Contains(col.Name))
                        {
                            continue;
                        }
                        object val = row.Cells[col.Index].Value;
                        if (val == null)
                        {
                            data[col.Name] = DBNull.Value;
                            if (changecols.Keys.Contains(col.Name))
                            {
                                data[changecols[col.Name]] = DBNull.Value;
                            }
                        }
                        else
                        {
                            data[col.Name] = val;
                            if (changecols.Keys.Contains(col.Name))
                            {
                                if (val is string)
                                {
                                    if (string.IsNullOrWhiteSpace(val as string))
                                    {
                                        data[changecols[col.Name]] = DBNull.Value;
                                        continue;
                                    }
                                }
                                data[changecols[col.Name]] = val;
                            }
                        }
                    }

                    tbl.Rows.Add(data);
                }
                tbl.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 
        private static void GetValues(PropertyInfo[] props, object data, DataTable tbl)
        {
            DataRow row = tbl.NewRow();
            //メンバを取得する
            foreach (PropertyInfo pi in props)
            {
                var col = pi.GetValue(data);
                if (col == null)
                {
                    row[pi.Name] = DBNull.Value;
                }
                else
                {
                    row[pi.Name] = pi.GetValue(data);
                }
            }
            tbl.Rows.Add(row);
            row.AcceptChanges();
        }

        public static Dictionary<string, ReportDesignParameter> GetBillreportConfigList()
        {
            Dictionary<string, ReportDesignParameter> result = new Dictionary<string, ReportDesignParameter>();

            var plist = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("reportDesignerParameterSettings");
            foreach (string key in plist)
            {
                var param = new ReportDesignParameter();
                string[] value = plist[key].Split(',');
                if (value.Length == 4)
                {
                    double.TryParse(value[0], out param.marginLeft);
                    double.TryParse(value[1], out param.marginTop);
                    int.TryParse(value[2], out param.PageRow1);
                    int.TryParse(value[3], out param.PageRow2);
                }
                result.Add(key, param);
            }
            return result;
        }

        public static ReportDesignParameter GetBillreportConfig(string key)
        {
            return (from p in GetBillreportConfigList() where p.Key == key select p.Value).FirstOrDefault();
        }

        public static string GetLocalConfigFilePath()
        {
            var dir = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var exeasm = System.Reflection.Assembly.GetEntryAssembly();
            var assemblyTitle = exeasm.GetCustomAttributes(typeof(AssemblyTitleAttribute), true).Single() as AssemblyTitleAttribute;
            FileInfo fi = new System.IO.FileInfo(dir + @"\KyoeiSystem\" + assemblyTitle.Title + @"\local.config");

            return fi.FullName;
        }
        #endregion

        #region sql関連
        public static bool SaveSqlConnectString(string server, string dbname, string username, string passwd)
        {
            bool result = false;
            try
            {
                FileInfo fi = new System.IO.FileInfo(GetLocalConfigFilePath());
                if (fi.Exists != true)
                {
                    if (fi.Directory.Exists != true)
                    {
                        fi.Directory.Create();
                    }
                }
                SqlConnectionStringBuilder sqlconn = ViewBaseCommon.MakeConnectString(server, dbname, username, passwd);
                ConfigXmlDocument cdoc = new ConfigXmlDocument();
                var cfg = cdoc.CreateElement("configuration");
                var apps = cdoc.CreateElement("settings");
                var node = cdoc.CreateElement("add");
                var attrK = cdoc.CreateAttribute("key");
                attrK.InnerText = "connectionString";
                var attrV = cdoc.CreateAttribute("value");
                attrV.InnerText = Utility.Encrypt(sqlconn.ToString());
                node.Attributes.Append(attrK);
                node.Attributes.Append(attrV);
                cdoc.AppendChild(cfg);
                cfg.AppendChild(apps);
                apps.AppendChild(node);
                cdoc.Save(fi.FullName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static SqlConnectionStringBuilder GetLocalPCConnStr()
        {
            SqlConnectionStringBuilder sqlBuilder = null;

            try
            {
                FileInfo fi = new FileInfo(GetLocalConfigFilePath());
                if (fi.Exists != true)
                {
                    // ローカルPC用設定ファイルが存在しない場合は新規作成の手順へ
                    return null;
                }
                ConfigXmlDocument cdoc = new ConfigXmlDocument();
                cdoc.Load(fi.FullName);
                var nodes = cdoc.SelectNodes("/configuration/settings/add");
                foreach (System.Xml.XmlNode node in nodes)
                {
                    var attrK = node.Attributes["key"];
                    if ((attrK != null ? attrK.InnerText : string.Empty) == "connectionString")
                    {
                        var attrV = node.Attributes["value"];
                        if (attrV != null)
                        {
                            sqlBuilder = new SqlConnectionStringBuilder(Utility.Decrypt(attrV.InnerText));
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return sqlBuilder;
        }

        public static SqlConnectionStringBuilder MakeSqlConnectString()
        {
            var plist = (NameValueCollection)ConfigurationManager.GetSection("serviceSettings");
            if (plist != null)
            {
                if (plist["mode"] != CommonConst.WithoutLicenceDBMode)
                {
                    return null;
                }
            }

            SqlConnectionStringBuilder sqlBuilder = GetLocalPCConnStr();

            return sqlBuilder;
        }

        #endregion

        #region value converter
        public static int IntParse(string str, int defval = 0)
        {
            int work = defval;
            return (int.TryParse(str, out work) ? work : defval);
        }

        public static int IntParse(string str, System.Globalization.NumberStyles numstyle, int defval = 0)
        {
            int work = defval;
            return (int.TryParse(str, numstyle, System.Globalization.NumberFormatInfo.CurrentInfo, out work) ? work : defval);
        }

        public static decimal DecimalParse(string str, decimal defval = 0m)
        {
            decimal work = defval;
            return (decimal.TryParse(str, out work) ? work : defval);
        }

        public static DateTime DatetimeParse(string str, DateTime defval)
        {
            DateTime work = defval;
            return (DateTime.TryParse(str, out work) ? work : defval);
        }

        /// <summary>
        /// 日付型(時刻含)から時刻を除いた結果を返す
        /// </summary>
        /// <param name="date"></param>
        /// <param name="defDate"></param>
        /// <returns></returns>
        public static DateTime DateTimeToDate(DateTime? date, DateTime defDate)
        {
            if (date == null)
                return DateTime.Parse(defDate.ToString("yyyy/MM/dd"));

            DateTime dt;
            return DateTime.TryParse(((DateTime)date).ToString("yyyy/MM/dd"), out dt) ?
                        dt : DateTime.Parse(defDate.ToString("yyyy/MM/dd"));

        }

        /// <summary>
        /// 日付型(時刻含)から時刻を除いた結果を返す
        /// </summary>
        /// <param name="date"></param>
        /// <param name="defDate"></param>
        /// <returns></returns>
        public static DateTime? DateTimeToDate(DateTime? date, DateTime? defDate = null)
        {
            DateTime? defDt = null;
            if (defDate != null)
                defDt = DateTime.Parse(((DateTime)defDate).ToString("yyyy/MM/dd"));

            if (date == null && defDate != null)
                return defDt;

            DateTime dt;
            return DateTime.TryParse(((DateTime)date).ToString("yyyy/MM/dd"), out dt) ?
                        dt : defDt;

        }

        #endregion

        #region 画面呼び出し
        ///// <summary>
        ///// 通常の画面呼び出し
        ///// </summary>
        ///// <param name="cname"></param>
        ///// <param name="plist"></param>
        ///// <returns></returns>
        public static object Start(string cname, Dictionary<string, object> plist = null)
        {
            var wnd = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            AppCommonData appdata = null;
            if (wnd is RibbonWindowViewBase)
            {
                appdata = (wnd as RibbonWindowViewBase).viewsCommData.AppData as AppCommonData;
            }
            else if (wnd is WindowViewBase)
            {
                appdata = (wnd as WindowViewBase).viewsCommData.AppData as AppCommonData;
            }
            if (appdata != null)
            {
                if (appdata._CtlWindow != null)
                    return appdata._CtlWindow.Start(cname, plist);
            }
            return null;
        }
        public static object Start(Type ctype, Dictionary<string, object> plist = null)
        {
            var wnd = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            AppCommonData appdata = null;
            if (wnd is RibbonWindowViewBase)
            {
                appdata = (wnd as RibbonWindowViewBase).viewsCommData.AppData as AppCommonData;
            }
            else if (wnd is WindowViewBase)
            {
                appdata = (wnd as WindowViewBase).viewsCommData.AppData as AppCommonData;
            }
            if (appdata != null)
            {
                if (appdata._CtlWindow != null)
                    return appdata._CtlWindow.Start(ctype.Name, plist);
            }
            return null;
        }

        ///// <summary>
        ///// マスターメンテナンス画面の呼び出しを行う
        ///// </summary>
        ///// <param name="mntwndTypes">マスター種別の一覧</param>
        //public static void CallMasterMainte(Dictionary<string, List<Type>> mntwndTypes)
        //{
        //    UcLabelTwinTextBox twintextbox = ViewBaseCommon.GetCurrentTwinText();
        //    if (twintextbox == null)
        //    {
        //        return;
        //    }
        //    if (mntwndTypes == null)
        //    {
        //        return;
        //    }
        //    try
        //    {
        //        var wndtp = mntwndTypes.Where(x => x.Key == twintextbox.DataAccessName).FirstOrDefault();
        //        if ((wndtp.Value is List<Type>) != true)
        //        {
        //            return;
        //        }
        //        Type tp = (wndtp.Value as List<Type>)[0];
        //        if (tp == null)
        //        {
        //            return;
        //        }

        //        var wnd = Window.GetWindow(twintextbox);
        //        if (wnd is RibbonWindowViewBase)
        //        {
        //            CallMasterMainte(tp.Name, wnd as RibbonWindowViewBase);
        //        }
        //        else
        //        {
        //            CallMasterMainte(tp.Name, wnd as WindowViewBase);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //    }
        //}

        ///// <summary>
        ///// 画面クラス名で指定されたマスターメンテナンス画面の呼び出しを行う（WindowViewBaseの画面用）
        ///// </summary>
        ///// <param name="cname"></param>
        ///// <param name="wnd"></param>
        ///// <returns></returns>
        //public static object CallMasterMainte(string cname, WindowViewBase wnd)
        //{
        //	AppCommonData appdata = wnd.viewsCommData.AppData as AppCommonData;
        //	if (appdata != null)
        //	{
        //		if (appdata._CtlWindow != null)
        //			return appdata._CtlWindow.Start(cname);
        //	}
        //	return null;
        //}

        ///// <summary>
        ///// 画面クラス名で指定されたマスターメンテナンス画面の呼び出しを行う（RibbonWindowViewBaseの画面用）
        ///// </summary>
        ///// <param name="cname"></param>
        ///// <param name="wnd"></param>
        ///// <returns></returns>
        //public static object CallMasterMainte(string cname, RibbonWindowViewBase wnd)
        //{
        //	AppCommonData appdata = wnd.viewsCommData.AppData as AppCommonData;
        //	if (appdata != null)
        //	{
        //		if (appdata._CtlWindow != null)
        //			return appdata._CtlWindow.Start(cname);
        //	}
        //	return null;
        //}
        #endregion

        //試験
        #region "DoEvents"
        public static void DoEvents()
        {
            System.Windows.Threading.DispatcherFrame frame = new System.Windows.Threading.DispatcherFrame();
            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new System.Windows.Threading.DispatcherOperationCallback(ExitFrames), frame);
            System.Windows.Threading.Dispatcher.PushFrame(frame);
        }

        public static object ExitFrames(object f)
        {
            ((System.Windows.Threading.DispatcherFrame)f).Continue = false;

            return null;
        }
        #endregion

        #region 日付取得関連

        /// <summary>
        /// 最大日付(時刻除く)を取得する
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMaxDate()
        {
            return DateTime.Parse(DateTime.MaxValue.ToString("yyyy/MM/dd"));

        }

        /// <summary>
        /// システム日付の初日を取得する
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateFirst()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        }

        /// <summary>
        /// システム日付の末日を取得する
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateLast()
        {
            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, days);

        }

        /// <summary>
        /// 指定年月の末日を取得する
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime GetDateLast(int year, int month)
        {
            int days = DateTime.DaysInMonth(year, month);
            return new DateTime(year, month, days);

        }

        #endregion

    }

    #endregion

    #region ReportDesignParameter
    public class ReportDesignParameter
    {
        public double marginLeft = 0;
        public double marginTop = 0;
        public int PageRow1 = 0;
        public int PageRow2 = 0;
    }
    #endregion

    #region 権限周り
    /// <summary>
    /// 権限マスタより
    /// 使用可能FLG　　(Authority_Disp_Close)
    /// データ更新FLG　(Authority_Update_Button)
    /// を返却
    /// </summary>
    public class 権限Get
    {
        /// <summary>
        /// 権限マスタより画面が表示可能か
        /// </summary>
        /// <param name="AuthorityData"></param>
        /// <param name="画面ID"></param>
        /// <returns>表示可能=True、表示不可能=False</returns>
        public static Boolean Authority_Disp_Close(CommonConfig AuthorityData, string 画面ID)
        {
            Boolean RetVal = true;
            try
            {
                string[] プログラムID = AuthorityData.プログラムID.ToArray();
                foreach (var ID in AuthorityData.プログラムID)
                {
                    if (ID == 画面ID)
                    {
                        RetVal = AuthorityData.使用可能FLG[Array.IndexOf(プログラムID, 画面ID)];
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return RetVal;
        }

        /// <summary>
        /// 権限マスタより登録ボタンが表示可能か
        /// </summary>
        /// <param name="AuthorityData"></param>
        /// <param name="画面ID"></param>
        /// <returns>表示可能=True、表示不可能=False</returns>
        public static Boolean Authority_Update_Button(CommonConfig AuthorityData, string 画面ID)
        {
            Boolean RetVal = true;
            try
            {
                string[] プログラムID = AuthorityData.プログラムID.ToArray();
                foreach (var ID in AuthorityData.プログラムID)
                {
                    if (ID == 画面ID)
                    {
                        RetVal = AuthorityData.データ更新FLG[Array.IndexOf(プログラムID, 画面ID)];
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return RetVal;
        }

        /// <summary>
        /// マスターメンテナンス画面の呼び出しを行う
        /// </summary>
        /// <param name="mntwndTypes">マスター種別の一覧</param>
        public static void CallMasterMainte(Dictionary<string, List<Type>> mntwndTypes, CommonConfig ccfg = null)
        {
            UcLabelTwinTextBox twintextbox = ViewBaseCommon.GetCurrentTwinText();
            if (twintextbox == null)
            {
                return;
            }
            if (mntwndTypes == null)
            {
                return;
            }
            try
            {
                var wndtp = mntwndTypes.Where(x => x.Key == twintextbox.DataAccessName).FirstOrDefault();
                if ((wndtp.Value is List<Type>) != true)
                {
                    return;
                }
                Type tp = (wndtp.Value as List<Type>)[0];
                if (tp == null)
                {
                    return;
                }
                WindowMasterMainteBase mstmnt = Activator.CreateInstance(tp) as WindowMasterMainteBase;
                var wnd = Window.GetWindow(twintextbox);
                if (ccfg != null)
                {
                    // 該当するプログラムIDが使用可の場合のみ開く
                    if (権限Get.Authority_Disp_Close(ccfg, mstmnt.GetType().Name))
                    {
                        mstmnt.ShowDialog(wnd);
                    }
                }
                else
                {
                    mstmnt.ShowDialog(wnd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "画面オブジェクト取得"
        public class WpfUtil
        {
            /// <summary>
            /// targetの論理ツリー上の子要素全てに対してactionを実行します。
            /// actionはtarget自身にも作用する。再帰処理なのでスタックフレームに注意。
            /// </summary>
            /// <param name="target">ルートとするオブジェクト</param>
            /// <param name="action">実行するメソッドのデリゲート</param>
            public static void OperateLogicalChildren(DependencyObject target, Action<DependencyObject> action)
            {
                action(target);
                foreach (var child in LogicalTreeHelper.GetChildren(target))
                {
                    if (child is DependencyObject)
                    {
                        OperateLogicalChildren((DependencyObject)child, action);
                    }
                }
            }
        }

        /// <summary>
        /// SpreadGridLock
        /// スプレッド内のデータをロックする
        /// </summary>
        /// <param name="target">自分自身</param>
        /// <param name="Protected">ロックする場合=True,ロックを外す場合=False</param>
        public static void SpreadGridLock(DependencyObject target, Boolean Protected)
        {
            WpfUtil.OperateLogicalChildren(target, t =>
            {
                Control cont = t as Control;
                if (cont != null)
                {
                    if (cont.GetType() == typeof(GcSpreadGrid))
                    {
                        GcSpreadGrid Gcsp = (GcSpreadGrid)cont;
                        //Gcsp.IsEnabled = !Protected;
                        Gcsp.Protected = Protected;

                        foreach (var row in Gcsp.Rows)
                        {
                            row.Locked = Protected;
                            for (int Col = 0; Col < row.Cells.Count(); Col++)
                            {
                                //if (row.Cells[Col].InheritedCellType.GetType() == typeof(ButtonCellType))
                                //{
                                //    ButtonCellType btOrg = (ButtonCellType)row.Cells[Col].InheritedCellType;
                                //    ButtonCellType btNew = new ButtonCellType();
                                //    btNew.Command = null;
                                //    btNew.Content = btOrg.Content;
                                //    row.Cells[Col].CellType = btNew;
                                //    row.Cells[Col].Locked = Protected; 
                                //}
                                if (row.Cells[Col].InheritedCellType.GetType() == typeof(ComboBoxCellType))
                                {
                                    ComboBoxCellType btOrg = (ComboBoxCellType)row.Cells[Col].InheritedCellType;
                                    ComboBoxCellType btNew = new ComboBoxCellType();
                                    btNew.ItemsSource = btOrg.ItemsSource;
                                    btNew.ContentPath = btOrg.ContentPath;
                                    btNew.SelectedValuePath = btOrg.SelectedValuePath;
                                    btNew.ValueType = ComboBoxValueType.SelectedValue;
                                    btNew.SpinButtonVisibility = CellButtonVisibility.NotShow;
                                    //btNew.DropDownButtonVisibility = CellButtonVisibility.NotShow;
                                    btNew.DropDownButtonVisibility = btOrg.DropDownButtonVisibility;
                                    btNew.DropDownControlStyle = btOrg.DropDownControlStyle;
                                    btNew.IsSelectable = false;
                                    btNew.AutoOpenDropDown = false;
                                    btNew.AllowDropDownOpen = false;
                                    row.Cells[Col].Locked = Protected;
                                    row.Cells[Col].CellType = btNew;
                                }
                                row.Cells[Col].Locked = Protected;
                            }
                        }
                    }
                }
            }
            );
        }

        /// <summary>
        /// ControlLock
        /// 画面内コントロールを非活性にする
        /// </summary>
        /// <param name="target">自分自身</param>
        /// <param name="DataUpdateVisible">親から呼び出された権限設定情報</param>
        public static void ControlLock(DependencyObject target, Visibility DataUpdateVisible)
        {
            if (DataUpdateVisible != Visibility.Hidden) { return; }

            WpfUtil.OperateLogicalChildren(target, t =>
            {
                Control cont = t as Control;
                if (cont != null)
                {
                    if (cont.GetType() == typeof(TextBox)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(Button)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(CheckBox)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(GcSpreadGrid)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(StackPanel)) { cont.IsEnabled = false; }
                    // カスタムコントロール
                    if (cont.GetType() == typeof(UcCheckBox)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(UcDataGrid)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(UcLabelComboBox)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(UcLabelTextBox)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(UcLabelTextRadioButton)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(UcLabelTwinTextBox)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(UcTextBox)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(UcTreeView)) { cont.IsEnabled = false; }
                    if (cont.GetType() == typeof(UcAutoCompleteTextBox)) { cont.IsEnabled = false; }
                }
            }
            );
        }

        #endregion

    }
    #endregion

}
