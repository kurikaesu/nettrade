// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace netTrade
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButtonCell PressMe { get; set; }

		[Action ("Click:")]
		partial void Click (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (PressMe != null) {
				PressMe.Dispose ();
				PressMe = null;
			}
		}
	}
}
