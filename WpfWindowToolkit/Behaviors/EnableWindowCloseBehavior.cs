using Microsoft.Xaml.Behaviors;
using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System;
using System.Windows;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Behaviors
{
    /// <summary>
    /// A behavior that enables to close a window in its view model, that view model should implement <see cref="IClosable"/> interface, and invoke CloseWindow action in a proper place.
    /// </summary>
    [TypeConstraint(typeof(Window))]
    public class EnableWindowCloseBehavior : Behavior<Window>
    {
        // See this answer for more info:
        // https://stackoverflow.com/questions/4376475/wpf-mvvm-how-to-close-a-window#answer-17392170

        /// <summary>
        /// When attached
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
            this.AssociatedObject.DataContextChanged += AssociatedObject_DataContextChanged;
        }

        /// <summary>
        /// When detaching
        /// </summary>
        protected override void OnDetaching()
        {
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            this.AssociatedObject.DataContextChanged -= AssociatedObject_DataContextChanged;
            base.OnDetaching();
        }

        /// <summary>
        /// Assign close action for CloseWindow action.
        /// </summary>
        private void AssignCloseWindowAction()
        {
            if (AssociatedObject.DataContext is IClosable)
            {
                (AssociatedObject.DataContext as IClosable).CloseWindow = new Action(AssociatedObject.Close);
            }
        }

        private void AssociatedObject_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            AssignCloseWindowAction();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            AssignCloseWindowAction();
        }
    }
}