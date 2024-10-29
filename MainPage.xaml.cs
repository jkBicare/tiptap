namespace tiptap
{
    public partial class MainPage : ContentPage
    {
        private decimal billAmount = 0;
        private decimal tipPercentage = 0;
        private decimal tipAmount = 0;
        private int numPersons = 1;

        public MainPage()
        {
            InitializeComponent();
        }

        private void txtBill_Completed(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtBill.Text, out decimal bill))
            {
                if (bill > 0)
                {
                    billAmount = bill;
                    UpdateTip();
                }
                else
                {
                    txtBill.Text = billAmount.ToString();
                    ShowInputError("Please enter a bill amount greater than 0.");
                }
            }
        }

        private void btnTen_Clicked(object sender, EventArgs e)
        {
            tipPercentage = 10;
            tipAmount = 0;
            lblTip.Text = "Tip Percentage: 10%";
            UpdateTip();
        }

        private void btnfifteen_Clicked(object sender, EventArgs e)
        {
            tipPercentage = 15;
            tipAmount = 0;
            lblTip.Text = "Tip Percentage: 15%";
            UpdateTip();
        }

        private void btnTwenty_Clicked(object sender, EventArgs e)
        {
            tipPercentage = 20;
            tipAmount = 0;
            lblTip.Text = "Tip Percentage: 20%";
            UpdateTip();
        }

        private void sldTip_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            tipPercentage = (int)e.NewValue;
            tipAmount = 0;
            lblTip.Text = $"Tip Percentage: {tipPercentage}%";
            UpdateTip();
        }

        private void btnMinus_Clicked(object sender, EventArgs e)
        {
            if (numPersons > 1)
            {
                numPersons--;
                txtNumPerson.Text = numPersons.ToString();
                UpdateTip();
            }
        }

        private void btnPlus_Clicked(object sender, EventArgs e)
        {
            numPersons++;
            txtNumPerson.Text = numPersons.ToString();
            UpdateTip();
        }

        private void UpdateTip()
        {
            if (billAmount > 0)
            {
                decimal tip = (tipAmount > 0) ? tipAmount : billAmount * tipPercentage / 100;
                decimal subTotal = billAmount / numPersons;
                decimal perPersonTip = tip / numPersons;
                decimal totalAmount = subTotal + perPersonTip;

                lblTotal.Text = $"P{totalAmount:F2}";
                lblSubtotal.Text = $"P{subTotal:F2}";
                lblPerperson.Text = $"P{perPersonTip:F2}";
            }
        }

        private void txtPercentage_Completed(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtPercentage.Text, out decimal customTip))
            {
                if (customTip > 0)
                {
                    tipAmount = customTip;
                    tipPercentage = 0;
                    decimal showTip = (customTip / billAmount) * 100;
                    lblTip.Text = $"Tip Percentage: {showTip.ToString("F2")}%";
                    UpdateTip();
                }
                else
                {
                    txtPercentage.Text = tipAmount.ToString();
                    ShowInputError("Please enter a tip amount greater than 0.");
                }
            }
            else
            {
                ShowInputError("Please enter a valid amount.");
            }
        }

        private void txtNumPerson_Completed(object sender, EventArgs e)
        {
            if (int.TryParse(txtNumPerson.Text, out int person))
            {
                if (person < 1)
                {
                    numPersons = 1;
                }
                else
                {
                    numPersons = person;
                }
                txtNumPerson.Text = numPersons.ToString();
                UpdateTip();
            }
            else
            {
                ShowInputError("Please enter a valid number of persons (greater than 1).");
                txtNumPerson.Text = numPersons.ToString();
            }
        }

        private bool ValidateDecimalInput(string input, out decimal value)
        {
            return decimal.TryParse(input, out value) && value >= 0;
        }
        private void ShowInputError(string message)
        {
            DisplayAlert("Input Error", message, "OK");
        }
    }

}
