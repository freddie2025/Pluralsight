using System;

namespace Module4.DomainModel.Business
{
    public sealed class Score  
    {
        private const Int32 MaxGoals = 99;

        public Score(Int32 goals1=0, Int32 goals2=0)
        {
            //Contract.Requires<ArgumentException>(goals1.BetweenWith(0, MaxGoals));
            //Contract.Requires<ArgumentException>(goals2.BetweenWith(0, MaxGoals));

            TotalGoals1 = goals1;
            TotalGoals2 = goals2;
        }

        public Int32 TotalGoals1 { get; private set; }
        public Int32 TotalGoals2 { get; private set; }

        #region Informational
        public override String ToString()
        {
            return String.Format("{0} - {1}", TotalGoals1, TotalGoals2);
        }
        public Boolean IsLeading(TeamId id)
        {
            if (id == TeamId.Home)
                return TotalGoals1 > TotalGoals2;
            if (id == TeamId.Visitors)
                return TotalGoals2 > TotalGoals1;
            return false;
        }
        #endregion

        public override bool Equals(object obj)
        {
            var otherScore = obj as Score;
            if (otherScore == null)
                return false;

            return otherScore.TotalGoals1 == TotalGoals1 && 
                otherScore.TotalGoals2 == TotalGoals2;
        }
        public override int GetHashCode()
        {
 	         return (TotalGoals2^TotalGoals2).GetHashCode();
        }
    }
}