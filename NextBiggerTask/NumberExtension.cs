using System;

namespace NextBiggerTask
{
    public static class NumberExtension
    {
        /// <summary>
        /// Finds the nearest largest integer consisting of the digits of the given positive integer number and null if no such number exists.
        /// </summary>
        /// <param name="number">Source number.</param>
        /// <returns>
        /// The nearest largest integer consisting of the digits of the given positive integer and null if no such number exists.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when source number is less than 0.</exception>
        public static int? NextBiggerThan(int number)
        {
            _ = number >= 0 ? number : throw new ArgumentException($"Value of parameter {nameof(number)} cannot be negative.");

            // 1st step - searching for the first biggest digit from right to left.
            int numberCopy = number;
            int currentDigit = 0;
            int previousDigit;
            int tens = 10;
            for (; tens <= numberCopy * 10; tens *= 10)
            {
                previousDigit = currentDigit;
                currentDigit = (numberCopy % tens) / (tens / 10);

                if (previousDigit > currentDigit)
                {
                    numberCopy -= numberCopy % tens;

                    int remainderToAdd = previousDigit * (tens / 10);
                    remainderToAdd += currentDigit * (tens / 100);
                    remainderToAdd += number % (tens / 100);

                    numberCopy += remainderToAdd;
                    break;
                }
            }

            // 2nd step - check for possible less values.
            int remainderToCheck = numberCopy % tens;
            if (remainderToCheck == number)
            {
                return null;
            }

            for (int outerDigit = 10; outerDigit <= tens; outerDigit *= 10)
            {
                int mostRight = remainderToCheck % 10;
                for (int innerDigit = 100; innerDigit < tens; innerDigit *= 10)
                {
                    int compareWithMostRight = (remainderToCheck % innerDigit) / (innerDigit / 10);
                    if (mostRight < compareWithMostRight)
                    {
                        int additionalRemainder = remainderToCheck % (innerDigit / 100);
                        remainderToCheck -= remainderToCheck % innerDigit;
                        remainderToCheck += (mostRight * (innerDigit / 10)) + (compareWithMostRight * (innerDigit / 100)) + additionalRemainder;
                    }
                }
            }

            numberCopy -= numberCopy % tens;
            numberCopy += remainderToCheck;
            return numberCopy == number ? null : numberCopy;
        }
    }
}
