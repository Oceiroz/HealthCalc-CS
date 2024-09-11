using System;

namespace Health_Calculator
{
    class Program
    {
        public static bool isMetric, isMale, overForty;
        static void Main(string[] args)
        {
            double weight, height, mhr;
            while (true)
            {
                string[] helpOptions = {"Calculate Health", "Help"};
                int age, helpChoice;
                helpChoice = GetChoice("would you like help?", helpOptions);
                if (helpChoice == 1)
                {
                    Gender();
                    Metric();
                    weight = Weight();
                    height = Height();
                    BMI(weight, height);
                    age = Age();
                    mhr = MHR(age);
                    THR(mhr);
                    BMR(weight, height, age);
                }
                else
                {
                    Help();
                }
            }
        }
        static double GetDoubleInput(string inputMessage)
        {
            double userInput = 0;
            while (true)
            {
                Console.WriteLine($"{inputMessage}\n");
                string rawInput = Console.ReadLine();
                try
                {
                    userInput = double.Parse(rawInput);
                    if (userInput < 1)
                    {
                        throw new FormatException();
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("invalid input");
                }
            }
            return userInput;
        }
        static int GetIntInput(string inputMessage)
        {
            int userInput = 0;
            while (true)
            {
                Console.WriteLine($"{inputMessage}\n");
                string rawInput = Console.ReadLine();
                try
                {
                    userInput = int.Parse(rawInput);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("invalid input");
                }
            }
            return userInput;
        }
        static int GetChoice(string inputMessage, string[] options)
        {
            int choice = 0;
            while (true)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"\n{i + 1} ---> {options[i]}\n");
                }
                Console.WriteLine($"{inputMessage}\n");
                string rawInput = Console.ReadLine();
                try
                {
                    choice = int.Parse(rawInput);
                    if (choice > options.Length || choice <= 0)
                    {
                        throw new FormatException();
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("this is not a valid input");
                }
            }
            return choice;
        }
        static int Age()
        {
            int age = 0;
            while (true)
            {
                age = GetIntInput("How old are you");
                if (age > 39 && age < 120)
                {
                    overForty = true;
                    break;
                }
                else if (age > 8 && age < 40)
                {
                    overForty = false;
                    break;
                }
                else
                {
                    Console.WriteLine("invalid age");
                }
            }
            return age;
        }
        static void Gender()
        {
            string[] choices = { "Male", "Female" };
            int choice = GetChoice("What is your gender?", choices);
            if (choice == 1)
            {
                isMale = true;
            }
            else
            {
                isMale = false;
            }
        }
        static void Metric()
        {
            string[] choices = { "Metric", "Imperial" };
            int choice = GetChoice("What measurement do you use", choices);
            if (choice == 1)
            {
                isMetric = true;
            }
            else
            {
                isMetric = false;
            }
        }

        static double ConversionWeight(double weight)
        {
            double newWeight = weight;
            if (isMetric == false)
            {
                newWeight = weight * 0.45359237;
            }
            return newWeight;
        }
        static double ConversionHeight(double height)
        {
            double newHeight = height;
            if (isMetric == false)
            {
                newHeight = height * 0.0254;
            }
            return newHeight;
        }
        static double Weight()
        {
            double weight = 0.0;
            if (isMetric == false)
            {
                weight = GetDoubleInput("What is your weight in pounds?");
            }
            else if (isMetric == true)
            {
                weight = GetDoubleInput("What is your weight in kilograms?");
            }
            weight = ConversionWeight(weight);
            return weight;
        }
        static double Height()
        {
            double height = 0.0;
            if (isMetric == false)
            {
                height = GetDoubleInput("What is your height in inches");
            }
            else if (isMetric == true)
            {
                height = GetDoubleInput("What is your height in centimeters");
            }
            height = ConversionHeight(height);
            return height;
        }
        static double BMI(double weight, double height)
        {
            string health = "";
            double bmi = weight / ((height/100) * (height/100));
            if (bmi < 18.5)
            {
                health = "underweight";
            }
            else if (bmi >= 18.5 && bmi <= 24.9)
            {
                health = "normal";
            }
            else if (bmi > 24.9 && bmi <= 29.9)
            {
                health = "overweight";
            }
            else if (bmi > 29.9)
            {
                health = "obese";
            }
            Console.WriteLine($"Your BMI is:{bmi}");
            Console.WriteLine($"You are {health}");
            return bmi;
        }
        static double MHR(int age)
        {
            double mhr = 0.0;
            if (overForty == false)
            {
                mhr = 220 - age;
            }
            else if (overForty == true)
            {
                mhr = 208 - (0.75 * age);
            }
            return mhr;
        }
        static double THR(double mhr)
        {
            double lhr, uhr, thr;
            lhr = mhr * 0.5;
            uhr = mhr * 0.85;
            thr = (lhr + uhr) / 2;
            Console.WriteLine($"your target heart rate is: {thr}");
            return thr;
        }
        static double BMR(double weight, double height, int age)
        {
            double bmr = 0.0;
            if (isMale == true)
            {
                bmr = (10 * weight + 6.25 * height - 5 * age) + 5;
            }
            else if (isMale == false)
            {
                bmr = (10 * weight + 6.25 * height - 5 * age) - 161;
            }
            Console.WriteLine($"your bmr is: {bmr}");
            return bmr;
        }
        static void Help()
        {
            Console.WriteLine("To calculate BMI using metric units:");
            Console.WriteLine("Weight in Kilograms / Height in Meters^2\n");
            Console.WriteLine("To Calculate BMI using imperial units:");
            Console.WriteLine("Weight in Pounds * 703 / height in inches^2\n");
            Console.WriteLine("To calculate MHR for users below 40 years of age:");
            Console.WriteLine("220 – age\n");
            Console.WriteLine("To calculate MHR for users 40 and above years of age:");
            Console.WriteLine("208 – (0.75 * age)\n");
            Console.WriteLine("The target heart rate is any number in between the lower heart rate and upper heart rate.");
            Console.WriteLine("To calculate Lower heart rate:");
            Console.WriteLine("MHR * 0.5\n");
            Console.WriteLine("To calculate Upper heart rate:");
            Console.WriteLine("MHR * 0.85\n");
            Console.WriteLine("To calculate BMR for men using the Mifflin - St Jeor Formula formulae:");
            Console.WriteLine("(10 * weight in Kilograms + 6.25 * height in centimetres – 5 * age) + 5\n");
            Console.WriteLine("To calculate BMR for women using the Mifflin - St Jeor Formula formulae:");
            Console.WriteLine("(10 * weight in Kilograms + 6.25 * height in centimetres – 5 * age) – 161\n");
        }
    }
}
