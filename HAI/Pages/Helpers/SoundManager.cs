using System.Media;

namespace HAI.Pages.Helpers
{
    public static class SoundManager
    {
        private static SoundPlayer _clickSound = new SoundPlayer(Properties.Resources.ClickSound);
        private static SoundPlayer _hoverSound = new SoundPlayer(Properties.Resources.HoverSound);

        public static void PlayClickSound()
            => _clickSound.Play();

        public static void PlayHoverSound()
            => _hoverSound.Play();
    }
}
