using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeNumberMath
{
    public class LNMath
    {
        public string add(void) // A lot of this code is temporary whilst I try to figure out what I'm actually trying to do
        {
            string firstNumber = "901"; // note: in reverse
            string secondNumber = "73";
            bool carry = false;
            string result = firstNumber; // large one

            for (int position = 0; position < secondNumber.Length; position++) // start with smallest
            {
                int firstNumberDigit = int.Parse(result[position].ToString());
                int secondNumberDigit = int.Parse(secondNumber[position].ToString());
                int sumOfDigits = firstNumberDigit + secondNumberDigit;

                int lastNum = sumOfDigits % 10;
                result = result.Remove(position, 1);
                result = result.Insert(position, lastNum.ToString());

                carry = false;

                if (sumOfDigits > 9)
                {
                    carry = true;

                    int currentCarry = sumOfDigits / 10;
                    //result = result.Remove(position + 1, 1);

                    int carryResult = int.Parse(result[position + 1].ToString()) + currentCarry;

                    //result = result.Insert(position + 1, carryResult.ToString());
                    result = result.Replace(result[position + 1], Convert.ToChar(carryResult.ToString())); 
                }

            }
        }

        // <summary>
        // Multiply a string, which can be a number of any length, by an interger. 
        // </summary>
        public string multiply(string toBeMultiplied, int multiplyBy)
        {
            if (toBeMultiplied.StartsWith("-") || multiplyBy < 1)
                throw new Exception("One of the numbers parsed into the function is smaller than 1.");

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
