namespace CIS375Final
{
    public partial class MainForm : Form
    {
        public int IDVCount = -1;
        public int WeightFactor = -1;
        public int NumFunctionPoints = -1;
        public float LOCPerFP = -1.0f;

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

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            CalculateInformationDomainValues();
            CalculateComplexityWeightValues();

            WriteInfoToBoxes();
        }

        private void CalculateInformationDomainValues()
        {
            int[] userValues = new int[5];

            // Get textbox results
            if(!int.TryParse(userInputsNum.Text, out userValues[0]))
            {
                return;
            }
            if (!int.TryParse(userOutputsNum.Text, out userValues[1]))
            {
                return;
            }
            if (!int.TryParse(userInquiriesNum.Text, out userValues[2]))
            {
                return;
            }
            if (!int.TryParse(filesNum.Text, out userValues[3]))
            {
                return;
            }
            if (!int.TryParse(externalInterfacesNum.Text, out userValues[4]))
            {
                return;
            }

            int[] weightValues = new int[5];
            weightValues = [0, 0, 0, 0, 0];

            switch (weightComboBox.SelectedIndex)
            {
                case 0:
                    weightValues = [3, 4, 3, 7, 5];
                    break;
                case 1:
                    weightValues = [4, 5, 4, 10, 7];
                    break;
                case 2:
                    weightValues = [6, 7, 6, 15, 10];
                    break;
                default:
                    MessageBox.Show("Invalid weight was chosen");
                    break;
            }

            int totalCount = 0;
            for(int i = 0; i < 5; i++)
            {
                totalCount += weightValues[i] * userValues[i];
            }
            IDVCount = totalCount;
        }

        private void CalculateComplexityWeightValues()
        {
            int[] complexityWeights = new int[14];
            complexityWeights[0] = QuestionOneComboBox.SelectedIndex;
            complexityWeights[1] = QuestionTwoComboBox.SelectedIndex;
            complexityWeights[2] = QuestionThreeComboBox.SelectedIndex;
            complexityWeights[3] = QuestionFourComboBox.SelectedIndex;
            complexityWeights[4] = QuestionFiveComboBox.SelectedIndex;
            complexityWeights[5] = QuestionSixComboBox.SelectedIndex;
            complexityWeights[6] = QuestionSevenComboBox.SelectedIndex;
            complexityWeights[7] = QuestionEightComboBox.SelectedIndex;
            complexityWeights[8] = QuestionNineComboBox.SelectedIndex;
            complexityWeights[9] = QuestionTenComboBox.SelectedIndex;
            complexityWeights[10] = QuestionElevenComboBox.SelectedIndex;
            complexityWeights[11] = QuestionTwelveComboBox.SelectedIndex;
            complexityWeights[12] = QuestionThirteenComboBox.SelectedIndex;
            complexityWeights[13] = QuestionFourteenComboBox.SelectedIndex;

            int totalWeightFactor = 0;
            for(int i  = 0; i < complexityWeights.Length; i++)
            {
                if (complexityWeights[i] < 0) return; //ensures that all have been selected

                totalWeightFactor += complexityWeights[i];
            }

            WeightFactor = totalWeightFactor;
        }

        private void WriteInfoToBoxes()
        {
            // IDV Count
            if (IDVCount != -1)
            {
                IDVCountTextBox.Text = IDVCount.ToString();
            }
            else
            {
                IDVCountTextBox.Text = "N/A";
            }

            // Weight Factor
            if (WeightFactor > -1)
            {
                WeightFactorTextBox.Text = WeightFactor.ToString();
            }
            else
            {
                WeightFactorTextBox.Text = "N/A";
            }
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