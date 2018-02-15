// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CoreMLDemo
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnPicture { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView capturedImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel outputLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView stackLayout { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnPicture != null) {
                btnPicture.Dispose ();
                btnPicture = null;
            }

            if (capturedImage != null) {
                capturedImage.Dispose ();
                capturedImage = null;
            }

            if (outputLabel != null) {
                outputLabel.Dispose ();
                outputLabel = null;
            }

            if (stackLayout != null) {
                stackLayout.Dispose ();
                stackLayout = null;
            }
        }
    }
}