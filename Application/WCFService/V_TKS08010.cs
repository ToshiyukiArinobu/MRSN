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
    public partial class V_TKS08010: IObjectWithChangeTracker, INotifyPropertyChanged
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
        public string 自社名
        {
            get { return _自社名; }
            set
            {
                if (_自社名 != value)
                {
                    _自社名 = value;
                    OnPropertyChanged("自社名");
                }
            }
        }
        private string _自社名;
    
        [DataMember]
        public Nullable<int> K入金年月
        {
            get { return _k入金年月; }
            set
            {
                if (_k入金年月 != value)
                {
                    _k入金年月 = value;
                    OnPropertyChanged("K入金年月");
                }
            }
        }
        private Nullable<int> _k入金年月;
    
        [DataMember]
        public Nullable<int> K入金日
        {
            get { return _k入金日; }
            set
            {
                if (_k入金日 != value)
                {
                    _k入金日 = value;
                    OnPropertyChanged("K入金日");
                }
            }
        }
        private Nullable<int> _k入金日;
    
        [DataMember]
        public int K得意先コード
        {
            get { return _k得意先コード; }
            set
            {
                if (_k得意先コード != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'K得意先コード' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _k得意先コード = value;
                    OnPropertyChanged("K得意先コード");
                }
            }
        }
        private int _k得意先コード;
    
        [DataMember]
        public int K得意先枝番
        {
            get { return _k得意先枝番; }
            set
            {
                if (_k得意先枝番 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'K得意先枝番' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _k得意先枝番 = value;
                    OnPropertyChanged("K得意先枝番");
                }
            }
        }
        private int _k得意先枝番;
    
        [DataMember]
        public string 得意先コード
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
        private string _得意先コード;
    
        [DataMember]
        public string 得意先名
        {
            get { return _得意先名; }
            set
            {
                if (_得意先名 != value)
                {
                    _得意先名 = value;
                    OnPropertyChanged("得意先名");
                }
            }
        }
        private string _得意先名;
    
        [DataMember]
        public string 入金日付
        {
            get { return _入金日付; }
            set
            {
                if (_入金日付 != value)
                {
                    _入金日付 = value;
                    OnPropertyChanged("入金日付");
                }
            }
        }
        private string _入金日付;
    
        [DataMember]
        public Nullable<long> 売上額
        {
            get { return _売上額; }
            set
            {
                if (_売上額 != value)
                {
                    _売上額 = value;
                    OnPropertyChanged("売上額");
                }
            }
        }
        private Nullable<long> _売上額;
    
        [DataMember]
        public Nullable<long> 消費税
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
        private Nullable<long> _消費税;
    
        [DataMember]
        public Nullable<long> 回収予定額
        {
            get { return _回収予定額; }
            set
            {
                if (_回収予定額 != value)
                {
                    _回収予定額 = value;
                    OnPropertyChanged("回収予定額");
                }
            }
        }
        private Nullable<long> _回収予定額;
    
        [DataMember]
        public string 請求年月
        {
            get { return _請求年月; }
            set
            {
                if (_請求年月 != value)
                {
                    _請求年月 = value;
                    OnPropertyChanged("請求年月");
                }
            }
        }
        private string _請求年月;
    
        [DataMember]
        public int 締日
        {
            get { return _締日; }
            set
            {
                if (_締日 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '締日' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _締日 = value;
                    OnPropertyChanged("締日");
                }
            }
        }
        private int _締日;
    
        [DataMember]
        public Nullable<int> 当月入金額
        {
            get { return _当月入金額; }
            set
            {
                if (_当月入金額 != value)
                {
                    _当月入金額 = value;
                    OnPropertyChanged("当月入金額");
                }
            }
        }
        private Nullable<int> _当月入金額;

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
