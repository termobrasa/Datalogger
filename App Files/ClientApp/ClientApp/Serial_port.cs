using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class Serial_port : Form
    {
        public static string com_port;
        public Serial_port()
        {
            InitializeComponent();
        }

        private void com_chose_btn_Click(object sender, EventArgs e)
        {

            com_port = listBox1.SelectedItem.ToString();
            this.Hide();
            var form1 = new Form1();
            form1.Closed += (s, args) => this.Close();
            form1.Show();

        }

        private void Serial_port_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();


            List<string> _items = new List<string>();

            foreach (string port in ports)
            {
                _items.Add(port);
            }

            listBox1.DataSource = _items;
        }
    }
}
