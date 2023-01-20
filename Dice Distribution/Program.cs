using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice_Distribution
{
    internal class Program
    {
        static List<int> RecSum(int facet, int amt, List<int> facetsARR) //метод получения 2х чисел от пользователя
        {
            List<int> secondFac = new List<int>(); //список с предыдущими суммами костей
            if (amt <= 2) //значение по умолчанию, когда дойдем до конца
            {
                secondFac = facetsARR; //равно значниям одной кости
            }
            else
            {
                secondFac = RecSum(facet, amt - 1, facetsARR); //предыдущие суммы костей
            }

            List<int> thFac = new List<int>(); //пустой список
            for (int i = 0; i < secondFac.Count; i++)
            {
                for (int j = 1; j <= facet; j++)
                {
                    thFac.Add(secondFac[i] + j); //заполняем старыми и новыми суммами костей
                }
            }

            return thFac;
        }

        static void ResultOutput(int diceFacet, int diceAmt, Dictionary<int, int> nums)
        {
            int maxVal = diceFacet * diceAmt; //максимальное значение суммы
            int minVal = diceAmt; //минимальная сумма костей

            for (int i = minVal; i <= maxVal; i++) //вывод результата
            {
                var vatiants = nums[i];

                Console.Write($"Сумма на костях: {i}. ");
                for (int j = Convert.ToString(i).Length; j < 8; j++)
                {
                    Console.Write(" ");
                }

                Console.Write($" Количество вариантов: {vatiants}. ");
                for (int j = Convert.ToString(vatiants).Length; j < 8; j++)
                {
                    Console.Write(" ");
                }

                var allVariantsAmt = Math.Pow(diceFacet, diceAmt);
                Console.Write($" Из: {allVariantsAmt}. ");
                for (int j = Convert.ToString(allVariantsAmt).Length; j < 8; j++)
                {
                    Console.Write(" ");
                }

                var chance = Math.Round(vatiants / Math.Pow(diceFacet, diceAmt) * 100, 3);
                Console.Write($" Шанс: {chance} ");
                for (int j = Convert.ToString(chance).Length; j < 5; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("%.");

                Console.WriteLine();
            }
        }

        static Dictionary<int, int> CalculateResult(int diceFacet, int diceAmt)
        {
            int maxVal = diceFacet * diceAmt; //максимальное значение суммы
            int minVal = diceAmt; //минимальная сумма костей

            List<int> facets = new List<int>(); //список значений по умолчанию
            for (int i = 1; i <= diceFacet; i++)
            {
                facets.Add(i);
            }

            List<int> bigFac = RecSum(diceFacet, diceAmt, facets); //конечный список с верным количеством вариантов

            var nums = new Dictionary<int, int>(); //словарь для удобной связи суммы кубов и количества вариантов
            for (int i = minVal; i <= maxVal; i++)
            {
                nums[i] = 0; //определяем размер и ключи словаря
            }
            for (int i = 0; i < bigFac.Count; i++)
            {
                nums[bigFac[i]] += 1; //заполняем значения словаря
            }

            return nums;
        }

        static int FacetsInput()
        {
            int facets = 0;
            while (true) //цикл проверки числа 
            {
                Console.WriteLine("Введите количество граней (от двух).");
                if ((int.TryParse(Console.ReadLine(), out int x)) && (x >= 2)) //условие проверки
                {
                    facets = x;
                    break; //выход из цикла проверки
                }
                Console.WriteLine("Введено некорректное значение!");
            }
            return facets;
        }

        static int DiceAmountInput()
        {
            int dice = 0;
            while (true) //цикл проверки числа 
            {
                Console.WriteLine("Введите количество костей (от двух).");
                if ((int.TryParse(Console.ReadLine(), out int x)) && (x >= 2)) //условие проверки
                {
                    dice = x;
                    break; //выход из цикла проверки
                }
                Console.WriteLine("Введено некорректное значение!");
            }
            return dice;
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(160, 50); //чуть увеличу консоль

            Console.WriteLine("Для выхода закройте приложение.\n");

            for (; ; )
            {
                try
                {
                    int diceFacet = FacetsInput(); //количество граней кости
                    int diceAmt = DiceAmountInput(); //количество костей

                    Console.WriteLine();

                    var nums = CalculateResult(diceFacet, diceAmt);

                    ResultOutput(diceFacet, diceAmt, nums);

                    Console.WriteLine("\nНажмите для продолжения.");
                    Console.ReadKey();
                }
                catch
                {
                    Console.WriteLine("Что-то пошло не так! :(");
                }
            }
        }
    }
}
