﻿#pragma checksum "..\..\..\..\Views\SCH\SCH07010.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "51A46CAB4FF6AA0CA06B556772704EFA"
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
    /// SCH07010
    /// </summary>
    public partial class SCH07010 : KyoeiSystem.Framework.Windows.ViewBase.WindowMasterSearchBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\Views\SCH\SCH07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Application.Windows.Views.SCH07010 MinWindow;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Views\SCH\SCH07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox txtName;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Views\SCH\SCH07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox txtKana;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Views\SCH\SCH07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox txtCD;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Views\SCH\SCH07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox OrderColumn;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Views\SCH\SCH07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelButton;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Views\SCH\SCH07010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OkButton;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\Views\SCH\SCH07010.xaml"
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
            System.Uri resourceLocater = new System.Uri("/KyoeiSystem.Application.Windows;component/views/sch/sch07010.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\SCH\SCH07010.xaml"
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
            this.MinWindow = ((KyoeiSystem.Application.Windows.Views.SCH07010)(target));
            
            #line 12 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.MinWindow.Loaded += new System.Windows.RoutedEventHandler(this.MinWindow_Loaded_1);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.MinWindow.Closed += new System.EventHandler(this.MainWindow_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtName = ((KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox)(target));
            
            #line 41 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.txtName.cTextChanged += new System.Windows.RoutedEventHandler(this.txtKana_cTextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtKana = ((KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox)(target));
            
            #line 42 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.txtKana.cTextChanged += new System.Windows.RoutedEventHandler(this.txtKana_cTextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtCD = ((KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox)(target));
            
            #line 44 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.txtCD.cTextChanged += new System.Windows.RoutedEventHandler(this.txtKana_cTextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.OrderColumn = ((KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox)(target));
            
            #line 55 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.OrderColumn.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.OrderColumn_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.CancelButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.CancelButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 66 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.CancelButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.CancelButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 66 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.CancelButton.Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.OkButton = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.OkButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.OkButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 73 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.OkButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.OkButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 73 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.OkButton.Click += new System.Windows.RoutedEventHandler(this.OkButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.SearchGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 82 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.SearchGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.UcGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 82 "..\..\..\..\Views\SCH\SCH07010.xaml"
            this.SearchGrid.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.SearchGrid_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

