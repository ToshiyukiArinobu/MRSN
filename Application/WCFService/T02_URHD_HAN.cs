//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace KyoeiSystem.Application.WCFService
{
    [DataContract(IsReference = true)]
    public partial class T02_URHD_HAN: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Simple Properties
    
        [DataMember]
        public int 伝票番号
        {
            get { return _伝票番号; }
            set
            {
                if (_伝票番号 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '伝票番号' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _伝票番号 = value;
                    OnPropertyChanged("伝票番号");
                }
            }
        }
        private int _伝票番号;
    
        [DataMember]
        public int 会社名コード
        {
            get { return _会社名コード; }
            set
            {
                if (_会社名コード != value)
                {
                    _会社名コード = value;
                    OnPropertyChanged("会社名コード");
                }
            }
        }
        private int _会社名コード;
    
        [DataMember]
        public int 伝票要否
        {
            get { return _伝票要否; }
            set
            {
                if (_伝票要否 != value)
                {
                    _伝票要否 = value;
                    OnPropertyChanged("伝票要否");
                }
            }
        }
        private int _伝票要否;
    
        [DataMember]
        public System.DateTime 売上日
        {
            get { return _売上日; }
            set
            {
                if (_売上日 != value)
                {
                    _売上日 = value;
                    OnPropertyChanged("売上日");
                }
            }
        }
        private System.DateTime _売上日;
    
        [DataMember]
        public int 売上区分
        {
            get { return _売上区分; }
            set
            {
                if (_売上区分 != value)
                {
                    _売上区分 = value;
                    OnPropertyChanged("売上区分");
                }
            }
        }
        private int _売上区分;
    
        [DataMember]
        public int 販社コード
        {
            get { return _販社コード; }
            set
            {
                if (_販社コード != value)
                {
                    _販社コード = value;
                    OnPropertyChanged("販社コード");
                }
            }
        }
        private int _販社コード;
    
        [DataMember]
        public int 在庫倉庫コード
        {
            get { return _在庫倉庫コード; }
            set
            {
                if (_在庫倉庫コード != value)
                {
                    _在庫倉庫コード = value;
                    OnPropertyChanged("在庫倉庫コード");
                }
            }
        }
        private int _在庫倉庫コード;
    
        [DataMember]
        public Nullable<int> 納品伝票番号
        {
            get { return _納品伝票番号; }
            set
            {
                if (_納品伝票番号 != value)
                {
                    _納品伝票番号 = value;
                    OnPropertyChanged("納品伝票番号");
                }
            }
        }
        private Nullable<int> _納品伝票番号;
    
        [DataMember]
        public System.DateTime 出荷日
        {
            get { return _出荷日; }
            set
            {
                if (_出荷日 != value)
                {
                    _出荷日 = value;
                    OnPropertyChanged("出荷日");
                }
            }
        }
        private System.DateTime _出荷日;
    
        [DataMember]
        public Nullable<int> 受注番号
        {
            get { return _受注番号; }
            set
            {
                if (_受注番号 != value)
                {
                    _受注番号 = value;
                    OnPropertyChanged("受注番号");
                }
            }
        }
        private Nullable<int> _受注番号;
    
        [DataMember]
        public Nullable<int> 出荷元コード
        {
            get { return _出荷元コード; }
            set
            {
                if (_出荷元コード != value)
                {
                    _出荷元コード = value;
                    OnPropertyChanged("出荷元コード");
                }
            }
        }
        private Nullable<int> _出荷元コード;
    
        [DataMember]
        public string 出荷先コード
        {
            get { return _出荷先コード; }
            set
            {
                if (_出荷先コード != value)
                {
                    _出荷先コード = value;
                    OnPropertyChanged("出荷先コード");
                }
            }
        }
        private string _出荷先コード;
    
        [DataMember]
        public Nullable<int> 仕入先コード
        {
            get { return _仕入先コード; }
            set
            {
                if (_仕入先コード != value)
                {
                    _仕入先コード = value;
                    OnPropertyChanged("仕入先コード");
                }
            }
        }
        private Nullable<int> _仕入先コード;
    
        [DataMember]
        public Nullable<int> 仕入先枝番
        {
            get { return _仕入先枝番; }
            set
            {
                if (_仕入先枝番 != value)
                {
                    _仕入先枝番 = value;
                    OnPropertyChanged("仕入先枝番");
                }
            }
        }
        private Nullable<int> _仕入先枝番;
    
        [DataMember]
        public string 備考
        {
            get { return _備考; }
            set
            {
                if (_備考 != value)
                {
                    _備考 = value;
                    OnPropertyChanged("備考");
                }
            }
        }
        private string _備考;
    
        [DataMember]
        public Nullable<int> 通常税率対象金額
        {
            get { return _通常税率対象金額; }
            set
            {
                if (_通常税率対象金額 != value)
                {
                    _通常税率対象金額 = value;
                    OnPropertyChanged("通常税率対象金額");
                }
            }
        }
        private Nullable<int> _通常税率対象金額;
    
        [DataMember]
        public Nullable<int> 軽減税率対象金額
        {
            get { return _軽減税率対象金額; }
            set
            {
                if (_軽減税率対象金額 != value)
                {
                    _軽減税率対象金額 = value;
                    OnPropertyChanged("軽減税率対象金額");
                }
            }
        }
        private Nullable<int> _軽減税率対象金額;
    
        [DataMember]
        public Nullable<int> 通常税率消費税
        {
            get { return _通常税率消費税; }
            set
            {
                if (_通常税率消費税 != value)
                {
                    _通常税率消費税 = value;
                    OnPropertyChanged("通常税率消費税");
                }
            }
        }
        private Nullable<int> _通常税率消費税;
    
        [DataMember]
        public Nullable<int> 軽減税率消費税
        {
            get { return _軽減税率消費税; }
            set
            {
                if (_軽減税率消費税 != value)
                {
                    _軽減税率消費税 = value;
                    OnPropertyChanged("軽減税率消費税");
                }
            }
        }
        private Nullable<int> _軽減税率消費税;
    
        [DataMember]
        public Nullable<int> 小計
        {
            get { return _小計; }
            set
            {
                if (_小計 != value)
                {
                    _小計 = value;
                    OnPropertyChanged("小計");
                }
            }
        }
        private Nullable<int> _小計;
    
        [DataMember]
        public int 消費税
        {
            get { return _消費税; }
            set
            {
                if (_消費税 != value)
                {
                    _消費税 = value;
                    OnPropertyChanged("消費税");
                }
            }
        }
        private int _消費税;
    
        [DataMember]
        public Nullable<int> 総合計
        {
            get { return _総合計; }
            set
            {
                if (_総合計 != value)
                {
                    _総合計 = value;
                    OnPropertyChanged("総合計");
                }
            }
        }
        private Nullable<int> _総合計;
    
        [DataMember]
        public Nullable<int> 調整消費税
        {
            get { return _調整消費税; }
            set
            {
                if (_調整消費税 != value)
                {
                    _調整消費税 = value;
                    OnPropertyChanged("調整消費税");
                }
            }
        }
        private Nullable<int> _調整消費税;
    
        [DataMember]
        public Nullable<decimal> 調整比率
        {
            get { return _調整比率; }
            set
            {
                if (_調整比率 != value)
                {
                    _調整比率 = value;
                    OnPropertyChanged("調整比率");
                }
            }
        }
        private Nullable<decimal> _調整比率;
    
        [DataMember]
        public Nullable<int> 登録者
        {
            get { return _登録者; }
            set
            {
                if (_登録者 != value)
                {
                    _登録者 = value;
                    OnPropertyChanged("登録者");
                }
            }
        }
        private Nullable<int> _登録者;
    
        [DataMember]
        public System.DateTime 登録日時
        {
            get { return _登録日時; }
            set
            {
                if (_登録日時 != value)
                {
                    _登録日時 = value;
                    OnPropertyChanged("登録日時");
                }
            }
        }
        private System.DateTime _登録日時;
    
        [DataMember]
        public Nullable<int> 最終更新者
        {
            get { return _最終更新者; }
            set
            {
                if (_最終更新者 != value)
                {
                    _最終更新者 = value;
                    OnPropertyChanged("最終更新者");
                }
            }
        }
        private Nullable<int> _最終更新者;
    
        [DataMember]
        public Nullable<System.DateTime> 最終更新日時
        {
            get { return _最終更新日時; }
            set
            {
                if (_最終更新日時 != value)
                {
                    _最終更新日時 = value;
                    OnPropertyChanged("最終更新日時");
                }
            }
        }
        private Nullable<System.DateTime> _最終更新日時;
    
        [DataMember]
        public Nullable<int> 削除者
        {
            get { return _削除者; }
            set
            {
                if (_削除者 != value)
                {
                    _削除者 = value;
                    OnPropertyChanged("削除者");
                }
            }
        }
        private Nullable<int> _削除者;
    
        [DataMember]
        public Nullable<System.DateTime> 削除日時
        {
            get { return _削除日時; }
            set
            {
                if (_削除日時 != value)
                {
                    _削除日時 = value;
                    OnPropertyChanged("削除日時");
                }
            }
        }
        private Nullable<System.DateTime> _削除日時;

        #endregion

        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
        }

        #endregion

    }
}
