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
    public partial class S02_SHRDTL: IObjectWithChangeTracker, INotifyPropertyChanged
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
        public int 支払年月
        {
            get { return _支払年月; }
            set
            {
                if (_支払年月 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '支払年月' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _支払年月 = value;
                    OnPropertyChanged("支払年月");
                }
            }
        }
        private int _支払年月;
    
        [DataMember]
        public int 支払締日
        {
            get { return _支払締日; }
            set
            {
                if (_支払締日 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '支払締日' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _支払締日 = value;
                    OnPropertyChanged("支払締日");
                }
            }
        }
        private int _支払締日;
    
        [DataMember]
        public int 支払先コード
        {
            get { return _支払先コード; }
            set
            {
                if (_支払先コード != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '支払先コード' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _支払先コード = value;
                    OnPropertyChanged("支払先コード");
                }
            }
        }
        private int _支払先コード;
    
        [DataMember]
        public int 支払先枝番
        {
            get { return _支払先枝番; }
            set
            {
                if (_支払先枝番 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '支払先枝番' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _支払先枝番 = value;
                    OnPropertyChanged("支払先枝番");
                }
            }
        }
        private int _支払先枝番;
    
        [DataMember]
        public int 支払日
        {
            get { return _支払日; }
            set
            {
                if (_支払日 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '支払日' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _支払日 = value;
                    OnPropertyChanged("支払日");
                }
            }
        }
        private int _支払日;
    
        [DataMember]
        public int 回数
        {
            get { return _回数; }
            set
            {
                if (_回数 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '回数' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _回数 = value;
                    OnPropertyChanged("回数");
                }
            }
        }
        private int _回数;
    
        [DataMember]
        public int 行
        {
            get { return _行; }
            set
            {
                if (_行 != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property '行' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _行 = value;
                    OnPropertyChanged("行");
                }
            }
        }
        private int _行;
    
        [DataMember]
        public int 伝票番号
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
        private int _伝票番号;
    
        [DataMember]
        public System.DateTime 仕入日
        {
            get { return _仕入日; }
            set
            {
                if (_仕入日 != value)
                {
                    _仕入日 = value;
                    OnPropertyChanged("仕入日");
                }
            }
        }
        private System.DateTime _仕入日;
    
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
