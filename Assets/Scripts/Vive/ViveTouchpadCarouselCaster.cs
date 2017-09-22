using Common.Communication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirtualReality.Vive
{
    public class ViveTouchpadCarouselCaster : ViveInputBase
    {
        [SerializeField] private CarouselCaster carouselCaster;

        // Swipe
        private float swipeSum = 0f;
        private float touchLast = 0f;
        private float touchCurrent = 0f;
        private float distance = 0f;
        private bool hasSwipedLeft = false;
        private bool hasSwipedRight = false;

        protected override void Awake()
        {
            base.Awake();
            carouselCaster.Init();
        }

	    void Update ()
        {
            if ( device.GetTouchDown( SteamVR_Controller.ButtonMask.Touchpad ) )
            {
                touchLast = device.GetAxis( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ).x;
                carouselCaster.SetCarouselActive( true );
            }

            if ( device.GetTouch( SteamVR_Controller.ButtonMask.Touchpad ) )
            {
                touchCurrent = device.GetAxis( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ).x;
                distance = touchLast - touchCurrent;
                touchLast = touchCurrent;
                swipeSum += distance;

                if ( !hasSwipedRight )
                {
                    if ( swipeSum > 0.5f )
                    {
                        carouselCaster.MoveRight();
                        hasSwipedRight = true;
                        hasSwipedLeft = false;
                    }
                }

                if ( !hasSwipedLeft )
                {
                    if ( swipeSum < -0.5f )
                    {
                        carouselCaster.MoveLeft();
                        hasSwipedLeft = true;
                        hasSwipedRight = false;
                    }
                }
            }

            if ( device.GetTouchUp( SteamVR_Controller.ButtonMask.Touchpad ) )
            {
                swipeSum = 0f;
                touchLast = 0f;
                touchCurrent = 0f;
                hasSwipedLeft = false;
                hasSwipedRight = false;
                carouselCaster.SetCarouselActive( false );
            }

            if ( device.GetPressDown( SteamVR_Controller.ButtonMask.Touchpad ) )
            {
                carouselCaster.Select();
            }
        }
    }
}
