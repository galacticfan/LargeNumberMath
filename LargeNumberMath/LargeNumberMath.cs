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
        private string ReverseInput(string inputString)
        {
            char[] charArray = inputString.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        // <summary>
        // A method to find the largest number from two strings of equal length.
        // </summary>
        private int LargestNumber(string firstNum, string secondNum)
        {
            int longest = 0;
            int length = firstNum.Length;

            for (int i = 0; i < length; i++)
            {
                if (firstNum[i] > secondNum[i])
                {
                    longest = 1;
                    break;
                }
                else if (firstNum[i] < secondNum[i])
                {
                    longest = 2;
                    break;
                }
            }

            return longest;
        }

        // <summary>
        // Additon of two numbers in the form of strings.
        // </summary>
        public string Add(string firstNumInput, string secondNumInput, bool areNumbersPositve = false)
        {
            if (areNumbersPositve == false) // Can set to true if you know both numbers will be positive (for performance purposes)
            {
                // Check to see if should be using 'Subtract()' method instead
                if (firstNumInput.StartsWith("-") && secondNumInput.StartsWith("-") == false)
                {
                    if (firstNumInput.TrimStart('-').Length > secondNumInput.Length)
                    {
                        return "-" + Subtract(firstNumInput.TrimStart('-'), secondNumInput);
                    }
                    else if (firstNumInput.TrimStart('-').Length < secondNumInput.Length)
                    {
                        return Subtract(secondNumInput, firstNumInput.TrimStart('-'));
                    }
                    else if (firstNumInput.TrimStart('-').Length == secondNumInput.Length)
                    {
                        int resultOfLargest = LargestNumber(firstNumInput.TrimStart('-'), secondNumInput);

                        switch (resultOfLargest)
                        {
                            case 1:
                                return "-" + Subtract(firstNumInput.TrimStart('-'), secondNumInput);
                            case 2:
                                return Subtract(secondNumInput, firstNumInput.TrimStart('-'));
                            case 0:
                                return "0";
                        }
                    }
                }
                else if (firstNumInput.StartsWith("-") == false && secondNumInput.StartsWith("-"))
                {
                    if (firstNumInput.Length > secondNumInput.TrimStart('-').Length)
                    {
                        return Subtract(firstNumInput, secondNumInput.TrimStart('-'));
                    }
                    else if (firstNumInput.Length < secondNumInput.TrimStart('-').Length)
                    {
                        return "-" + Subtract(secondNumInput.TrimStart('-'), firstNumInput);
                    }
                    else if (firstNumInput.Length == secondNumInput.TrimStart('-').Length)
                    {
                        int resultOfLargest = LargestNumber(firstNumInput, secondNumInput.TrimStart('-'));

                        switch (resultOfLargest)
                        {
                            case 1:
                                return Subtract(firstNumInput.TrimStart('-'), secondNumInput);
                            case 2:
                                return "-" + Subtract(firstNumInput.TrimStart('-'), secondNumInput);
                            case 0:
                                return "0";
                        }
                    }
                }
                else if (firstNumInput.StartsWith("-") && secondNumInput.StartsWith("-"))
                {
                    return Subtract(firstNumInput, secondNumInput.TrimStart('-'));
                }
            }

            // Reverse order of input strings
            string firstNum = ReverseInput(firstNumInput);
            string secondNum = ReverseInput(secondNumInput);

            char[] result = new char[Math.Max(firstNum.Length, secondNum.Length) + 1]; // Set to largest number
            int resultLength = 0;
            int carry = 0;

            // Treat the two numbers as having the same length by 'virtually' padding the shorter number with zeroes
            for (int i = 0; i < Math.Max(firstNum.Length, secondNum.Length); i++)
            {
                int currentDigFirstNum = (i < firstNum.Length) ? int.Parse(firstNum[i].ToString()) : 0;
                int currentDigSecondNum = (i < secondNum.Length) ? int.Parse(secondNum[i].ToString()) : 0;
                // Add the two digits and the carry
                int sumOfDigts = currentDigFirstNum + currentDigSecondNum + carry;

                if (sumOfDigts > 9)
                {
                    carry = 1;
                    sumOfDigts -= 10;
                }
                else
                {
                    carry = 0;
                }

                result[resultLength++] = (char)(sumOfDigts + '0');
            }

            // When carry isn't 0, store it in the result
            if (carry != 0)
                result[resultLength++] = '1';

            // Create the result string from the char array and reverse order
            return ReverseInput(new string(result, 0, resultLength));
        }

        // <summary>
        // Subtraction of two numbers in the form of strings.
        // </summary>
        public string Subtract(string firstNumInput, string secondNumInput, bool areNumbersPositive = false)
        {
            string firstNum = String.Empty;
            string secondNum = String.Empty;
            bool negative = false;

            if (areNumbersPositive == false) // Can set to true if you know both numbers will be positive (for performance purposes)
            {
                // Check to see if 'Add()' method should be used instead and deal with minus inputs
                if (firstNumInput.StartsWith("-") && secondNumInput.StartsWith("-") == false)
                {
                    return "-" + Add(firstNumInput.TrimStart('-'), secondNumInput);
                }
                else if (firstNumInput.StartsWith("-") == false && secondNumInput.StartsWith("-"))
                {
                    return Add(firstNumInput, secondNumInput.TrimStart('-'));
                }
                else if (firstNumInput.StartsWith("-") && secondNumInput.StartsWith("-"))
                {
                    string firstNumTrim = firstNumInput.TrimStart('-');
                    string secondNumTrim = secondNumInput.TrimStart('-');

                    if (firstNumTrim.Length > secondNumTrim.Length)
                    {
                        firstNumInput = firstNumTrim;
                        secondNumInput = secondNumTrim;
                    }
                    else if (firstNumTrim.Length < secondNumTrim.Length)
                    {
                        firstNumInput = secondNumTrim;
                        secondNumInput = firstNumTrim;
                    }
                    else if (firstNumTrim.Length == secondNumTrim.Length)
                    {
                        // Iterate through strings to find largest
                        int resultOfLargest = LargestNumber(firstNumTrim, secondNumTrim);

                        switch (resultOfLargest)
                        {
                            case 1:
                                firstNumInput = firstNumTrim;
                                secondNumInput = secondNumTrim;
                                negative = true;
                                break;
                            case 2:
                                firstNumInput = secondNumTrim;
                                secondNumInput = firstNumTrim;
                                break;
                            case 0:
                                return "0"; // Must be equal to each other
                        }
                    }
                }
            }

            // Reverse order of string input and set 'firstNum' to largest
            if (firstNumInput.Length > secondNumInput.Length)
            {
                firstNum = ReverseInput(firstNumInput);
                secondNum = ReverseInput(secondNumInput);
            }
            else if (firstNumInput.Length < secondNumInput.Length)
            {
                negative = true;
                firstNum = ReverseInput(secondNumInput);
                secondNum = ReverseInput(firstNumInput);
            }
            else if (firstNumInput.Length == secondNumInput.Length)
            {
                // Iterate through strings to find largest
                int largerString = LargestNumber(firstNumInput, secondNumInput);

                switch (largerString)
                {
                    case 1:
                        firstNum = ReverseInput(firstNumInput);
                        secondNum = ReverseInput(secondNumInput);
                        break;
                    case 2:
                        firstNum = ReverseInput(secondNumInput);
                        secondNum = ReverseInput(firstNumInput);
                        negative = true;
                        break;
                    case 0:
                        return "0"; // Must be equal to each other
                }

                //for (int i = 0; i <= firstNumInput.Length - 1; i++)
                //{
                //    if (firstNumInput[i] > secondNumInput[i])
                //    {
                //        firstNum = ReverseInput(firstNumInput);
                //        secondNum = ReverseInput(secondNumInput);
                //        break;
                //    }
                //    else if (firstNumInput[i] < secondNumInput[i])
                //    {
                //        firstNum = ReverseInput(secondNumInput);
                //        secondNum = ReverseInput(firstNumInput);
                //        negative = true;
                //        break;
                //    }
                //}

                //if (firstNum == String.Empty && secondNum == String.Empty)
                //{
                //    return "0"; // Must be equal to each other
                //}
            }

            char[] result = new char[firstNum.Length + 1];
            int resultLength = 0;
            int carry = 0;

            for (int i = 0; i < firstNum.Length; i++)
            {
                int currentDigFirstNum = (i < firstNum.Length) ? int.Parse(firstNum[i].ToString()) : 0;
                int currentDigSecondNum = (i < secondNum.Length) ? int.Parse(secondNum[i].ToString()) : 0;
                // Work out the difference
                int difference = currentDigFirstNum - currentDigSecondNum - carry;

                if (difference < 0)
                {
                    carry = 1;
                    difference += 10;
                }
                else { carry = 0; }

                result[resultLength++] = (char)(difference + '0');
            }

            // Create the result string from the char array and reverse order
            string finalResult = ReverseInput(new string(result, 0, resultLength));

            // Remove any leading zeros
            bool isZero = false;
            for (int i = 0; i <= finalResult.Length - 1; i++)
            {
                if (finalResult[i] == '0')
                {
                    isZero = true;
                }
                else
                {
                    isZero = false;
                    break;
                }
            }

            if (isZero)
                return "0";
            else
                finalResult = finalResult.TrimStart('0');

            // Check to see if answer is negative
            if (negative)
                finalResult = '-' + finalResult;

            return finalResult;
        }

        // <summary>
        // Multiply a string, which can be a number of any length, by an interger. 
        // </summary>
        public string Multiply(string toBeMultipliedInput, int multiplyBy)
        {
            try
            {
                string toBeMultiplied = String.Empty;
                bool productNeg = false;

                if (toBeMultipliedInput[0] == '-')
                {
                    productNeg = true;
                    toBeMultiplied = toBeMultipliedInput.Substring(0);
                }
                else if (multiplyBy < 0)
                {
                    productNeg = true;
                    multiplyBy *= -1;
                }
                else if (toBeMultipliedInput[0] == '-' && multiplyBy < 0)
                {
                    toBeMultiplied = toBeMultipliedInput.Substring(0);
                    multiplyBy *= -1;
                }
                else { toBeMultiplied = toBeMultipliedInput; }

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

                if (productNeg)
                {
                    string newProduct = '-' + product;
                    return newProduct;
                }
                else { return product; }
            }
            catch (Exception ex) // Display error message
            {
                // IF statement to work out if console or windows forms being used
                if (ConsolePresent() == false)
                    Console.WriteLine("An error occured:\n" + ex.Message + "\nOccured at: " + ex.StackTrace.ToString());
                else if (ConsolePresent() == true)
                {
                    System.Windows.Forms.DialogResult diagResult = 
                        System.Windows.Forms.MessageBox.Show(
                            "An error occured:\n" + ex.Message + "\nOccured at: " + ex.StackTrace.ToString(),
                            "Error Message",
                            System.Windows.Forms.MessageBoxButtons.RetryCancel,
                            System.Windows.Forms.MessageBoxIcon.Error
                        );

                    if (diagResult == System.Windows.Forms.DialogResult.Retry)
                        Multiply(toBeMultipliedInput, multiplyBy);  
                }

                return String.Empty;
            }
        }

        // <summary>
        // Function for finding factorial of a number (must be an interger).
        // </summary>
        public string Factorial(int number)
        {
            if (number < 1) // Validation of input
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
