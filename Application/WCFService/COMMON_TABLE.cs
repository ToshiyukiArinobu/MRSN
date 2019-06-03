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
    public partial class COMMON_TABLE: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Simple Properties
    
        [DataMember]
        public Nullable<int> 顧客コード
        {
            get { return _顧客コード; }
            set
            {
                if (_顧客コード != value)
                {
                    _顧客コード = value;
                    OnPropertyChanged("顧客コード");
                }
            }
        }
        private Nullable<int> _顧客コード;
    
        [DataMember]
        public string ユーザーID
        {
            get { return _ユーザーID; }
            set
            {
                if (_ユーザーID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'ユーザーID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _ユーザーID = value;
                    OnPropertyChanged("ユーザーID");
                }
            }
        }
        private string _ユーザーID;
    
        [DataMember]
        public string パスワード
        {
            get { return _パスワード; }
            set
            {
                if (_パスワード != value)
                {
                    _パスワード = value;
                    OnPropertyChanged("パスワード");
                }
            }
        }
        private string _パスワード;
    
        [DataMember]
        public Nullable<int> ログインフラグ
        {
            get { return _ログインフラグ; }
            set
            {
                if (_ログインフラグ != value)
                {
                    _ログインフラグ = value;
                    OnPropertyChanged("ログインフラグ");
                }
            }
        }
        private Nullable<int> _ログインフラグ;
    
        [DataMember]
        public Nullable<System.DateTime> アクセス時間
        {
            get { return _アクセス時間; }
            set
            {
                if (_アクセス時間 != value)
                {
                    _アクセス時間 = value;
                    OnPropertyChanged("アクセス時間");
                }
            }
        }
        private Nullable<System.DateTime> _アクセス時間;
    
        [DataMember]
        public string DB接続先
        {
            get { return _dB接続先; }
            set
            {
                if (_dB接続先 != value)
                {
                    _dB接続先 = value;
                    OnPropertyChanged("DB接続先");
                }
            }
        }
        private string _dB接続先;
    
        [DataMember]
        public string ユーザーDB
        {
            get { return _ユーザーDB; }
            set
            {
                if (_ユーザーDB != value)
                {
                    _ユーザーDB = value;
                    OnPropertyChanged("ユーザーDB");
                }
            }
        }
        private string _ユーザーDB;
    
        [DataMember]
        public string DBログインID
        {
            get { return _dBログインID; }
            set
            {
                if (_dBログインID != value)
                {
                    _dBログインID = value;
                    OnPropertyChanged("DBログインID");
                }
            }
        }
        private string _dBログインID;
    
        [DataMember]
        public string DBパスワード
        {
            get { return _dBパスワード; }
            set
            {
                if (_dBパスワード != value)
                {
                    _dBパスワード = value;
                    OnPropertyChanged("DBパスワード");
                }
            }
        }
        private string _dBパスワード;
    
        [DataMember]
        public Nullable<System.DateTime> 開始日
        {
            get { return _開始日; }
            set
            {
                if (_開始日 != value)
                {
                    _開始日 = value;
                    OnPropertyChanged("開始日");
                }
            }
        }
        private Nullable<System.DateTime> _開始日;
    
        [DataMember]
        public Nullable<System.DateTime> 有効期限
        {
            get { return _有効期限; }
            set
            {
                if (_有効期限 != value)
                {
                    _有効期限 = value;
                    OnPropertyChanged("有効期限");
                }
            }
        }
        private Nullable<System.DateTime> _有効期限;
    
        [DataMember]
        public Nullable<System.DateTime> 登録日
        {
            get { return _登録日; }
            set
            {
                if (_登録日 != value)
                {
                    _登録日 = value;
                    OnPropertyChanged("登録日");
                }
            }
        }
        private Nullable<System.DateTime> _登録日;
    
        [DataMember]
        public string メッセージ1
        {
            get { return _メッセージ1; }
            set
            {
                if (_メッセージ1 != value)
                {
                    _メッセージ1 = value;
                    OnPropertyChanged("メッセージ1");
                }
            }
        }
        private string _メッセージ1;
    
        [DataMember]
        public string メッセージ2
        {
            get { return _メッセージ2; }
            set
            {
                if (_メッセージ2 != value)
                {
                    _メッセージ2 = value;
                    OnPropertyChanged("メッセージ2");
                }
            }
        }
        private string _メッセージ2;
    
        [DataMember]
        public string メッセージ3
        {
            get { return _メッセージ3; }
            set
            {
                if (_メッセージ3 != value)
                {
                    _メッセージ3 = value;
                    OnPropertyChanged("メッセージ3");
                }
            }
        }
        private string _メッセージ3;
    
        [DataMember]
        public string メッセージ4
        {
            get { return _メッセージ4; }
            set
            {
                if (_メッセージ4 != value)
                {
                    _メッセージ4 = value;
                    OnPropertyChanged("メッセージ4");
                }
            }
        }
        private string _メッセージ4;

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
