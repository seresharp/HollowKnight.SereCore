using JetBrains.Annotations;
using UnityEngine;

namespace SereCore
{
    [PublicAPI]
    public static class Ref
    {
        private static HeroController _hero;
        private static Rigidbody2D _heroRb2d;
        private static PlayMakerFSM _heroNailFSM;

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

        public static Rigidbody2D HeroRigidbody
        {
            get
            {
                if (_heroRb2d == null)
                {
                    if (Hero == null)
                    {
                        return null;
                    }

                    _heroRb2d = Hero.GetComponent<Rigidbody2D>();
                }

                return _heroRb2d;
            }
        }

        public static PlayMakerFSM HeroNailFSM
        {
            get
            {
                if (_heroNailFSM == null)
                {
                    if (Hero == null)
                    {
                        return null;
                    }

                    _heroNailFSM = Hero.transform.Find("Attacks/Slash").GetComponent<PlayMakerFSM>();
                }

                return _heroNailFSM;
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