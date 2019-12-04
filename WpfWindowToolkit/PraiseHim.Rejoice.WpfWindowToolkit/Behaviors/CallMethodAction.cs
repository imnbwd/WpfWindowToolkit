using Microsoft.Xaml.Behaviors;
using System;
using System.Reflection;
using System.Windows;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Behaviors
{
    /// <summary>
    /// An action that calls a method on a specified object when invoked.
    /// </summary>
    public class CallMethodAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// Identifies the <seealso cref="MethodName"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MethodNameProperty = DependencyProperty.Register(
            "MethodName",
            typeof(string),
            typeof(CallMethodAction),
            new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <seealso cref="TargetObject"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register(
            "TargetObject",
            typeof(object),
            typeof(CallMethodAction),
            new PropertyMetadata(null));

        private Type _targetObjectType;

        /// <summary>
        /// Gets or sets the name of the method to invoke. This is a dependency property.
        /// </summary>
        public string MethodName
        {
            get { return (string)this.GetValue(CallMethodAction.MethodNameProperty); }
            set { this.SetValue(CallMethodAction.MethodNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the object that exposes the method of interest. This is a dependency property.
        /// </summary>
        public object TargetObject
        {
            get { return this.GetValue(CallMethodAction.TargetObjectProperty); }
            set { this.SetValue(CallMethodAction.TargetObjectProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            object target;
            if (this.ReadLocalValue(CallMethodAction.TargetObjectProperty) != DependencyProperty.UnsetValue)
            {
                target = this.TargetObject;
            }
            else
            {
                target = this.AssociatedObject;
            }

            if (target == null || string.IsNullOrEmpty(this.MethodName))
            {
                return;
            }

            var targetMethod = target.GetType().GetMethod(MethodName);

            if (targetMethod == null)
            {
                throw new Exception("Cannot find the target method on the specified object");
            }

            var parameters = targetMethod.GetParameters();
            if (parameters.Length == 0)
            {
                targetMethod.Invoke(target, null);
            }
            else if (parameters.Length == 1)
            {
                if (parameters[0].ParameterType == parameter.GetType())
                {
                    targetMethod.Invoke(target, new object[] { parameter });
                }
                else
                {
                    throw new TargetInvocationException("The parameter type of the target method is unexpected", null);
                }
            }
            else
            {
                throw new TargetParameterCountException("The number of parameters of the target method is out of range");
            }
        }
    }
}