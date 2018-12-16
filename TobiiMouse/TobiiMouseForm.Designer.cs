using Tobii.Interaction;
using System;

namespace TobiiMouse
{
    partial class TobiiMouseForm
    {
        GazePointDataStream _stream;

        public TobiiMouseForm(Host host)
        {
            _stream = host.Streams.CreateGazePointDataStream();
            // TODO: Exit the program
            if (_stream == null)
            {
                return;
            }

            _stream.GazePoint(OnGazeData);
        }

        void OnGazeData(double x, double y, double timestamp)
        {
            Console.WriteLine("X: {0} Y:{1}", x, y);
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion
    }
}

