using System;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace xptv.Behaviors
{
    public class MediaElementRetryBehavior : Behavior<MediaElement>
    {
        public static readonly BindableProperty RetryCommandProperty = BindableProperty.Create(
            nameof(RetryCommand),
            typeof(ICommand),
            typeof(MediaElementRetryBehavior)
        );

        public ICommand RetryCommand
        {
            get => (ICommand)GetValue(RetryCommandProperty);
            set => SetValue(RetryCommandProperty, value);
        }

        protected override void OnAttachedTo(MediaElement bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.MediaFailed += OnMediaFailed;
        }

        protected override void OnDetachingFrom(MediaElement bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.MediaFailed -= OnMediaFailed;
        }

        private void OnMediaFailed(object sender, EventArgs e)
        {
            if (RetryCommand?.CanExecute(null) == true)
            {
                RetryCommand.Execute(null);
            }
        }
    }
}
