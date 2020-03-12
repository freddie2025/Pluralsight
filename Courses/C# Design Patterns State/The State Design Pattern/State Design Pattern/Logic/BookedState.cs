using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_Design_Pattern.Logic
{
	class BookedState : BookingState
	{
		public override void Cancel(BookingContext booking)
		{
			booking.TransitionToState(new ClosedState("Booking canceled: Expect a refund"));
		}

		public override void DatePassed(BookingContext booking)
		{
			booking.TransitionToState(new ClosedState("We hope you enjoyed the event!"));
		}

		public override void EnterDetails(BookingContext booking, string attendee, int ticketCount)
		{

		}

		public override void EnterState(BookingContext booking)
		{
			booking.ShowState("Booked");
			booking.View.ShowStatusPage("Enjoy the Event");
		}
	}
}
