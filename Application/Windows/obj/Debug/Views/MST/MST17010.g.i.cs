﻿#pragma checksum "..\..\..\..\Views\MST\MST17010.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FF162F262A9C3DD728C28A51C4EC02F5"
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
    /// MST17010
    /// </summary>
    public partial class MST17010 : KyoeiSystem.Framework.Windows.ViewBase.WindowReportBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\Views\MST\MST17010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Application.Windows.Views.MST17010 MainWindow;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\..\Views\MST\MST17010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox LabelTextTokuisaki;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\..\Views\MST\MST17010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BeforeIdButton;
        
        #line default
        #line hidden
        
        
        #line 164 "..\..\..\..\Views\MST\MST17010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NextIdButton;
        
        #line default
        #line hidden
        
        
        #line 166 "..\..\..\..\Views\MST\MST17010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Csv_Output;
        
        #line default
        #line hidden
        
        
        #line 167 "..\..\..\..\Views\MST\MST17010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Csv_Input;
        
        #line default
        #line hidden
        
        
        #line 177 "..\..\..\..\Views\MST\MST17010.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GrapeCity.Windows.SpreadGrid.GcSpreadGrid gcSpreadGrid1;
        
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
            System.Uri resourceLocater = new System.Uri("/KyoeiSystem.Application.Windows;component/views/mst/mst17010.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\MST\MST17010.xaml"
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
            this.MainWindow = ((KyoeiSystem.Application.Windows.Views.MST17010)(target));
            
            #line 11 "..\..\..\..\Views\MST\MST17010.xaml"
            this.MainWindow.Loaded += new System.Windows.RoutedEventHandler(this.RibbonWindow_Loaded_1);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\..\Views\MST\MST17010.xaml"
            this.MainWindow.Closed += new System.EventHandler(this.MainWindow_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 53 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 57 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 61 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 65 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 69 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 77 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 79 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 81 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 83 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 92 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 94 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 96 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 104 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 106 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 108 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 110 "..\..\..\..\Views\MST\MST17010.xaml"
            ((System.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            this.LabelTextTokuisaki = ((KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox)(target));
            
            #line 161 "..\..\..\..\Views\MST\MST17010.xaml"
            this.LabelTextTokuisaki.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Preview_KeyDown);
            
            #line default
            #line hidden
            return;
            case 19:
            this.BeforeIdButton = ((System.Windows.Controls.Button)(target));
            
            #line 163 "..\..\..\..\Views\MST\MST17010.xaml"
            this.BeforeIdButton.Click += new System.Windows.RoutedEventHandler(this.BeforeIdButton_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            this.NextIdButton = ((System.Windows.Controls.Button)(target));
            
            #line 164 "..\..\..\..\Views\MST\MST17010.xaml"
            this.NextIdButton.Click += new System.Windows.RoutedEventHandler(this.NextIdButton_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            this.Csv_Output = ((System.Windows.Controls.Button)(target));
            
            #line 166 "..\..\..\..\Views\MST\MST17010.xaml"
            this.Csv_Output.Click += new System.Windows.RoutedEventHandler(this.OutPut);
            
            #line default
            #line hidden
            return;
            case 22:
            this.Csv_Input = ((System.Windows.Controls.Button)(target));
            
            #line 167 "..\..\..\..\Views\MST\MST17010.xaml"
            this.Csv_Input.Click += new System.Windows.RoutedEventHandler(this.InPut);
            
            #line default
            #line hidden
            return;
            case 23:
            this.gcSpreadGrid1 = ((GrapeCity.Windows.SpreadGrid.GcSpreadGrid)(target));
            
            #line 180 "..\..\..\..\Views\MST\MST17010.xaml"
            this.gcSpreadGrid1.CellEditEnded += new System.EventHandler<GrapeCity.Windows.SpreadGrid.SpreadCellEditEndedEventArgs>(this.gcSpreadGrid1_CellEditEnded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

