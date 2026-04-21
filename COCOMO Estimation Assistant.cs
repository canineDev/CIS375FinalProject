namespace CIS375Final
{
    public partial class MainForm : Form
    {
        public class ModeCoefficients
        {
            public ModeCoefficients() { }

            public double a;
            public double b;
            public double c;
            public double d;
        }

        public enum ProjectType
        {
            Organic,
            Semidetached,
            Embedded
        };

        public MainForm()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (!double.TryParse(textBox1.Text, out double kloc))
        //    {
        //        return;
        //    }


        //    ProjectType projectType;
        //    ModeCoefficients mc = new();

        //    switch (weightComboBox.SelectedIndex)
        //    {
        //        case 0:
        //            projectType = ProjectType.Organic;
        //            SetCoefficients(mc, 2.4, 1.05, 2.5, 0.38);
        //            break;
        //        case 1:
        //            projectType = ProjectType.Semidetached;
        //            SetCoefficients(mc, 3.0, 1.12, 2.5, 0.35);
        //            break;
        //        case 2:
        //            projectType = ProjectType.Embedded;
        //            SetCoefficients(mc, 3.6, 1.20, 2.5, 0.32);
        //            break;
        //        default:
        //            projectType = ProjectType.Organic;
        //            SetCoefficients(mc, 2.4, 1.05, 2.5, 0.38);
        //            break;
        //    }

        //    (label5.Text, label6.Text) = GetEstimations(kloc, projectType, mc);
        //}

        public void SetCoefficients(ModeCoefficients mc, double a, double b, double c, double d)
        {
            mc.a = a;
            mc.b = b;
            mc.c = c;
            mc.d = d;
        }

        public (string, string) GetEstimations(double kloc, ProjectType projectType, ModeCoefficients mc)
        {
            double personMonths = mc.a * Math.Pow(kloc, mc.b);
            double months = mc.c * Math.Pow(personMonths, mc.d);
            double peopleNeeded = personMonths / months;



            return (months.ToString("F2"), peopleNeeded.ToString("F2"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string helpText = string.Empty;

            helpText += "Calculate the estimated values via the following:\n";
            helpText += "1. Input your estimated lines of code in thousands\n";
            helpText += "2. Select your project type\n";
            helpText += "3. Press Calculate and see your answers below!\n";

            MessageBox.Show(helpText, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}