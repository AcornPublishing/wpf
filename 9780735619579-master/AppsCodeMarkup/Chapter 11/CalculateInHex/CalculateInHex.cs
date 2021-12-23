//-----------------------------------------------
// CalculateInHex.cs (c) 2006 by Charles Petzold
//-----------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Petzold.CalculateInHex
{
    public class CalculateInHex : Window
    {
        // Private fields.
        RoundedButton btnDisplay;
        ulong numDisplay;
        ulong numFirst;
        bool bNewNumber = true;
        char chOperation = '=';

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CalculateInHex());
        }
        // Constructor.
        public CalculateInHex()
        {
            Title = "Calculate in Hex";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;

            // Create Grid as content of window.
            Grid grid = new Grid();
            grid.Margin = new Thickness(4);
            Content = grid;

            // Create five columns.
            for (int i = 0; i < 5; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(col);
            }

            // Create seven rows.
            for (int i = 0; i < 7; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                grid.RowDefinitions.Add(row);
            }

            // Text to appear in buttons.
            string[] strButtons = { "0",
                                    "D", "E", "F", "+", "&",
                                    "A", "B", "C", "-", "|",
                                    "7", "8", "9", "*", "^",
                                    "4", "5", "6", "/", "<<",
                                    "1", "2", "3", "%", ">>", 
                                    "0", "Back", "Equals" };
            int iRow = 0, iCol = 0;

            // Create the buttons.
            foreach (string str in strButtons)
            {
                // Create RoundedButton.
                RoundedButton btn = new RoundedButton();
                btn.Focusable = false;
                btn.Height = 32;
                btn.Margin = new Thickness(4);
                btn.Click += ButtonOnClick;

                // Create TextBlock for Child of RoundedButton.
                TextBlock txt = new TextBlock();
                txt.Text = str;
                btn.Child = txt;

                // Add RoundedButton to Grid.
                grid.Children.Add(btn);
                Grid.SetRow(btn, iRow);
                Grid.SetColumn(btn, iCol);

                // Make an exception for the Display button.
                if (iRow == 0 && iCol == 0)
                {
                    btnDisplay = btn;
                    btn.Margin = new Thickness(4, 4, 4, 6);
                    Grid.SetColumnSpan(btn, 5);
                    iRow = 1;
                }

                // Also for Back and Equals.
                else if (iRow == 6 && iCol > 0)
                {
                    Grid.SetColumnSpan(btn, 2);
                    iCol += 2;
                }

                // For all other buttons.
                else
                {
                    btn.Width = 32;
                    if (0 == (iCol = (iCol + 1) % 5))
                        iRow++;
                }
            }
        }
        // Click event handler.
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            // Get the clicked button.
            RoundedButton btn = args.Source as RoundedButton;

            if (btn == null)
                return;

            // Get the button text and the first character.
            string strButton = (btn.Child as TextBlock).Text;
            char chButton = strButton[0];

            // Some special cases.
            if (strButton == "Equals")
                chButton = '=';

            if (btn == btnDisplay)
                numDisplay = 0;

            else if (strButton == "Back")
                numDisplay /= 16;

            // Hexadecimal digits.
            else if (Char.IsLetterOrDigit(chButton))
            {
                if (bNewNumber)
                {
                    numFirst = numDisplay;
                    numDisplay = 0;
                    bNewNumber = false;
                }
                if (numDisplay <= ulong.MaxValue >> 4)
                    numDisplay = 16 * numDisplay + (ulong)(chButton - 
                                    (Char.IsDigit(chButton) ? '0' : 'A' - 10));
            }
            // Operation.
            else
            {
                if (!bNewNumber)
                {
                    switch (chOperation)
                    {
                        case '=': break;
                        case '+': numDisplay = numFirst + numDisplay; break;
                        case '-': numDisplay = numFirst - numDisplay; break;
                        case '*': numDisplay = numFirst * numDisplay; break;
                        case '&': numDisplay = numFirst & numDisplay; break;
                        case '|': numDisplay = numFirst | numDisplay; break;
                        case '^': numDisplay = numFirst ^ numDisplay; break;
                        case '<': numDisplay = numFirst << (int)numDisplay; break;
                        case '>': numDisplay = numFirst >> (int)numDisplay; break;
                        case '/':
                            numDisplay = 
                                numDisplay != 0 ? numFirst / numDisplay : 
                                                  ulong.MaxValue;
                            break;
                        case '%':
                            numDisplay = 
                                numDisplay != 0 ? numFirst % numDisplay : 
                                                  ulong.MaxValue;
                            break;
                        default: numDisplay = 0; break;
                    }
                }
                bNewNumber = true;
                chOperation = chButton;
            }
            // Format display.
            TextBlock text = new TextBlock();
            text.Text = String.Format("{0:X}", numDisplay);
            btnDisplay.Child = text;
        }
        protected override void OnTextInput(TextCompositionEventArgs args)
        {
            base.OnTextInput(args);

            if (args.Text.Length == 0)
                return;

            // Get character input.
            char chKey = Char.ToUpper(args.Text[0]);

            // Loop through buttons.
            foreach (UIElement child in (Content as Grid).Children)
            {
                RoundedButton btn = child as RoundedButton;
                string strButton = (btn.Child as TextBlock).Text;

                // Messy logic to check for matching button.
                if ((chKey == strButton[0] && btn != btnDisplay &&
                        strButton != "Equals" && strButton != "Back") ||
                    (chKey == '=' && strButton == "Equals") ||
                    (chKey == '\r' && strButton == "Equals") ||
                    (chKey == '\b' && strButton == "Back") ||
                    (chKey == '\x1B' && btn == btnDisplay))
                {
                    // Simulate Click event to process keystroke.
                    RoutedEventArgs argsClick = 
                        new RoutedEventArgs(RoundedButton.ClickEvent, btn);
                    btn.RaiseEvent(argsClick);

                    // Make the button appear as if it's pressed.
                    btn.IsPressed = true;

                    // Set timer to unpress button.
                    DispatcherTimer tmr = new DispatcherTimer();
                    tmr.Interval = TimeSpan.FromMilliseconds(100);
                    tmr.Tag = btn;
                    tmr.Tick += TimerOnTick;
                    tmr.Start();

                    args.Handled = true;
                } 
            }
        }
        void TimerOnTick(object sender, EventArgs args)
        {
            // Unpress button.
            DispatcherTimer tmr = sender as DispatcherTimer;
            RoundedButton btn = tmr.Tag as RoundedButton;
            btn.IsPressed = false;

            // Turn off timer and remove event handler. 
            tmr.Stop();
            tmr.Tick -= TimerOnTick;
        }
    }
}
