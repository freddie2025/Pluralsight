using System;

namespace Module4.DomainModel.Persistence
{
    public class Match  
    {
        public Match()
        {
            Id = String.Empty;
            Team1 = "Home";
            Team2 = "Visitors";
            CurrentScore = new Score();
            State = MatchState.ToBePlayed;
        }

        public String Id { get; set; }
        public String Team1 { get; set; }
        public String Team2 { get; set; }
        public MatchState State { get; set; }
        public Score CurrentScore { get; set; }

        public Boolean IsInProgress()
        {
            return State == MatchState.InProgress;
        }
    }
}
