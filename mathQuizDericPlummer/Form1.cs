using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mathQuizDericPlummer
{
    public partial class Form1 : Form
    {
        // This creates a random object used to generate random numbers
        Random randomizer = new Random(); 

        // These are the integers used for the addition problem
        int addNum1;
        int addNum2;

        // These are the integers used for the subtration problem
        int minNum1;
        int minNum2;

        // These are the integers used for multiplication problem 
        int multNum1;
        int multNum2;

        // These are the integers used for the division problem
        int dividend;
        int divisor;

        // Holds the variable with the time is up value. 
        int timeLeft;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startQuiz();
            startButton.Enabled = false;
        }


        /******************************************
         * checkAnswer(): checks that the answer 
         * is correct.
         ******************************************/
        private bool checkAnswer()
        {
            if ((addNum1 + addNum2 == sum.Value) 
                && (minNum1 - minNum2 == difference.Value)
                && (multNum1 * multNum2 == product.Value)
                && (dividend / divisor == quotient.Value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*************************************************
         * startQuiz(): When the user clicks on the Start
         * Quiz button random values will be generated on the 
         * form and the timer will begin.
         * **********************************************/
        public void startQuiz()
        {
            timeLabel.BackColor = Color.White;
            // Create the addition problem
            addNum1 = randomizer.Next(51); // Generates random numbers
            addNum2 = randomizer.Next(51);
            plusLeftLabel.Text = addNum1.ToString(); // Displays those numbers to the form
            plusRightLabel.Text = addNum2.ToString();
            sum.Value = 0; // Sets the initial value of sum to zero

            // Create the subraction problem
            minNum1 = randomizer.Next(1, 101);
            minNum2 = randomizer.Next(1, minNum1);
            minusLeftLabel.Text = minNum1.ToString();
            minusRightLabel.Text = minNum2.ToString();
            difference.Value = 0;

            // Create the multiplication problem.
            multNum1 = randomizer.Next(2, 11);
            multNum2 = randomizer.Next(2, 11);
            timesLeftLabel.Text = multNum1.ToString();
            timesRightLabel.Text = multNum2.ToString();
            product.Value = 0;

            // Create the division problem.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        /*****************************************************
         * timer1_tick(object, EventArgs): This event handlers
         * is called every second and determines whether the test 
         * is complete or if time is up.
         * **************************************************/
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkAnswer())
            {
                timer1.Stop();
                MessageBox.Show("Congratulations! You got all the answers right");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // Subtract one second
                timeLeft--;
                timeLabel.Text = timeLeft + " seconds";
                if (timeLeft == 5)
                {
                    timeLabel.BackColor = Color.Red;
                }
            }
            else
            {
                // Timer stops when time runs out
                timer1.Stop();
                timeLabel.Text = "Time is up!";
                MessageBox.Show("You didnt finish on time.", "Sorry!");
                sum.Value = addNum1 + addNum2;
                difference.Value = minNum1 - minNum2;
                product.Value = multNum1 * multNum2;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;
            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
