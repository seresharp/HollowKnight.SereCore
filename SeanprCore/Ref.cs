using System;

namespace SeanprCore
{
    public static class Ref
    {
        private static HeroController _hero;
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

        private static GameManager _gm;
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

        private static InputHandler _input;
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
    }
}
