using System;


namespace BusinessEntities
{
    public class ServingQueueItem : IServingQueueItem
    {

        public int ServingQueueItemID { get; set; }
        public int? BarSaleItemID { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime TimeCompleted { get; set; }
        public bool IsComplete { get; set; }
    
        public ServingQueueItem(DateTime timeAdded, DateTime timeCompleted)
        {
        }

        public ServingQueueItem(int servingQueueItemID)
        {
            ServingQueueItemID = servingQueueItemID;
            BarSaleItemID = 0;
            TimeAdded = new DateTime();
            TimeCompleted = new DateTime();
            IsComplete = false;
        }

        public ServingQueueItem(int servingQueueItemID, int? barSaleItemID, DateTime timeAdded, DateTime timeCompleted, bool isComplete)
        {
            ServingQueueItemID = servingQueueItemID;
            BarSaleItemID = barSaleItemID;
            TimeAdded = timeAdded;
            TimeCompleted = timeCompleted;
            IsComplete = isComplete;
        }

        public ServingQueueItem(DateTime timeAdded, DateTime timeCompleted, bool isComplete) : this(timeAdded, timeCompleted)
        {
            IsComplete = isComplete;
        }
    }
}
