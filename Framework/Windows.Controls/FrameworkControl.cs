using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;
using System.Runtime.CompilerServices;
using System.Reflection;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;

namespace KyoeiSystem.Framework.Windows.Controls
{
    /// <summary>
    /// Framework Control クラス
    /// </summary>
    public class FrameworkControl : UserControl, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged メンバー
        /// <summary>
        /// プロパティ変化イベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// プロパティ変化通知
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

        /// <summary>
        /// ウィンドウを閉じようとしているかどうか
        /// </summary>
        public bool IsWindowClosing = false;

        private string _connectStringUserDB = string.Empty;
        /// <summary>
        /// DB接続文字列
        /// </summary>
        public string ConnectStringUserDB
        {
            get { return this._connectStringUserDB; }
            set { this._connectStringUserDB = value; }
        }

        /// <summary>
        /// ログ出力用インスタンス
        /// </summary>
        public AppLogger appLog = AppLogger.Instance;
        /// <summary>
        /// VIEWクラス共通データ
        /// </summary>
        public ViewsCommon viewsCommData = null;
        /// <summary>
        /// 値チェック結果
        /// </summary>
        public string ValidationMessage = string.Empty;

        private bool _isTagetForEnabler = true;
        /// <summary>
        /// 
        /// </summary>
        public bool IsTagetForEnabler
        {
            get { return this._isTagetForEnabler; }
            set
            {
                this._isTagetForEnabler = value;
                NotifyPropertyChanged();
                ChangeIsTagetForEnablerProperty(this, value);
            }
        }

        private bool _isKeyItem = false;
        /// <summary>
        /// キー項目であるかどうかを指定します。
        /// </summary>
        [Category("動作")]
        public bool IsKeyItem
        {
            get
            {
                return this._isKeyItem;
            }
            set
            {
                this._isKeyItem = value;
                NotifyPropertyChanged();
                ChangeIsKeyItemProperty(this, value);
            }
        }

        private bool _isEnabled = true;
        /// <summary>
        /// 使用可能かどうかの状態
        /// </summary>
        [Category("動作")]
        public new bool IsEnabled
        {
            get
            {
                return this._isEnabled;
            }
            set
            {
                this._isEnabled = value;
                this.ChangeIsEnabledAll(this, value);
                NotifyPropertyChanged();
            }
        }

