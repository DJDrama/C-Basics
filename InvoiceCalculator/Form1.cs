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
        public fimInvoiceCalculator()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            decimal subTotal = Convert.ToDecimal(txtSubtotal.Text);
            decimal discountPercent = 0m; // m indicates decimal, not an integer.
            if (subTotal >= 500)
            {
                discountPercent = .2m;
            }
            else if (subTotal >= 250 && subTotal < 500)
            {
                discountPercent = .15m;
            }
            else if (subTotal >= 100 && subTotal < 250)
            {
                discountPercent = .1m;
            }

            decimal discountAmount = subTotal * discountPercent;
            decimal invoiceTotal = subTotal - discountAmount;

            txtDiscountPercent.Text = discountPercent.ToString("p1"); // percent formatting (p1)
            txtDiscountAmount.Text = discountAmount.ToString("c"); // currency formatting (c)
            txtTotal.Text = invoiceTotal.ToString("c");

            txtSubtotal.Focus(); 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
