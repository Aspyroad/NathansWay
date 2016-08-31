// Mono
using UIKit;

namespace NathansWay.iOS.Numeracy.Controls
{
    public class TextControlDelegate : UITextFieldDelegate
    {
        public override bool ShouldBeginEditing(UITextField textField)
        {
            return false;
        }    
    }
}

