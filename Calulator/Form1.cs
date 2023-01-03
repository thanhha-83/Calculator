using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calulator
{
    public partial class Form1 : Form
    {
        private Double result = 0;
        private string operation = string.Empty;
        private string fstNum, secNum;
        private bool enterValue = false;
        private bool isEmptyHistory = true;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        ///
        /// Handling the window messages
        ///
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnMathOperation_Click(object sender, EventArgs e)
        {
            if (result != 0)
            {
                BtnEquals.PerformClick();
            } else
            {
                result = Double.Parse(TxtDisplay1.Text);
            }
            Button button = (Button)sender;
            operation = button.Text;
            enterValue = true;
            if (TxtDisplay1.Text != "0")
            {
                TxtDisplay2.Text = fstNum = $"{result} {operation}";
                TxtDisplay1.Text = string.Empty;
            }
        }

        private void BtnEquals_Click(object sender, EventArgs e)
        {
            secNum = TxtDisplay1.Text;
            TxtDisplay2.Text = $"{TxtDisplay2.Text} {TxtDisplay1.Text} =";
            if (TxtDisplay1.Text != string.Empty)
            {
                if (TxtDisplay1.Text != "0")
                {
                    TxtDisplay2.Text = string.Empty;
                }
                if (isEmptyHistory)
                {
                    RtBoxDisplayHistory.Text = "";
                    isEmptyHistory = false;
                }
                switch (operation)
                {
                    case "+":
                        TxtDisplay1.Text = (result + Double.Parse(TxtDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {secNum} = {TxtDisplay1.Text}\n");
                        break;
                    case "-":
                        TxtDisplay1.Text = (result - Double.Parse(TxtDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {secNum} = {TxtDisplay1.Text}\n");
                        break;
                    case "×":
                        TxtDisplay1.Text = (result * Double.Parse(TxtDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {secNum} = {TxtDisplay1.Text}\n");
                        break;
                    case "÷":
                        TxtDisplay1.Text = (result / Double.Parse(TxtDisplay1.Text)).ToString();
                        RtBoxDisplayHistory.AppendText($"{fstNum} {secNum} = {TxtDisplay1.Text}\n");
                        break;
                    default:
                        TxtDisplay2.Text = $"{TxtDisplay1.Text} = ";
                        RtBoxDisplayHistory.AppendText($"{fstNum} {secNum} = {TxtDisplay1.Text}\n");
                        break;
                }
                result = Double.Parse(TxtDisplay1.Text);
                operation = string.Empty;
            }
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            PnlHistory.Height = (PnlHistory.Height == 5) ? 335 : 5;
        }

        private void BtnClearHistory_Click(object sender, EventArgs e)
        {
            RtBoxDisplayHistory.Clear();
            if (RtBoxDisplayHistory.Text == string.Empty)
            {
                RtBoxDisplayHistory.Text = "There's no history yet";
                isEmptyHistory = true;
            }
        }

        private void BtnBackspace_Click(object sender, EventArgs e)
        {
            if (TxtDisplay1.Text.Length > 0)
            {
                TxtDisplay1.Text = TxtDisplay1.Text.Remove(TxtDisplay1.Text.Length -1, 1);
            }
            if (TxtDisplay1.Text == string.Empty)
            {
                TxtDisplay1.Text = "0";
            }
        }

        private void BtnC_Click(object sender, EventArgs e)
        {
            TxtDisplay1.Text = "0";
            TxtDisplay2.Text = string.Empty;
            result = 0;
        }

        private void BtnCE_Click(object sender, EventArgs e)
        {
            TxtDisplay1.Text = "0";
        }

        private void BtnOperation_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operation = button.Text;
            if (isEmptyHistory)
            {
                RtBoxDisplayHistory.Text = "";
                isEmptyHistory = false;
            }
            switch (operation)
            {
                case "√":
                    TxtDisplay2.Text = $"√({TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(Math.Sqrt(Double.Parse(TxtDisplay1.Text)));
                    break;
                case "^2":
                    TxtDisplay2.Text = $"({TxtDisplay1.Text})^2";
                    TxtDisplay1.Text = Convert.ToString(Double.Parse(TxtDisplay1.Text) * Double.Parse(TxtDisplay1.Text));
                    break;
                case "1/x":
                    TxtDisplay2.Text = $"1/({TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(1.0 / Double.Parse(TxtDisplay1.Text));
                    break;
                case "%":
                    TxtDisplay2.Text = $"%({TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(Double.Parse(TxtDisplay1.Text) / 100.0);
                    break;
                case "±":
                    TxtDisplay1.Text = Convert.ToString(-1 * Double.Parse(TxtDisplay1.Text));
                    break;

            }
            RtBoxDisplayHistory.AppendText($"{TxtDisplay2.Text} = {TxtDisplay1.Text}\n");
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }


        private void BtnNum_Click(object sender, EventArgs e)
        {
            if (TxtDisplay1.Text == "0" || enterValue)
            {
                TxtDisplay1.Text = string.Empty;
            }
            enterValue = false;
            Button button = (Button) sender;
            if (button.Text == ".")
            {
                if (!TxtDisplay1.Text.Contains("."))
                {
                    TxtDisplay1.Text = TxtDisplay1.Text + button.Text;
                }
            } else
            {
                TxtDisplay1.Text = TxtDisplay1.Text + button.Text;
            }
        }
    }
}
