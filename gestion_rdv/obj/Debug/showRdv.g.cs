﻿#pragma checksum "..\..\showRdv.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "744EC3EECB8ABA1501A71585E97561C5E1067652"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace gestion_rdv {
    
    
    /// <summary>
    /// showRdv
    /// </summary>
    public partial class showRdv : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ggrid;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label date;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer appscroll;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel appointementStack;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label nameLabel;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox hour;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox minute;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox description;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ajouterRDV;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label annulerAjout;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\showRdv.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label addNewLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/gestion_rdv;component/showrdv.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\showRdv.xaml"
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
            this.ggrid = ((System.Windows.Controls.Grid)(target));
            
            #line 12 "..\..\showRdv.xaml"
            this.ggrid.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.closeButton = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\showRdv.xaml"
            this.closeButton.Click += new System.Windows.RoutedEventHandler(this.closeButton_Click);
            
            #line default
            #line hidden
            
            #line 16 "..\..\showRdv.xaml"
            this.closeButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.closeButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 16 "..\..\showRdv.xaml"
            this.closeButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.closeButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.date = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.appscroll = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 5:
            this.appointementStack = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            
            #line 29 "..\..\showRdv.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseEnter);
            
            #line default
            #line hidden
            
            #line 29 "..\..\showRdv.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 7:
            this.nameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.hour = ((System.Windows.Controls.TextBox)(target));
            
            #line 39 "..\..\showRdv.xaml"
            this.hour.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.increment_KeyDown);
            
            #line default
            #line hidden
            
            #line 39 "..\..\showRdv.xaml"
            this.hour.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.time_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 40 "..\..\showRdv.xaml"
            this.hour.LostFocus += new System.Windows.RoutedEventHandler(this.time_LostFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.minute = ((System.Windows.Controls.TextBox)(target));
            
            #line 44 "..\..\showRdv.xaml"
            this.minute.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.increment_KeyDown);
            
            #line default
            #line hidden
            
            #line 44 "..\..\showRdv.xaml"
            this.minute.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.time_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 45 "..\..\showRdv.xaml"
            this.minute.LostFocus += new System.Windows.RoutedEventHandler(this.time_LostFocus);
            
            #line default
            #line hidden
            return;
            case 10:
            this.description = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.ajouterRDV = ((System.Windows.Controls.Label)(target));
            
            #line 52 "..\..\showRdv.xaml"
            this.ajouterRDV.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ajouterRDV_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 12:
            this.annulerAjout = ((System.Windows.Controls.Label)(target));
            
            #line 55 "..\..\showRdv.xaml"
            this.annulerAjout.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.annulerAjout_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 13:
            this.addNewLabel = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
