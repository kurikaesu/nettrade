using System;

using Foundation;
using AppKit;

namespace netTrade
{
	public partial class TradeJournalEntryWindowController : NSWindowController
	{
		public TradeJournalEntryWindowController(IntPtr handle) : base(handle)
		{
		}

		[Export("initWithCoder:")]
		public TradeJournalEntryWindowController(NSCoder coder) : base(coder)
		{
		}

		public TradeJournalEntryWindowController() : base("TradeJournalEntryWindow")
		{
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
		}

		public new TradeJournalEntryWindow Window
		{
			get { return (TradeJournalEntryWindow)base.Window; }
		}
	}
}
