using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Behaviors
{
    /// <summary>
    /// An action to close the specific TabItem of the specific TabControl
    /// Source: stackoverflow
    /// </summary>
    public class CloseTabItemAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty ClosingCheckFuncProperty =
            DependencyProperty.Register("ClosingCheckFunc", typeof(Func<bool>), typeof(CloseTabItemAction), new PropertyMetadata(null));

        public static readonly DependencyProperty TabControlProperty =
                    DependencyProperty.Register(nameof(TabControl), typeof(TabControl), typeof(CloseTabItemAction), new PropertyMetadata(default(TabControl)));

        public static readonly DependencyProperty TabItemProperty =
            DependencyProperty.Register(nameof(TabItem), typeof(TabItem), typeof(CloseTabItemAction), new PropertyMetadata(default(TabItem)));

        public Func<bool> ClosingCheckFunc
        {
            get { return (Func<bool>)GetValue(ClosingCheckFuncProperty); }
            set { SetValue(ClosingCheckFuncProperty, value); }
        }

        public TabControl TabControl
        {
            get { return (TabControl)this.GetValue(TabControlProperty); }
            set { this.SetValue(TabControlProperty, value); }
        }

        public TabItem TabItem
        {
            get { return (TabItem)this.GetValue(TabItemProperty); }
            set { this.SetValue(TabItemProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            var tabControl = this.TabControl;
            var tabItem = this.TabItem;
            if (tabControl == null || tabItem == null)
            {
                return;
            }

            // TODO Raise a closing event to cancel this action
            if (ClosingCheckFunc != null && !ClosingCheckFunc())
            {
                return;
            }

            if (tabControl.ItemsSource == null)
            {
                // if the list is hard-coded (i.e. has no ItemsSource)
                // then we remove the item from the collection
                tabControl.Items.Remove(tabItem);
            }
            else
            {
                // if ItemsSource is something we cannot work with, bail out
                var collection = tabControl.ItemsSource as IList;
                if (collection == null)
                {
                    return;
                }

                // find the item and kill it (I mean, remove it)
                var item2Remove = collection.OfType<object>().FirstOrDefault(item => tabItem == item || tabItem.DataContext == item);
                if (item2Remove != null)
                {
                    collection.Remove(item2Remove);
                }
            }
        }
    }
}