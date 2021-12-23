//-----------------------------------------------
// AnimatedCircle.cs (c) 2006 by Charles Petzold
//-----------------------------------------------
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Petzold.RenderTheAnimation
{
    class AnimatedCircle : FrameworkElement
    {
        protected override void OnRender(DrawingContext dc)
        {
            DoubleAnimation anima = new DoubleAnimation();
            anima.From = 0;
            anima.To = 100;
            anima.Duration = new Duration(TimeSpan.FromSeconds(1));
            anima.AutoReverse = true;
            anima.RepeatBehavior = RepeatBehavior.Forever;
            AnimationClock clock = anima.CreateClock();

            dc.DrawEllipse(Brushes.Blue, new Pen(Brushes.Red, 3),
                new Point(125, 125), null, 0, clock, 0, clock);
        }
    }
}