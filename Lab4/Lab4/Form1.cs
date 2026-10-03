using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        enum Mode
        {
            Idle,
            CreatingPolygon,
            PickPoint,
            DynamicEdge
        }

        Mode currentMode = Mode.Idle;

        List<List<PointF>> polygons = new List<List<PointF>>();
        List<PointF> currentPolygon = null;

        Bitmap canvas;
        Graphics graphics;

        PointF? userPoint = null;
        PointF? dynamicEdgeStart = null;
        PointF dynamicEdgeEnd = new PointF();
        bool isDrawingEdge = false;

        int selectedPolygon = -1;
        string pendingAction = null;

        Pen polygonPen = new Pen(Color.Black, 2);
        Pen selectedPen = new Pen(Color.OrangeRed, 2);
        Pen tempPen = new Pen(Color.Red, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
        Pen previewPen = new Pen(Color.Blue, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
        Brush pointBrush = Brushes.DarkBlue;

        public Form1()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.Resize += Form1_Resize;
            pictureBox1.MouseClick += PictureBox1_MouseClick;
            pictureBox1.MouseMove += PictureBox1_MouseMove;

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            button5.Click += Button5_Click;
            button6.Click += Button6_Click;
            button7.Click += Button7_Click;

            InitCanvas(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);

            label1.Text =
@"Инструкции:
 ЛКМ — добавление вершины
 ПКМ — завершить полигон или выбрать существующий
 Ctrl+ЛКМ — проверка принадлежности точки полигону
 Alt+ЛКМ — определение положения точки относительно ребра";
            label6.Text = "";
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PointF p = e.Location;

            if ((ModifierKeys & Keys.Control) == Keys.Control && e.Button == MouseButtons.Left)
            {
                HandlePointInPolygon(p);
                RedrawAll();
                return;
            }

            if ((ModifierKeys & Keys.Alt) == Keys.Alt && e.Button == MouseButtons.Left)
            {
                HandlePointEdgeSide(p);
                RedrawAll();
                return;
            }

            if (currentMode == Mode.PickPoint && e.Button == MouseButtons.Left)
            {
                Part2_PickPointClick(p);
                return;
            }

            if (currentMode == Mode.DynamicEdge && e.Button == MouseButtons.Left)
            {
                Part2_DynamicEdgeClick(p);
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                Part1_AddVertex(p);
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                Part1_RightClick(p);
                return;
            }
        }
    }
}