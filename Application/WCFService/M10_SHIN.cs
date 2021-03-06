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
    public partial class M10_SHIN: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Simple Properties
    
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
        public int 部品行
        {
            get { return _部品行; }
            set
            {
                if (_部品行 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '部品行' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _部品行 = value;
                    OnPropertyChanged("部品行");
                }
            }
        }
        private int _部品行;
    
        [DataMember]
        public int 構成品番コード
        {
            get { return _構成品番コード; }
            set
            {
                if (_構成品番コード != value)
                {
                    _構成品番コード = value;
                    OnPropertyChanged("構成品番コード");
                }
            }
        }
        private int _構成品番コード;
    
        [DataMember]
        public int 使用数量
        {
            get { return _使用数量; }
            set
            {
                if (_使用数量 != value)
                {
                    _使用数量 = value;
                    OnPropertyChanged("使用数量");
                }
            }
        }
        private int _使用数量;
    
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
