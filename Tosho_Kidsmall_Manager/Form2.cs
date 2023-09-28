using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tosho_Kidsmall_Manager
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DrawToDisplay(Form1.globalVar.datas);
        }

        private void DrawToDisplay(List<DashimonoDatas> DrawDatas)
        {
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(canvas);
            g.FillRectangle(new SolidBrush(Color.FromArgb(250,250,250)), new Rectangle(0, 0, canvas.Width, canvas.Height));
            int row;
            if(DrawDatas.Count%4 > 0)
            {
                row = (int)DrawDatas.Count / 4 + 1;
            }
            else
            {
                row = (int)DrawDatas.Count / 4;
            }
            
            int blockw = (int)canvas.Width / 4;
            int blockh = (int)canvas.Height/row;
            int mozitate = (int)blockh / 4;
            int moziyoko = (int)blockw / 2;
            int magine = mozitate *25/10;

            Font viewfont = new Font("ＤＦ特太ゴシック体",42);
            StringFormat sf = new StringFormat() { 
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            };
            int yokostart = 0;
            int tatestart = 0;
            int count = 0;
            foreach(DashimonoDatas value in DrawDatas)
            {
                Bitmap tempCanvas = new Bitmap(blockw, blockh);
                using (Graphics tg = Graphics.FromImage(tempCanvas))
                {
                    tg.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    if(value.ZanNinzu == 0)
                    {
                        tg.FillRectangle(new SolidBrush(Color.Pink),new Rectangle(0,0,blockw,blockh));
                        tg.DrawRectangle(new Pen(Color.DarkRed, 1), new Rectangle(0, 0, blockw-1, blockh-1));
                    }
                    else if (value.ZanNinzu > 0)
                    {
                        tg.FillRectangle(new SolidBrush(Color.LightCyan), new Rectangle(0, 0, blockw, blockh));
                        tg.DrawRectangle(new Pen(Color.DarkSlateGray, 1), new Rectangle(0, 0, blockw-1, blockh-1));
                    }
                    //tg.DrawString(value.Name, viewfont, new SolidBrush(Color.Black), new Point(moziyoko, mozitate),sf);
                    tg.DrawString(value.Name, viewfont, new SolidBrush(Color.Black), new Rectangle(0,0,blockw,mozitate*3), sf);
                    tg.DrawString(value.ZanNinzu.ToString(), viewfont, new SolidBrush(Color.Black), new Point(moziyoko, mozitate+magine),sf);
                }
                g.DrawImage(tempCanvas, yokostart, tatestart);
                yokostart += blockw;
                count++;
                if(count == 4)
                {
                    yokostart = 0;
                    tatestart += blockh;
                    count = 0;
                }
                tempCanvas.Dispose();
            }
            pictureBox1.Image = canvas;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawToDisplay(Form1.globalVar.datas);
        }
    }
}
