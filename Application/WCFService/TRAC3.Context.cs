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
    public partial class TRAC3Entities : ObjectContext
    {
        public const string ConnectionString = "name=TRAC3Entities";
        public const string ContainerName = "TRAC3Entities";
    
        #region Constructors
    
        public TRAC3Entities()
            : base(ConnectionString, ContainerName)
        {
            Initialize();
        }
    
        public TRAC3Entities(string connectionString)
            : base(connectionString, ContainerName)
        {
            Initialize();
        }
    
        public TRAC3Entities(EntityConnection connection)
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
    
        public ObjectSet<M02_BAIKA> M02_BAIKA
        {
            get { return _m02_BAIKA  ?? (_m02_BAIKA = CreateObjectSet<M02_BAIKA>("M02_BAIKA")); }
        }
        private ObjectSet<M02_BAIKA> _m02_BAIKA;
    
        public ObjectSet<M03_BAIKA> M03_BAIKA
        {
            get { return _m03_BAIKA  ?? (_m03_BAIKA = CreateObjectSet<M03_BAIKA>("M03_BAIKA")); }
        }
        private ObjectSet<M03_BAIKA> _m03_BAIKA;
    
        public ObjectSet<M04_BAIKA> M04_BAIKA
        {
            get { return _m04_BAIKA  ?? (_m04_BAIKA = CreateObjectSet<M04_BAIKA>("M04_BAIKA")); }
        }
        private ObjectSet<M04_BAIKA> _m04_BAIKA;
    
        public ObjectSet<M06_IRO> M06_IRO
        {
            get { return _m06_IRO  ?? (_m06_IRO = CreateObjectSet<M06_IRO>("M06_IRO")); }
        }
        private ObjectSet<M06_IRO> _m06_IRO;
    
        public ObjectSet<M09_HIN> M09_HIN
        {
            get { return _m09_HIN  ?? (_m09_HIN = CreateObjectSet<M09_HIN>("M09_HIN")); }
        }
        private ObjectSet<M09_HIN> _m09_HIN;
    
        public ObjectSet<M10_SHIN> M10_SHIN
        {
            get { return _m10_SHIN  ?? (_m10_SHIN = CreateObjectSet<M10_SHIN>("M10_SHIN")); }
        }
        private ObjectSet<M10_SHIN> _m10_SHIN;
    
        public ObjectSet<M10_TOKHIN> M10_TOKHIN
        {
            get { return _m10_TOKHIN  ?? (_m10_TOKHIN = CreateObjectSet<M10_TOKHIN>("M10_TOKHIN")); }
        }
        private ObjectSet<M10_TOKHIN> _m10_TOKHIN;
    
        public ObjectSet<M11_TEK> M11_TEK
        {
            get { return _m11_TEK  ?? (_m11_TEK = CreateObjectSet<M11_TEK>("M11_TEK")); }
        }
        private ObjectSet<M11_TEK> _m11_TEK;
    
        public ObjectSet<M12_DAIBUNRUI> M12_DAIBUNRUI
        {
            get { return _m12_DAIBUNRUI  ?? (_m12_DAIBUNRUI = CreateObjectSet<M12_DAIBUNRUI>("M12_DAIBUNRUI")); }
        }
        private ObjectSet<M12_DAIBUNRUI> _m12_DAIBUNRUI;
    
        public ObjectSet<M13_TYUBUNRUI> M13_TYUBUNRUI
        {
            get { return _m13_TYUBUNRUI  ?? (_m13_TYUBUNRUI = CreateObjectSet<M13_TYUBUNRUI>("M13_TYUBUNRUI")); }
        }
        private ObjectSet<M13_TYUBUNRUI> _m13_TYUBUNRUI;
    
        public ObjectSet<M14_BRAND> M14_BRAND
        {
            get { return _m14_BRAND  ?? (_m14_BRAND = CreateObjectSet<M14_BRAND>("M14_BRAND")); }
        }
        private ObjectSet<M14_BRAND> _m14_BRAND;
    
        public ObjectSet<M15_SERIES> M15_SERIES
        {
            get { return _m15_SERIES  ?? (_m15_SERIES = CreateObjectSet<M15_SERIES>("M15_SERIES")); }
        }
        private ObjectSet<M15_SERIES> _m15_SERIES;
    
        public ObjectSet<M16_HINGUN> M16_HINGUN
        {
            get { return _m16_HINGUN  ?? (_m16_HINGUN = CreateObjectSet<M16_HINGUN>("M16_HINGUN")); }
        }
        private ObjectSet<M16_HINGUN> _m16_HINGUN;
    
        public ObjectSet<M21_SYUK> M21_SYUK
        {
            get { return _m21_SYUK  ?? (_m21_SYUK = CreateObjectSet<M21_SYUK>("M21_SYUK")); }
        }
        private ObjectSet<M21_SYUK> _m21_SYUK;
    
        public ObjectSet<M22_SOUK> M22_SOUK
        {
            get { return _m22_SOUK  ?? (_m22_SOUK = CreateObjectSet<M22_SOUK>("M22_SOUK")); }
        }
        private ObjectSet<M22_SOUK> _m22_SOUK;
    
        public ObjectSet<M70_JIS> M70_JIS
        {
            get { return _m70_JIS  ?? (_m70_JIS = CreateObjectSet<M70_JIS>("M70_JIS")); }
        }
        private ObjectSet<M70_JIS> _m70_JIS;
    
        public ObjectSet<M72_TNT> M72_TNT
        {
            get { return _m72_TNT  ?? (_m72_TNT = CreateObjectSet<M72_TNT>("M72_TNT")); }
        }
        private ObjectSet<M72_TNT> _m72_TNT;
    
        public ObjectSet<M73_ZEI> M73_ZEI
        {
            get { return _m73_ZEI  ?? (_m73_ZEI = CreateObjectSet<M73_ZEI>("M73_ZEI")); }
        }
        private ObjectSet<M73_ZEI> _m73_ZEI;
    
        public ObjectSet<M74_KGRP> M74_KGRP
        {
            get { return _m74_KGRP  ?? (_m74_KGRP = CreateObjectSet<M74_KGRP>("M74_KGRP")); }
        }
        private ObjectSet<M74_KGRP> _m74_KGRP;
    
        public ObjectSet<M74_KGRP_NAME> M74_KGRP_NAME
        {
            get { return _m74_KGRP_NAME  ?? (_m74_KGRP_NAME = CreateObjectSet<M74_KGRP_NAME>("M74_KGRP_NAME")); }
        }
        private ObjectSet<M74_KGRP_NAME> _m74_KGRP_NAME;
    
        public ObjectSet<M88_SEQ> M88_SEQ
        {
            get { return _m88_SEQ  ?? (_m88_SEQ = CreateObjectSet<M88_SEQ>("M88_SEQ")); }
        }
        private ObjectSet<M88_SEQ> _m88_SEQ;
    
        public ObjectSet<M89_KENALL> M89_KENALL
        {
            get { return _m89_KENALL  ?? (_m89_KENALL = CreateObjectSet<M89_KENALL>("M89_KENALL")); }
        }
        private ObjectSet<M89_KENALL> _m89_KENALL;
    
        public ObjectSet<M90_GRID> M90_GRID
        {
            get { return _m90_GRID  ?? (_m90_GRID = CreateObjectSet<M90_GRID>("M90_GRID")); }
        }
        private ObjectSet<M90_GRID> _m90_GRID;
    
        public ObjectSet<M99_COMBOLIST> M99_COMBOLIST
        {
            get { return _m99_COMBOLIST  ?? (_m99_COMBOLIST = CreateObjectSet<M99_COMBOLIST>("M99_COMBOLIST")); }
        }
        private ObjectSet<M99_COMBOLIST> _m99_COMBOLIST;
    
        public ObjectSet<M99_MSG> M99_MSG
        {
            get { return _m99_MSG  ?? (_m99_MSG = CreateObjectSet<M99_MSG>("M99_MSG")); }
        }
        private ObjectSet<M99_MSG> _m99_MSG;
    
        public ObjectSet<M99_ZIP> M99_ZIP
        {
            get { return _m99_ZIP  ?? (_m99_ZIP = CreateObjectSet<M99_ZIP>("M99_ZIP")); }
        }
        private ObjectSet<M99_ZIP> _m99_ZIP;
    
        public ObjectSet<S04_HISTORY> S04_HISTORY
        {
            get { return _s04_HISTORY  ?? (_s04_HISTORY = CreateObjectSet<S04_HISTORY>("S04_HISTORY")); }
        }
        private ObjectSet<S04_HISTORY> _s04_HISTORY;
    
        public ObjectSet<S05_STOK_MONTH> S05_STOK_MONTH
        {
            get { return _s05_STOK_MONTH  ?? (_s05_STOK_MONTH = CreateObjectSet<S05_STOK_MONTH>("S05_STOK_MONTH")); }
        }
        private ObjectSet<S05_STOK_MONTH> _s05_STOK_MONTH;
    
        public ObjectSet<T03_SRHD_HAN> T03_SRHD_HAN
        {
            get { return _t03_SRHD_HAN  ?? (_t03_SRHD_HAN = CreateObjectSet<T03_SRHD_HAN>("T03_SRHD_HAN")); }
        }
        private ObjectSet<T03_SRHD_HAN> _t03_SRHD_HAN;
    
        public ObjectSet<T04_AGRDTL> T04_AGRDTL
        {
            get { return _t04_AGRDTL  ?? (_t04_AGRDTL = CreateObjectSet<T04_AGRDTL>("T04_AGRDTL")); }
        }
        private ObjectSet<T04_AGRDTL> _t04_AGRDTL;
    
        public ObjectSet<T04_AGRHD> T04_AGRHD
        {
            get { return _t04_AGRHD  ?? (_t04_AGRHD = CreateObjectSet<T04_AGRHD>("T04_AGRHD")); }
        }
        private ObjectSet<T04_AGRHD> _t04_AGRHD;
    
        public ObjectSet<T04_AGRWK> T04_AGRWK
        {
            get { return _t04_AGRWK  ?? (_t04_AGRWK = CreateObjectSet<T04_AGRWK>("T04_AGRWK")); }
        }
        private ObjectSet<T04_AGRWK> _t04_AGRWK;
    
        public ObjectSet<T05_IDODTL> T05_IDODTL
        {
            get { return _t05_IDODTL  ?? (_t05_IDODTL = CreateObjectSet<T05_IDODTL>("T05_IDODTL")); }
        }
        private ObjectSet<T05_IDODTL> _t05_IDODTL;
    
        public ObjectSet<T05_IDOHD> T05_IDOHD
        {
            get { return _t05_IDOHD  ?? (_t05_IDOHD = CreateObjectSet<T05_IDOHD>("T05_IDOHD")); }
        }
        private ObjectSet<T05_IDOHD> _t05_IDOHD;
    
        public ObjectSet<T11_NYKNDTL> T11_NYKNDTL
        {
            get { return _t11_NYKNDTL  ?? (_t11_NYKNDTL = CreateObjectSet<T11_NYKNDTL>("T11_NYKNDTL")); }
        }
        private ObjectSet<T11_NYKNDTL> _t11_NYKNDTL;
    
        public ObjectSet<T11_NYKNHD> T11_NYKNHD
        {
            get { return _t11_NYKNHD  ?? (_t11_NYKNHD = CreateObjectSet<T11_NYKNHD>("T11_NYKNHD")); }
        }
        private ObjectSet<T11_NYKNHD> _t11_NYKNHD;
    
        public ObjectSet<T12_PAYDTL> T12_PAYDTL
        {
            get { return _t12_PAYDTL  ?? (_t12_PAYDTL = CreateObjectSet<T12_PAYDTL>("T12_PAYDTL")); }
        }
        private ObjectSet<T12_PAYDTL> _t12_PAYDTL;
    
        public ObjectSet<T12_PAYHD> T12_PAYHD
        {
            get { return _t12_PAYHD  ?? (_t12_PAYHD = CreateObjectSet<T12_PAYHD>("T12_PAYHD")); }
        }
        private ObjectSet<T12_PAYHD> _t12_PAYHD;
    
        public ObjectSet<V_M01_TOK> V_M01_TOK
        {
            get { return _v_M01_TOK  ?? (_v_M01_TOK = CreateObjectSet<V_M01_TOK>("V_M01_TOK")); }
        }
        private ObjectSet<V_M01_TOK> _v_M01_TOK;
    
        public ObjectSet<V_M09_HIN> V_M09_HIN
        {
            get { return _v_M09_HIN  ?? (_v_M09_HIN = CreateObjectSet<V_M09_HIN>("V_M09_HIN")); }
        }
        private ObjectSet<V_M09_HIN> _v_M09_HIN;
    
        public ObjectSet<V_M10_TOKHIN> V_M10_TOKHIN
        {
            get { return _v_M10_TOKHIN  ?? (_v_M10_TOKHIN = CreateObjectSet<V_M10_TOKHIN>("V_M10_TOKHIN")); }
        }
        private ObjectSet<V_M10_TOKHIN> _v_M10_TOKHIN;
    
        public ObjectSet<V_S04_HISTORY> V_S04_HISTORY
        {
            get { return _v_S04_HISTORY  ?? (_v_S04_HISTORY = CreateObjectSet<V_S04_HISTORY>("V_S04_HISTORY")); }
        }
        private ObjectSet<V_S04_HISTORY> _v_S04_HISTORY;
    
        public ObjectSet<V_SHR06010> V_SHR06010
        {
            get { return _v_SHR06010  ?? (_v_SHR06010 = CreateObjectSet<V_SHR06010>("V_SHR06010")); }
        }
        private ObjectSet<V_SHR06010> _v_SHR06010;
    
        public ObjectSet<V_TKS08010> V_TKS08010
        {
            get { return _v_TKS08010  ?? (_v_TKS08010 = CreateObjectSet<V_TKS08010>("V_TKS08010")); }
        }
        private ObjectSet<V_TKS08010> _v_TKS08010;
    
        public ObjectSet<T02_URHD> T02_URHD
        {
            get { return _t02_URHD  ?? (_t02_URHD = CreateObjectSet<T02_URHD>("T02_URHD")); }
        }
        private ObjectSet<T02_URHD> _t02_URHD;
    
        public ObjectSet<T02_URHD_HAN> T02_URHD_HAN
        {
            get { return _t02_URHD_HAN  ?? (_t02_URHD_HAN = CreateObjectSet<T02_URHD_HAN>("T02_URHD_HAN")); }
        }
        private ObjectSet<T02_URHD_HAN> _t02_URHD_HAN;
    
        public ObjectSet<T03_SRHD> T03_SRHD
        {
            get { return _t03_SRHD  ?? (_t03_SRHD = CreateObjectSet<T03_SRHD>("T03_SRHD")); }
        }
        private ObjectSet<T03_SRHD> _t03_SRHD;
    
        public ObjectSet<S01_SEIHD> S01_SEIHD
        {
            get { return _s01_SEIHD  ?? (_s01_SEIHD = CreateObjectSet<S01_SEIHD>("S01_SEIHD")); }
        }
        private ObjectSet<S01_SEIHD> _s01_SEIHD;
    
        public ObjectSet<S06_URIHD> S06_URIHD
        {
            get { return _s06_URIHD  ?? (_s06_URIHD = CreateObjectSet<S06_URIHD>("S06_URIHD")); }
        }
        private ObjectSet<S06_URIHD> _s06_URIHD;
    
        public ObjectSet<S02_SHRHD> S02_SHRHD
        {
            get { return _s02_SHRHD  ?? (_s02_SHRHD = CreateObjectSet<S02_SHRHD>("S02_SHRHD")); }
        }
        private ObjectSet<S02_SHRHD> _s02_SHRHD;
    
        public ObjectSet<S07_SRIHD> S07_SRIHD
        {
            get { return _s07_SRIHD  ?? (_s07_SRIHD = CreateObjectSet<S07_SRIHD>("S07_SRIHD")); }
        }
        private ObjectSet<S07_SRIHD> _s07_SRIHD;
    
        public ObjectSet<S11_KAKUTEI> S11_KAKUTEI
        {
            get { return _s11_KAKUTEI  ?? (_s11_KAKUTEI = CreateObjectSet<S11_KAKUTEI>("S11_KAKUTEI")); }
        }
        private ObjectSet<S11_KAKUTEI> _s11_KAKUTEI;
    
        public ObjectSet<S03_STOK> S03_STOK
        {
            get { return _s03_STOK  ?? (_s03_STOK = CreateObjectSet<S03_STOK>("S03_STOK")); }
        }
        private ObjectSet<S03_STOK> _s03_STOK;
    
        public ObjectSet<S05_STOK_ADJUST> S05_STOK_ADJUST
        {
            get { return _s05_STOK_ADJUST  ?? (_s05_STOK_ADJUST = CreateObjectSet<S05_STOK_ADJUST>("S05_STOK_ADJUST")); }
        }
        private ObjectSet<S05_STOK_ADJUST> _s05_STOK_ADJUST;
    
        public ObjectSet<S10_STOCKTAKING> S10_STOCKTAKING
        {
            get { return _s10_STOCKTAKING  ?? (_s10_STOCKTAKING = CreateObjectSet<S10_STOCKTAKING>("S10_STOCKTAKING")); }
        }
        private ObjectSet<S10_STOCKTAKING> _s10_STOCKTAKING;
    
        public ObjectSet<T11_NYKNTR> T11_NYKNTR
        {
            get { return _t11_NYKNTR  ?? (_t11_NYKNTR = CreateObjectSet<T11_NYKNTR>("T11_NYKNTR")); }
        }
        private ObjectSet<T11_NYKNTR> _t11_NYKNTR;
    
        public ObjectSet<S03_STOK_JUST> S03_STOK_JUST
        {
            get { return _s03_STOK_JUST  ?? (_s03_STOK_JUST = CreateObjectSet<S03_STOK_JUST>("S03_STOK_JUST")); }
        }
        private ObjectSet<S03_STOK_JUST> _s03_STOK_JUST;
    
        public ObjectSet<M01_TOK> M01_TOK
        {
            get { return _m01_TOK  ?? (_m01_TOK = CreateObjectSet<M01_TOK>("M01_TOK")); }
        }
        private ObjectSet<M01_TOK> _m01_TOK;
    
        public ObjectSet<S01_SEIDTL> S01_SEIDTL
        {
            get { return _s01_SEIDTL  ?? (_s01_SEIDTL = CreateObjectSet<S01_SEIDTL>("S01_SEIDTL")); }
        }
        private ObjectSet<S01_SEIDTL> _s01_SEIDTL;
    
        public ObjectSet<S02_SHRDTL> S02_SHRDTL
        {
            get { return _s02_SHRDTL  ?? (_s02_SHRDTL = CreateObjectSet<S02_SHRDTL>("S02_SHRDTL")); }
        }
        private ObjectSet<S02_SHRDTL> _s02_SHRDTL;
    
        public ObjectSet<T02_URDTL> T02_URDTL
        {
            get { return _t02_URDTL  ?? (_t02_URDTL = CreateObjectSet<T02_URDTL>("T02_URDTL")); }
        }
        private ObjectSet<T02_URDTL> _t02_URDTL;
    
        public ObjectSet<T02_URDTL_HAN> T02_URDTL_HAN
        {
            get { return _t02_URDTL_HAN  ?? (_t02_URDTL_HAN = CreateObjectSet<T02_URDTL_HAN>("T02_URDTL_HAN")); }
        }
        private ObjectSet<T02_URDTL_HAN> _t02_URDTL_HAN;
    
        public ObjectSet<T03_SRDTL> T03_SRDTL
        {
            get { return _t03_SRDTL  ?? (_t03_SRDTL = CreateObjectSet<T03_SRDTL>("T03_SRDTL")); }
        }
        private ObjectSet<T03_SRDTL> _t03_SRDTL;
    
        public ObjectSet<T03_SRDTL_HAN> T03_SRDTL_HAN
        {
            get { return _t03_SRDTL_HAN  ?? (_t03_SRDTL_HAN = CreateObjectSet<T03_SRDTL_HAN>("T03_SRDTL_HAN")); }
        }
        private ObjectSet<T03_SRDTL_HAN> _t03_SRDTL_HAN;
    
        public ObjectSet<S08_URIKAKE> S08_URIKAKE
        {
            get { return _s08_URIKAKE  ?? (_s08_URIKAKE = CreateObjectSet<S08_URIKAKE>("S08_URIKAKE")); }
        }
        private ObjectSet<S08_URIKAKE> _s08_URIKAKE;
    
        public ObjectSet<S09_KAIKAKE> S09_KAIKAKE
        {
            get { return _s09_KAIKAKE  ?? (_s09_KAIKAKE = CreateObjectSet<S09_KAIKAKE>("S09_KAIKAKE")); }
        }
        private ObjectSet<S09_KAIKAKE> _s09_KAIKAKE;
    
        public ObjectSet<T04_AGRDTB> T04_AGRDTB
        {
            get { return _t04_AGRDTB  ?? (_t04_AGRDTB = CreateObjectSet<T04_AGRDTB>("T04_AGRDTB")); }
        }
        private ObjectSet<T04_AGRDTB> _t04_AGRDTB;

        #endregion

    }
}
