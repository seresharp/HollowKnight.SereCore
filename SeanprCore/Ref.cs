// ReSharper disable file UnusedMember.Global

namespace SeanprCore
{
    public static class Ref
    {
        private static HeroController _hero;

        private static GameManager _gm;

        private static InputHandler _input;

        private static UIManager _ui;

        // Can't be cached because it's not a Unity object
        public static PlayerData PD => PlayerData.instance;

        public static HeroController Hero
        {
            get
            {
                if (_hero == null)
                {
                    _hero = HeroController.instance;
                }

                return _hero;
            }
        }

        public static GameManager GM
        {
            get
            {
                if (_gm == null)
                {
                    _gm = GameManager.instance;
                }

                return _gm;
            }
        }

        public static InputHandler Input
        {
            get
            {
                if (_input == null && GM != null)
                {
                    _input = GM.inputHandler;
                }

                return _input;
            }
        }

        public static UIManager UI
        {
            get
            {
                if (_ui == null)
                {
                    _ui = UIManager.instance;
                }

                return _ui;
            }
        }
    }
}