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
    public partial class T12_PAYHD: IObjectWithChangeTracker, INotifyPropertyChanged
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
        public int 出金元自社コード
        {
            get { return _出金元自社コード; }
            set
            {
                if (_出金元自社コード != value)
                {
                    _出金元自社コード = value;
                    OnPropertyChanged("出金元自社コード");
                }
            }
        }
        private int _出金元自社コード;
    
        [DataMember]
        public System.DateTime 出金日
        {
            get { return _出金日; }
            set
            {
                if (_出金日 != value)
                {
                    _出金日 = value;
                    OnPropertyChanged("出金日");
                }
            }
        }
        private System.DateTime _出金日;
    
        [DataMember]
        public Nullable<int> 出金先販社コード
        {
            get { return _出金先販社コード; }
            set
            {
                if (_出金先販社コード != value)
                {
                    _出金先販社コード = value;
                    OnPropertyChanged("出金先販社コード");
                }
            }
        }
        private Nullable<int> _出金先販社コード;
    
        [DataMember]
        public Nullable<int> 得意先コード
        {
            get { return _得意先コード; }
            set
            {
                if (_得意先コード != value)
                {
                    _得意先コード = value;
                    OnPropertyChanged("得意先コード");
                }
            }
        }
        private Nullable<int> _得意先コード;
    
        [DataMember]
        public Nullable<int> 得意先枝番
        {
            get { return _得意先枝番; }
            set
            {
                if (_得意先枝番 != value)
                {
                    _得意先枝番 = value;
                    OnPropertyChanged("得意先枝番");
                }
            }
        }
        private Nullable<int> _得意先枝番;
    
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
