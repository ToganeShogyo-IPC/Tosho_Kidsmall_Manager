using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Tosho_Kidsmall_Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static class globalVar
        {
            public static string selectedClass;
            public static string selectedName;
            public static int selectedLastNinzu;
            public static int selectedOKNinzu;
            public static List<DashimonoDatas> datas;

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int selectindex = 0;
                selectindex = listView1.SelectedItems[0].Index;
                label4.Text = "クラス：" + globalVar.datas[selectindex].Class;
                label5.Text = "出し物の名前：" + globalVar.datas[selectindex].Name;
                label6.Text = "受け入れ可能人数：" + globalVar.datas[selectindex].OKNinzu;
                label7.Text = "残人数：" + globalVar.datas[selectindex].ZanNinzu;
                if (globalVar.datas[selectindex].ZanNinzu >= 1)
                {
                    label8.Text = "受付可能です";
                    label8.ForeColor = Color.Green;
                }
                else if (globalVar.datas[selectindex].ZanNinzu == 0)
                {
                    label8.Text = "!!受付不可です";
                    label8.ForeColor = Color.Crimson;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int result = LoadDashimonoData();
            if (result == 1) { Application.Exit(); }
            int i = 0;
            foreach (DashimonoDatas dashimono in globalVar.datas)
            {
                dashimono.ZanNinzu = dashimono.OKNinzu;
                string[] items = { dashimono.Class, dashimono.Name, dashimono.OKNinzu.ToString(), dashimono.ZanNinzu.ToString() };
                listView1.Items.Add(new ListViewItem(items));
                listView1.Items[i].BackColor = Color.LightCyan;
                i++;
                if(i == 10 || i == 17)
                {
                    label2.Text += $"\r\n{dashimono.Class} ( {dashimono.ZanNinzu} 人), ";
                }
                else
                {
                    label2.Text += $"{dashimono.Class} ( {dashimono.ZanNinzu} 人), ";
                }
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateNinzu(listView1.SelectedItems[0].Index,"-");
            }
            catch
            {
                MessageBox.Show($"エラー: \r\n操作対象を指定してから操作してください。(E01)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateNinzu(listView1.SelectedItems[0].Index, "+");
            }
            catch
            {
                MessageBox.Show($"エラー: \r\n操作対象を指定してから操作してください。(E01)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
        }
        
        /// <summary>
        /// 人数更新の計算を行います。
        /// </summary>
        /// <param name="index">どの番を更新するか</param>
        /// <param name="hanbetsushi">人が帰ってきたときは+、人が行ったときは-</param>
        private void UpdateNinzu(int index,string hanbetsushi)
        {
            if(globalVar.datas[index].ZanNinzu == 0 && hanbetsushi == "-")
            {
                MessageBox.Show($"情報: \r\n操作対象の場所はすでに残人数枠が0です。\r\n他の場所を選択してください。", "満員", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
            }
            if (globalVar.datas[index].ZanNinzu == globalVar.datas[index].OKNinzu && hanbetsushi == "+")
            {
                MessageBox.Show($"情報: \r\n操作対象の場所はすでに残人数枠が全回復しています。\r\n他の場所を選択してください。", "残人数枠全回復済み", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
            }
            label4.Text = "クラス：";
            label5.Text = "出し物の名前：";
            label6.Text = "受け入れ可能人数：";
            label7.Text = "残人数：";
            if (hanbetsushi == "+") 
            {
                globalVar.datas[index].ZanNinzu++;
            }
            if (hanbetsushi == "-")
            {
                globalVar.datas[index].ZanNinzu--;
            }
            listView1.Items.Clear();
            label2.Text = "受付可能：";
            label3.Text = "受付不可：";
            List<string> OKDashimono = new List<string>();
            List<string> NGDashimono = new List<string>();
            int listkazu = 0;
            foreach (DashimonoDatas dashimono in globalVar.datas)
            {
                string[] items = { dashimono.Class, dashimono.Name, dashimono.OKNinzu.ToString(), dashimono.ZanNinzu.ToString() };
                listView1.Items.Add(new ListViewItem(items));        
                if (dashimono.ZanNinzu == 0)
                {
                    NGDashimono.Add($"{dashimono.Class} ( {dashimono.ZanNinzu}人 ), ");
                    listView1.Items[listkazu].BackColor = Color.Pink;
                }
                else
                {
                    OKDashimono.Add($"{dashimono.Class} ( {dashimono.ZanNinzu}人 ), ");
                    listView1.Items[listkazu].BackColor = Color.LightCyan;
                }
                listkazu++;
            }
            int i = 0;
            int r = 0;
            foreach (string ok in  OKDashimono)
            {
                i++;
                if (i == 10 || i == 17)
                {
                    label2.Text += $"\r\n{ok}";
                }
                else
                {
                    label2.Text += ok;
                }
            }
            foreach(string ng in NGDashimono)
            {
                r++;
                if (r == 10 || r == 17)
                {
                    label3.Text += $"\r\n{ng}";
                }
                else
                {
                    label3.Text += ng;
                }
            }
            label8.Text = "非選択";
            label8.ForeColor = Color.Black;
        }

        /// <summary>
        /// 出し物データを読み込みます。
        /// </summary>
        private int LoadDashimonoData()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "DashimonoDataFile(*.json)|*.json";
            ofd.Title = "読み込む出し物データを選んでください。";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var stream = new FileStream(ofd.FileName, FileMode.Open))
                    {
                        using (var sr = new StreamReader(stream))
                        {
                            var js_l = JsonConvert.DeserializeObject<List<DashimonoDatas>>(sr.ReadToEnd());
                            globalVar.datas = js_l;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show($"エラー: \r\n指定されたファイルは見つかりませんでした。\r\nファイルの場所を確認して、もう一度お試しください。(E01)", "データ読み込み失敗", MessageBoxButtons.OK, MessageBoxIcon.Error); return 1;
                }
            }
            else
            {
                MessageBox.Show($"エラー: \r\nファイルが指定されませんでした。\r\nソフトウェアを終了します。(E01)", "データ未指定", MessageBoxButtons.OK, MessageBoxIcon.Error); return 1;
            }
            return 0;
        }

    }
}