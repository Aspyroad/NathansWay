while (myButton.gestureRecognizers.count) 
{
    [myButton removeGestureRecognizer:[myButton.gestureRecognizers objectAtIndex:0]];
}