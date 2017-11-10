using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Behaviors
{
    /// <summary>
    /// A trigger that fires when scrolling to a specific element in a ScrollViewer
    /// </summary>
    public class ScrollingTrigger : TriggerBase<ScrollViewer>
    {
        public static readonly DependencyProperty TargetElementProperty =
            DependencyProperty.Register("TargetElement", typeof(FrameworkElement), typeof(ScrollingTrigger), new PropertyMetadata(null));

        /// <summary>
        /// The target element to scroll to
        /// </summary>
        public FrameworkElement TargetElement
        {
            get { return (FrameworkElement)GetValue(TargetElementProperty); }
            set { SetValue(TargetElementProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ScrollChanged -= AssociatedObject_ScrollChanged;
            AssociatedObject.ScrollChanged += AssociatedObject_ScrollChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.ScrollChanged -= AssociatedObject_ScrollChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // get the current vertical offset of the ScrollViewer
            var currentScrollPosition = AssociatedObject.VerticalOffset;
            var point = new Point(0, currentScrollPosition);

            var targetBound = TargetElement.TransformToVisual(AssociatedObject).TransformBounds(new Rect(0, currentScrollPosition,
                TargetElement.ActualWidth, TargetElement.ActualHeight));

            if (currentScrollPosition >= targetBound.Y && currentScrollPosition <= targetBound.Y + targetBound.Height)
            {
                InvokeActions(null);
            }
        }
    }
}