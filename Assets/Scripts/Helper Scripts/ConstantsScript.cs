using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;


namespace BAConstants {
	public class ConstantsScript {
		
		//Pre-Game Instantiated GameObject Names
		public const string tileMapObjectName = "Tile Map";
		public const string guiManagerObjectName = "GUI Manager";
		public const string gameManagerObjectName = "Game Manager";
		
		//GameObject Tags
		public const string characterTag = "Character";
		
		//Prefab Tags
		public const string spriteCollectionTag = "Sprite Collection";

		//FavorWave Enums
		public enum TileFavorEffect {
			IncreaseDamage = 1,
			DecreaseAbilityCost = 2,
			DecreaseEnemyDamage = 3,
			IncreaseEnemyAbilityCost = 4
		};

		public enum TileFavorDirection {
			Top = 1,
			TopRight = 2,
			BottomRight = 3,
			Bottom = 4,
			BottomLeft = 5,
			TopLeft = 6
		};

		//FavorWave tile #
		public const int favorWaveTileCount = 22;

		private static readonly ReadOnlyCollection<Vector2> topStartingTiles =
    	new ReadOnlyCollection<Vector2>(new[]
        {
            new Vector2(1,1),new Vector2(2,1),new Vector2(3,1),new Vector2(4,1),new Vector2(5,1),new Vector2(6,1)
        });

        private static readonly ReadOnlyCollection<Vector2> topRightStartingTiles =
    	new ReadOnlyCollection<Vector2>(new[]
        {
            new Vector2(6,1),new Vector2(7,2),new Vector2(8,3),new Vector2(9,4),new Vector2(10,5),new Vector2(11,6)
        });

        private static readonly ReadOnlyCollection<Vector2> bottomRightStartingTiles =
    	new ReadOnlyCollection<Vector2>(new[]
        {
            new Vector2(11,6),new Vector2(10,7),new Vector2(9,8),new Vector2(8,9),new Vector2(7,10),new Vector2(6,11)
        });

    	
        private static readonly ReadOnlyCollection<Vector2> bottomStartingTiles =
    	new ReadOnlyCollection<Vector2>(new[]
        {
            new Vector2(6,11),new Vector2(5,11),new Vector2(4,11),new Vector2(3,11),new Vector2(2,11),new Vector2(1,11)
        });

    	private static readonly ReadOnlyCollection<Vector2> bottomLeftStartingTiles =
    	new ReadOnlyCollection<Vector2>(new[]
        {
            new Vector2(1,11),new Vector2(1,10),new Vector2(1,9),new Vector2(1,8),new Vector2(1,7),new Vector2(1,6)
        });

        private static readonly ReadOnlyCollection<Vector2> topLeftStartingTiles =
    	new ReadOnlyCollection<Vector2>(new[]
        {
            new Vector2(1,6),new Vector2(1,5),new Vector2(1,4),new Vector2(1,3),new Vector2(1,2),new Vector2(1,1)
        });
    	
    	//Returns the top left tile coordinate for each favor wave's starting position
		private static readonly Dictionary<TileFavorDirection, ReadOnlyCollection<Vector2>> favorWavesStartingTiles
    	= new Dictionary<TileFavorDirection, ReadOnlyCollection<Vector2>>
		{
			{ TileFavorDirection.Top, bottomStartingTiles},
			{ TileFavorDirection.TopRight, bottomLeftStartingTiles },
			{ TileFavorDirection.BottomRight, topLeftStartingTiles},
			{ TileFavorDirection.Bottom, topStartingTiles},
			{ TileFavorDirection.BottomLeft, topRightStartingTiles},
			{ TileFavorDirection.TopLeft, bottomRightStartingTiles }
		};

		public static ReadOnlyCollection<Vector2> GetFavorWaveStartingTiles(TileFavorDirection startingSide) {
			return favorWavesStartingTiles[startingSide];
		}
	}

    /*
    *
    *
    *TILE CONSTANTS
    *
    *
    */

    public class TileConstants {
		public static Color baseColor = Color.white;
        public static Color increaseDamageColor = new Color(1.0f,0.75f,0f,1f);
		public static Color decreaseAbilityCostColor = new Color(1.0f,0f,0.9f,1f);
		public static Color decreaseEnemyDamageColor = new Color(1.0f,0f,0f,1f);
		public static Color increaseEnemyAbilityCost = new Color(0.5f,1f,0.9f,1f);
		//public static Color baseTileColor = new Color(0.15f, 0.69f, 0.11f, 1f);
        public static Color highlightedMovementColor = new Color(0.2f,0.9f,0.15f,1f);
		public static Color highlightedAttackColor = new Color(1.0f,0f,0f,1f);
    
        public static Vector2[] tileNeighbours = { 
        new Vector2(0,-1), 
        new Vector2(1,0),
        new Vector2(0,1), 
        new Vector2(-1,1),
        new Vector2(-1,0), 
        new Vector2(-1,-1)
        };
    }
    /*
    *
    *
    *CHARACTER CONSTANTS
    *
    */
    public class CharacterConstants {

        //TODO: Refactor this or CharacterScript.CharacterConstants.CharacterType
        //We don't need both, it's redundant?
        //Fix: Each character has a type variable that makes it npc,enemy or player
        public enum CharacterClass {
            Fighter,
            Gnoll
        };

        //Character Types
        public enum CharacterType {
            player,
            enemy,
            npc
        }

        public const string GNOLL_PREFAB_NAME = "Enemy";
        public const string GNOLL_NAME = "Gnoll";
        public const int GNOLL_HEALTH_MAX = 5;
        public const int GNOLL_STAMINA_MAX = 5;
        public const int GNOLL_FAVOR_AWARDED = 5;
    }

    /*
    *
    *
    * AUDIO CONSTANTS
    *
    */
    public class AudioConstants {

        public enum AudioClipType {
            Cheer1,
            Cheer2,
            Cheer3,
            CrowdLoop,
            GameStartSound,
            MonsterGrowl1,
            MonsterGrowl2,
            SwordHit1,
            SwordPickup,
        }

    }
}

