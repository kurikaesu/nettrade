using System;

using Foundation;
using AppKit;

namespace netTrade
{
	public partial class TradeJournalEntryWindow : NSWindow
	{
		public TradeJournalEntryWindow(IntPtr handle) : base(handle)
		{
		}

		[Export("initWithCoder:")]
		public TradeJournalEntryWindow(NSCoder coder) : base(coder)
		{
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
		}
	}
}
