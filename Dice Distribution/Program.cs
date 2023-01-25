using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                for (int j = Convert.ToString(chance).Length; j < 7; j++)
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

            List<int> facetsSum = new List<int>(); //список значений по умолчанию
            for (int i = 1; i <= diceFacet; i++)
            {
                facetsSum.Add(i);
            }

            List<int> bigFac = RecSum(diceFacet, diceAmt, facetsSum); //конечный список с верным количеством вариантов

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

        static Dictionary<ulong, ulong> ExperimentResult(ulong diceFacet, ulong diceAmt)
        {
            ulong maxVal = diceFacet * diceAmt; //максимальное значение суммы
            ulong minVal = diceAmt; //минимальная сумма костей

            string dF = Convert.ToString(diceFacet);
            int dicFac = Convert.ToInt32(dF);

            var nums = new Dictionary<ulong, ulong>(); //словарь для удобной связи суммы кубов и количества вариантов
            for (ulong i = minVal; i <= maxVal; i++)
            {
                nums[i] = 0; //определяем размер и ключи словаря
            }

            var numsNonRep = new List<bool>();
            for (ulong i = 0; i <= diceFacet; i++)
            {
                numsNonRep.Add(true);
            }


            ulong amtTests = Convert.ToUInt64(Math.Pow(diceFacet, diceAmt)) * 10;
            for (ulong i = 0; i < amtTests; i++) //сколько раз надо провести эксперимент?
            {

                ulong sumGenNums = 0;
                for (ulong j = 1; j <= diceAmt; j += 1)
                {
                    Random x = new Random(); // объявление переменной для генерации чисел
                    int number = x.Next(1, dicFac + 1); //минимум входит, а максимум - нет
                    if (numsNonRep[number] == true)
                    {
                        sumGenNums += (ulong)number;


                        numsNonRep[number] = !numsNonRep[number];
                    }
                    else
                    {
                        Thread.Sleep(x.Next(1, 4));
                        numsNonRep[number] = !numsNonRep[number];
                        j -= 1;
                    }
                    //sumGenNums += (ulong)number;
                    Thread.Sleep(x.Next(0, 3));
                }
                //Console.WriteLine(sumGenNums);
                nums[sumGenNums] += 1;
            }
            return nums;
        }

        static void ResultOutputULONG(ulong diceFacet, ulong diceAmt, Dictionary<ulong, ulong> nums)
        {
            //у меня не получилось применить обобщения (тип "T"), поэтому просто новый метод с верными типами данных
            //можно и математический метод переделать под вывод с ulong, но мне лень

            ulong maxVal = diceFacet * diceAmt; //максимальное значение суммы
            ulong minVal = diceAmt; //минимальная сумма костей

            for (ulong i = minVal; i <= maxVal; i++) //вывод результата
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
                for (int j = Convert.ToString(chance).Length; j < 7; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("%.");

                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(160, 50); //чуть увеличу консоль

        exitInBeg: //оправдано ли применение goto?

            Console.WriteLine("Для выхода закройте приложение.\n");

            Console.WriteLine("Выберите метод: математический (1) или экспериментальный (2).\n");

            int method = 0;
            while (true) //цикл проверки числа 
            {
                Console.WriteLine("Введите \"1\" или \"2\".");
                if ((int.TryParse(Console.ReadLine(), out int x)) && (x == 1 || x == 2)) //условие проверки
                {
                    method = x;
                    break; //выход из цикла проверки
                }
                Console.WriteLine("Введено некорректное значение!");
            }

            if (method == 1)
            {
                for (; ; )
                {
                    try
                    {
                        int diceFacet = FacetsInput(); //количество граней кости
                        int diceAmt = DiceAmountInput(); //количество костей

                        Console.WriteLine();

                        var nums = CalculateResult(diceFacet, diceAmt); //основная работа подсчета

                        ResultOutput(diceFacet, diceAmt, nums); //вывод результата

                        Console.WriteLine("\nНажмите для продолжения.");
                        Console.WriteLine("Для выхода из математического метода нажмите \"e\".");
                        var exit = Console.ReadLine();
                        if (exit == "e")
                        {
                            goto exitInBeg; //оправдано ли применение goto? -узнаю потом
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Что-то пошло не так! :(");
                    }
                }
            }

            if (method == 2)
            {
                for (; ; )
                {
                    //try
                    //{
                    int diceFacet = FacetsInput(); //количество граней кости
                    int diceAmt = DiceAmountInput(); //количество костей

                    Console.WriteLine();

                    //var nums = ExperimentResult((int)diceFacet, (int)diceAmt); //основная работа подсчета
                    var nums = ExperimentResult((ulong)diceFacet, (ulong)diceAmt); //основная работа подсчета

                    ResultOutputULONG((ulong)diceFacet, (ulong)diceAmt, nums); //вывод результата

                    Console.WriteLine("\nНажмите для продолжения.");
                    Console.WriteLine("Для выхода из экспериментального метода нажмите \"e\".");
                    var exit = Console.ReadLine();
                    if (exit == "e")
                    {
                        goto exitInBeg; //оправдано ли применение goto? -узнаю потом
                    }
                    //}
                    //catch
                    //{
                    //    Console.WriteLine("Что-то пошло не так! :(");
                    //}
                }
            }
        }
    }
}
