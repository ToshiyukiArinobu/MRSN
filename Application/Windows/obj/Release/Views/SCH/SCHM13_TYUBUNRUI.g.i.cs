﻿#pragma checksum "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2169CD41AC615FFFF9B304A2BF9CA3392C43B4AF"
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
    /// SCHM13_TYUBUNRUI
    /// </summary>
    public partial class SCHM13_TYUBUNRUI : KyoeiSystem.Framework.Windows.ViewBase.WindowMasterSearchBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Application.Windows.Views.SCHM13_TYUBUNRUI MainWindow;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox txtKana;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox txtCD;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OkButton;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelButton;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
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
            System.Uri resourceLocater = new System.Uri("/KyoeiSystem.Application.Windows;component/views/sch/schm13_tyubunrui.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
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
            this.MainWindow = ((KyoeiSystem.Application.Windows.Views.SCHM13_TYUBUNRUI)(target));
            
            #line 14 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.MainWindow.Loaded += new System.Windows.RoutedEventHandler(this.MainWindow_Loaded);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.MainWindow.Closed += new System.EventHandler(this.MainWindow_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtKana = ((KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox)(target));
            
            #line 49 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.txtKana.cTextChanged += new System.Windows.RoutedEventHandler(this.txtKana_cTextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtCD = ((KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox)(target));
            
            #line 57 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.txtCD.cTextChanged += new System.Windows.RoutedEventHandler(this.txtKana_cTextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.OkButton = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.OkButton.Click += new System.Windows.RoutedEventHandler(this.OkButton_Click);
            
            #line default
            #line hidden
            
            #line 65 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.OkButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.OkButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 65 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.OkButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.OkButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.CancelButton.Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            
            #line 77 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.CancelButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.CancelButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 77 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.CancelButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.CancelButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SearchGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 90 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.SearchGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.UcGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 90 "..\..\..\..\Views\SCH\SCHM13_TYUBUNRUI.xaml"
            this.SearchGrid.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.SearchGrid_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

