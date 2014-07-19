using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Home.TrackLoader.AccessToken
{
    internal static class WebBrowserExtensions
    {
        public static readonly DependencyProperty SourceUriProperty = DependencyProperty.RegisterAttached(
            "SourceUri", typeof (Uri), typeof (WebBrowserExtensions),
            new PropertyMetadata(default(Uri), onPropertyChanged));

        private static void onPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browser = (WebBrowser) d;
            var newUri = (Uri) e.NewValue;
            if (browser.Source == newUri)
            {
                return;
            }
            browser.Source = newUri;
            browser.Navigated -= onNavigated;
            browser.Navigated += onNavigated;
        }

        private static void onNavigated(object sender, NavigationEventArgs navigationEventArgs)
        {
            var browser = (WebBrowser) sender;
            SetSourceUri(browser, navigationEventArgs.Uri);
        }

        public static void SetSourceUri(DependencyObject element, Uri value)
        {
            element.SetValue(SourceUriProperty, value);
        }

        public static Uri GetSourceUri(DependencyObject element)
        {
            return (Uri) element.GetValue(SourceUriProperty);
        }
    }
}