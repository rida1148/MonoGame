using System;
using System.Drawing;
#if !PORTABLE
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

namespace Microsoft.Xna.Framework.GamerServices
{
    [CLSCompliant(false)]
#if !PORTABLE
    public class TextFieldAlertView : UIAlertView
#else
    public class TextFieldAlertView 
#endif
	{
#if !PORTABLE
		private UITextField _tf = null;
#endif
        private bool _secureTextEntry;
		
		private string _initEditValue;
		private string _placeHolderValue;
		
		public TextFieldAlertView() : this(false) {}
		
        [CLSCompliant(false)]
#if !PORTABLE
		public TextFieldAlertView(bool secureTextEntry, string title, string message, UIAlertViewDelegate alertViewDelegate, string cancelBtnTitle, params string[] otherButtons)
			: base(title, message, alertViewDelegate, cancelBtnTitle, otherButtons)
#else
        public TextFieldAlertView(bool secureTextEntry, string title, string message, object alertViewDelegate, string cancelBtnTitle, params string[] otherButtons)
#endif
        {
			InitializeControl(secureTextEntry);
		}
		
		public TextFieldAlertView(bool secureTextEntry)
		{	
			InitializeControl(secureTextEntry);
		}
		
		public TextFieldAlertView(bool secureTextEntry, string initEditValue, string placeHolderValue )
		{	
			InitializeControl(secureTextEntry);
			_initEditValue = initEditValue;
			_placeHolderValue = placeHolderValue;
		}
		
		private void InitializeControl(bool secureTextEntry)
		{
			_secureTextEntry = secureTextEntry;
#if !PORTABLE
			this.AddButton("Cancel");
			this.AddButton("Ok");
			
			// build out the text field
			_tf = ComposeTextFieldControl(_secureTextEntry);
			
			// add the text field to the alert view
			this.AddSubview(_tf);
#endif
		}
		
		public string EnteredText 
		{ 
			get 
			{ 
#if !PORTABLE
				return _tf.Text; 
#else
                return null;
#endif
            } 
		}
		
		public override void LayoutSubviews ()
		{
#if !PORTABLE
			// layout the stock UIAlertView control
			base.LayoutSubviews ();
			
			// We can only force it to become a First Responder after it has been added to the MainView.
			_tf.BecomeFirstResponder();
#endif
            }

#if !PORTABLE
		private UITextField ComposeTextFieldControl(bool secureTextEntry)
        {
			UITextField textField = new UITextField (new System.Drawing.RectangleF(12f, 45f, 260f, 25f));
			textField.BackgroundColor = UIColor.White;
			textField.UserInteractionEnabled = true;
			textField.AutocorrectionType = UITextAutocorrectionType.No;
			textField.AutocapitalizationType = UITextAutocapitalizationType.None;
			textField.ReturnKeyType = UIReturnKeyType.Done;
			textField.SecureTextEntry = secureTextEntry;
			
			textField.Text = _initEditValue;
			textField.Placeholder = _placeHolderValue;
			textField.BecomeFirstResponder();
			return textField;
#else
        private object ComposeTextFieldControl(bool secureTextEntry)
        {
            return null;
#endif	
        }
		
		public override void Show ()
		{
#if !PORTABLE
			base.Show ();
#endif			
			// shift the contents of the alert view around to accomodate the extra text field
			this.AdjustControlSize();
		}
		
		private void AdjustControlSize()
		{
#if !PORTABLE
            float tfExtH = _tf.Frame.Size.Height + 16.0f;
			
			RectangleF frame = new RectangleF(this.Frame.X, 
			                                  this.Frame.Y - tfExtH/2,
			                                  this.Frame.Size.Width,
			                                  this.Frame.Size.Height + tfExtH);
			this.Frame = frame;
			
			foreach(var view in this.Subviews)
			{
				if(view is UIControl)
				{
					view.Frame = new RectangleF(view.Frame.X, 
					                            view.Frame.Y + tfExtH,
					                            view.Frame.Size.Width, 
					                            view.Frame.Size.Height);
				}
			}
#endif
		}
	}
}