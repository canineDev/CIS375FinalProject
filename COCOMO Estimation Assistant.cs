namespace CIS375Final
{
    /// <summary>
    /// The Main Form that is generated and drawn to the screen as the main
    /// application for the COCOMO Estimation Assistant
    /// </summary>
    public partial class MainForm : Form
    {
        public int IDVCount = -1;
        public int WeightFactor = -1;
        public float NumFunctionPoints = -1;
        public float LOCPerFP = -1;
        public float Effort = -1;
        public float Duration = -1;
        public ModeCoefficients mc = new();

        /// <summary>
        /// Holds the values for a, b, c, and d
        /// </summary>
        public class ModeCoefficients
        {
            public ModeCoefficients() { }

            public float a;
            public float b;
            public float c;
            public float d;
        }

        /// <summary>
        /// Initializes the form and its values
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            InitializeDefaultComboBoxValues();
        }

        /// <summary>
        /// Initializes the combo box values to the first option
        /// </summary>
        private void InitializeDefaultComboBoxValues()
        {
            weightComboBox.SelectedIndex = 0;

            QuestionOneComboBox.SelectedIndex = 0;
            QuestionTwoComboBox.SelectedIndex = 0;
            QuestionThreeComboBox.SelectedIndex = 0;
            QuestionFourComboBox.SelectedIndex = 0;
            QuestionFiveComboBox.SelectedIndex = 0;
            QuestionSixComboBox.SelectedIndex = 0;
            QuestionSevenComboBox.SelectedIndex = 0;
            QuestionEightComboBox.SelectedIndex = 0;
            QuestionNineComboBox.SelectedIndex = 0;
            QuestionTenComboBox.SelectedIndex = 0;
            QuestionElevenComboBox.SelectedIndex = 0;
            QuestionTwelveComboBox.SelectedIndex = 0;
            QuestionThirteenComboBox.SelectedIndex = 0;
            QuestionFourteenComboBox.SelectedIndex = 0;

            programmingLanguageComboBox.SelectedIndex = 0;
            projectTypeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets called when the calculate button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateButton_Click(object sender, EventArgs e)
        {
            CalculateInformationDomainValues();
            CalculateComplexityWeightValues();
            CalculateProjectInfoValues();
            CalculateEffortAndDuration();

            WriteInfoToBoxes();
        }

        /// <summary>
        /// Will calculate the information domain values by reading the text box values and summing them up
        /// </summary>
        private void CalculateInformationDomainValues()
        {
            int[] userValues = new int[5];

            // Get textbox results
            if (!int.TryParse(userInputsNum.Text, out userValues[0]))
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
            for (int i = 0; i < 5; i++)
            {
                totalCount += weightValues[i] * userValues[i];
            }
            IDVCount = totalCount;
        }

        /// <summary>
        /// Calculates the weight factor and the number of function points by 
        /// summing the weights up and using the function point formula
        /// </summary>
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
            for (int i = 0; i < complexityWeights.Length; i++)
            {
                if (complexityWeights[i] < 0) return; //ensures that all have been selected

                totalWeightFactor += complexityWeights[i];
            }

            WeightFactor = totalWeightFactor;

            if (IDVCount > 0 && WeightFactor > 0)
            {
                NumFunctionPoints = (float)IDVCount * (0.65f + 0.01f * (float)WeightFactor);
            }
        }

        /// <summary>
        /// Calculates the LOC/FP, effort, and duration values using their appropriate formulas
        /// </summary>
        private void CalculateProjectInfoValues()
        {
            int avgLOC = -1;
            switch (programmingLanguageComboBox.SelectedIndex)
            {
                case 0:
                    avgLOC = 320;
                    break;
                case 1:
                    avgLOC = 128;
                    break;
                case 2:
                case 3:
                    avgLOC = 105;
                    break;
                case 4:
                    avgLOC = 90;
                    break;
                case 5:
                    avgLOC = 70;
                    break;
                case 6:
                    avgLOC = 30;
                    break;
                case 7:
                    avgLOC = 20;
                    break;
                case 8:
                    avgLOC = 15;
                    break;
                case 9:
                    avgLOC = 6;
                    break;
                case 10:
                    avgLOC = 4;
                    break;
                default:
                    break;
            }

            if (NumFunctionPoints > -1)
            {
                LOCPerFP = (float)avgLOC * NumFunctionPoints;
            }

            switch (projectTypeComboBox.SelectedIndex)
            {
                case 0:
                    SetCoefficients(mc, 2.4f, 1.05f, 2.5f, 0.38f);
                    break;
                case 1:
                    SetCoefficients(mc, 3.0f, 1.12f, 2.5f, 0.35f);
                    break;
                case 2:
                    SetCoefficients(mc, 3.6f, 1.20f, 2.5f, 0.32f);
                    break;
                default:
                    SetCoefficients(mc, 2.4f, 1.05f, 2.5f, 0.38f);
                    break;
            }
        }

        /// <summary>
        /// Calculates the effort and duration using the E and D formulas
        /// </summary>
        private void CalculateEffortAndDuration()
        {
            float effort = mc.a * (float)Math.Pow(LOCPerFP / 1000, mc.b);
            Effort = effort;

            float duration = mc.c * (float)Math.Pow(Effort, mc.d);
            Duration = duration;
        }

        /// <summary>
        /// Will write the values into the text boxes at the bottom of the application
        /// </summary>
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

            // Function Points
            if (NumFunctionPoints > -1)
            {
                FPCountTextBox.Text = NumFunctionPoints.ToString("F2");
            }
            else
            {
                FPCountTextBox.Text = "N/A";
            }

            // LOC per FP
            if (LOCPerFP > -1)
            {
                LOCPerFPTextBox.Text = LOCPerFP.ToString("F2");
            }
            else
            {
                LOCPerFPTextBox.Text = "N/A";
            }

            // Effort
            if (Effort > -1)
            {
                EffortTextBox.Text = Effort.ToString("F2");
            }
            else
            {
                EffortTextBox.Text = "N/A";
            }

            // Duration
            if (Duration > -1)
            {
                DurationTextBox.Text = Duration.ToString("F2");
            }
            else
            {
                DurationTextBox.Text = "N/A";
            }
        }

        /// <summary>
        /// helper to set the coefficients of the formula based on project type
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        public void SetCoefficients(ModeCoefficients mc, float a, float b, float c, float d)
        {
            mc.a = a;
            mc.b = b;
            mc.c = c;
            mc.d = d;
        }

        /// <summary>
        /// Pops up the help menu for the Information Domain Values tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDVHelpButton_Click(object sender, EventArgs e)
        {
            string helpText = string.Empty;

            helpText += "1. Input your project information in each textbox\n";
            helpText += "2. Select the weight factor in the dropdown menu\n";
            helpText += "3. Click the calculate button to see your information domain values count\n";

            MessageBox.Show(helpText, "Information Domain Value Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Pops up the help menu for the Complexity Weights tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CWHelpButton_Click(object sender, EventArgs e)
        {
            string helpText = string.Empty;

            helpText += "1. Answer each question for the complexity weight of each topic\n";
            helpText += "2. Click the calculate button to see your weight factor and # of function points\n";

            MessageBox.Show(helpText, "Complexity Weight Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Pops up the help menu for the Project Info tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PIHelpButton_Click(object sender, EventArgs e)
        {
            string helpText = string.Empty;

            helpText += "1. Select the programming language you are using in the project in the dropdown menu\n";
            helpText += "2. Select the project type in the dropdown window\n";
            helpText += "3. Click the calculate button to see the LOC/FP value, effort value, and duration values\n";

            MessageBox.Show(helpText, "Project Info Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}