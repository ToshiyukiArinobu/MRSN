﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace KyoeiSystem.Application.WCFService
{
    public partial class COMMONDBEntities : ObjectContext
    {
        public const string ConnectionString = "name=COMMONDBEntities";
        public const string ContainerName = "COMMONDBEntities";
    
        #region Constructors
    
        public COMMONDBEntities()
            : base(ConnectionString, ContainerName)
        {
            Initialize();
        }
    
        public COMMONDBEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            Initialize();
        }
    
        public COMMONDBEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            Initialize();
        }
    
        private void Initialize()
        {
            // Creating proxies requires the use of the ProxyDataContractResolver and
            // may allow lazy loading which can expand the loaded graph during serialization.
            ContextOptions.ProxyCreationEnabled = false;
            ObjectMaterialized += new ObjectMaterializedEventHandler(HandleObjectMaterialized);
        }
    
        private void HandleObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            var entity = e.Entity as IObjectWithChangeTracker;
            if (entity != null)
            {
                bool changeTrackingEnabled = entity.ChangeTracker.ChangeTrackingEnabled;
                try
                {
                    entity.MarkAsUnchanged();
                }
                finally
                {
                    entity.ChangeTracker.ChangeTrackingEnabled = changeTrackingEnabled;
                }
                this.StoreReferenceKeyValues(entity);
            }
        }

        #endregion

        #region ObjectSet Properties
    
        public ObjectSet<COMMON_TABLE> COMMON_TABLE
        {
            get { return _cOMMON_TABLE  ?? (_cOMMON_TABLE = CreateObjectSet<COMMON_TABLE>("COMMON_TABLE")); }
        }
        private ObjectSet<COMMON_TABLE> _cOMMON_TABLE;

        #endregion

    }
}
