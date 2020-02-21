using System;
using Module4.DomainModel.Extensions;

namespace Module4.DomainModel.Business
{
    public class Match  
    {
        public Match(String id, String team1, String team2)
        {
            Id = id;
            Team1 = team1;
            Team2 = team2;
            State = MatchState.ToBePlayed;
            CurrentScore = new Score();
            IsBallInPlay = false;
            CurrentPeriod = 0;
        }

        public String Id { get; private set; }
        public String Team1 { get; private set; }
        public String Team2 { get; private set; }
        public MatchState State { get; internal set; }
        public Score CurrentScore { get; internal set; }
        public Boolean IsBallInPlay { get; private set; }
        public Int32 CurrentPeriod { get; internal set; }

        #region Informational
        public Boolean IsInProgress()
        {
            return State == MatchState.InProgress;
        }
        public Boolean IsFinished()
        {
            return State == MatchState.Finished;
        }
        public Boolean IsScheduled()
        {
            return State == MatchState.ToBePlayed;
        }
        public override String ToString()
        {
            return IsScheduled()
                ? String.Format("{0} vs. {1}", Team1, Team2)
                : String.Format("{0} / {1}  {2}", Team1, Team2, CurrentScore);
        }
        #endregion

        #region Behavior
        /// <summary>
        /// Starts the match
        /// </summary>
        /// <returns>this</returns>
        public Match Start()
        {
            //Contract.Requires<ArgumentException>(IsScheduled());

            State = MatchState.InProgress;
            return this;
        }

        /// <summary>
        /// Ends the match
        /// </summary>
        /// <returns></returns>
        public void Finish()
        {
            //Contract.Requires<ArgumentException>(IsInProgress());
            //Contract.Requires<ArgumentException>(CurrentPeriod == 4);
            //Contract.Requires<ArgumentException>(!IsBallInPlay);

            State = MatchState.Finished;
        }

        /// <summary>
        /// Scores a goal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Match Goal(TeamId id)
        {
            //Contract.Requires<ArgumentException>(IsInProgress());
            //Contract.Requires<ArgumentException>(IsBallInPlay);

            if (id == TeamId.Home)
            {
                CurrentScore = new Score(CurrentScore.TotalGoals1.Increment(),
                                         CurrentScore.TotalGoals2);
            }
            if (id == TeamId.Visitors)
            {
                CurrentScore = new Score(CurrentScore.TotalGoals1,
                                         CurrentScore.TotalGoals2.Increment());
            }

            //IsBallInPlay = false;
            return this;
        }

        /// <summary>
        /// Starts next period
        /// </summary>
        /// <returns>this</returns>
        public Match StartPeriod()
        {
            //Contract.Requires<ArgumentException>(IsInProgress());
            //Contract.Requires<ArgumentException>(!IsBallInPlay);
            //Contract.Ensures(IsBallInPlay);
            //Contract.Ensures(CurrentPeriod > 0);

            CurrentPeriod = CurrentPeriod.Increment(4);
            IsBallInPlay = true;
            return this;
        }

        /// <summary>
        /// Starts next period
        /// </summary>
        /// <returns>this</returns>
        public Match EndPeriod()
        {
            //Contract.Requires<ArgumentException>(IsInProgress());
            //Contract.Requires<ArgumentException>(IsBallInPlay);
            //Contract.Ensures(!IsBallInPlay);

            IsBallInPlay = false;

            if (CurrentPeriod == 4)
                Finish();

            return this;
        }
        #endregion
    }
}
