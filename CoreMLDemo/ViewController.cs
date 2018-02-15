using CoreML;
using Foundation;
using Plugin.Media;
using System;
using System.Diagnostics;
using UIKit;
using Vision;

namespace CoreMLDemo
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            btnPicture.TouchUpInside += BtnPicture_TouchUpInside;
        }

        async void BtnPicture_TouchUpInside(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                Debug.WriteLine("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;
            Debug.WriteLine("File Location", file.Path, "OK");

            var imagedata = NSData.FromStream(file.GetStream());
            capturedImage.Image =
                UIImage.LoadFromData(imagedata);

            var requestHandler = new VNImageRequestHandler(imagedata, new VNImageOptions());
            requestHandler.Perform(ClassificationRequest, out NSError error);

            if (error != null)
                Debug.WriteLine($"Error identifying {error}");
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public VNRequest[] ClassificationRequest
        {
            get
            {
                if (model == null)
                {
                    var modelPath = NSBundle.MainBundle.GetUrlForResource("Link", "mlmodel");
                    var compiledPath = MLModel.CompileModel(modelPath, out NSError compileError);
                    var mlModel = MLModel.Create(compiledPath, out NSError createError);

                    model = VNCoreMLModel.FromMLModel(mlModel, out NSError mlError);
                }

                if (classificationRequests == null)
                {
                    var classificationRequest = new VNCoreMLRequest(model, HandleClassificationRequest);
                    classificationRequests = new[] { classificationRequest };
                }

                return classificationRequests;
            }
        }

        void HandleClassificationRequest(VNRequest request, NSError error)
        {
            var observations = request.GetResults<VNClassificationObservation>();
            var best = observations?[0];

            outputLabel.Text = $"{best.Identifier.Trim()}, {best.Confidence:P0}";
        }

        VNCoreMLModel model;
        VNRequest[] classificationRequests;
    }
}