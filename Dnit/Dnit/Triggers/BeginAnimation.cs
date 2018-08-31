using Dnit.Core.Animations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dnit.Core.Triggers
{
    public class BeginAnimation : TriggerAction<VisualElement>
    {
        public AnimationBase Animation { get; set; }

        protected override async void Invoke(VisualElement sender)
        {
            if (Animation != null)
                await Animation.Begin();
        }
    }
}
