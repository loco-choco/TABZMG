using System.Collections.Generic;
using UnityEngine;

namespace TABZMGamemodes.Arena
{
    public class ArenaGamemodeData
    {
        public static readonly Vector3 ArenaPositionOne = new Vector3(1031f, 100f, 637f);
        public static readonly Vector3 ArenaScaleOne = 540f * new Vector3(10f, 1, 10f);
        public static readonly Vector3[] SpawnPointSetOne = new Vector3[]
        {
            new Vector3(1024,73,663),
            new Vector3(976,83,662),
            new Vector3(978,83,686),
            new Vector3(975,85,707),
        };
        public static readonly ArenaKit[] KitSetOne = new ArenaKit[]
        {
            new ArenaKit
            {
                Weapons = new string[]{ "Items/Makarov"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoSmall", 12) }
            },
            new ArenaKit
            {
                Weapons = new string[]{"Items/Scorpion"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoSmall", 24) }
            },
            new ArenaKit
            {
                Weapons = new string[]{ "Items/Revolver", "Items/Revolver"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoMedium", 3) }
            },

            new ArenaKit
            {
                Weapons = new string[]{ "Items/Ak74"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoMedium", 25)}
            },
            new ArenaKit
            {
                Weapons = new string[]{ "Items/Mp5"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoSmall", 35)}
            },
            new ArenaKit
            {
                Weapons = new string[]{ "Items/EvilGun"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoMedium", 10)}
            },

            new ArenaKit
            {
                Weapons = new string[]{ "Items/Pump"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoShell", 3)}
            },
            new ArenaKit
            {
                Weapons = new string[]{ "Items/DoubleBarrel"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoShell", 4)}
            },
            
            new ArenaKit
            {
                Weapons = new string[]{ "Items/TheGrabberShot"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoShell", 3)}
            },

            new ArenaKit
            {
                Weapons = new string[]{"Items/HuntingSniper"},
                Ammo = new KeyValuePair<string, int>[]{new KeyValuePair<string, int>("Items/AmmoBig", 1)}
            },
            new ArenaKit
            {
                Weapons = new string[]{"Items/BigSniper"},
                Ammo = new KeyValuePair<string, int>[]{new KeyValuePair<string, int>("Items/AmmoBig", 1)}
            },
             new ArenaKit
            {
                Weapons = new string[]{ "Items/Musket"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoMedium", 1) }
            },

             new ArenaKit
            {
                Weapons = new string[]{ "Items/ÖverMusket"},
                Ammo = new KeyValuePair<string, int>[]{}
            },
            new ArenaKit
            {
                Weapons = new string[]{ "Items/Homerun Gun"},
                Ammo = new KeyValuePair<string, int>[]{}
            },
            new ArenaKit
            {
                Weapons = new string[]{ "Items/TheGrabberShot"},
                Ammo = new KeyValuePair<string, int>[]{}
            },
            new ArenaKit
            {
                Weapons = new string[]{ "Items/bouncer"},
                Ammo = new KeyValuePair<string, int>[]{}
            },
            new ArenaKit
            {
                Weapons = new string[]{ "Items/Annoyer"},
                Ammo = new KeyValuePair<string, int>[]{}
            },

            new ArenaKit
            {
                Weapons = new string[]{"Items/Axe"},
                Ammo = new KeyValuePair<string, int>[]{}
            },
            new ArenaKit
            {
                Weapons = new string[]{"Items/HuntingKnife"},
                Ammo = new KeyValuePair<string, int>[]{}
            },
            new ArenaKit
            {
                Weapons = new string[]{},
                Ammo = new KeyValuePair<string, int>[]{}
            },
        };

        public static readonly Vector3 ArenaPositionTwo = new Vector3(766f, 134f, 1140f);
        public static readonly Vector3 ArenaScaleTwo = 100f * new Vector3(100f, 6f, 100f);
        public static readonly Vector3[] SpawnPointSetTwo = new Vector3[]
        {
            new Vector3(760f,108f,1200f),
            new Vector3(736f,100f,1229f),
            new Vector3(702f,100f,1250f),
            new Vector3(678.68f,110.8f,1220.59f),
            new Vector3(661.4f,99.95f,1197.04f),

            new Vector3(690.84f,132.9f,1218.18f),
            new Vector3(711.23f,132.9f,1203.09f),

            new Vector3(721.75f,124.85f,1216.37f),

            new Vector3(711.54f,122.7f,1202.14f),
            new Vector3(689.29f,122.22f,1220.12f),
        };
    }
}
