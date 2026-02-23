using StardewModdingAPI;

namespace MobileTouchEnhancer
{
    public class ModConfig
    {
        public SButton SingleTapButton { get; set; } = SButton.MouseRight; // Tek tık -> Sağ Tık
        public SButton DoubleTapButton { get; set; } = SButton.MouseLeft;  // Çift tık -> Sol Tık
        public SButton LongPressButton { get; set; } = SButton.F;          // Uzun basış -> F tuşu
        public int LongPressMs { get; set; } = 500; 
        public int DoubleTapMs { get; set; } = 300;
    }
}
