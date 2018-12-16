using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.Interaction;

using Rec = System.Drawing.Rectangle;
using Vec2 = System.Numerics.Vector2;

namespace TobiiMouse
{
    public partial class TobiiMouseForm : Form
    {
        // Weight used for exponetial moving average
        // https://en.wikipedia.org/wiki/Moving_average#Exponential_moving_average
        private const float MovingAverageWeight = 0.1f;

        GlobalKeyboardHook _keyboardHook = new GlobalKeyboardHook();
        GazePointDataStream _stream;

        // TODO: Don't hardcode these
        Keys _moveKey = Keys.F12;
        int _warpPointRadius = 50;

        bool _enableDebugLogs = false;
        bool _moveKeyPressed = false;
        Vec2 _curPos;
        Vec2 _lastWarpPoint;

        public TobiiMouseForm(Host host)
        {
            InitializeComponent();

            _stream = host.Streams.CreateGazePointDataStream();
            // TODO: Exit the program
            if (_stream == null)
            {
                return;
            }

            _stream.GazePoint(OnGazeData);
            _keyboardHook.KeyboardPressed += OnKeyPressed;
        }

        void OnGazeData(double x, double y, double timestamp)
        {
            _curPos.X = MovingAverageWeight * (float)x + (1 - MovingAverageWeight) * _curPos.X;
            _curPos.Y = MovingAverageWeight * (float)y + (1 - MovingAverageWeight) * _curPos.Y;

            if (Vec2.Distance(_curPos, _lastWarpPoint) > _warpPointRadius)
            {
                _lastWarpPoint = _curPos;
            }

            if (_enableDebugLogs)
            {
                Console.WriteLine("New Position: ({0},{1})", _curPos.X, _curPos.Y);
            }
            if (_moveKeyPressed)
            {
                SetMousePosition();
            }
        }

        void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardData.VirtualCode == (int)_moveKey)
            {
                if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
                {
                    if (!_moveKeyPressed)
                    {
                        _moveKeyPressed = true;
                        SetMousePosition();
                    }
                }
                else
                {
                    _moveKeyPressed = false;
                }
            }
        }

        void SetMousePosition()
        {
            Cursor.Position = new Point((int)_lastWarpPoint.X, (int)_lastWarpPoint.Y);
        }
    }
}
