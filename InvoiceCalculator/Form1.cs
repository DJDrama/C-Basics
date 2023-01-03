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

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            decimal subTotal = Convert.ToDecimal(txtEnterSubtotal.Text);
            decimal discountPercent = .25m;
            decimal discountAmount = Math.Round(subTotal * discountPercent, 2);
            decimal invoiceTotal = subTotal - discountAmount;

            maximumInvoice = Math.Max(maximumInvoice, invoiceTotal);
            minimumInvoice = Math.Min(minimumInvoice, invoiceTotal);

            txtMinimumInvoice.Text = minimumInvoice.ToString("c");
            txtMaximumInvoice.Text = maximumInvoice.ToString("c");

            txtSubtotal.Text = subTotal.ToString("c");
            txtDiscountPercent.Text = discountPercent.ToString("p1"); // percent formatting (p1)
            txtDiscountAmount.Text = discountAmount.ToString("c"); // currency formatting (c)
            txtTotal.Text = invoiceTotal.ToString("c");

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

            txtEnterSubtotal.Focus();

        }

    }
}
