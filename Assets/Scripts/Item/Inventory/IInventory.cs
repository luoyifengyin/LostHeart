using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameApplication.Item.Inventory {
    public interface IInventory {
        int AddItem(int id, ItemType type, int cnt);
    }
}
