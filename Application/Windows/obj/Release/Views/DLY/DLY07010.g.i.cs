﻿#pragma checksum "..\..\..\..\Views\DLY\DLY07010.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D39FC75356C925D2250B5DA10CB272D6F99267CC"
//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

using GrapeCity.Windows.SpreadGrid;
using GrapeCity.Windows.SpreadGrid.Editors;
using GrapeCity.Windows.SpreadGrid.Presenters;
using KyoeiSystem.Application.Windows.Views;
using KyoeiSystem.Framework.Windows.Controls;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace KyoeiSystem.Application.Windows.Views {
    
    
    /// <summary>
    /// DLY07010
    /// </summary>
    public partial class DLY07010 : KyoeiSystem.Framework.Windows.ViewBase.RibbonWindowViewBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 133 "..\..\..\..\Views\DLY\DLY07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grdHeader;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\..\Views\DLY\DLY07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FuncTitle;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\..\Views\DLY\DLY07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox ProductCode;
        
        #line default
        #line hidden
        
        
        #line 164 "..\..\..\..\Views\DLY\DLY07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox Subcontractor;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\..\Views\DLY\DLY07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GrapeCity.Windows.SpreadGrid.GcSpreadGrid gcSpreadGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/KyoeiSystem.Application.Windows;component/views/dly/dly07010.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\DLY\DLY07010.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 18 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((KyoeiSystem.Application.Windows.Views.DLY07010)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((KyoeiSystem.Application.Windows.Views.DLY07010)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 48 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 52 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 57 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 64 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 68 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 78 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 82 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 86 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 97 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 99 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 101 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 103 "..\..\..\..\Views\DLY\DLY07010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.grdHeader = ((System.Windows.Controls.Grid)(target));
            return;
            case 15:
            this.FuncTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 16:
            this.ProductCode = ((KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox)(target));
            
            #line 160 "..\..\..\..\Views\DLY\DLY07010.xaml"
            this.ProductCode.LostFocus += new System.Windows.RoutedEventHandler(this.TwinTextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 17:
            this.Subcontractor = ((KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox)(target));
            return;
            case 18:
            this.gcSpreadGrid = ((GrapeCity.Windows.SpreadGrid.GcSpreadGrid)(target));
            
            #line 183 "..\..\..\..\Views\DLY\DLY07010.xaml"
            this.gcSpreadGrid.CellEditEnded += new System.EventHandler<GrapeCity.Windows.SpreadGrid.SpreadCellEditEndedEventArgs>(this.gcSpredGrid_CellEditEnded);
            
            #line default
            #line hidden
            
            #line 183 "..\..\..\..\Views\DLY\DLY07010.xaml"
            this.gcSpreadGrid.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.gcSpredGrid_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 183 "..\..\..\..\Views\DLY\DLY07010.xaml"
            this.gcSpreadGrid.RowCollectionChanged += new System.EventHandler<GrapeCity.Windows.SpreadGrid.SpreadCollectionChangedEventArgs>(this.gcSpreadGrid_RowCollectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