        private bool _isLastField = false;
        /// <summary>
        /// 画面の中で最終フィールドであることを示す
        /// </summary>
        [Category("動作")]
        public bool IsLastField
        {
            get
            {
                return this._isLastField;
            }
            set
            {
                this._isLastField = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// ThreadManagerのインスタンス
        /// </summary>
        public ThreadManeger thmgr = null;
        //public DataAccessConfig Daccfg
        //{
        //	get
        //	{
        //		if (this.thmgr == null)
        //		{
        //			return null;
        //		}
        //		return this.thmgr.daccfg;

        //	}
        //	set
        //	{
        //		if (this.thmgr != null)
        //		{
        //			this.thmgr.daccfg = value;
        //		}
        //	}
        //}


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FrameworkControl()
        {
            //GetConfig();
            //this.Unloaded += FrameworkControl_Unloaded;
        }

        //private void FrameworkControl_Unloaded(object sender, RoutedEventArgs e)
        //{
        //	this.Unloaded -= FrameworkControl_Unloaded;

        //	if (this is UcLabelTwinTextBox || this is UcLabelComboBox)
        //	{
        //		if (this.thmgr != null)
        //		{
        //			thmgr.OnReceived -= new MessageReceiveHandler(OnReceived);
        //			this.thmgr.Dispose();
        //			this.thmgr = null;
        //		}
        //	}
        //	this.OnUnload();
        //}

        //public virtual void OnUnload()
        //{
        //}

        /// <summary>
        /// VIEW共通データを設定する
        /// </summary>
        /// <param name="cfg">VIEW共通データ</param>
        public void SetConfig(ViewsCommon cfg)
        {
            this.viewsCommData = cfg;
            if (this is UcLabelTwinTextBox || this is UcLabelComboBox)
            {
                this.thmgr = new ThreadManeger(cfg.DacConf);
                thmgr.OnReceived += new MessageReceiveHandler(OnReceived);
            }
        }

        /// <summary>
        /// データ受信イベント（データアクセス用）
        /// </summary>
        /// <param name="message">受信メッセージ</param>
        public virtual void OnReceived(CommunicationObject message)
        {
        }


        private void GetConfig()
        {
            //var wnd = Window.GetWindow(this);
            //if (wnd == null)
            //{
            //	return;
            //}
            //foreach (PropertyInfo pi in wnd.GetType().GetProperties())
            //{
            //	if (pi.PropertyType == typeof(ViewsCommon))
            //	{
            //		this.viewsCommData = pi.GetValue(wnd) as ViewsCommon;
            //		break;
            //	}
            //}
            //foreach (FieldInfo fi in wnd.GetType().GetFields())
            //{
            //	if (fi.FieldType == typeof(AppLogger))
            //	{
            //		this.appLog = fi.GetValue(wnd) as AppLogger;
            //		if (this.appLog == null)
            //		{
            //			this.appLog = new AppLogger();
            //		}
            //		break;
            //	}
            //}
        }

        // 内包する子アイテムのIsTagetForEnablerプロパティを揃える
        private void ChangeIsTagetForEnablerProperty(DependencyObject target, bool value)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(target))
            {
                if (child is DependencyObject)
                {
                    if (child is FrameworkControl)
                    {
                        (child as FrameworkControl).IsTagetForEnabler = value;
                        return;
                    }
                    ChangeIsTagetForEnablerProperty((DependencyObject)child, value);
                }
            }
        }

        // 内包する子アイテムのIsKeyItemプロパティを揃える
        private void ChangeIsKeyItemProperty(DependencyObject target, bool value)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(target))
            {
                if (child is DependencyObject)
                {
                    if (child is FrameworkControl)
                    {
                        (child as FrameworkControl).IsKeyItem = value;
                        return;
                    }
                    ChangeIsKeyItemProperty((DependencyObject)child, value);
                }
            }
        }

        private void ChangeIsEnabledAll(DependencyObject target, bool isEnabled)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(target))
            {
                if (child is DependencyObject)
                {
                    if (child is FrameworkControl)
                    {
                        (child as FrameworkControl).IsEnabled = IsEnabled;
                        continue;
                    }
                    if (child is UIElement)
                    {
                        (child as UIElement).IsEnabled = isEnabled;
                    }
                    ChangeIsEnabledAll(child as DependencyObject, isEnabled);
                }
            }
        }

        /// <summary>
        /// 値チェックは各ユーザコントロール側でこのメソッドをoverrideして実装する。
        /// チェックメソッドが必要ないものは単純にtrueを返す。
        /// </summary>
        /// <returns>OK=true、NG=false</returns>
        public virtual bool CheckValidation()
        {
            return true;
        }

        /// <summary>
        /// 値チェック結果のメッセージは各ユーザコントロール側でこのメソッドをoverrideして実装する。
        /// 規定値として空白を返す。
        /// </summary>
        /// <returns></returns>
        public virtual string GetValidationMessage()
        {
            return string.Empty;
        }

        /// <summary>
        /// 指定されたコントロールから指定された型の親コントロールを検索する
        /// </summary>
        /// <typeparam name="T">検索する親コントロールの型</typeparam>
        /// <param name="child">検索開始コントロール</param>
        /// <returns>親コントロール</returns>
        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            // 連続でセル移動したとき、child が null になる場合がある（詳細は未検証）
            if (child == null)
            {
                return null;
            }

            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }

            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindVisualParent<T>(parentObject);
            }
        }


    }
}
