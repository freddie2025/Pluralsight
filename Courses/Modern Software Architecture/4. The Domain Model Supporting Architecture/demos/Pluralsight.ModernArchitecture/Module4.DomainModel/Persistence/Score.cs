using System;

namespace Module4.DomainModel.Persistence
{
    public class Score  
    {
        public Score()
        {
            IsBallInPlay = false;
            TotalGoals1 = 0;
            TotalGoals2 = 0;
            CurrentPeriod = 1;
        }

        public Boolean IsBallInPlay { get; set; }
        public Int32 TotalGoals1 { get; set; }
        public Int32 TotalGoals2 { get; set; }
        public Int32 CurrentPeriod { get; set; }
    }
}