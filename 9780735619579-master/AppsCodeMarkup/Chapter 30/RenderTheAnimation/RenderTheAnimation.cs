//---------------------------------------------------
// RenderTheAnimation.cs (c) 2006 by Charles Petzold
//---------------------------------------------------
using System;
using System.Windows;

namespace Petzold.RenderTheAnimation
{
    class RenderTheAnimation : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RenderTheAnimation());
        }
        public RenderTheAnimation()
        {
            Title = "Render the Animation";
            Content = new AnimatedCircle();
        }
    }
}
