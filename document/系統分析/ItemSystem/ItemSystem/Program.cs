using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemSystem
{
    public class ItemValue
    {
        public string name;
        public int id;
        public int type;
        public int att;
        public int def;
        public int hp;
        public int slots;
    }

    public class Deco
    {
        public ItemValue valueId;
        public Deco deco;
    }
    class Item
    {
        public int id;
        public ItemValue itemValue;
        public int slots;
        public Deco deco = null;
        public void InjectDeco(ItemValue id)
        {
            var newDeco = new Deco { valueId = id };
            newDeco.deco = deco;
            deco = newDeco;
        }
        public string Name()
        {
            string str = "";
            Deco node = deco;
            while(node!=null)
            {
                str += node.valueId.name;
                node = node.deco;
            }
            return str + itemValue.name;
        }
        // public string valueName;
    }

    class ItemV2
    {
        static int serno = 1;
        public int id = serno++;
        public ItemValue itemValue;
        public int slots;
        public List<ItemValue> prefixDeco = new List<ItemValue>();
        public List<ItemValue> suffixDeco = new List<ItemValue>();
        public void InjectDeco(ItemValue id)
        {
            prefixDeco.Add(id);
            prefixDeco = prefixDeco.OrderBy(v => v.id).ToList();
        }
        public string Name => string.Join("", prefixDeco.Select(v => v.name)) + itemValue.name;
    }

    class Program
    {
        static ItemValue sword = new ItemValue
        {
            name = "劍",
            id = 1,
            att = 3,
        };
        static ItemValue bow = new ItemValue
        {
            name = "弓",
            id = 2,
            att = 4,
        };
        static ItemValue bro = new ItemValue
        {
            name = "青銅",
            id = 1001,
            att = 1,
        };
        static ItemValue holy = new ItemValue
        {
            name = "神聖的",
            id = 1002,
            att = 1,
        };
        static void Main(string[] args)
        {
            Item item = new Item { itemValue = sword };
            item.InjectDeco(bro);
            item.InjectDeco(holy);
            Console.WriteLine(item.Name());
            ItemV2 item2= new ItemV2 { itemValue = bow };
            item2.InjectDeco(holy);
            item2.InjectDeco(bro);
            Console.WriteLine(item2.Name);
            ItemV2 item3 = new ItemV2 { itemValue = bow };
            item3.InjectDeco(bro);
            item3.InjectDeco(holy);
            Console.WriteLine(item3.Name);
        }
    }
}
