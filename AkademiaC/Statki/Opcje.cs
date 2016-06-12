using System;
using System.Windows.Input;

namespace Statki
{
    class Opcje
    {
        public static readonly RoutedUICommand NewGame = new RoutedUICommand(
            "_Nowa Gra", "Nowa Gra", typeof(Opcje),
            new InputGestureCollection { new KeyGesture(Key.F2) });
        public static readonly RoutedUICommand AutomatedGame = new RoutedUICommand(
            "_Gra Automatyczna", "Gra Automatyczna", typeof(Opcje),
            new InputGestureCollection { new KeyGesture(Key.F5) });
        public static readonly RoutedUICommand Exit = new RoutedUICommand(
            "_Wyjscie", "Wyjscie", typeof(Opcje),
            new InputGestureCollection { new KeyGesture(Key.F4, ModifierKeys.Alt) });
    }
}
