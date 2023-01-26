using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dice_Distribution
{
    internal class Program
    {

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
                for (ulong j = Convert.ToUInt64(Convert.ToString(vatiants).Length); j < 6; j++)
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
                for (ulong j = Convert.ToUInt64(Convert.ToString(chance).Length); j < 6; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("%.");

                Console.WriteLine();
            }
        }

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

        //static List<ulong> StrRecSum(ulong facet, ulong amt, List<ulong> facetsARR) //метод получения 2х чисел от пользователя
        //{
        //    List<ulong> secondFac = new List<ulong>(); //список с предыдущими суммами костей
        //    if (amt <= 2) //значение по умолчанию, когда дойдем до конца
        //    {
        //        secondFac = facetsARR; //равно значниям одной кости
        //    }
        //    else
        //    {
        //        secondFac = StrRecSum(facet, amt - 1, facetsARR); //предыдущие суммы костей
        //    }

        //    List<ulong> thFac = new List<ulong>(); //пустой список
        //    for (ulong i = 0; i < Convert.ToUInt64(secondFac.Count); i++)
        //    {
        //        for (ulong j = 1; j <= facet; j++)
        //        {
        //            thFac.Add(secondFac[(int)i] + j); //заполняем старыми и новыми суммами костей
        //        }
        //    }

        //    return thFac;
        //}

        static Dictionary<ulong, ulong> StrCalculateResult(ulong diceFacet, ulong diceAmt)
        {
            ulong maxVal = diceFacet * diceAmt; //максимальное значение суммы
            ulong minVal = diceAmt; //минимальная сумма костей

            List<string> facets = new List<string>(); //список значений по умолчанию
            for (ulong i = 1; i <= diceFacet; i++)
            {
                facets.Add($"{i}");
            }

            List<string> bigFac = StrRS(diceFacet, diceAmt, facets); //конечный список с верным количеством вариантов

            var nums = new Dictionary<ulong, ulong>(); //словарь для удобной связи суммы кубов и количества вариантов
            for (ulong i = minVal; i <= maxVal; i++)
            {
                nums[i] = 0; //определяем размер и ключи словаря
            }
            for (ulong i = 0; i < Convert.ToUInt64(bigFac.Count); i++)
            {
                string[] words = bigFac[(int)i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (ulong j = 0; j < diceFacet; j++)
                {
                    //Console.WriteLine(words[(int)j]);
                    nums[Convert.ToUInt64(words[(int)j])] += 1; //заполняем значения словаря
                    //nums[bigFac[(int)i]] += 1; //заполняем значения словаря
                }
            }

            return nums;
        }

        static List<string> StrRS(ulong facet, ulong amt, List<string> facetArr)
        {
            //for (int i = 0; i < facetArr.Count; i++)
            //{
            //    Console.WriteLine(facetArr[i]);
            //}
            List<string> secondFac = new List<string>(); //список с предыдущими суммами костей
            if (amt <= 2) //значение по умолчанию, когда дойдем до конца
            {
                #region
                //secondFac.Capacity = facetArr.Count;
                //for (ulong i = 0; i < (ulong)facetArr.Count - 1; i += facet)
                //{

                //    //for (ulong j = i; j < facet + i; j++)
                //    for (ulong j = 0; j < facet; j++)
                //    {
                //        Console.WriteLine($"i = {i} j = {j} sf.c = {secondFac.Capacity} fA.C = {facetArr.Count}");
                //        Console.WriteLine(facetArr[(int)j] + 12); //все ок
                //        Console.WriteLine(secondFac[(int)i] + 1);
                //        secondFac[(int)i] += ($"{Convert.ToString(facetArr[(int)j])},");
                //    }
                //}
                #endregion
                string firstStr = "";
                for (int i = 0; i < facetArr.Count; i++)
                {
                    firstStr += ($"{Convert.ToString(facetArr[(int)i])},");
                }
                secondFac.Add(firstStr);
                //secondFac = facetsARR; //равно значниям одной кости
            }
            else
            {
                secondFac = StrRS(facet, amt - 1, facetArr); //предыдущие суммы костей
            }

            List<string> thFac = new List<string>(/*secondFac.Count * (int)facet*/);
            //Console.WriteLine(thFac[1]);
            for (int i = 0; (int)i < Convert.ToInt32(secondFac.Count * (int)facet); i++)
            {
                thFac.Add("");
            }
            ulong tfc = 0;
            for (ulong i = 0; i < Convert.ToUInt64(secondFac.Count); i++)
            //for (ulong i = 0; i < Convert.ToUInt64(thFac.Count); i++)
            {
                string[] words = secondFac[(int)i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (ulong j = 0; j < (ulong)words.Length; j++)
                {
                    for (ulong k = 1; k <= facet; k++)
                    {
                        #region
                        //Console.WriteLine(words[(int)j]); //все ок
                        //Console.WriteLine(tfc);
                        //Console.WriteLine(words[tfc]);
                        //string tfc2 = Convert.ToString(tfc);
                        //Console.WriteLine(thFac[Convert.ToInt32(tfc2)]);
                        //Console.WriteLine(thFac[(int)tfc]);
                        #endregion
                        thFac[(int)tfc] += ($"{Convert.ToString((Convert.ToInt32(words[(int)j])) + (int)k)},");
                    }
                    //Console.WriteLine(thFac[(int)tfc]);
                    tfc++;
                }
            }

            return thFac;
        }

        #region
        //static Dictionary<ulong, ulong> StrCalculateResult(ulong diceFacet, ulong diceAmt)
        //{
        //    ulong maxVal = diceFacet * diceAmt; //максимальное значение суммы
        //    ulong minVal = diceAmt; //минимальная сумма костей

        //    List<ulong> facets = new List<ulong>(); //список значений по умолчанию
        //    for (ulong i = 1; i <= diceFacet; i++)
        //    {
        //        facets.Add(i);
        //    }

        //    List<ulong> bigFac = StrRecSum(diceFacet, diceAmt, facets); //конечный список с верным количеством вариантов

        //    var nums = new Dictionary<ulong, ulong>(); //словарь для удобной связи суммы кубов и количества вариантов
        //    for (ulong i = minVal; i <= maxVal; i++)
        //    {
        //        nums[i] = 0; //определяем размер и ключи словаря
        //    }
        //    for (ulong i = 0; i < Convert.ToUInt64(bigFac.Count); i++)
        //    {
        //        nums[bigFac[(int)i]] += 1; //заполняем значения словаря
        //    }

        //    return nums;
        //}
        #endregion

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
                //ResultOutput(diceFacet, diceAmt, nums);

                //var nums = AnotherCalcResult(diceFacet, diceAmt);
                //vivod(diceFacet, diceAmt);
                var nums = StrCalculateResult(diceFacet, diceAmt);
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
