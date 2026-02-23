using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace MobileTouchEnhancer
{
    public class ModEntry : Mod
    {
        private ModConfig Config = null!;
        private DateTime _pressStartTime;
        private DateTime _lastClickTime;
        private bool _isTracking = false;
        private Vector2 _circlePos = Vector2.Zero;

        public override void Entry(IModHelper helper)
        {
            this.Config = helper.ReadConfig<ModConfig>();
            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.Events.Input.ButtonReleased += OnButtonReleased;
            helper.Events.Input.CursorMoved += (s, e) => _circlePos = e.NewPosition.GetScreenPixels();
            helper.Events.Display.RenderGuiStep += OnRenderGuiStep;
        }

        private void OnRenderGuiStep(object? sender, RenderGuiStepEventArgs e)
        {
            // İmlere yardımcı bir daire/işaretçi çiz
            e.SpriteBatch.Draw(Game1.mouseCursors, _circlePos, new Rectangle(0, 0, 64, 64), Color.White * 0.7f, 0f, new Vector2(32, 32), 1f, SpriteEffects.None, 1f);
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
                var duration = (now - _pressStartTime).TotalMilliseconds;

                if (duration >= Config.LongPressMs)
                {
                    this.Helper.Input.TriggerPress(Config.LongPressButton);
                }
                else
                {
                    if ((now - _lastClickTime).TotalMilliseconds <= Config.DoubleTapMs)
                    {
                        this.Helper.Input.TriggerPress(Config.DoubleTapButton);
                    }
                    else
                    {
                        this.Helper.Input.TriggerPress(Config.SingleTapButton);
                    }
                }
                _lastClickTime = now;
            }
        }
    }
}
