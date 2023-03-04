namespace GB
{
    public class ConsoleApp
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the small strings array finder!");
            Console.WriteLine("Please, insert your array, where elements are separated with comma (example: qwer, qwerty, fdss)");
            var input = Console.ReadLine();
            if (!TryParse(input, ',', out var arr))
            {
                Console.WriteLine("Sorry, program could not handle inserted value... Bye!");
            }

            var filteredArr = Select(arr, (string s) => s.Length < 4 );
            Console.WriteLine(filteredArr.ToStringArray());
        }

        static string[] Select(string[] arr, Func<string, bool> satisfies)
        {
            var lenght = 0;
            if (arr == null || (lenght = arr.Length) == 0)
            {
                return default(string[]);
            }

            var resultLenght = 0;
            var indexes = new int[lenght];
            for (var i = 0; i < lenght; i++)
            {
                var element = arr[i];
                if (element != null && satisfies(element))
                {
                    indexes[resultLenght] = i;
                    resultLenght++;
                }
            }

            var result = new string[resultLenght];

            for (; resultLenght > 0;)
            {
                resultLenght--;
                result[resultLenght] = arr[indexes[resultLenght]];
            }

            return result;
        }

        static bool TryParse(string input, char separator, out string[] arr)
        {
            arr = default(string[]);
            if (input == null)
            {
                return false;
            }

            var elementsCount = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] == separator) elementsCount++;
            }

            elementsCount++;
            var hasTrimmed = false;
            var newString = string.Empty;
            var resultCount = 0;
            arr = new string[elementsCount];
            for (var i = 0; i < input.Length; i++)
            {
                if (!hasTrimmed)
                {
                    if (input[i] == ' ')
                    {
                        hasTrimmed = true;
                        continue;
                    }
                }

                if (input[i] == ',')
                {
                    if (!string.IsNullOrEmpty(newString))
                    {
                        arr[resultCount] = newString;
                        resultCount++;
                        newString = string.Empty;
                    }

                    hasTrimmed = false;
                    continue;
                }

                newString += input[i];
            }

            if (!string.IsNullOrEmpty(newString))
            {
                arr[resultCount] = newString;
            }
            
            return true;
        }
    }

    public static class ArrayExtension
    {
        public static string ToStringArray(this string[] arr)
        {
            var result = "[";
            foreach (var element in arr)
            {
                result += $"\"{element}\", ";
            }

            if (result.Length > 2)
            {
                result = result.Remove(result.Length - 2);
            }
            
            result += "]";
            return result;
        }
    }
}