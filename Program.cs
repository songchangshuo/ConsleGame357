using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleGame357
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"========================开始============================");
            Console.WriteLine();

            //简单实现
            RunSimple();

            Console.WriteLine();

            Console.WriteLine($"========================结束============================");

            Console.WriteLine();

            Console.WriteLine();

            Console.WriteLine($"========================开始============================");

            Console.WriteLine();

            //稍微加点拓展
            RunExpand();

            Console.WriteLine();
            Console.WriteLine($"========================结束============================");
        }


        #region 简单实现

        //List<List<int>> 可改为 Dictionary<int, List<int>>
        private static List<List<int>> lstRow = new List<List<int>>();

        private static List<string> lstRowName = new List<string> { "一", "二", "三" };

        private static List<int> lstRowChild1 = new List<int> { 1, 2, 3 };

        private static List<int> lstRowChild2 = new List<int> { 1, 2, 3, 4, 5 };

        private static List<int> lstRowChild3 = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

        private static Random random = new Random();

        /// <summary>
        /// 执行简单实现
        /// </summary>
        private static void RunSimple()
        {

            lstRow.Add(lstRowChild1);
            lstRow.Add(lstRowChild2);
            lstRow.Add(lstRowChild3);

            while (lstRow.Count > 0)
            {
                TakeToothpickSimple("张三");

                if (lstRow.Count == 0)
                {
                    break;
                }

                TakeToothpickSimple("李四");
            }

        }

        /// <summary>
        /// 拿牙签
        /// </summary>
        private static void TakeToothpickSimple(string strUserName)
        {

            //获取随机行
            int intRandomIndex = random.Next(0, lstRow.Count);

            var lstRowChild = lstRow[intRandomIndex];

            string strRowName = lstRowName[intRandomIndex];

            Console.WriteLine($"【{strUserName}】将拿走第【{strRowName}】行的牙签");


            if (lstRowChild?.Count == 0)
            {
                return;
            }

            //本次要拿的随机数量
            int intRandomNum = random.Next(1, lstRowChild.Count);

            Console.WriteLine($"【{strUserName}】将拿走第【{strRowName}】行【{intRandomNum}】根牙签");

            //清除行内数据 当作拿牙签
            lstRowChild.RemoveRange(0, intRandomNum);

            //当本行拿空的时候 从整体中剔除本行
            if (lstRowChild.Count == 0)
            {
                Console.WriteLine($"======第【{strRowName}】行牙签【{strUserName}】已拿光=======");

                lstRowName.RemoveAt(intRandomIndex);

                lstRow.RemoveAt(intRandomIndex);
            }

            if (lstRow.Count == 0)
            {
                Console.WriteLine();

                Console.WriteLine($"======【{strUserName}】在最后一个拿牙签，输了=====");
            }
        }


        #endregion


        #region 稍微加点拓展


        /// <summary>
        /// 牙签整体 
        /// </summary>
        private static Dictionary<int, List<int>> dicRow = new Dictionary<int, List<int>>();
        /// <summary>
        /// 玩家
        /// </summary>
        private static List<string> listUser = new List<string> { "张三", "李四", "王五", "赵六" };
        /// <summary>
        /// 每行数量
        /// </summary>
        private static List<int> listRowConfig = new List<int> { 3, 5, 7, 9, 11, 13, 15, 17, 19 };


        /// <summary>
        /// 拓展实现
        /// </summary>
        private static void RunExpand()
        {
            #region 初始化整体

            int n = 0;

            foreach (var row in listRowConfig)
            {
                n++;

                var lstRowThis = new List<int>();

                for (int i = 1; i < row + 1; i++)
                {
                    lstRowThis.Add(i);
                }

                dicRow.Add(n, lstRowThis);
            }



            #endregion

            while (dicRow.Any(d=>d.Value?.Count>0))
            {
                foreach (var user in listUser)
                {
                    TakeToothpick(user);
                }
            }
        }

        /// <summary>
        /// 拿牙签
        /// </summary>
        private static void TakeToothpick(string strUserName)
        {

            List<int> lst = dicRow.Where(d => d.Value?.Count > 0).Select(d => d.Key).ToList();

            if (lst.Count == 0)
            {
                return;
            }

            //获取随机行
            int intRandomIndex = random.Next(0, lst.Count);
            
            var kvpRow = dicRow.First(info => info.Key == lst[intRandomIndex]);

            string strRowName = kvpRow.Key.ToString();

            var lstRowChild = kvpRow.Value;

            if (lstRowChild?.Count == 0)
            {
                return;
            }

            //本次要拿的随机数量
            int intRandomNum = random.Next(1, lstRowChild.Count);

            Console.WriteLine($"【{strUserName}】将拿走第【{strRowName}】行【{intRandomNum}】根牙签");

            //清除行内数据 当作拿牙签
            kvpRow.Value.RemoveRange(0, intRandomNum);

            //当本行拿空的时候 从整体中剔除本行
            if (kvpRow.Value.Count == 0)
            {
                Console.WriteLine($"======第【{strRowName}】行牙签【{strUserName}】已拿光=======");
            }

            if (dicRow.All(d => d.Value?.Count == 0))
            {
                Console.WriteLine();

                Console.WriteLine($"======【{strUserName}】在最后一个拿牙签，输了=====");
            }
        }

        #endregion
    }
}
