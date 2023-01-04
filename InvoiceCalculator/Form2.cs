using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceCalculator
{
    public partial class frmFutureValue : Form
    {
        public frmFutureValue()
        {
            InitializeComponent();
        }

        string[,] calculationsTable = new string[10, 4];
        int row = 0;


        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidData())
                {
                    return;
                }
                decimal monthlyInvestment = Convert.ToDecimal(txtMonthlyInvestment.Text);
                decimal yearlyInterestRate = Convert.ToDecimal(txtYearlyInterestRate.Text);
                int years = Convert.ToInt32(txtNumberOfYears.Text);
                int month = years * 12;

                decimal monthlyIntersetRate = yearlyInterestRate / 12 / 100;

                decimal futureValue = CalculateFutureValue(monthlyInvestment, monthlyIntersetRate, month);
                txtFutureValue.Text = futureValue.ToString("c");

                decimal intresetRatePercent = yearlyInterestRate / 100;
                calculationsTable[row, 0] = monthlyInvestment.ToString("c");
                calculationsTable[row, 1] = intresetRatePercent.ToString("p1");
                calculationsTable[row, 2] = years.ToString();
                calculationsTable[row, 3] = futureValue.ToString("c");
                row++;
            }
            catch (FormatException fex)
            {
                MessageBox.Show(fex.Message, fex.GetType().ToString());
            }
            catch (OverflowException)
            {
                MessageBox.Show("Overflow Error!", "Entry Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
            finally
            {
                Console.WriteLine("Finally Called!");
            }

            txtMonthlyInvestment.Focus();
        }

        private string IsDecimal(string value, string name)
        {
            string msg = "";

            if (!Decimal.TryParse(value, out _))
                msg = name + " must be a valid decimal value!\n";

            return msg;
        }

        private string IsInt32(string value, string name)
        {
            string msg = "";

            if (!Int32.TryParse(value, out _))
                msg = name + " must be a valid integer value!\n";

            return msg;
        }

        private string IsWithinRange(string value, string name, decimal min, decimal max)
        {
            string msg = "";
            
            if(Decimal.TryParse(value, out decimal number)) // if parsed correctly, number will hold the value
            {
                if (number < min || number > max)
                    msg = name + " must be between " + min + " and " + max + "\n";
            }

            return msg;
        }

        private bool IsValidData()
        {
            bool success = true;
            string errorMessage = "";
            errorMessage += IsDecimal(txtMonthlyInvestment.Text, txtMonthlyInvestment.Tag.ToString());
            errorMessage += IsWithinRange(txtMonthlyInvestment.Text, txtMonthlyInvestment.Tag.ToString(), 1, 1000);

            errorMessage += IsDecimal(txtYearlyInterestRate.Text, txtYearlyInterestRate.Tag.ToString());
            errorMessage += IsWithinRange(txtYearlyInterestRate.Text, txtYearlyInterestRate.Tag.ToString(), 1, 20);

            errorMessage += IsInt32(txtNumberOfYears.Text, txtNumberOfYears.Tag.ToString());
            errorMessage += IsWithinRange(txtNumberOfYears.Text, txtNumberOfYears.Tag.ToString(), 1, 40);

            if (errorMessage != "")
            {
                success = false;
                MessageBox.Show(errorMessage, "Entry Error");
            }
            return success;
        }

        private decimal CalculateFutureValue(decimal monthlyInvestment, decimal monthlyInterestRate, int months)
        {
            decimal futureValue = 0m;

            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + monthlyInvestment) * (monthlyInterestRate + 1);
            }
            return futureValue;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            string message = "Inv/Mo\tRate\tYears\tFuture Value\n";
            for (int i = 0; i<calculationsTable.GetLength(0); i++)
            {
                if (calculationsTable[i, 0] != null)
                {
                    for(int j=0; j<calculationsTable.GetLength(1); j++)
                    {
                        message += calculationsTable[i, j] +"\t";
                    }
                }
                message += "\n";
            }
            MessageBox.Show(message, "Future Value calculations");
            Close();
        }

        private void ClearFutureValue(object sender, EventArgs e)
        {
            txtFutureValue.Text = null;
        }
    }
}
