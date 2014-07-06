LargeNumberMath
===============

A C# library for performing maths on super-duper large numbers.

<h4>Usage Of The Library:</h4>

For the addition method:

    string result = objName.Add("firstNum", "secondNum");
For the multipication method:

    string result = objName.Multiply("firstNum", intNum);
For calculating factorials (input number must be larger than 0):

    string result = objName.Factorial(intNum);
Once more methods have been released, the README will be updated.

<h4>Newbie's Guide:</h4>
After <a href="https://github.com/galacticfan/LargeNumberMath/wiki/Usage-and-Installation-of-the-DLL">importing</a> your DLL and referencing the library, you are then ready to begin using the supplied methods.

To start, you will need to create an object of the class `LNMath` by doing the following:

    LNMath nameOfObject = new LNMath();
Then, using the object that you just created, you can access the methods within the library such as the `Add()` method.

    string result = nameOfObject.Add("firstNumber", "secondNumber");
<i>Please note that the methods within the library are designed to take in numbers in the form of strings so that there is no limit in the size of calculation.</i>