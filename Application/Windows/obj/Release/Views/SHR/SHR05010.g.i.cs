﻿#pragma checksum "..\..\..\..\Views\SHR\SHR05010.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D8EB61754926D9DD658484828CD6EA07CC1B958A"
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
    /// SHR05010
    /// </summary>
    public partial class SHR05010 : KyoeiSystem.Framework.Windows.ViewBase.WindowReportBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\Views\SHR\SHR05010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Application.Windows.Views.SHR05010 MainWindow;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\..\Views\SHR\SHR05010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox myCompany;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\..\Views\SHR\SHR05010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox CreateYearMonth;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\..\..\Views\SHR\SHR05010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox ClosingDate;
        
        #line default
        #line hidden
        
        
        #line 166 "..\..\..\..\Views\SHR\SHR05010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox isClosingAllDays;
        
        #line default
        #line hidden
        
        
        #line 173 "..\..\..\..\Views\SHR\SHR05010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox Customer;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\..\..\Views\SHR\SHR05010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox CreateType;
        
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
            System.Uri resourceLocater = new System.Uri("/KyoeiSystem.Application.Windows;component/views/shr/shr05010.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\SHR\SHR05010.xaml"
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
            this.MainWindow = ((KyoeiSystem.Application.Windows.Views.SHR05010)(target));
            
            #line 21 "..\..\..\..\Views\SHR\SHR05010.xaml"
            this.MainWindow.Loaded += new System.Windows.RoutedEventHandler(this.RibbonWindow_Loaded);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\..\Views\SHR\SHR05010.xaml"
            this.MainWindow.Closed += new System.EventHandler(this.MainWindow_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 59 "..\..\..\..\Views\SHR\SHR05010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 72 "..\..\..\..\Views\SHR\SHR05010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 83 "..\..\..\..\Views\SHR\SHR05010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 95 "..\..\..\..\Views\SHR\SHR05010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 105 "..\..\..\..\Views\SHR\SHR05010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 107 "..\..\..\..\Views\SHR\SHR05010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 109 "..\..\..\..\Views\SHR\SHR05010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 111 "..\..\..\..\Views\SHR\SHR05010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.myCompany = ((KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox)(target));
            return;
            case 11:
            this.CreateYearMonth = ((KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox)(target));
            return;
            case 12:
            this.ClosingDate = ((KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox)(target));
            
            #line 162 "..\..\..\..\Views\SHR\SHR05010.xaml"
            this.ClosingDate.cTextChanged += new System.Windows.RoutedEventHandler(this.ClosingDate_TextChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            this.isClosingAllDays = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 14:
            this.Customer = ((KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox)(target));
            return;
            case 15:
            this.CreateType = ((KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

