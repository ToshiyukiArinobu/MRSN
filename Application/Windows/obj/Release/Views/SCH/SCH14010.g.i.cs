﻿#pragma checksum "..\..\..\..\Views\SCH\SCH14010.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F5DAA9C58F308FA1717576E29E973C4BC172AC5C"
//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

using KyoeiSystem.Framework.Windows.Controls;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
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
    /// SCH14010
    /// </summary>
    public partial class SCH14010 : KyoeiSystem.Framework.Windows.ViewBase.WindowMasterSearchBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\Views\SCH\SCH14010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Application.Windows.Views.SCH14010 MainWindow;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Views\SCH\SCH14010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox txtName;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\Views\SCH\SCH14010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelButton;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\Views\SCH\SCH14010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OkButton;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\Views\SCH\SCH14010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid SearchGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/KyoeiSystem.Application.Windows;component/views/sch/sch14010.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\SCH\SCH14010.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.MainWindow = ((KyoeiSystem.Application.Windows.Views.SCH14010)(target));
            
            #line 12 "..\..\..\..\Views\SCH\SCH14010.xaml"
            this.MainWindow.Loaded += new System.Windows.RoutedEventHandler(this.MainWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 21 "..\..\..\..\Views\SCH\SCH14010.xaml"
            ((System.Windows.Controls.Grid)(target)).PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Grid_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtName = ((KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox)(target));
            return;
            case 4:
            
            #line 49 "..\..\..\..\Views\SCH\SCH14010.xaml"
            ((KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox)(target)).cTextChanged += new System.Windows.RoutedEventHandler(this.txtKana_cTextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\..\Views\SCH\SCH14010.xaml"
            this.CancelButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.CancelButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 58 "..\..\..\..\Views\SCH\SCH14010.xaml"
            this.CancelButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.CancelButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 58 "..\..\..\..\Views\SCH\SCH14010.xaml"
            this.CancelButton.Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.OkButton = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\..\..\Views\SCH\SCH14010.xaml"
            this.OkButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.OkButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 65 "..\..\..\..\Views\SCH\SCH14010.xaml"
            this.OkButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.OkButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 65 "..\..\..\..\Views\SCH\SCH14010.xaml"
            this.OkButton.Click += new System.Windows.RoutedEventHandler(this.OkButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SearchGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 78 "..\..\..\..\Views\SCH\SCH14010.xaml"
            this.SearchGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.UcGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 78 "..\..\..\..\Views\SCH\SCH14010.xaml"
            this.SearchGrid.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.SearchGrid_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

