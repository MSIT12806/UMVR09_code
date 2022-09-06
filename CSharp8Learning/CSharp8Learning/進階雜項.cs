using System;

namespace AdvanceCS
{
    class Program
    {
        class Npc {
            [NpcExt("ss")]
            string MobName;
        }
        abstract class ExtAttribute : Attribute { }
        class NpcExtAttribute : ExtAttribute
        {
            public string Name { get; set; }
            public NpcExtAttribute(string name)
            {
                Name = name;
            }
        }
        static void Main(string[] args)
        {
            #region sugar
            // var

            //※
            // ??
            // (8.0) ??=
            Npc npc = null;
            npc ??= new Npc();

            // nullable
            // out 變數
            // 具名引數
            // internal/ref/in/out/readonly

            //※
            // (8.0) 索引 ^0 ^1 ...
            // (8.0)範圍 0..^0 => 從後面數過來(==Length-val)
            var v = 1..4;
            var npcs = new Npc[9];
            var qq = npcs[v];
            #endregion
            #region Inteface set/get來模擬變數
            #endregion
            #region Using 變數(括住的範圍結尾"處置"所宣告的變數)
            // 傳統 using (var file = new )
            // 新版 using var file = ...
            #endregion
            #region tuple 元組 +   捨棄的支援(更容易閱讀的程式碼流程)   
            // 基本用法
            (int, string, DT) TryTuple<DT>() { return (3, "3", Activator.CreateInstance<DT>()); }
            // 具名用法
            (int he, string llo, DT) TryTupleWithName<DT>() { return (3, "3", Activator.CreateInstance<DT>()); }
            // "_" 捨棄特定變數
            (var _, var _, Npc onlyIt) = TryTuple<Npc>();
            // Swap資料交換應用
            double a = 50;
            double b = 20;
            (a, b) = (b, a);

            // 解構: 每個值宣告不同變數 
            // 類別解構
            //public class Point
            //{
            //    public double X { get; set; }
            //    public double Y { get; set; }

            //    public void Deconstruct(out double x, out double y) =>
            //        (x, y) = (X, Y);
            //}
            #endregion
            #region             if/switch 模式比對
            // is 運算子
            // switch(不再限於整數型別、Enum 型別、string)
            // when
            int SumPositiveNumbers(IEnumerable<object> sequence)
            {
                int sum = 0;
                foreach (var i in sequence)
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case IEnumerable<int> childSequence:
                            {
                                foreach (var item in childSequence)
                                    sum += (item > 0) ? item : 0;
                                break;
                            }
                        case int n when n > 0:
                            sum += n;
                            break;
                        case null:
                            throw new NullReferenceException("Null found in sequence");
                        default:
                            throw new InvalidOperationException("Unrecognized type");
                    }
                }
                return sum;
            }
            #endregion
            #region Switch 運算式 (8.0 .net core 3.x .net standard2.1)

            // data switch vs switch(data)
            // 屬性模式: data switch{ { data 內的屬性}=>, }
            object switchSampleValue = 5;
            int c = switchSampleValue.GetType().Name switch
            {
                "Int32" => 0,
                "String" => 1,
                //...
                _ => throw new NotImplementedException(),//其他所有情況
            };
            // tuple模式: (t1, t2) switch{ (v1, v2)=>, }
            var test = ('A', 'B');
            var result = test switch
            {
                ('A', 'B') => "OK",
                ('A', _) => "First part OK",
                (_, 'B') => "Second part OK",
                _ => "Not OK",
            };

            #endregion
            #region Type 類別的類別 + relection技術
            //找到從基類派生的所有類
            IEnumerable<BaseClass> GetAll<BaseClass>() where BaseClass : class
            {
                return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsSubclassOf(typeof(BaseClass)))
                    .Select(type => Activator.CreateInstance(type) as BaseClass);
            }
            #endregion
            #region Enum列舉
            // Count應用
            // Enum.GetNames/Enum.GetValues
            #endregion
            #region Delegate
            // 宣告型(預設型)
            // += = -=
            // lamda
            #endregion
            #region template
            // 基本
            // 類別樣板
            // 函式樣板
            // 限制式
            // 轉型應用
            #endregion
            #region default
            // default & template
            #endregion
            #region 區域函式
            var aa = Func();
            int Func() { return 0; }
            #endregion
            #region             非同步(Task/async/await)
            // 支援unity(PS: 官方建議不保證，因unity為Thread不友善)
            #endregion
            #region Attribute & Reflection
            // Attribute => 請繼承這個類，若此繼承者尾綴Attribute，使用時請省略。
            // Type
            string n= ((NpcExtAttribute)typeof(Npc).GetProperty("MobName").GetCustomAttributes(false)[0]).Name;
            #endregion
        }
    }
}
