using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CeleryApp
{
    internal class Animation
    {
        private Storyboard Storyboard;

        public Animation()
        {
            Storyboard = new Storyboard();

            // Set default easing function
            QuadraticEase quadraticEase = new QuadraticEase();
            quadraticEase.EasingMode = EasingMode.EaseInOut;
            Easing = quadraticEase;
        }

        public async void MoveAnimation(DependencyObject obj, Thickness from, Thickness to)
        {
            ThicknessAnimation animation = new ThicknessAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(1000),
                EasingFunction = Easing
            };

            Storyboard.SetTarget(animation, obj);
            Storyboard.SetTargetProperty(animation, new PropertyPath(FrameworkElement.MarginProperty));
            Storyboard.Children.Add(animation);
            Storyboard.Begin();

            await Task.Delay(100);

            Storyboard.Children.Remove(animation);
        }

        public async void TimedMoveAnimation(DependencyObject obj, Thickness from, Thickness to, double duration)
        {
            ThicknessAnimation animation = new ThicknessAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(duration),
                EasingFunction = Easing
            };

            Storyboard.SetTarget(animation, obj);
            Storyboard.SetTargetProperty(animation, new PropertyPath(FrameworkElement.MarginProperty));
            Storyboard.Children.Add(animation);
            Storyboard.Begin();

            await Task.Delay(100);

            Storyboard.Children.Remove(animation);
        }

        public async void WidthAnimation(DependencyObject obj, double to)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                To = to,
                EasingFunction = Easing
            };

            Storyboard.SetTarget(animation, obj);
            Storyboard.SetTargetProperty(animation, new PropertyPath(FrameworkElement.WidthProperty));
            Storyboard.Children.Add(animation);
            Storyboard.Begin();

            await Task.Delay(1000);

            Storyboard.Children.Remove(animation);
        }

        public async void HeightAnimation(DependencyObject obj, double to)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                To = to,
                EasingFunction = Easing
            };

            Storyboard.SetTarget(animation, obj);
            Storyboard.SetTargetProperty(animation, new PropertyPath(FrameworkElement.HeightProperty));
            Storyboard.Children.Add(animation);
            Storyboard.Begin();

            await Task.Delay(1000);

            Storyboard.Children.Remove(animation);
        }

        public async void OpacityAnimation(DependencyObject obj, double from, double to, double duration)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(duration),
                EasingFunction = Easing
            };

            Storyboard.SetTarget(animation, obj);
            Storyboard.SetTargetProperty(animation, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.Children.Add(animation);
            Storyboard.Begin();

            await Task.Delay(1000);

            Storyboard.Children.Remove(animation);
        }

        public async void WidthAnimation(DependencyObject obj, double from, double to, double duration)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(duration),
                EasingFunction = Easing
            };

            Storyboard.SetTarget(animation, obj);
            Storyboard.SetTargetProperty(animation, new PropertyPath(FrameworkElement.WidthProperty));
            Storyboard.Children.Add(animation);
            Storyboard.Begin();

            await Task.Delay(100);

            Storyboard.Children.Remove(animation);
        }

        public async void HeightAnimation(DependencyObject obj, double from, double to, double duration)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(duration),
                EasingFunction = Easing
            };

            Storyboard.SetTarget(animation, obj);
            Storyboard.SetTargetProperty(animation, new PropertyPath(FrameworkElement.HeightProperty));
            Storyboard.Children.Add(animation);
            Storyboard.Begin();

            await Task.Delay(1000);

            Storyboard.Children.Remove(animation);
        }

        public void Rotate(RotateTransform obj, double from, double to, double duration)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(duration),
                EasingFunction = Easing
            };

            obj.BeginAnimation(RotateTransform.AngleProperty, animation, HandoffBehavior.SnapshotAndReplace);
        }

        private IEasingFunction Easing { get; set; }
    }
}
