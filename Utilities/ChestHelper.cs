using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ShardsOfAtheria.Utilities
{
    public static class ChestHelper
    {
        public static Item DefaultItem(int type)
        {
            var item = new Item();
            item.SetDefaults(type);
            return item;
        }

        public static bool Insert(this Chest chest, int itemType, int itemStack, int index)
        {
            var item = DefaultItem(itemType);
            item.stack = itemStack;
            return InsertIntoUnresizableArray(chest.item, item, index);
        }

        public static bool Insert(this Chest chest, int itemType, int index)
        {
            return chest.Insert(itemType, 1, index);
        }

        public static bool InsertIntoUnresizableArray<T>(T[] arr, T value, int index)
        {
            if (index >= arr.Length)
            {
                return false;
            }
            for (int j = arr.Length - 1; j > index; j--)
            {
                arr[j] = arr[j - 1];
            }
            arr[index] = value;
            return true;
        }
    }
}
