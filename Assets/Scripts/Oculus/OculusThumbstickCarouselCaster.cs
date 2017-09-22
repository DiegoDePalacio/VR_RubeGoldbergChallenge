using Common.Communication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirtualReality.Oculus
{
    public class OculusThumbstickCarouselCaster : OculusTouchInputBase
    {
        [SerializeField] private CarouselCaster carouselCaster;

        private bool isActive = false;
        private bool menuIsSwipable = false;
        private float menuStickX = 0f;

        protected override void Awake()
        {
            base.Awake();
            carouselCaster.Init();
        }

	    void Update ()
        {
            if ( OVRInput.GetUp( OVRInput.Button.PrimaryThumbstick, thisController ) )
            {
                isActive = !isActive;
                carouselCaster.SetCarouselActive( isActive );
            }

            if ( !isActive )
            {
                return;
            }

            menuStickX = OVRInput.Get( OVRInput.Axis2D.PrimaryThumbstick, thisController ).x;

            if ( menuStickX < 0.45f && menuStickX > -0.45f )
            {
                menuIsSwipable = true;
            }

            if ( menuIsSwipable )
            {
                if ( menuStickX >= 0.45f )
                {
                    // Fire function that looks at menuList,
                    // disables current item, and enables next item
                    carouselCaster.MoveRight();
                    menuIsSwipable = false;
                }
                else if ( menuStickX <= -0.45f )
                {
                    carouselCaster.MoveLeft();
                    menuIsSwipable = false;
                }
            }

            if ( OVRInput.GetDown( OVRInput.Button.PrimaryIndexTrigger, thisController ) )
            {
                carouselCaster.Select();
                isActive = false;
                carouselCaster.SetCarouselActive( false );
            }
        }
    }
}
