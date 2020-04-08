using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
// Levi Martin 
// 04/02/2020

namespace TestScoreLookupandUpdateLeviMartin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // 2 dimmensional array to hold student grades
        int[,] studentGradesArray = new int[10, 3];

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                //read file into the array and place first column in the list box
                int row = 0;
                string lineofdata;

                //need a one dimmensional, 3 element string array for the split method
                string[] splitlineofdata = new string[10];

                StreamReader studentReader;
                studentReader = File.OpenText("Grades.csv");

                // loop through until end of file
                while (!studentReader.EndOfStream)
                {
                    lineofdata = studentReader.ReadLine();
                    splitlineofdata = lineofdata.Split(',');
                    studentNumberListbox.Items.Add(splitlineofdata[0]);


                    //organizing the data
                    studentGradesArray[row, 0] = int.Parse(splitlineofdata[0]);
                    studentGradesArray[row, 1] = int.Parse(splitlineofdata[1]);
                    studentGradesArray[row, 2] = int.Parse(splitlineofdata[2]);

                    
                    row++;
                }

                studentReader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //write array info to file
                StreamWriter studentWriter;
                studentWriter = File.CreateText("Grades.csv");
                for (int row = 0; row < 10; row++)
                {
                    studentWriter.Write(studentGradesArray[row, 0] + ",");
                    studentWriter.Write(studentGradesArray[row, 1] + ",");
                    studentWriter.WriteLine(studentGradesArray[row, 2]);

                }

                studentWriter.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void displayButton_Click(object sender, EventArgs e)
        {
            //variables
            decimal allTestAverage = 0; decimal midtermAverage; decimal finalAverage;
            int allTestScores = 0; int midtermTestScores = 0; int finalTestScores = 0;


            //radio button selections

            if (averageaAllTestsRadioButton.Checked)
            {
                for (int row = 0; row < 10; row++)
                {
                    for (int col = 1; col < 3; col++)
                    {
                        // display average of all test scores held in array 
                        allTestScores += studentGradesArray[row, col];
                        allTestAverage = (decimal)allTestScores / 20m;
                        averageLabel.Text = "Average of all tests is:" + "\n" + allTestAverage.ToString();

                        midtermLabel.Text = "";
                        finalLabel.Text = "";
                    }
                }
            }
            else
            {
                if (averageMidtermsRadioButton.Checked)
                {
                    for (int row = 0; row < 10; row++)
                    {
                        for (int col = 1; col < 2; col++)
                        {
                            // display average of all midterm test scores held in array 

                            midtermTestScores += studentGradesArray[row, col];
                            midtermAverage = (decimal)midtermTestScores / 10m;
                            averageLabel.Text = "Average of midterms are:" + "\n" + midtermAverage.ToString();

                            midtermLabel.Text = "";
                            finalLabel.Text = "";
                        }
                    }
                }
                else if (averageFinalsRadiobutton.Checked)
                {
                    for (int row = 0; row < 10; row++)
                    {
                        for (int col = 2; col < 3; col++)
                        {
                            // display average of all finals test scores held in array 

                            finalTestScores += studentGradesArray[row, col];
                            finalAverage = (decimal)finalTestScores / 10m;
                            averageLabel.Text = "Average of finals is:" + "\n" + finalAverage.ToString();

                            midtermLabel.Text = "";
                            finalLabel.Text = "";
                        }
                    }
                }

                else if (findMidtermRadiobutton.Checked)
                {
                    if (studentNumberListbox.SelectedIndex != -1)
                    {
                        // display student midterm test score 

                        int row = studentNumberListbox.SelectedIndex;
                        int col = 1;
                        midtermTestScores = studentGradesArray[row, col];
                        midtermLabel.Text = midtermTestScores.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Please select a student ID number");
                    }
                }

                else if (finalScoreRadioButton.Checked)
                {
                    if (studentNumberListbox.SelectedIndex != -1)
                    {
                        // display student final test score 

                        int row = studentNumberListbox.SelectedIndex;
                        int col = 2;
                        midtermTestScores = studentGradesArray[row, col];
                        finalLabel.Text = midtermTestScores.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Please select a student ID number");
                    }
                }
            }
        }

        private void updateMidtermButton_Click(object sender, EventArgs e)
        {
            // student ID needs selected and score must be 0-100
            if (studentNumberListbox.SelectedIndex != -1)
            {
                int row = studentNumberListbox.SelectedIndex;
                int score;
                bool isValid;
                isValid = int.TryParse(midtermTextbox.Text, out score);
                if (isValid && score >= 0 && score <= 100)
                {
                    studentGradesArray[row, 1] = score;
                    MessageBox.Show("Midterm grade updated.");
                    midtermTextbox.Clear();
                    averageLabel.Text = "";
                    midtermLabel.Text = score.ToString();

                    finalLabel.Text = "";
                }
                else MessageBox.Show("Please enter a whole number 1-100");
            }
            else
            {
                MessageBox.Show("Please select a student ID");
            }
                     
        }

        private void updateFinalButton_Click(object sender, EventArgs e)
        {
            // student ID needs selected and score must be 0-100
            if (studentNumberListbox.SelectedIndex != -1)
            {
                int row = studentNumberListbox.SelectedIndex;
                int score;
                bool isValid;
                isValid = int.TryParse(finalTextbox.Text, out score);
                if (isValid && score >= 0 && score <= 100)
                {
                    studentGradesArray[row, 2] = score;
                    MessageBox.Show("Final grade updated.");
                    finalTextbox.Clear();
                    averageLabel.Text = "";
                    finalLabel.Text = score.ToString();

                    midtermLabel.Text = "";

                }
                else MessageBox.Show("Please enter a whole number 1-100");
            }
            else
            {
                MessageBox.Show("Please select a student ID");
            }
        }

    }
}
