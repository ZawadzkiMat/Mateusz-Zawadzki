using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace Okregi
{
    public partial class Form1 : Form
    {

        Thread th;
        Graphics g;
        Graphics fG;
        Bitmap btm;

        bool drawing = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btm = new Bitmap(580, 580);
            g = Graphics.FromImage(btm);
            fG = CreateGraphics();
            th = new Thread(Draw) { IsBackground = true };
            th.Start();
        }
        
        public void Draw()
        {
            float rad = 100;
            float rad1 = 50;
            float rad2 = 5;

            float angle = 0.0f;
            PointF org = new PointF(100, 100);


            Pen pen = new Pen(Brushes.Azure, 3.0f);
            Rectangle area = new Rectangle(100, 100, 200, 200);
            Rectangle circle = new Rectangle(0, 0, 100, 100);
            Rectangle circle1 = new Rectangle(0, 0, 50, 50);
            Rectangle circle2 = new Rectangle(0, 0, 5, 5);

            PointF loc = PointF.Empty;
            PointF loc1 = PointF.Empty;
            PointF loc2 = PointF.Empty;
            PointF img = new PointF(20, 20);

            var points = new List<PointF>();
            
            //czestotlowosc obrotu
            int fi1 = 3;
            int fi2 = 5;
            int fi3 = 11;
           

            fG.Clear(Color.Black);

            while (drawing)
            {
                g.Clear(Color.Black);

                g.DrawEllipse(pen, area);//duże koło
                //1 okreg
                loc = CirclePoint(rad, fi1*angle, org);
                circle.X = (int)(loc.X - (circle.Width / 2) + area.X);
                circle.Y = (int)(loc.Y - (circle.Height / 2) + area.Y);

                //2 okreg
                PointF org1 = new PointF(loc.X, loc.Y);

                loc1 = CirclePoint(rad1,  fi2*(3 * angle), org1);
                circle1.X = (int)(loc1.X - (circle1.Width / 2) + org.X);
                circle1.Y = (int)(loc1.Y - (circle1.Height / 2) + org.Y);

                //pkt na 2 okregu

                PointF org2 = new PointF(loc1.X, loc1.Y);

                loc2 = CirclePoint(5 * rad2, fi3* angle, org2);
                circle2.X = (int)(loc2.X - (circle2.Width / 2) + org.X);
                circle2.Y = (int)(loc2.Y - (circle2.Height / 2) + org.Y);
                points.Add(new PointF(circle2.X, circle2.Y));

                g.DrawEllipse(pen, circle); // okreg1
                g.DrawEllipse(pen, circle1); //okreg2
                g.DrawEllipse(pen, circle2); //pkt na 2 okregu

                //rysowanie sladu za punktem
                if (points.Count > 1)
                    g.DrawLines(new Pen(Brushes.Green, 1.0f), points.ToArray());

                fG.DrawImage(btm, img);

                if (angle < 360)
                {
                    angle += 0.5f;
                }
                else
                {
                    angle = 0;
                }
            }
        }

        public PointF CirclePoint(float radius, float angleInDegrees, PointF origin)
        {
            float x = (float)(radius * Math.Cos(angleInDegrees * Math.PI / 180F)) + origin.X;
            float y = (float)(radius * Math.Sin(angleInDegrees * Math.PI / 180F)) + origin.Y;

            return new PointF(x, y);
        }

        
    }
}
