using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContract.Contracts
{
    public class InventoryItemCreated : DemoEvent
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }

    public class DemoEvent
    {
        public DateTime CreatedAt{ get; set; } = DateTime.Now;
    }
}
