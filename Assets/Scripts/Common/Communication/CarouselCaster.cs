using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Communication
{
    public delegate void OnCarouselSetActive( bool active );
    public delegate void OnCarouselMoveLeft();
    public delegate void OnCarouselMoveRight();
    public delegate void OnCarouselSelection();

    public interface ICarouselReceiver
    {
        OnCarouselSetActive OnCarouselSetActive { get; }
        OnCarouselMoveLeft OnCarouselMoveLeft { get; }
        OnCarouselMoveRight OnCarouselMoveRight { get; }
        OnCarouselSelection OnCarouselSelection { get; }
    }

    [Serializable]
    public class CarouselCaster : Caster<ICarouselReceiver>
    {
        public OnCarouselSetActive SetCarouselActive = null;
        public OnCarouselMoveLeft MoveLeft = null;
        public OnCarouselMoveRight MoveRight = null;
        public OnCarouselSelection Select = null;

        public override void Init()
        {
            base.Init();

            foreach ( ICarouselReceiver receiver in receivers )
            {
                SetCarouselActive -= receiver.OnCarouselSetActive;
                SetCarouselActive += receiver.OnCarouselSetActive;

                MoveLeft -= receiver.OnCarouselMoveLeft;
                MoveLeft += receiver.OnCarouselMoveLeft;

                MoveRight -= receiver.OnCarouselMoveRight;
                MoveRight += receiver.OnCarouselMoveRight;

                Select -= receiver.OnCarouselSelection;
                Select += receiver.OnCarouselSelection;
            }
        }
    }
}
