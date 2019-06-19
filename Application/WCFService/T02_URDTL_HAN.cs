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
    public partial class T02_URDTL_HAN: IObjectWithChangeTracker, INotifyPropertyChanged
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
                    _品番コード = value;
                    OnPropertyChanged("品番コード");
                }
            }
        }
        private int _品番コード;
    
        [DataMember]
        public Nullable<System.DateTime> 賞味期限
        {
            get { return _賞味期限; }
            set
            {
                if (_賞味期限 != value)
                {
                    _賞味期限 = value;
                    OnPropertyChanged("賞味期限");
                }
            }
        }
        private Nullable<System.DateTime> _賞味期限;
    
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
        public string 単位
        {
            get { return _単位; }
            set
            {
                if (_単位 != value)
                {
                    _単位 = value;
                    OnPropertyChanged("単位");
                }
            }
        }
        private string _単位;
    
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
        public Nullable<int> 金額
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
        private Nullable<int> _金額;
    
        [DataMember]
        public string 摘要
        {
            get { return _摘要; }
            set
            {
                if (_摘要 != value)
                {
                    _摘要 = value;
                    OnPropertyChanged("摘要");
                }
            }
        }
        private string _摘要;
    
        [DataMember]
        public Nullable<decimal> 調整単価
        {
            get { return _調整単価; }
            set
            {
                if (_調整単価 != value)
                {
                    _調整単価 = value;
                    OnPropertyChanged("調整単価");
                }
            }
        }
        private Nullable<decimal> _調整単価;
    
        [DataMember]
        public Nullable<int> 調整金額
        {
            get { return _調整金額; }
            set
            {
                if (_調整金額 != value)
                {
                    _調整金額 = value;
                    OnPropertyChanged("調整金額");
                }
            }
        }
        private Nullable<int> _調整金額;
    
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
