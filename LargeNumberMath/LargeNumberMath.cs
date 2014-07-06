using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeNumberMath
{
    public class LNMath
    {
        // <summary>
        // Is console present - used later on for error processing.
        // </summary>
        private bool consolePresent()
        {
            // False if console present
            return (System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle != IntPtr.Zero);
        }

        // <summary>
        // Additon of two numbers in the form of strings.
        // </summary>
        public string add(string firstNum, string secondNum)
        {
            // Check to see which is larger number
            string firstNumString = firstNum.Length >= secondNum.Length ? firstNum : secondNum; // Larger number
            string secondNumString = firstNum.Length >= secondNum.Length ? secondNum : firstNum;

            int lenOfFirstNum = firstNumString.Length - 1;
            int lenOfSecondNum = secondNumString.Length - 1;
            int differenceOfLen = lenOfFirstNum - lenOfSecondNum;
            int carry = 0;

            string reference = "01234567890123456789";
            string result = String.Empty;

            for (int i = lenOfFirstNum; i >= 0; i--)
            {
                int ix = reference.IndexOf(firstNumString[i]);
                if (i <= lenOfSecondNum + differenceOfLen && lenOfFirstNum - i <= lenOfSecondNum)
                    ix += reference.IndexOf(secondNumString[i - differenceOfLen]);
                ix += carry;
                carry = ix > 9 ? 1 : 0;

                result = reference[ix] + result;
            }

            // Return result
            return carry > 0 ? result = '1' + result : result;
        }

        // <summary>
        // Multiply a string, which can be a number of any length, by an interger. 
        // </summary>
        public string multiply(string toBeMultiplied, int multiplyBy)
        {
            try
            {
                string product = toBeMultiplied;
                int carry = 0;
                int position;

                for (position = toBeMultiplied.Length; position >= 1; position--)
                {
                    string currentDigit = product.Substring(position - 1, 1); // Get digit in string value at current index
                    int intDigit = int.Parse(currentDigit);
                    int currentProduct = intDigit * multiplyBy; // Multiply by 'multiplyBy'
                    if (carry != 0)
                    {
                        currentProduct += carry % 10; // Add last digit in carry onto product
                        carry /= 10; // Take digit just added onto 'currentProduct' off of 'carry'
                    }
                    int currentCarry = currentProduct / 10; // Take all but the last digit off 'currentProduct' and add it to 'carry'
                    currentProduct %= 10; // Now only holds last digit
                    carry += currentCarry;

                    product = product.Remove(position - 1, 1);
                    product = product.Insert(position - 1, currentProduct.ToString());
                }

                // Add remaining 'carry' to front of 'product'
                if (carry != 0)
                {
                    product = carry.ToString() + product;
                }

                return product;
            }
            catch (Exception ex) // Display error message
            {
                // IF statement to work out if console or windows forms being used
                if (consolePresent() == false)
                    Console.WriteLine("Something went wrong:\n" + ex.Message);
                else if (consolePresent() == true)
                {
                    System.Windows.Forms.DialogResult diagResult = 
                        System.Windows.Forms.MessageBox.Show(
                            "An error occured:\n" + ex.Message + "\nOccured at: " + ex.StackTrace,
                            "Error Message",
                            System.Windows.Forms.MessageBoxButtons.RetryCancel,
                            System.Windows.Forms.MessageBoxIcon.Error
                        );

                    if (diagResult == System.Windows.Forms.DialogResult.Retry)
                        multiply(toBeMultiplied, multiplyBy);  
                }

                return String.Empty;
            }
        }

        // <summary>
        // Function for finding factorial of a number (must be an interger).
        // </summary>
        public string factorial(int number)
        {
            if (number < 1) // Validation of 'number'
                throw new Exception("The interger parsed into the function cannot be smaller than 1.");

            string product = "1"; // Initial start value

            for (int i = number; i > 1; i--)
            {
                product = multiply(product, i);
            }

            return product;
        }

    }
}
