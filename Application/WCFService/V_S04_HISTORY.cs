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
    public partial class V_S04_HISTORY: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Simple Properties
    
        [DataMember]
        public long SEQ
        {
            get { return _sEQ; }
            set
            {
                if (_sEQ != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'SEQ' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _sEQ = value;
                    OnPropertyChanged("SEQ");
                }
            }
        }
        private long _sEQ;
    
        [DataMember]
        public System.DateTime 入出庫日
        {
            get { return _入出庫日; }
            set
            {
                if (_入出庫日 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '入出庫日' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _入出庫日 = value;
                    OnPropertyChanged("入出庫日");
                }
            }
        }
        private System.DateTime _入出庫日;
    
        [DataMember]
        public System.TimeSpan 入出庫時刻
        {
            get { return _入出庫時刻; }
            set
            {
                if (_入出庫時刻 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '入出庫時刻' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _入出庫時刻 = value;
                    OnPropertyChanged("入出庫時刻");
                }
            }
        }
        private System.TimeSpan _入出庫時刻;
    
        [DataMember]
        public int 入出庫区分
        {
            get { return _入出庫区分; }
            set
            {
                if (_入出庫区分 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '入出庫区分' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _入出庫区分 = value;
                    OnPropertyChanged("入出庫区分");
                }
            }
        }
        private int _入出庫区分;
    
        [DataMember]
        public string 入出庫区分名
        {
            get { return _入出庫区分名; }
            set
            {
                if (_入出庫区分名 != value)
                {
                    _入出庫区分名 = value;
                    OnPropertyChanged("入出庫区分名");
                }
            }
        }
        private string _入出庫区分名;
    
        [DataMember]
        public int 倉庫コード
        {
            get { return _倉庫コード; }
            set
            {
                if (_倉庫コード != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '倉庫コード' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _倉庫コード = value;
                    OnPropertyChanged("倉庫コード");
                }
            }
        }
        private int _倉庫コード;
    
        [DataMember]
        public string 倉庫名
        {
            get { return _倉庫名; }
            set
            {
                if (_倉庫名 != value)
                {
                    _倉庫名 = value;
                    OnPropertyChanged("倉庫名");
                }
            }
        }
        private string _倉庫名;
    
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
        public string 自社品番
        {
            get { return _自社品番; }
            set
            {
                if (_自社品番 != value)
                {
                    _自社品番 = value;
                    OnPropertyChanged("自社品番");
                }
            }
        }
        private string _自社品番;
    
        [DataMember]
        public string 自社品名
        {
            get { return _自社品名; }
            set
            {
                if (_自社品名 != value)
                {
                    _自社品名 = value;
                    OnPropertyChanged("自社品名");
                }
            }
        }
        private string _自社品名;
    
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
        public Nullable<int> 数量
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
        private Nullable<int> _数量;
    
        [DataMember]
        public Nullable<int> 伝票番号
        {
            get { return _伝票番号; }
            set
            {
                if (_伝票番号 != value)
                {
                    _伝票番号 = value;
                    OnPropertyChanged("伝票番号");
                }
            }
        }
        private Nullable<int> _伝票番号;
    
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
        public string 作成機能名
        {
            get { return _作成機能名; }
            set
            {
                if (_作成機能名 != value)
                {
                    _作成機能名 = value;
                    OnPropertyChanged("作成機能名");
                }
            }
        }
        private string _作成機能名;
    
        [DataMember]
        public System.DateTime 登録日時
        {
            get { return _登録日時; }
            set
            {
                if (_登録日時 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '登録日時' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
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