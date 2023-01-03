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
    public partial class fimInvoiceCalculator : Form
    {
        int numberOfInvoices = 0;
        decimal totalOfInvoices = 0m;
        decimal invoiceAverage = 0m;

        decimal minimumInvoice = Decimal.MaxValue;
        decimal maximumInvoice = 0;

        public fimInvoiceCalculator()
        {
            InitializeComponent();
        }

        private (decimal discountPercent, decimal discountAmount, decimal invoiceTotal) GetInvoiceAmounts(string customerType, decimal subTotal)
        {
            decimal discountPercent = 0m;
            switch (customerType)
            {
                case "R":
                case "r":
                    if (subTotal < 100)
                        discountPercent = .0m;
                    else if (subTotal >= 100 && subTotal < 250)
                        discountPercent = .1m;
                    else
                        discountPercent = .25m;
                    break;
                case "C":
                case "c":
                    if (subTotal < 250)
                        discountPercent = .2m;
                    else
                        discountPercent = .3m;
                    break;
                case "T":
                case "t":
                    if (subTotal < 500)
                        discountPercent = .4m;
                    else
                        discountPercent = .5m;
                    break;
                default:
                    discountPercent = .4m;
                    break;
            }

            decimal discountAmount = Math.Round(subTotal * discountPercent, 2);
            decimal invoiceTotal = subTotal - discountAmount;

            return (discountPercent, discountAmount, invoiceTotal);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string customerType = txtCustomerType.Text;
            decimal subTotal = Convert.ToDecimal(txtEnterSubtotal.Text);

            // Tuple
            (decimal discountPercent, 
                decimal discountAmount, 
                decimal invoiceTotal) = GetInvoiceAmounts(customerType, subTotal);

         
            maximumInvoice = Math.Max(maximumInvoice, invoiceTotal);
            minimumInvoice = Math.Min(minimumInvoice, invoiceTotal);

            txtMinimumInvoice.Text = minimumInvoice.ToString("c");
            txtMaximumInvoice.Text = maximumInvoice.ToString("c");

            txtSubtotal.Text = subTotal.ToString("c");
             txtTotal.Text = invoiceTotal.ToString("c");

            txtDiscountPercent.Text = discountPercent.ToString("p1"); // percent formatting (p1)
            txtDiscountAmount.Text = discountAmount.ToString("c"); // currency formatting (c)

            numberOfInvoices++;
            totalOfInvoices += invoiceTotal;
            invoiceAverage = totalOfInvoices / numberOfInvoices;

            txtNumberOfInvoices.Text = numberOfInvoices.ToString();
            txtTotalOfInvoices.Text = totalOfInvoices.ToString("c");
            txtInvoiceAverage.Text = invoiceAverage.ToString("c");

            txtEnterSubtotal.Text = null; // or .Text = "";
            txtEnterSubtotal.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            numberOfInvoices = 0;
            totalOfInvoices = 0m;
            invoiceAverage = 0m;

            txtNumberOfInvoices.Text = null;
            txtTotalOfInvoices.Text = null;
            txtInvoiceAverage.Text = null;
            txtDiscountPercent.Text = null;
            txtSubtotal.Text = null;
            txtDiscountAmount.Text = null;
            txtTotal.Text = null;
            txtEnterSubtotal.Text = null;

            txtMinimumInvoice.Text = null;
            txtMaximumInvoice.Text = null;
            txtCustomerType.Text = null;
            
        }
    }
}
