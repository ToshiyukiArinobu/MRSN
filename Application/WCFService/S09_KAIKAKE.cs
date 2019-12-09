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
    public partial class S09_KAIKAKE: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Simple Properties
    
        [DataMember]
        public int 自社コード
        {
            get { return _自社コード; }
            set
            {
                if (_自社コード != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '自社コード' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _自社コード = value;
                    OnPropertyChanged("自社コード");
                }
            }
        }
        private int _自社コード;
    
        [DataMember]
        public int 得意先コード
        {
            get { return _得意先コード; }
            set
            {
                if (_得意先コード != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '得意先コード' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _得意先コード = value;
                    OnPropertyChanged("得意先コード");
                }
            }
        }
        private int _得意先コード;
    
        [DataMember]
        public int 得意先枝番
        {
            get { return _得意先枝番; }
            set
            {
                if (_得意先枝番 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '得意先枝番' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _得意先枝番 = value;
                    OnPropertyChanged("得意先枝番");
                }
            }
        }
        private int _得意先枝番;
    
        [DataMember]
        public System.DateTime 日付
        {
            get { return _日付; }
            set
            {
                if (_日付 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '日付' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _日付 = value;
                    OnPropertyChanged("日付");
                }
            }
        }
        private System.DateTime _日付;
    
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
        public int 行番号
        {
            get { return _行番号; }
            set
            {
                if (_行番号 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '行番号' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _行番号 = value;
                    OnPropertyChanged("行番号");
                }
            }
        }
        private int _行番号;
    
        [DataMember]
        public int 品番コード
        {
            get { return _品番コード; }
            set
            {
                if (_品番コード != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '品番コード' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _品番コード = value;
                    OnPropertyChanged("品番コード");
                }
            }
        }
        private int _品番コード;
    
        [DataMember]
        public int 作成機能ID
        {
            get { return _作成機能ID; }
            set
            {
                if (_作成機能ID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '作成機能ID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _作成機能ID = value;
                    OnPropertyChanged("作成機能ID");
                }
            }
        }
        private int _作成機能ID;
    
        [DataMember]
        public Nullable<int> 金種コード
        {
            get { return _金種コード; }
            set
            {
                if (_金種コード != value)
                {
                    _金種コード = value;
                    OnPropertyChanged("金種コード");
                }
            }
        }
        private Nullable<int> _金種コード;
    
        [DataMember]
        public decimal 数量
        {
            get { return _数量; }
            set
            {
                if (_数量 != value)
                {
                    _数量 = value;
                    OnPropertyChanged("数量");
                }
            }
        }
        private decimal _数量;
    
        [DataMember]
        public decimal 単価
        {
            get { return _単価; }
            set
            {
                if (_単価 != value)
                {
                    _単価 = value;
                    OnPropertyChanged("単価");
                }
            }
        }
        private decimal _単価;
    
        [DataMember]
        public int 金額
        {
            get { return _金額; }
            set
            {
                if (_金額 != value)
                {
                    _金額 = value;
                    OnPropertyChanged("金額");
                }
            }
        }
        private int _金額;
    
        [DataMember]
        public int 通常税率消費税
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
        private int _通常税率消費税;
    
        [DataMember]
        public int 軽減税率消費税
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
        private int _軽減税率消費税;
    
        [DataMember]
        public int 出金額
        {
            get { return _出金額; }
            set
            {
                if (_出金額 != value)
                {
                    _出金額 = value;
                    OnPropertyChanged("出金額");
                }
            }
        }
        private int _出金額;
    
        [DataMember]
        public int 前月繰越
        {
            get { return _前月繰越; }
            set
            {
                if (_前月繰越 != value)
                {
                    _前月繰越 = value;
                    OnPropertyChanged("前月繰越");
                }
            }
        }
        private int _前月繰越;
    
        [DataMember]
        public int 残高
        {
            get { return _残高; }
            set
            {
                if (_残高 != value)
                {
                    _残高 = value;
                    OnPropertyChanged("残高");
                }
            }
        }
        private int _残高;
    
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
