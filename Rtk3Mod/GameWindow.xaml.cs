using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Rtk3Mod
{
    public partial class GameWindow : Window
    {
        public static State state;
        public static State nextState;
        private static bool endGameThread = false;
        public static GameKey key = GameKey.None;
        public static GameWindow instance;

        public GameWindow()
        {
            InitializeComponent();
            this.Loaded += GameWindowLoaded;
            this.Closed += GameWindow_Closed;
            instance = this;
        }

        private void GameWindow_Closed(object sender, EventArgs e)
        {
            endGameThread = true;
        }

        private void GameWindowLoaded(object sender, RoutedEventArgs e)
        {
            Gv.c.LoadData();
            Gv.c.AddPlayer("曹操");
            Gv.Init();

            SetWindowSize();
            this.KeyDown += MainWindow_KeyDown;
            DisableMouseInput();

            SetNextState(Gv.turnControllerState);

            new Thread(GameThread).Start();
        }

        private void DisableMouseInput()
        {
            this.PreviewMouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { e.Handled = true; };
            this.PreviewMouseLeftButtonUp += (object sender, MouseButtonEventArgs e) => { e.Handled = true; };
            this.PreviewMouseRightButtonDown += (object sender, MouseButtonEventArgs e) => { e.Handled = true; };
            this.PreviewMouseRightButtonUp += (object sender, MouseButtonEventArgs e) => { e.Handled = true; };
            this.PreviewMouseDoubleClick += (object sender, MouseButtonEventArgs e) => { e.Handled = true; };
            this.PreviewMouseMove += (object sender, MouseEventArgs e) => { e.Handled = true; };
            this.PreviewMouseWheel += (object sender, MouseWheelEventArgs e) => { e.Handled = true; };
        }

        private void SetWindowSize()
        {
            Width = 502 + SystemParameters.ResizeFrameVerticalBorderWidth * 2;
            Height = 436 + SystemParameters.WindowCaptionHeight + SystemParameters.ResizeFrameVerticalBorderWidth * 2;
            ResizeMode = ResizeMode.CanMinimize;
        }

        public static void HandleKey(KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                key = GameKey.Left;
            else if (e.Key == Key.Right)
                key = GameKey.Right;
            else if (e.Key == Key.Up)
                key = GameKey.Up;
            else if (e.Key == Key.Down)
                key = GameKey.Down;
            else if (e.Key == Key.F)
                key = GameKey.Forward;
            else if (e.Key == Key.D)
                key = GameKey.Backward;
            else
                key = GameKey.None;
        }

        private static void GameThread()
        {
            while (!endGameThread)
            {
                if (nextState != null)
                {
                    if (state != null)
                        state.ExitState();

                    state = nextState;
                    SetNextState(null);
                    state.EnterState();
                }

                state.Update(key);
                key = GameKey.None;
                Thread.Sleep(50);
            }
        }

        public static void SetNextState(State state)
        {
            nextState = state;
        }

        public static void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKey(e);
        }
    }

    public enum GameKey
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Backward,
        None
    }
}
