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
        private bool ConsolePresent()
        {
            // False if console present
            return (System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle != IntPtr.Zero);
        }

        // <summary>
        // A method to reverse the order of a string.
        // </summary>
        static string ReverseInput(string inputString)
        {
            char[] charArray = inputString.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        // <summary>
        // Additon of two numbers in the form of strings.
        // </summary>
        static string Add(string firstNumInput, string secondNumInput)
        {
            // Reverse order of input strings
            string firstNum = ReverseInput(firstNumInput);
            string secondNum = ReverseInput(secondNumInput);

            char[] result = new char[Math.Max(firstNum.Length, secondNum.Length) + 1]; // Set to largest number
            int resultLength = 0;
            int carry = 0;

            // Treat the two numbers as having the same length by 'virtually' padding the shorter number with zeroes
            for (int i = 0; i < Math.Max(firstNum.Length, secondNum.Length); i++)
            {
                int an = (i < firstNum.Length) ? int.Parse(firstNum[i].ToString()) : 0;
                int bn = (i < secondNum.Length) ? int.Parse(secondNum[i].ToString()) : 0;
                // Add the two digits and the carry
                int rn = an + bn + carry;

                if (rn > 9)
                {
                    carry = 1;
                    rn -= 10;
                }
                else
                {
                    carry = 0;
                }

                result[resultLength++] = (char)(rn + '0');
            }

            // When carry isn't 0, store it in the result
            if (carry != 0)
                result[resultLength++] = '1';

            // Create the result string from the char array and reverse order
            return ReverseInput(new string(result, 0, resultLength));
        }

        // <summary>
        // Multiply a string, which can be a number of any length, by an interger. 
        // </summary>
        public string Multiply(string toBeMultiplied, int multiplyBy)
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
                if (ConsolePresent() == false)
                    Console.WriteLine("Something went wrong:\n" + ex.Message);
                else if (ConsolePresent() == true)
                {
                    System.Windows.Forms.DialogResult diagResult = 
                        System.Windows.Forms.MessageBox.Show(
                            "An error occured:\n" + ex.Message + "\nOccured at: " + ex.StackTrace,
                            "Error Message",
                            System.Windows.Forms.MessageBoxButtons.RetryCancel,
                            System.Windows.Forms.MessageBoxIcon.Error
                        );

                    if (diagResult == System.Windows.Forms.DialogResult.Retry)
                        Multiply(toBeMultiplied, multiplyBy);  
                }

                return String.Empty;
            }
        }

        // <summary>
        // Function for finding factorial of a number (must be an interger).
        // </summary>
        public string Factorial(int number)
        {
            if (number < 1) // Validation of 'number'
                throw new Exception("The interger parsed into the function cannot be smaller than 1.");

            string product = "1"; // Initial start value

            for (int i = number; i > 1; i--)
            {
                product = Multiply(product, i);
            }

            return product;
        }

    }
}
