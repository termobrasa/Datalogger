using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;



namespace ClientApp
{
    class print_chart
    {
        static String path = Path.GetDirectoryName(Application.ExecutablePath);


        public static void save_image(Chart c, Panel p, string dir)
        {
            foreach (CheckBox checkbox in p.Controls.OfType<CheckBox>())
                checkbox.Checked = false;

            c.Series["T_água_baixo"].Enabled = false;
            c.Series["T_água_cima"].Enabled = false;
            c.Series["Entrada_perm"].Enabled = false;
            c.Series["Saída_perm"].Enabled = false;
            c.Series["Antes_válvula"].Enabled = false;
            c.Series["Depois_válvula"].Enabled = false;
            c.Series["Entrada_compr"].Enabled = false;
            c.Series["Saida_ar"].Enabled = false;
            c.Series["Entrada_ar"].Enabled = false;
            c.Series["T_água2"].Enabled = false;
            c.Series["Entrada_perm2"].Enabled = false;
            c.Series["Saída_perm2"].Enabled = false;
            c.Series["Entrada_compr2"].Enabled = false;
            c.Series["Saida_válvula2"].Enabled = false;
            c.Series["Entrada_ar2"].Enabled = false;
            c.Series["Fluxo"].Enabled = false;

            dir = path + "\\" + dir;
            Directory.CreateDirectory(dir + "\\imagens");

            c.Series["T_água_baixo"].Enabled = true;
            c.Series["T_água_cima"].Enabled = true;
            c.SaveImage("" + dir + "\\imagens\\T_agua_cimaVSbaixo.png", ChartImageFormat.Png);

            c.Series["T_água_baixo"].Enabled = false;
            c.Series["T_água_cima"].Enabled = false;
            c.Series["Entrada_perm"].Enabled = true;
            c.Series["Saída_perm"].Enabled = true;
            c.SaveImage("" + dir + "\\imagens\\T_entrada_permVSsaida.png", ChartImageFormat.Png);

            c.Series["Entrada_perm"].Enabled = false;
            c.Series["Saída_perm"].Enabled = false;
            c.Series["Antes_válvula"].Enabled = true;
            c.Series["Depois_válvula"].Enabled = true;
            c.SaveImage("" + dir + "\\imagens\\T_entrada_valVSsaida.png", ChartImageFormat.Png);

            c.Series["Antes_válvula"].Enabled = false;
            c.Series["Depois_válvula"].Enabled = false;
            c.Series["Entrada_compr"].Enabled = true;
            c.SaveImage("" + dir + "\\imagens\\T_entrada_compr.png", ChartImageFormat.Png);

            c.Series["Entrada_compr"].Enabled = false;
            c.Series["Saida_ar"].Enabled = true;
            c.Series["Entrada_ar"].Enabled = true;
            c.SaveImage("" + dir + "\\imagens\\T_entrada_arVSsaida.png", ChartImageFormat.Png);

        }
                    

    }
}
