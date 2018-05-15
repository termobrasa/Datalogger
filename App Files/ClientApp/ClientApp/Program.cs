using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Xls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;

namespace ClientApp
{
    static class Program
    {
        static int scroll_pox;
        public static string [] id = new string[16]  ;

        static string path = Path.GetFullPath(Environment.CurrentDirectory);
        static string databaseName = "client.mdf";
        public static SqlConnection cnx = new SqlConnection(@"Data Source=(localdb)\v11.0;AttachDbFilename="+path+@"\"+databaseName+";Integrated Security=True");
        public static SqlCommand cmd;
        public static SqlDataReader dr;
        public static DataTable dt;
        

        public static void OpenCnx()
        {
            try
            {
                if (cnx.State != ConnectionState.Open)
                    cnx.Open();
            }
            
            ///
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel abrir coneção");
                Form1.LogError(ex);
            }
        
          
            
        }

        public static void CloseCnx()
        {
            try
            {
                if (cnx.State != ConnectionState.Closed)
                    cnx.Close();
            }


            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel fechar coneção");
                Form1.LogError(ex);
            }
            
        }

        public static void Excute(string req)
        {
            //try
           // {
                cmd = new SqlCommand(req, cnx);
                OpenCnx();                
                cmd.ExecuteNonQuery();
                CloseCnx();
           // }


            /*catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel executar");
                Form1.LogError(ex);
            }*/

            
        }

        public static SqlDataReader FillDataReader(string req)
        {
            cmd = new SqlCommand(req, cnx);
            OpenCnx();
            dr = cmd.ExecuteReader();
            return dr;
            
        }

        public static void FillTable(string req)
        {
            try
            {
                OpenCnx();
                cmd = new SqlCommand(req, cnx);
                dr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);

                CloseCnx();
            }


            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel preencher tabela");
                Form1.LogError(ex);
            }
            
        }

        public static void Fillchart(System.Windows.Forms.DataVisualization.Charting.Chart
            d,string req)
        {
            try
            {
                OpenCnx();
                cmd = new SqlCommand(req, cnx);
                dr = cmd.ExecuteReader();
                foreach (var series in d.Series)
                {
                    series.Points.Clear();
                }
                while (dr.Read())
                {

                    d.Series["T_água_baixo"].Points.AddXY(dr.GetString(0), dr.GetDouble(1));                    
                    d.Series["T_água_cima"].Points.AddXY(dr.GetString(0), dr.GetDouble(2));
                    d.Series["Entrada_perm"].Points.AddXY(dr.GetString(0), dr.GetDouble(3));
                    d.Series["Saída_perm"].Points.AddXY(dr.GetString(0), dr.GetDouble(4));
                    d.Series["Antes_válvula"].Points.AddXY(dr.GetString(0), dr.GetDouble(5));
                    d.Series["Depois_válvula"].Points.AddXY(dr.GetString(0), dr.GetDouble(6));
                    d.Series["Entrada_compr"].Points.AddXY(dr.GetString(0), dr.GetDouble(7));
                    d.Series["Saida_ar"].Points.AddXY(dr.GetString(0), dr.GetDouble(8));
                    d.Series["Entrada_ar"].Points.AddXY(dr.GetString(0), dr.GetDouble(9));
                    d.Series["T_água2"].Points.AddXY(dr.GetString(0), dr.GetDouble(10));
                    d.Series["Entrada_perm2"].Points.AddXY(dr.GetString(0), dr.GetDouble(11));
                    d.Series["Saída_perm2"].Points.AddXY(dr.GetString(0), dr.GetDouble(12));
                    d.Series["Entrada_compr2"].Points.AddXY(dr.GetString(0), dr.GetDouble(13));
                    d.Series["Saida_válvula2"].Points.AddXY(dr.GetString(0), dr.GetDouble(14));
                    d.Series["Entrada_ar2"].Points.AddXY(dr.GetString(0), dr.GetDouble(15));
                    d.Series["Fluxo"].Points.AddXY(dr.GetString(0), dr.GetDouble(16));
                }

                CloseCnx();
            }


            catch (IOException ex)
            {
                MessageBox.Show("Não foi possivel preencher grafico");
                Form1.LogError(ex);
            }

            
        }

        public static void FillDataGridView(DataGridView d,string req)
        {
            try
            {
                FillTable(req);
                d.DataSource = dt;
                d.HorizontalScrollingOffset = scroll_pox;
            }


            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel preencher tabela");
                Form1.LogError(ex);
            }
            
            

        }

        public static void chart(string     req)
        {
            try
            {
                OpenCnx();
                cmd = new SqlCommand(req, cnx);
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                CloseCnx();
            }


            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel preencher grafico");
                Form1.LogError(ex);
            }

        }

        public static void createtable(string req)
        {
            OpenCnx();
            cmd = new SqlCommand(req, cnx);
            dr = cmd.ExecuteReader();
            do
            {
                int count = dr.FieldCount;
                int cnt = 0;
                while (dr.Read())
                {
                    
                    for (int i = 0; i < count; i++)
                    {

                        
                        id[cnt] = (dr.GetValue(0).ToString());
                        
                        cnt++;
                    }
                }
            } while (dr.NextResult());
            

            CloseCnx();
        }


        
        public static void datatable(string req, ListBox c)
        {
            OpenCnx();
            cmd = new SqlCommand(req, cnx);
            dr = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            c.DataSource = dt;
            c.DisplayMember = "TABLE_NAME";
            c.ValueMember = "TABLE_NAME";


            CloseCnx();

        }
            
        

        public static void Scrollpossition(int req)
        {
            scroll_pox = req;
        }


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Serial_port());
        }
    }
}
