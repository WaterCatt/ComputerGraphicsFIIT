using System;
using System.Globalization;
using System.Windows.Forms;

namespace Lab8
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
                double rad = (double)numAxisAngle.Value * Math.PI / 180.0;
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
                double rad = (double)numLineAngle.Value * Math.PI / 180.0;
                polyhedronDrawer.Polyhedron.RotateAroundLine(
                    new Point3D((double)numLx1.Value, (double)numLy1.Value, (double)numLz1.Value),
                    new Point3D((double)numLx2.Value, (double)numLy2.Value, (double)numLz2.Value),
                    rad);
                polyhedronDrawer.Invalidate();
            };

            btnLoadObj.Click += (s, e) =>
            {
                using (var dlg = new OpenFileDialog { Filter = "OBJ Files (*.obj)|*.obj" })
                {
                    if (dlg.ShowDialog() != DialogResult.OK) return;
                    try
                    {
                        polyhedronDrawer.Polyhedron = ObjModelIO.Load(dlg.FileName);
                        polyhedronDrawer.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка загрузки");
                    }
                }
            };

            btnSaveObj.Click += (s, e) =>
            {
                if (polyhedronDrawer.Polyhedron == null)
                    return;
                using (var dlg = new SaveFileDialog { Filter = "OBJ Files (*.obj)|*.obj", DefaultExt = "obj" })
                {
                    if (dlg.ShowDialog() != DialogResult.OK) return;
                    try
                    {
                        ObjModelIO.Save(polyhedronDrawer.Polyhedron, dlg.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка сохранения");
                    }
                }
            };

            btnBuildRevolution.Click += (s, e) =>
            {
                try
                {
                    var pts = ParseProfile(txtProfile.Text);
                    int segs = (int)numSegments.Value;
                    polyhedronDrawer.Polyhedron = RevolutionBuilder.Build(pts, cmbRevolutionAxis.Text, segs);
                    polyhedronDrawer.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка построения");
                }
            };

            btnBuildSurface.Click += (s, e) =>
            {
                try
                {
                    var f = GetSurfaceFunction(cmbFunction.Text);
                    int n = (int)numSurfaceSegments.Value;
                    polyhedronDrawer.Polyhedron = SurfaceBuilder.Build(f,
                        (double)numX0.Value, (double)numX1.Value,
                        (double)numY0.Value, (double)numY1.Value,
                        n, n);
                    polyhedronDrawer.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка построения");
                }
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

        private static List<Point3D> ParseProfile(string text)
        {
            var result = new List<Point3D>();
            if (string.IsNullOrWhiteSpace(text))
                return result;

            foreach (var chunk in text.Split(';'))
            {
                var trimmed = chunk.Trim();
                if (trimmed.Length == 0)
                    continue;
                var parts = trimmed.Split(',');
                if (parts.Length < 2)
                    throw new FormatException("Точка должна быть в формате x,y[,z]");
                double x = double.Parse(parts[0], CultureInfo.InvariantCulture);
                double y = double.Parse(parts[1], CultureInfo.InvariantCulture);
                double z = parts.Length >= 3 ? double.Parse(parts[2], CultureInfo.InvariantCulture) : 0.0;
                result.Add(new Point3D(x, y, z));
            }
            return result;
        }

        private static Func<double, double, double> GetSurfaceFunction(string name)
        {
            switch (name)
            {
                case "x^2 - y^2": return (x, y) => x * x - y * y;
                case "cos(x) * sin(y)": return (x, y) => Math.Cos(x) * Math.Sin(y);
                case "sin(sqrt(x^2 + y^2))": return (x, y) => Math.Sin(Math.Sqrt(x * x + y * y));
                case "sin(x) * cos(y) * x": return (x, y) => Math.Sin(x) * Math.Cos(y) * x;
                case "exp(-(x^2 + y^2))": return (x, y) => Math.Exp(-(x * x + y * y));
                default:
                    return (x, y) => Math.Exp(-(x * x + y * y));
            }
        }

        private void polyhedronDrawer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}