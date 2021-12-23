//---------------------------------------------
// JeuDeTacquin.cs (c) 2006 by Charles Petzold
//---------------------------------------------
using Petzold.PlayJeuDeTacquin;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Petzold.JeuDeTacquin
{
    public partial class JeuDeTacquin : Page
    {
        public int NumberRows = 4;
        public int NumberCols = 4;
        bool isLoaded = false;

        int xEmpty, yEmpty, iCounter;
        Key[] keys = { Key.Left, Key.Right, Key.Up, Key.Down };
        Random rand;
        UIElement elEmptySpare = new Empty();

        public JeuDeTacquin()
        {
            Loaded += PageOnLoaded;
            InitializeComponent();
        }
        void PageOnLoaded(object sender, RoutedEventArgs args)
        {
            if (!isLoaded)
            {
                Title = String.Format("Jeu de Tacquin ({0}\x00D7{1})", 
                                      NumberCols, NumberRows);

                unigrid.Rows = NumberRows;
                unigrid.Columns = NumberCols;

                // Create Tile objects to fill all but one cell.
                for (int i = 0; i < NumberRows * NumberCols - 1; i++)
                {
                    Tile tile = new Tile();
                    tile.Text = (i + 1).ToString();
                    tile.MouseLeftButtonDown += TileOnMouseLeftButtonDown; ;
                    unigrid.Children.Add(tile);
                }
                // Create Empty object to fill the last cell.
                unigrid.Children.Add(new Empty());
                xEmpty = NumberCols - 1;
                yEmpty = NumberRows - 1;

                isLoaded = true;
            }
            Focus();
        }
        void TileOnMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            Focus();

            Tile tile = sender as Tile;

            int iMove = unigrid.Children.IndexOf(tile);
            int xMove = iMove % NumberCols;
            int yMove = iMove / NumberCols;

            if (xMove == xEmpty)
                while (yMove != yEmpty)
                    MoveTile(xMove, yEmpty + (yMove - yEmpty) / 
                                        Math.Abs(yMove - yEmpty));
            if (yMove == yEmpty)
                while (xMove != xEmpty)
                    MoveTile(xEmpty + (xMove - xEmpty) / 
                                        Math.Abs(xMove - xEmpty), yMove);
            args.Handled = true;
        }
        protected override void OnKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);

            switch (args.Key)
            {
                case Key.Right: MoveTile(xEmpty - 1, yEmpty);  break;
                case Key.Left:  MoveTile(xEmpty + 1, yEmpty);  break;
                case Key.Down:  MoveTile(xEmpty, yEmpty - 1);  break;
                case Key.Up:    MoveTile(xEmpty, yEmpty + 1);  break;
                default:
                    return;
            }
            args.Handled = true;
        }
        void ScrambleOnClick(object sender, RoutedEventArgs args)
        {
            rand = new Random();
            iCounter = 16 * NumberCols * NumberRows;

            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromMilliseconds(10);
            tmr.Tick += TimerOnTick;
            tmr.Start();
        }
        void TimerOnTick(object sender, EventArgs args)
        {
            for (int i = 0; i < 5; i++)
            {
                MoveTile(xEmpty, yEmpty + rand.Next(3) - 1);
                MoveTile(xEmpty + rand.Next(3) - 1, yEmpty);
            }
            if (0 == iCounter--)
                (sender as DispatcherTimer).Stop();
        }
        void MoveTile(int xTile, int yTile)
        {
            if ((xTile == xEmpty && yTile == yEmpty) ||
                xTile < 0 || xTile >= NumberCols ||
                yTile < 0 || yTile >= NumberRows)
                return;

            int iTile = NumberCols * yTile + xTile;
            int iEmpty = NumberCols * yEmpty + xEmpty;

            UIElement elTile = unigrid.Children[iTile];
            UIElement elEmpty = unigrid.Children[iEmpty];

            unigrid.Children.RemoveAt(iTile);
            unigrid.Children.Insert(iTile, elEmptySpare);
            unigrid.Children.RemoveAt(iEmpty);
            unigrid.Children.Insert(iEmpty, elTile);

            xEmpty = xTile;
            yEmpty = yTile;
            elEmptySpare = elEmpty;
        }
        void NextOnClick(object sender, RoutedEventArgs args)
        {
            if (!NavigationService.CanGoForward)
            {
                JeuDeTacquin page = new JeuDeTacquin();
                page.NumberRows = NumberRows + 1;
                page.NumberCols = NumberCols + 1;

                NavigationService.Navigate(page);
            }
            else
                NavigationService.GoForward();
        }
    }
}
