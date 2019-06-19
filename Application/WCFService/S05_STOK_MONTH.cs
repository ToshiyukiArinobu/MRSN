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
    public partial class S05_STOK_MONTH: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Simple Properties
    
        [DataMember]
        public int 締年月
        {
            get { return _締年月; }
            set
            {
                if (_締年月 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '締年月' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _締年月 = value;
                    OnPropertyChanged("締年月");
                }
            }
        }
        private int _締年月;
    
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
        public System.DateTime 賞味期限
        {
            get { return _賞味期限; }
            set
            {
                if (_賞味期限 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '賞味期限' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _賞味期限 = value;
                    OnPropertyChanged("賞味期限");
                }
            }
        }
        private System.DateTime _賞味期限;
    
        [DataMember]
        public int 在庫数量
        {
            get { return _在庫数量; }
            set
            {
                if (_在庫数量 != value)
                {
                    _在庫数量 = value;
                    OnPropertyChanged("在庫数量");
                }
            }
        }
        private int _在庫数量;
    
        [DataMember]
        public int 登録者
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
        private int _登録者;
    
        [DataMember]
        public Nullable<System.DateTime> 登録日時
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
        private Nullable<System.DateTime> _登録日時;

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
