#pragma checksum "..\..\OrdersAccounting.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F9F122C6396E5DFAF00951FEE38BF8D9AB9F22D4F13C33DE730242719EFCF71C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using SuperPizzeria;
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


namespace SuperPizzeria {
    
    
    /// <summary>
    /// OrdersAccounting
    /// </summary>
    public partial class OrdersAccounting : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\OrdersAccounting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox Orders;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\OrdersAccounting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SuggestSale;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\OrdersAccounting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AccountOrders;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\OrdersAccounting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Sales;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\OrdersAccounting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SalesLabel;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\OrdersAccounting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Spravka;
        
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
            System.Uri resourceLocater = new System.Uri("/SuperPizzeria;component/ordersaccounting.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\OrdersAccounting.xaml"
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
            this.Orders = ((System.Windows.Controls.ListBox)(target));
            
            #line 12 "..\..\OrdersAccounting.xaml"
            this.Orders.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Orders_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SuggestSale = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\OrdersAccounting.xaml"
            this.SuggestSale.Click += new System.Windows.RoutedEventHandler(this.SuggestSale_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AccountOrders = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\OrdersAccounting.xaml"
            this.AccountOrders.Click += new System.Windows.RoutedEventHandler(this.AccountOrders_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Sales = ((System.Windows.Controls.ComboBox)(target));
            
            #line 79 "..\..\OrdersAccounting.xaml"
            this.Sales.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Sales_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SalesLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.Spravka = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\OrdersAccounting.xaml"
            this.Spravka.Click += new System.Windows.RoutedEventHandler(this.Spravka_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

