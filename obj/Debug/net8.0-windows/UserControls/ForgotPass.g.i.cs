﻿#pragma checksum "..\..\..\..\UserControls\ForgotPass.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1177D7010C539F3D67C35C7DAFF06BDF62762C93"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Human_Resources_Management_System.UserControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Human_Resources_Management_System.UserControls {
    
    
    /// <summary>
    /// ForgotPass
    /// </summary>
    public partial class ForgotPass : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\UserControls\ForgotPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush BackgroundPicture;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\UserControls\ForgotPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image LogoImage;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\UserControls\ForgotPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox emailTextBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\UserControls\ForgotPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox otpTextbox;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\UserControls\ForgotPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox newpasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\UserControls\ForgotPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox confirmNewPassword;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Human Resources Management System;component/usercontrols/forgotpass.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\ForgotPass.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.BackgroundPicture = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 2:
            this.LogoImage = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.emailTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.otpTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 49 "..\..\..\..\UserControls\ForgotPass.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Send_OTPBtn);
            
            #line default
            #line hidden
            return;
            case 6:
            this.newpasswordTextBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 7:
            this.confirmNewPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 8:
            
            #line 71 "..\..\..\..\UserControls\ForgotPass.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BackForgotPass_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 72 "..\..\..\..\UserControls\ForgotPass.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveForgotPass_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

