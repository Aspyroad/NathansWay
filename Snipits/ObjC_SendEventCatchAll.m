
// A trick for capturing all touch input for the duration of a touch
// By MICHAEL TYSON | Published: AUGUST 14, 2010
// If you’ve ever tried to implement an interactive control that makes 
// use of gestures within a UITableView, or tried to implement a view 
// that you can drag around, like pins on a MKMapView, you’ll know that 
// you’re either generally thwarted by the scroll view (and the table 
// view will just scroll, instead of your control getting the vertical
// drag gesture), or as soon as the touch moves outside the bounds of 
// your view, you’ll get no more touchesMoved events, making for a very
// frustrating dragging interaction.

// Some platforms give you a way to capture all pointer input for a time,
// but this doesn’t appear to be available out of the box on the iPhone — at least, I haven’t found it.

// Here’s a way to make it work:

// Subclass UIWindow and replace sendEvent: with your own method
// Provide a way for objects to be registered with your UIWindow 
// subclass to gain ‘touch priority’ – either add a property that
// takes a UIView/UIResponder, or add a mutable array to be able
// to register multiple views.
// When you receive a touch began event, check to see if the touch
// was within the bounds of any ‘priority’ views – if they are, the
// view subsequently gets all events until the touch ended event.

// Here’s what the UIWindow subclass could look like:
 
@interface CTTouchInterceptingWindow : UIWindow 
{
    NSMutableArray *views;
 
    @private
    UIView *touchView;
}
 
- (void)addViewForTouchPriority:(UIView*)view;
- (void)removeViewForTouchPriority:(UIView*)view;
 
@end
 
 
@implementation CTTouchInterceptingWindow
 
- (void)dealloc 
{
    if ( views ) [views release];
    [super dealloc];
}
 
- (void)addViewForTouchPriority:(UIView*)view 
{
    if ( !views ) 
    {
        views = [[NSMutableArray alloc] init];
    }
    [views addObject:view];
}
- (void)removeViewForTouchPriority:(UIView*)view 
{

    if ( !views ) 
    {
        return;
    }
    [views removeObject:view];
}
 
- (void)sendEvent:(UIEvent *)event 
{
    if ( !views || [views count] == 0 ) 
    {
        [super sendEvent:event];
        return;
    }
 
    UITouch *touch = [[event allTouches] anyObject];
 
    switch ( touch.phase ) 
    {
        case UITouchPhaseBegan:
            for ( UIView *view in views ) 
            {
                if ( CGRectContainsPoint([view frame], [touch locationInView:[view superview]]) ) 
                {
                    touchView = view;
                    [touchView touchesBegan:[event allTouches] withEvent:event];
                    return;
                }
            }
            break;
        case UITouchPhaseMoved:
            if ( touchView ) 
            {
                [touchView touchesMoved:[event allTouches] withEvent:event];
                return;
            }
            break;
        case UITouchPhaseCancelled:
            if ( touchView ) 
            {
                [touchView touchesCancelled:[event allTouches] withEvent:event];
                return;
            }
            touchView = nil;
            break;
        case UITouchPhaseEnded:
            if ( touchView ) 
            {
                [touchView touchesEnded:[event allTouches] withEvent:event];
                return;
            }
            touchView = nil;
            break;
    }
 
    [super sendEvent:event];
}
 
@end








TouchCapturingWindow.h
01
#import <Foundation/Foundation.h>
 
@interface TouchCapturingWindow : UIWindow 
{
    NSMutableArray *views;
 
@private
    UIView *touchView;
}
 
- (void)addViewForTouchPriority:(UIView*)view;
- (void)removeViewForTouchPriority:(UIView*)view;
 
@end


TouchCapturingWindow.m
01

#import "TouchCapturingWindow.h"
 
@implementation TouchCapturingWindow
 
- (void)dealloc 
{
    if ( views ) [views release];
    [super dealloc];
}
 
- (void)addViewForTouchPriority:(UIView*)view 
{
    if ( !views ) views = [[NSMutableArray alloc] init];
    [views addObject:view];
}
 
- (void)removeViewForTouchPriority:(UIView*)view 
{
    if ( !views ) return;
    [views removeObject:view];
}
 
- (void)sendEvent:(UIEvent *)event 
{
    //we need to send the message to the super for the    
    //text overlay to work (holding touch to show copy/paste)
    //NOTE: this used to be called at the beginning of this method
    //for the copy/paste and magnifying class overlay to work. As
    //of iOS5, it stopped working, and this code needs to go at
    //the end of this method.    
    //[super sendEvent:event];    
 
    //get a touch
    UITouch *touch = [[event allTouches] anyObject];
 
    //check which phase the touch is at, and process it
    if (touch.phase == UITouchPhaseBegan) 
    {
            for ( UIView *view in views ) 
            {
                //NOTE: I added the isHidden check so that hiding the windows doesn't catch events
                //and changed it checks if the touch is in the frame.
                //if ( CGRectContainsPoint([view frame], [touch locationInView:[view superview]]) ) {
                if ( ![view isHidden] && [view pointInside:[touch locationInView:view] withEvent:event] ) 
                {    
                    touchView = view;
                    [touchView touchesBegan:[event allTouches] withEvent:event];
                    break; 
                    //NOTE: this used to be a return in the previous version
                }
            }
    }
    else if (touch.phase == UITouchPhaseMoved) 
    {
        if ( touchView ) 
        {
            [touchView touchesMoved:[event allTouches] withEvent:event];
        }
    }
    else if (touch.phase == UITouchPhaseCancelled) 
    {
        if ( touchView ) 
        {
            [touchView touchesCancelled:[event allTouches] withEvent:event];
            touchView = nil;
        }
    }
    else if (touch.phase == UITouchPhaseEnded) 
    {
        if ( touchView ) 
        {
            [touchView touchesEnded:[event allTouches] withEvent:event];
            touchView = nil;
        }
    }
 
    //we need to send the message to the super for the
    //text overlay to work (holding touch to show copy/paste)
    [super sendEvent:event];
}
@end