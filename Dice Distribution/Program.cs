using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice_Distribution
{
    internal class Program
    {
        static List<ulong> RecSum(ulong facet, ulong amt, List<ulong> facetsARR) //метод получения 2х чисел от пользователя
        {
            List<ulong> secondFac = new List<ulong>(); //список с предыдущими суммами костей
            if (amt <= 2) //значение по умолчанию, когда дойдем до конца
            {
                secondFac = facetsARR; //равно значниям одной кости
            }
            else
            {
                secondFac = RecSum(facet, amt - 1, facetsARR); //предыдущие суммы костей
            }

            List<ulong> thFac = new List<ulong>(); //пустой список
            for (ulong i = 0; i < Convert.ToUInt64(secondFac.Count); i++)
            {
                for (ulong j = 1; j <= facet; j++)
                {
                    thFac.Add(secondFac[(int)i] + j); //заполняем старыми и новыми суммами костей
                }
            }

            return thFac;
        }

        static void ResultOutput(ulong diceFacet, ulong diceAmt, Dictionary<ulong, ulong> nums)
        {
            ulong maxVal = diceFacet * diceAmt; //максимальное значение суммы
            ulong minVal = diceAmt; //минимальная сумма костей

            for (ulong i = minVal; i <= maxVal; i++) //вывод результата
            {
                var vatiants = nums[i];

                Console.Write($"Сумма на костях: {i}. ");
                for (ulong j = Convert.ToUInt64(Convert.ToString(i).Length); j < 8; j++)
                {
                    Console.Write(" ");
                }

                Console.Write($" Количество вариантов: {vatiants}. ");
                for (ulong j = Convert.ToUInt64(Convert.ToString(vatiants).Length); j < 8; j++)
                {
                    Console.Write(" ");
                }

                var allVariantsAmt = Math.Pow(diceFacet, diceAmt);
                Console.Write($" Из: {allVariantsAmt}. ");
                for (ulong j = Convert.ToUInt64(Convert.ToString(allVariantsAmt).Length); j < 8; j++)
                {
                    Console.Write(" ");
                }

                var chance = Math.Round(vatiants / Math.Pow(diceFacet, diceAmt) * 100, 3);
                Console.Write($" Шанс: {chance} ");
                for (ulong j = Convert.ToUInt64(Convert.ToString(chance).Length); j < 5; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("%.");

                Console.WriteLine();
            }
        }

        static Dictionary<ulong, ulong> CalculateResult(ulong diceFacet, ulong diceAmt)
        {
            ulong maxVal = diceFacet * diceAmt; //максимальное значение суммы
            ulong minVal = diceAmt; //минимальная сумма костей

            List<ulong> facets = new List<ulong>(); //список значений по умолчанию
            for (ulong i = 1; i <= diceFacet; i++)
            {
                facets.Add(i);
            }

            List<ulong> bigFac = RecSum(diceFacet, diceAmt, facets); //конечный список с верным количеством вариантов

            var nums = new Dictionary<ulong, ulong>(); //словарь для удобной связи суммы кубов и количества вариантов
            for (ulong i = minVal; i <= maxVal; i++)
            {
                nums[i] = 0; //определяем размер и ключи словаря
            }
            for (ulong i = 0; i < Convert.ToUInt64(bigFac.Count); i++)
            {
                nums[bigFac[(int)i]] += 1; //заполняем значения словаря
            }

            return nums;
        }

        static ulong FacetsInput()
        {
            ulong facets = 0;
            while (true) //цикл проверки числа 
            {
                Console.WriteLine("Введите количество граней (от двух).");
                if ((ulong.TryParse(Console.ReadLine(), out ulong x)) && (x >= 2)) //условие проверки
                {
                    facets = x;
                    break; //выход из цикла проверки
                }
                Console.WriteLine("Введено некорректное значение!");
            }
            return facets;
        }

        static ulong DiceAmountInput()
        {
            ulong dice = 0;
            while (true) //цикл проверки числа 
            {
                Console.WriteLine("Введите количество костей (от двух).");
                if ((ulong.TryParse(Console.ReadLine(), out ulong x)) && (x >= 2)) //условие проверки
                {
                    dice = x;
                    break; //выход из цикла проверки
                }
                Console.WriteLine("Введено некорректное значение!");
            }
            return dice;
        }

        static Dictionary<ulong, ulong> AnotherCalcResult(ulong diceFacet, ulong diceAmt)
        {
            ulong maxVal = diceFacet * diceAmt; //максимальное значение суммы
            ulong minVal = diceAmt; //минимальная сумма костей

            var nums = new Dictionary<ulong, ulong>(); //словарь для удобной связи суммы кубов и количества вариантов
            for (ulong i = minVal; i <= maxVal; i++)
            {
                nums[i] = 0; //определяем размер и ключи словаря
            }

            List<ulong> numericList = new List<ulong>();
            for (ulong i = 0; i < diceAmt; i++)
            {
                numericList.Add(1);
                //Console.WriteLine(numericList[(int)i]);
                //Console.WriteLine();
            }




            return nums;
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(160, 50); //чуть увеличу консоль

            Console.WriteLine("Для выхода закройте приложение.\n");

            for (; ; )
            {
                //try
                //{
                ulong diceFacet = FacetsInput(); //количество граней кости
                ulong diceAmt = DiceAmountInput(); //количество костей

                Console.WriteLine();

                //var nums = CalculateResult(diceFacet, diceAmt);
                var nums = AnotherCalcResult(diceFacet, diceAmt);

                ResultOutput(diceFacet, diceAmt, nums);

                Console.WriteLine("\nНажмите enter для продолжения.");
                Console.ReadLine();
                //}
                //catch
                //{
                //    Console.WriteLine("Что-то пошло не так! :(");
                //}
            }
        }
    }
}
