using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace MobileTouchEnhancer
{
    public class ModEntry : Mod
    {
        private DateTime _pressStartTime;
        private DateTime _lastClickTime;
        private bool _isTracking = false;

        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.Events.Input.ButtonReleased += OnButtonReleased;
        }

        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
            if (e.Button == SButton.MouseLeft)
            {
                _isTracking = true;
                _pressStartTime = DateTime.Now;
            }
        }

        private void OnButtonReleased(object? sender, ButtonReleasedEventArgs e)
        {
            if (e.Button == SButton.MouseLeft && _isTracking)
            {
                _isTracking = false;
                var now = DateTime.Now;
                var holdDuration = (now - _pressStartTime).TotalMilliseconds;

                if (holdDuration >= 500) { this.Helper.Input.TriggerPress(SButton.F); }
                else {
                    var timeSinceLastClick = (now - _lastClickTime).TotalMilliseconds;
                    if (timeSinceLastClick <= 300) { this.Helper.Input.TriggerPress(SButton.MouseLeft); }
                    else { this.Helper.Input.TriggerPress(SButton.MouseRight); }
                }
                _lastClickTime = now;
            }
        }
    }
}
