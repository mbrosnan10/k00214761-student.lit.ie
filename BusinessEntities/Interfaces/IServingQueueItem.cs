using System;

namespace BusinessEntities
{
    public interface IServingQueueItem
    {
         int ServingQueueItemID { get; set; }
         int? BarSaleItemID { get; set; }
         DateTime TimeAdded { get; set; }
         DateTime TimeCompleted { get; set; }
         bool IsComplete { get; set; }
    }
}
