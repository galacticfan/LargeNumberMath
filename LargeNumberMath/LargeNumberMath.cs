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
        // Multiply a string, which can be a number of any length, by an interger. 
        // </summary>
        public string multiply(string toBeMultiplied, int multiplyBy)
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
    }
}
