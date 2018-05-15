using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Spire.Xls;
using Spire.Xls.Charts;



namespace ClientApp
{
    /// <summary>
    /// primeirapaina
    /// </summary>
    public partial class Form1 : Form
    {
        
        string data;
        string address;
        string temp;
        string resol;
        int idsensore ,cnt;
        string  flow, tempdata2, tempdata3, tempdata4, tempdata1, tempdata5, tempdata7, tempdata8, tempdata9, tempdata10, tempdata11, tempdata12, tempdata13, tempdata14, tempdata15;
        string tempdata6;
        float a = 22;
        String path = Path.GetDirectoryName(Application.ExecutablePath);
        /// <summary>
        /// Inicializa componentes
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            serialPort1.PortName = (Serial_port.com_port);
           
            serialPort1.Open();
            serialPort1.DtrEnable = true;
            serialPort1.RtsEnable = true;
        }
        /// <summary>
        /// Inicializa componentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            chart_setup();
            

            string q = "select * from lista_sensores";
            Program.FillDataGridView(dataGridView1, q);
        }

       
                    /// <summary>
                    /// Para lista de sensores
                    /// </summary>
                    /// <param name="sender"></param>
                    /// <param name="e"></param>
        private void stop_btn_Click(object sender, EventArgs e)
        {
            try
            {
                timer_lista_sensores.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Já esta parado");
                LogError(ex);
            }
            
        }
        /// <summary>
        /// Inicia lista de sensores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_btn_Click(object sender, EventArgs e)
        {
            try
            {
                timer_lista_sensores.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel iniciar");
                LogError(ex);
            }
        }
        /// <summary>
        /// Pede informação dos sensores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_lista_sensores_Tick(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("T");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel receber dados");
                LogError(ex);
            }
        }
        /// <summary>
        /// adiciona na base de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string q3 = "insert into lista_sensores (ID,Nickname) values('" + int.Parse(textBox1.Text) + "','" + textBox2.Text + "')";
                string q2 = "select * from lista_Sensores";
                //string q3 = "ALTER TABLE lista_sensores ADD ID int NOT NULL PRIMARY KEY ";
                Program.Excute(q3);
                Program.FillDataGridView(dataGridView1, q2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel adicionar");
                LogError(ex);
            }

        }
        /// <summary>
        /// altera na base de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_btn_Click(object sender, EventArgs e)
        {

            try
            {
                string q3 = "update lista_sensores set Nickname='" + textBox2.Text + "' where ID='" + textBox1.Text + "'";
                string q2 = "select * from lista_Sensores";
                Program.Excute(q3);
                Program.FillDataGridView(dataGridView1, q2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel modificar");
                LogError(ex);
            }
        }
        /// <summary>
        /// elimina na base de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_btn_Click(object sender, EventArgs e)
        {

            try
            {
                string q3 = "delete lista_sensores where ID ='" + textBox1.Text + "'";
                string q2 = "select * from lista_Sensores";
                Program.Excute(q3);
                Program.FillDataGridView(dataGridView1, q2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel remover");
                LogError(ex);
            }

        }

        private void update_sensor_table()
        {
            try
            {
                string q1 = "update lista_sensores set Endereço='" + address + "' , Date= '" + DateTime.Now + "', Temperatura='" + temp + "',Resolução='" + resol + "' where ID ='" + idsensore + "'";
                string q2 = "select * from lista_Sensores";
                Program.Excute(q1);
                Program.FillDataGridView(dataGridView1, q2);
            }
            catch (Exception ex)
            {
                LogError(ex);
                MessageBox.Show("não foi possivel atuatualizar tabela");
            }

        }

      

        private void stop2_btn_Click(object sender, EventArgs e)
        {
            try
            {
                timer_novo_teste.Stop();
            }
           
            catch (Exception ex)
            {
                MessageBox.Show("Já esta parado");
                LogError(ex);
            }
        }

        private void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            int Scroll_possition = (dataGridView2.HorizontalScrollingOffset);
            Program.Scrollpossition(Scroll_possition);
        }

        private void table_name_Validated(object sender, EventArgs e)
        {
            table_name.Enabled = false;
        }

        private void timer_novo_teste_Tick(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write("A");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel receber dados");
                LogError(ex);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {

                string q = "SELECT Nickname From lista_sensores";
                Program.createtable(q);
                //string q1 = "CREATE TABLE " + table_name.Text + "(Date float)";

                string q1 = "CREATE TABLE " + table_name.Text + "(Date varchar(50), " + Program.id[0]+ " float, " + Program.id[1] + " float, " + Program.id[2] + " float, " + Program.id[3] + " float, " + Program.id[4] + " float, " + Program.id[5] + " float, " + Program.id[6] + " float, " + Program.id[7] + " float, " + Program.id[8] + " float, " + Program.id[9] + " float, " + Program.id[10] + " float, " + Program.id[11] + " float, " + Program.id[12] + " float, " + Program.id[13] + " float, " + Program.id[14] + " float , Flow float)";
                string q2 = "select * from " + table_name.Text + "";
                Program.Excute(q1);
                Program.FillDataGridView(dataGridView2, q2);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Não foi possivel criar nova tabela, escolha outro nome");
                LogError(ex);
            }

        }

        private void tempo_amostra_btn_Click(object sender, EventArgs e)
        {
            try
            {
                timer_novo_teste.Interval = int.Parse(tempo_amostra.Text) * 1000;
                timer_novo_teste.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("numero invalido");
                LogError(ex);
            }

        }

        private void table_update()
        {
            try
            {
                
                

                Console.WriteLine(tempdata6);
                string q1 = "INSERT into " + table_name.Text + " (Date,"+Program.id[0]+ "," + Program.id[1] + "," + Program.id[2] + "," + Program.id[3] + "," + Program.id[4] + "," + Program.id[5] + "," + Program.id[6] + "," + Program.id[7] + "," + Program.id[8] + "," + Program.id[9] + "," + Program.id[10] + "," + Program.id[11] + "," + Program.id[12] + "," + Program.id[13] + "," + Program.id[14] + ",Flow) values('" + DateTime.Now + "'," + tempdata1 + "," + tempdata2 + "," + tempdata3 + "," + tempdata4 + "," + tempdata5 + ","+tempdata6+"," + tempdata7 + "," + tempdata8 + "," + tempdata9 + "," + tempdata10 + "," + tempdata11 + "," + tempdata12 + "," + tempdata13 + "," + tempdata14 + "," + tempdata15 + "," + flow + ")";
                
                string q2 = "select * from " + table_name.Text + "";
                Program.Excute(q1);
                Program.FillDataGridView(dataGridView2, q2);

                string q = "select * from " + table_name.Text + "";
                Program.Fillchart(chart1, q);
            }
            catch (IOException ex)
            {
                LogError(ex);
                MessageBox.Show("não foi possivel atuatualizar tabela");
            }

        }

        private void save_excel_Click(object sender, EventArgs e)
        {

            save_Excel();

        }
       

        
        void save_Excel()
        {
            
            print_chart.save_image(chart1, panel1, table_name.Text);
            string c = path + "\\" + table_name.Text + "\\" + table_name.Text + ".xls";
            

            try
            {
                SqlCommand command = new SqlCommand();

                command.CommandText = "select * from " + table_name.Text + "";
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command.CommandText, Program.cnx))
                {
                    DataTable t = new DataTable();
                    dataAdapter.Fill(t);

                    Workbook book = new Workbook();

                    Worksheet sheet = book.Worksheets[0];

                    sheet.InsertDataTable(t, true, 1, 1);
                    

                    Spire.Xls.Chart chart = sheet.Charts.Add(ExcelChartType.Line);
                    //chart.DataRange = sheet.Range["B1:B700"].AddCombinedRange(sheet.Range["C1:C700"]); //.AddCombinedRange(sheet.Range["D2:D200"]).AddCombinedRange(sheet.Range["E2:E200"]).AddCombinedRange(sheet.Range["I2:I200"]).AddCombinedRange(sheet.Range["J2:J200"]);

                    chart.SeriesDataFromRange = false;
                    chart.TopRow = 7;
                    chart.LeftColumn = 3;
                   
                    chart.Width = 43200 / int.Parse(tempo_amostra.Text) * 4000 / 200+4000;
                    chart.Height = 500;
                    chart.PlotArea.ForeGroundColor = System.Drawing.Color.White;
                    chart.ChartTitle = "Chart with Data Table";                    
                    chart.ChartTitleArea.IsBold = true;
                    chart.ChartTitleArea.Size = 12;

                   

                   
                    var cs1 = (ChartSerie)chart.Series.Add();                   
                    cs1.Name = sheet.Range["B1"].Value;                    
                    cs1.CategoryLabels = sheet.Range["A2:A700"];
                    cs1.Values = sheet.Range["B2:B700"];                    
                    cs1.SerieType = ExcelChartType.Line;

                    var cs2 = (ChartSerie)chart.Series.Add();                    
                    cs2.Name = sheet.Range["C1"].Value;                    
                    cs2.CategoryLabels = sheet.Range["A2:A700"];
                    cs2.Values = sheet.Range["C2:C700"];
                    cs2.SerieType = ExcelChartType.Line;

                    var cs3 = (ChartSerie)chart.Series.Add();
                    cs3.Name = sheet.Range["D1"].Value;
                    cs3.CategoryLabels = sheet.Range["A2:A700"];
                    cs3.Values = sheet.Range["D2:D700"];
                    cs3.SerieType = ExcelChartType.Line;

                    var cs4 = (ChartSerie)chart.Series.Add();
                    cs4.Name = sheet.Range["E1"].Value;
                    cs4.CategoryLabels = sheet.Range["A2:A700"];
                    cs4.Values = sheet.Range["E2:E700"];
                    cs4.SerieType = ExcelChartType.Line;

                    var cs5 = (ChartSerie)chart.Series.Add();
                    cs5.Name = sheet.Range["I1"].Value;
                    cs5.CategoryLabels = sheet.Range["A2:A700"];
                    cs5.Values = sheet.Range["I2:I700"];
                    cs5.SerieType = ExcelChartType.Line;

                    var cs6 = (ChartSerie)chart.Series.Add();
                    cs6.Name = sheet.Range["J1"].Value;
                    cs6.CategoryLabels = sheet.Range["A2:A700"];
                    cs6.Values = sheet.Range["J2:J700"];
                    cs6.SerieType = ExcelChartType.Line;


                    book.SaveToFile(c, ExcelVersion.Version97to2003);

                    MessageBox.Show("guardado com sucesso");

                    //System.Diagnostics.Process.Start(c);

                }
            }
            catch (IOException ex)
            {
                LogError(ex);
                MessageBox.Show("não foi possivel guardar no excel");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            try
            {
                serialPort1.Write(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Escolher o modelo");
                LogError(ex);
            }
        }

       

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (T_água_baixo_che.Checked)
            {
                chart1.Series["T_água_baixo"].Enabled = true;           
               
            }
            else
            {
                chart1.Series["T_água_baixo"].Enabled = false;
            }
        }

        private void T_água_cima_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (T_água_cima_che.Checked)
            {
                chart1.Series["T_água_cima"].Enabled = true;
            }
            else
            {
                chart1.Series["T_água_cima"].Enabled = false;
            }
        }

        private void Entrada_perm_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Entrada_perm_che.Checked)
            {
                chart1.Series["Entrada_perm"].Enabled = true;
            }
            else
            {
                chart1.Series["Entrada_perm"].Enabled = false;
            }
        }

        private void Saída_perm_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Saída_perm_che.Checked)
            {
                chart1.Series["Saída_perm"].Enabled = true;
            }
            else
            {
                chart1.Series["Saída_perm"].Enabled = false;
            }
        }

        private void Antes_válvula_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Antes_válvula_che.Checked)
            {
                chart1.Series["Antes_válvula"].Enabled = true;
            }
            else
            {
                chart1.Series["Antes_válvula"].Enabled = false;
            }
        }

        private void Depois_válvula_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Depois_válvula_che.Checked)
            {
                chart1.Series["Depois_válvula"].Enabled = true;
            }
            else
            {
                chart1.Series["Depois_válvula"].Enabled = false;
            }
        }

        private void Entrada_compr_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Entrada_compr_che.Checked)
            {
                chart1.Series["Entrada_compr"].Enabled = true;
            }
            else
            {
                chart1.Series["Entrada_compr"].Enabled = false;
            }
        }

        private void Saída_ar_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Saída_ar_che.Checked)
            {
                chart1.Series["Saida_ar"].Enabled = true;
            }
            else
            {
                chart1.Series["Saida_ar"].Enabled = false;
            }
        }


        private void Entrada_ar_che_CheckedChanged(object sender, EventArgs e)
        {
           
            if (Entrada_ar_che.Checked)
            {
                chart1.Series["Entrada_ar"].Enabled = true;
            }
            else
            {
                chart1.Series["Entrada_ar"].Enabled = false;
            }
        }

        private void T_água2_che_CheckedChanged(object sender, EventArgs e)
        {
           
            if (T_água2_che.Checked)
            {
                chart1.Series["T_água2"].Enabled = true;
            }
            else
            {
                chart1.Series["T_água2"].Enabled = false;
            }
        }

        private void Entrada_perm2_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Entrada_perm2_che.Checked)
            {
                chart1.Series["Entrada_perm2"].Enabled = true;
            }
            else
            {
                chart1.Series["Entrada_perm2"].Enabled = false;
            }
        }

        private void Saída_perm2_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Saída_perm2_che.Checked)
            {
                chart1.Series["Saída_perm2"].Enabled = true;
            }
            else
            {
                chart1.Series["Saída_perm2"].Enabled = false;
            }
        }

        private void Entrada_compr2_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Entrada_compr2_che.Checked)
            {
                chart1.Series["Entrada_compr2"].Enabled = true;
            }
            else
            {
                chart1.Series["Entrada_compr2"].Enabled = false;
            }
        }

        private void Saida_válvula2_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Saida_válvula2_che.Checked)
            {
                chart1.Series["Saida_válvula2"].Enabled = true;
            }
            else
            {
                chart1.Series["Saida_válvula2"].Enabled = false;
            }
        }

        private void Entrada_ar2_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Entrada_ar2_che.Checked)
            {
                chart1.Series["Entrada_ar2"].Enabled = true;
            }
            else
            {
                chart1.Series["Entrada_ar2"].Enabled = false;
            }
        }

        private void Fluxo_che_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Fluxo_che.Checked)
            {
                chart1.Series["Fluxo"].Enabled = true;
                chart1.Series["Fluxo"].IsVisibleInLegend = false;
            }
            else
            {
                chart1.Series["Fluxo"].Enabled = false;
            }
        }

        void chart_setup()
        {
            
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -70;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].CursorX.AutoScroll = true;
            chart1.ChartAreas[0].CursorY.AutoScroll = true;
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Regular);
            
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            




            chart1.ApplyPaletteColors();
            Color c = chart1.Series["T_água_baixo"].Color;
            T_água_baixo_che.ForeColor = c;

            c = chart1.Series["T_água_cima"].Color;
            T_água_cima_che.ForeColor = c;

            c = chart1.Series["Entrada_perm"].Color;
            Entrada_perm_che.ForeColor = c;
            
            c = chart1.Series["Saída_perm"].Color;
            Saída_perm_che.ForeColor = c;
           
            c = chart1.Series["Antes_válvula"].Color;
            Antes_válvula_che.ForeColor = c;

            c = chart1.Series["Depois_válvula"].Color;
            Depois_válvula_che.ForeColor = c;
            
            c = chart1.Series["Entrada_compr"].Color;
            Entrada_compr_che.ForeColor = c;
            
            c = chart1.Series["Saida_ar"].Color;
            Saída_ar_che.ForeColor = c;
          
            c = chart1.Series["Entrada_ar"].Color;
            Entrada_ar_che.ForeColor = c;
            c = chart1.Series["T_água2"].Color;
            T_água2_che.ForeColor = c;

            c = chart1.Series["Entrada_perm2"].Color;
            Entrada_perm2_che.ForeColor = c;

            c = chart1.Series["Saída_perm2"].Color;
            Saída_perm2_che.ForeColor = c;

            c = chart1.Series["Entrada_compr2"].Color;
            Entrada_compr2_che.ForeColor = c;

            c = chart1.Series["Saida_válvula2"].Color;
            Saida_válvula2_che.ForeColor = c;

            c = chart1.Series["Entrada_ar2"].Color;
            Entrada_ar2_che.ForeColor = c;

            c = chart1.Series["Fluxo"].Color;
            Fluxo_che.ForeColor = c;
        }
        



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja gravar o teste em excel?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                save_Excel();

            }
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
       
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {

                data = serialPort1.ReadLine();
                Console.WriteLine(data);
                if (data[0] == 'T')
                {
                    address = serialPort1.ReadLine();
                    temp = serialPort1.ReadLine();
                    resol = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    Console.WriteLine(address);
                    Console.WriteLine(temp);
                    Console.WriteLine(resol);
                    Console.WriteLine(idsensore);
                    this.Invoke(new MethodInvoker(update_sensor_table));

                }
                if (data[0] == 'A')
                {

                    //Single.Parse(text, CultureInfo.InvariantCulture)
                    System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                    customCulture.NumberFormat.NumberDecimalSeparator = ".";

                    System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

                    /*tempdata1 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata2 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata3 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata4 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata5 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());                    
                    tempdata6 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);                                      
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata7 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata8 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata9 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata10 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata11 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata12 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata13 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata14 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata15 = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);
                    idsensore = int.Parse(serialPort1.ReadLine());
                    flow = Single.Parse(serialPort1.ReadLine(), CultureInfo.InvariantCulture);*/

                    tempdata1 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata2 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata3 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata4 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata5 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());                    
                    tempdata6 = serialPort1.ReadLine();                                      
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata7 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata8 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata9 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata10 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata11 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata12 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata13 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata14 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    tempdata15 = serialPort1.ReadLine();
                    idsensore = int.Parse(serialPort1.ReadLine());
                    flow = serialPort1.ReadLine();


                    this.Invoke(new MethodInvoker(table_update));



                    
                }


            }
            catch (IOException ex)
            {
                MessageBox.Show("Não foi possivel receber os dados");
               LogError(ex);
            }




        }

        public static void LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path2 = Path.GetDirectoryName(Application.ExecutablePath);
            string path = @"" + path2 + "/log.txt";
            using (StreamWriter writer = new StreamWriter(path,true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}
