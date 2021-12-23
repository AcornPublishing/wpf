//----------------------------------------------
// NavigationBar.cs (c) 2006 by Charles Petzold
//----------------------------------------------
using System;
using System.Collections;               // for IList.
using System.Collections.Specialized;   // for NotifyCollectionChangedEventArgs.
using System.ComponentModel;            // for ICollectionView.
using System.Reflection;                // for ConstructorInfo.
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Petzold.DataEntry
{
    public partial class NavigationBar : ToolBar
    {
        IList coll;
        ICollectionView collview;
        Type typeItem;

        // Public constructor.
        public NavigationBar()
        {
            InitializeComponent();
        }

        // Public properties.
        public IList Collection
        {
            set
            {
                coll = value;

                // Create CollectionView and set event handlers.
                collview = CollectionViewSource.GetDefaultView(coll);
                collview.CurrentChanged += CollectionViewOnCurrentChanged;
                collview.CollectionChanged += CollectionViewOnCollectionChanged;

                // Call an event handler to initialize TextBox and Buttons.
                CollectionViewOnCurrentChanged(null, null);

                // Initialize TextBlock.
                txtblkTotal.Text = coll.Count.ToString();
            }
            get
            {
                return coll;
            }
        }

        // This is the type of the item in the collection. 
        // It's used for the Add command.
        public Type ItemType
        {
            set { typeItem = value; }
            get { return typeItem; }
        }
      
        // Event handlers for CollectionView.
        void CollectionViewOnCollectionChanged(object sender, 
                                        NotifyCollectionChangedEventArgs args)
        {
            txtblkTotal.Text = coll.Count.ToString();
        }

        void CollectionViewOnCurrentChanged(object sender, EventArgs args)
        {
            txtboxCurrent.Text = (1 + collview.CurrentPosition).ToString();
            btnPrev.IsEnabled = collview.CurrentPosition > 0;
            btnNext.IsEnabled = collview.CurrentPosition < coll.Count - 1;
            btnDel.IsEnabled = coll.Count > 1;
        }

        // Event handlers for buttons.
        void FirstOnClick(object sender, RoutedEventArgs args)
        {
            collview.MoveCurrentToFirst();
        }
        void PreviousOnClick(object sender, RoutedEventArgs args)
        {
            collview.MoveCurrentToPrevious();
        }
        void NextOnClick(object sender, RoutedEventArgs args)
        {
            collview.MoveCurrentToNext();
        }
        void LastOnClick(object sender, RoutedEventArgs args)
        {
            collview.MoveCurrentToLast();
        }
        void AddOnClick(object sender, RoutedEventArgs args)
        {
            ConstructorInfo info = 
                typeItem.GetConstructor(System.Type.EmptyTypes);
            coll.Add(info.Invoke(null));
            collview.MoveCurrentToLast();
        }
        void DeleteOnClick(object sender, RoutedEventArgs args)
        {
            coll.RemoveAt(collview.CurrentPosition);
        }

        // Event handlers for txtboxCurrent TextBox.
        string strOriginal;

        void TextBoxOnGotFocus(object sender, 
                               KeyboardFocusChangedEventArgs args)
        {
            strOriginal = txtboxCurrent.Text;
        }
        void TextBoxOnLostFocus(object sender, 
                                KeyboardFocusChangedEventArgs args)
        {
            int current;

            if (Int32.TryParse(txtboxCurrent.Text, out current))
                if (current > 0 && current <= coll.Count)
                    collview.MoveCurrentToPosition(current - 1);
            else
                txtboxCurrent.Text = strOriginal;
        }
        void TextBoxOnKeyDown(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Escape)
            {
                txtboxCurrent.Text = strOriginal;
                args.Handled = true;
            }
            else if (args.Key == Key.Enter)
            {
                args.Handled = true;
            }
            else
                return;

            MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
        }
    }
}
