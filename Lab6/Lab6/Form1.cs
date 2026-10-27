using System;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupEvents();
            LoadFigure();
        }

        private void SetupEvents()
        {
            cmbFigure.SelectedIndexChanged += (s, e) => LoadFigure();
            cmbProjection.SelectedIndexChanged += (s, e) =>
            {
                polyhedronDrawer.Camera.Type = cmbProjection.SelectedIndex == 0 ? ProjectionType.Perspective : ProjectionType.Axonometric;
                polyhedronDrawer.Invalidate();
            };

            btnReset.Click += (s, e) => LoadFigure();

            btnTranslate.Click += (s, e) =>
            {
                polyhedronDrawer.Polyhedron.Translate((double)numTx.Value, (double)numTy.Value, (double)numTz.Value);
                polyhedronDrawer.Invalidate();
            };

            btnScale.Click += (s, e) =>
            {
                double sx = (double)numSx.Value;
                double sy = (double)numSy.Value;
                double sz = (double)numSz.Value;

                const double minScale = 0.01;
                if (Math.Abs(sx) < minScale)
                    sx = Math.Sign(sx) == 0 ? minScale : Math.Sign(sx) * minScale;
                if (Math.Abs(sy) < minScale)
                    sy = Math.Sign(sy) == 0 ? minScale : Math.Sign(sy) * minScale;
                if (Math.Abs(sz) < minScale)
                    sz = Math.Sign(sz) == 0 ? minScale : Math.Sign(sz) * minScale;

                const double maxScale = 10.0;
                if (Math.Abs(sx) > maxScale)
                    sx = Math.Sign(sx) * maxScale;
                if (Math.Abs(sy) > maxScale)
                    sy = Math.Sign(sy) * maxScale;
                if (Math.Abs(sz) > maxScale)
                    sz = Math.Sign(sz) * maxScale;

                polyhedronDrawer.Polyhedron.ScaleAroundCenter(sx, sy, sz);
                polyhedronDrawer.Invalidate();
            };

            btnRotateAxis.Click += (s, e) =>
            {
                double rad = (double)numAxisAngle.Value * Math.PI/180.0;
                polyhedronDrawer.Polyhedron.RotateAroundCenterAxis(cmbAxis.Text, rad);
                polyhedronDrawer.Invalidate();
            };

            btnReflectCoord.Click += (s, e) =>
            {
                polyhedronDrawer.Polyhedron.ReflectCoordinatePlane(cmbReflectCoord.Text);
                polyhedronDrawer.Invalidate();
            };

            btnRotateLine.Click += (s, e) =>
            {
                double rad = (double)numLineAngle.Value * Math.PI/180.0;
                polyhedronDrawer.Polyhedron.RotateAroundLine(
                    new Point3D((double)numLx1.Value, (double)numLy1.Value, (double)numLz1.Value),
                    new Point3D((double)numLx2.Value, (double)numLy2.Value, (double)numLz2.Value),
                    rad);
                polyhedronDrawer.Invalidate();
            };
        }

        private void LoadFigure()
        {
            if (cmbFigure.SelectedIndex < 0) 
                return;

            Polyhedron p = cmbFigure.SelectedIndex switch
            {
                0 => PolyhedronFactory.CreateTetrahedron(),
                1 => PolyhedronFactory.CreateCube(),
                2 => PolyhedronFactory.CreateOctahedron(),
                3 => PolyhedronFactory.CreateIcosahedron(),
                4 => PolyhedronFactory.CreateDodecahedron(),
                _ => PolyhedronFactory.CreateCube()
            };

            polyhedronDrawer.Polyhedron = p;
            polyhedronDrawer.Invalidate();
        }

        private void polyhedronDrawer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}