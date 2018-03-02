NSSet* allTouches = [event allTouches];
UITouch* touch = [allTouches anyObject];
UIView* touchView = [touch view];

        if (touch.phase == UITouchPhaseBegan) 
        {

            _initialView = touchView;
            startTouchPosition1 = [touch locationInView:self];
            startTouchTime = touch.timestamp;

            if ([allTouches count] > 1) 
            {
                startTouchPosition2 = [[[allTouches allObjects] objectAtIndex:1] locationInView:self];
                previousTouchPosition1 = startTouchPosition1;
                previousTouchPosition2 = startTouchPosition2;
            }
        }

        if (touch.phase == UITouchPhaseMoved) 
        {

            if ([allTouches count] > 1) 
            {
                CGPoint currentTouchPosition1 = [[[allTouches allObjects] objectAtIndex:0] locationInView:self];
                CGPoint currentTouchPosition2 = [[[allTouches allObjects] objectAtIndex:1] locationInView:self];

                CGFloat currentFingerDistance = CGPointDist(currentTouchPosition1, currentTouchPosition2);
                CGFloat previousFingerDistance = CGPointDist(previousTouchPosition1, previousTouchPosition2);
                if (fabs(currentFingerDistance - previousFingerDistance) > ZOOM_DRAG_MIN) 
                {
                    NSNumber* movedDistance = [NSNumber numberWithFloat:currentFingerDistance - previousFingerDistance];
                    if (currentFingerDistance > previousFingerDistance) 
                    {
//                          NSLog(@"zoom in");
                        [[NSNotificationCenter defaultCenter] postNotificationName:NOTIFICATION_ZOOM_IN object:movedDistance];
                    } 
                    else 
                    {
//                          NSLog(@"zoom out");
                        [[NSNotificationCenter defaultCenter] postNotificationName:NOTIFICATION_ZOOM_OUT object:movedDistance];
                    }
                }
            }
        }

        if (touch.phase == UITouchPhaseEnded) 
        {
            CGPoint currentTouchPosition = [touch locationInView:self];

            // Check if it's a swipe
            if (fabsf(startTouchPosition1.x - currentTouchPosition.x) >= SWIPE_DRAG_HORIZ_MIN && fabsf(startTouchPosition1.x - currentTouchPosition.x) > fabsf(startTouchPosition1.y - currentTouchPosition.y) && touch.timestamp - startTouchTime < 0.7) 
            {
                // It appears to be a swipe.
                if (startTouchPosition1.x < currentTouchPosition.x) 
                {
                        NSLog(@"swipe right");
                    [[NSNotificationCenter defaultCenter] postNotificationName:NOTIFICATION_SWIPE_RIGHT object:self];
                } 
                else 
                {
                        NSLog(@"swipe left");
                    [[NSNotificationCenter defaultCenter] postNotificationName:NOTIFICATION_SWIPE_LEFT object:self];
                }
            } 
            else 
            {
                //-- else, check if it's a single touch
                if (touch.tapCount == 1) 
                {
                    NSDictionary* uInfo = [NSDictionary dictionaryWithObject:touch forKey:@"touch"];
                    [[NSNotificationCenter defaultCenter] postNotificationName:NOTIFICATION_TAP object:self userInfo:uInfo];                        
                }/* else if (touch.tapCount > 1) {
                    handle multi-touch
                }
                */
            }

            startTouchPosition1 = CGPointMake(-1, -1);
            _initialView = nil;
        }

        if (touch.phase == UITouchPhaseCancelled) {
            _initialView = nil;
//          NSLog(@"TOUCH CANCEL");
        }