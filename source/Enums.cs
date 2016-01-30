//--------------------------------------------------------------------------------------------
//  File      : Enums.cs
//  Author(s) : Adam Schaeffer
//
//  Summary   : 
//
//  Copyright © 2012 Gas Powered Games, Inc.  All rights reserved.
//--------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Game.Shared.Mechanics
{
	public enum EGender
	{
		[ToolName()]
		None = 0,
		[ToolName()]
		Male,
		[ToolName()]
		Female,
	}

	public enum EChampionClass
	{
		[ToolName()]
		None = 0,
		[ToolName()]
		Mage,
		[ToolName()]
		Warrior,
		[ToolName()]
		Cleric,
		[ToolName()]
		Rogue,
		[ToolName()]
		Warlock,
		[ToolName()]
		Ranger,
	}

	public enum ERace
	{
		None = 0,
		[ToolName()]
		Human,
		[ToolName()]
		Elf,
		[ToolName()]
		Coyotle,
		[ToolName()]
		Orc,
		[ToolName()]
		Dwarf,
		[ToolName()]
		ShinHare,
		[ToolName()]
		Vennen,
		[ToolName()]
        Necrotic,
        [ToolName()]
        Avatar,
        [ToolName()]
        Dragon,
	}

	public enum EChampionType
	{
		[ToolName("Legend (PvP)")]
		PvPChampion = 0,
		[ToolName()]
        Mercenary,
        [ToolName()]
        Hero,
	}

	public enum ECurrencyType
	{
		[ToolName()]
		Gold = 0,
		[ToolName()]
		Platinum,
	}

	/*
	public enum ERaces
	{
		Unknown = 0,
		// Aria
		Coyotle,
		Elf,
		Human,
		Ork,
		// Underworld
		Ageless,
		Dwarf,
		Rabbiten,
        Vennen,
		// non-faction ?
		Demon,
		Demigod,
		Spirit,
	}
	*/
	public static partial class Extensions
	{
		/// <summary>
		/// Extension method on ERace to return the faction.
		/// </summary>
		public static EFactions GetFaction(this ERace race)
		{
			switch (race)
			{
				case ERace.Coyotle:
				case ERace.Elf:
				case ERace.Human:
				case ERace.Orc:
					return EFactions.Aria;

				case ERace.Necrotic:
				case ERace.Dwarf:
				case ERace.ShinHare:
				case ERace.Vennen:
					return EFactions.Underworld;

				default:
					return EFactions.Unknown;
			}
		}
	}

	public enum EChampionStatType
	{
		MatchesPlayed = 1,
		MatchesWon = 2,
		MatchesLost = 3,
		Experience = 4,
		MaxDamageDealt = 5,
		MaxDamageRecieved = 6,
	}

	public enum ETalentSource
	{
		Class = 0,
		Race,
        RaceClassCombo,
	}

	public enum EUnlockContingencyType
	{
		None = 0,
		All,
		SomeNumberOf,
	}

	public enum EFactions
	{
		Unknown = 0,
		[ToolName]
		None,
		[ToolName("Ardent")]
		Aria,
		[ToolName]
		Underworld,
	}

	public enum ECardExtensionMode
	{
		Unknown = 0,
		[ToolName]
		Normal,
		[ToolName]
		Extended,
	}

	public enum EPlayerTargets
	{
		Unknown = 0,
		ActivePlayer,
		InactivePlayers,
		AllPlayers,
	}

    public enum ECostTypes
    {
        Unknown = 0,
        ExhaustCost,
    }

	public enum EPlayerCardTargets
	{
		Unknown = 0,
		Self,
		SingleOpponent,
		SinglePlayer,       // self or opponent
		MultipleOpponents,
		MultiplePlayers,    // anyone!
	}

	public enum EPlayerRevealTargets
	{
		Unknown = 0,
		/// <summary>
		/// Reveal the cards only to the player using the ability.
		/// </summary>
		Self,
		/// <summary>
		/// Reveal the cards to all players.
		/// </summary>
		Everyone,
	}

	[Flags]
	public enum EPlayerAttributes : ulong
	{
		None = 0,
		[ToolName("Can't Attack")]
		CantAttack = (1 << 0),
		[ToolName("Can't Block")]
		CantBlock = (1 << 1),
		[ToolName("Can't Cast")]
		CantCast = (1 << 2),
		[ToolName("Can Play From Top of Deck")]
		CanPlayFromTopOfDeck = (1 << 3),
		// now that cards can be made individually damage resistant, this is kinda redundant for champions.
		[ToolName("Prevent Champion Combat Damage")]
		PreventChampionCombatDamage = (1 << 4),
		[ToolName("Prevent Champion Non-Combat Damage")]
		PreventChampionNonCombatDamage = (1 << 5),
		[ToolName("Prevent All Champion Damage")]
		PreventAllChampionDamage = PreventChampionCombatDamage | PreventChampionNonCombatDamage,
		[ToolName("Prevent Troop Combat Damage")]
		PreventTroopCombatDamage = (1 << 6),
		[ToolName("Prevent Troop Non-Combat Damage")]
		PreventTroopNonCombatDamage = (1 << 7),
		[ToolName("Prevent All Troop Damage")]
		PreventAllTroopDamage = PreventTroopCombatDamage | PreventTroopNonCombatDamage,
		[ToolName("Player's Cards Can't Inflict Combat Damage")]
		CantInflictCombatDamage = (1 << 8),
		[ToolName("Player's Cards Can't Inflict Non-Combat Damage")]
		CantInflictNonCombatDamage = (1 << 9),
		[ToolName("Player's Cards Can't Inflict Any Damage")]
		CantInflictAnyDamage = CantInflictCombatDamage | CantInflictNonCombatDamage,
		[ToolName("Can't Play Troops")]
		CantPlayTroops = (1 << 11),
		[ToolName("Can't Play Artifacts")]
		CantPlayArtifacts = (1 << 12),
		//[ToolName("Can't Gain Life")]
		//CantGainLife = (1 << 13), // deprecated by CantGainHealth
		[ToolName("Malignant Death")]
		MalignantDeath = (1 << 14),
		[ToolName("Can't Lose The Game")]
		CantLoseTheGame = (1 << 15),
	}

	// todo: how to handle multiple decks?
	[Flags]
	public enum ECardCollections
	{
		[ToolName(false)]
		None = 0x0000,
		Deck = 0x0001,
		Hand = 0x0002,
		Champions = 0x0004,
		Warzone = 0x0008,
		Discard = 0x0010,
		Void = 0x0020,
        [ToolName(false)]
		PlayedResources = 0x0040, 
		[ToolName(false)]
		CastSpells = 0x0080, // this is where spell cards go to await chain resolution.
        Underground = 0x0100,
        Choosing = 0x0200,   // This is where we put cards that don't really exist while the UI is letting the player choose one of them
        Mod = 0x0400,
    }


	// todo: how to handle multiple decks?
	[Flags]
	public enum ECardArtStatus
	{
		[ToolName("None")]
		None = 0x0000,
		[ToolName("Work in Progress")]
		WorkInProgress = 0x0001,
		[ToolName("Placeholder")]
		Placeholder = 0x0002,
		[ToolName("Done")]
		Done = 0x0003,
	}

	public enum EArtStatus
	{
		[ToolName("None")]
		None = 0x0000,
		[ToolName("Work in Progress")]
		WorkInProgress,
		[ToolName("Placeholder")]
		Placeholder,
		[ToolName("Done")]
		Done,
	}

    public enum EArtProductionStatus
    {
        [ToolName("None")]
        None = 0x0000,
        [ToolName("Ready to assign")]
        ReadyToAssign,
        [ToolName("Offered")]
        Offered,
        [ToolName("Assigned")]
        Assigned,
        [ToolName("Sketch Submitted")]
        SketchSubmitted,
        [ToolName("Sketch Approved")]
        SketchApproved,
        [ToolName("Color WIP Submitted")]
        ColorWIPSubmitted,
        [ToolName("Final Approved")]
        FinalApproved,
        [ToolName("Internal Touch Up Pass")]
        InternalTouchUpPass,
        [ToolName("Final Internal Approval")]
        FinalInternalApproval,
        [ToolName("External Revision")]
        ExternalRevision,
        [ToolName("In P4")]
        InP4,
        [ToolName("Do Not Use")]
        DoNotUse,
        [ToolName("MIA Artist")]
        MIAArtist,
    }

    public enum EArtCategories
    {
        [ToolName("Card/Champion")]
        CardOrChampion,
        [ToolName("Icon")]
        Icon,
        [ToolName("Nodescape")]
        Nodescape,
    }

    public enum EArtRegion
    {
        [ToolName("None")]
        None,
        [ToolName("China")]
        China,
    }

    public enum ECardLocations
	{
		Unknown = 0,
		Top,
		Bottom,
	}


	public enum EComparisons
	{
		LessThan,
		LessThanOrEqual,
		GreaterThan,
		GreaterThanOrEqual,
		Equals,
		NotEqual,
        OneMoreThan,
        TwoMoreThan,
        OneLessThan,
	}


	public enum EValueStorageType
	{
		Min,
		Current,
		Max
	}


	public static partial class Extensions
	{
		/// <summary>
		/// Extension method that applies EComparisons relational operators to two IComparables.
		/// </summary>
		public static bool Test<T>(this EComparisons comparison, T lhs, T rhs) where T : IComparable
		{
			// http://stackoverflow.com/questions/2357410/having-to-implement-a-generic-less-than-and-greater-than-operation
			int c = lhs.CompareTo(rhs);

			switch (comparison)
            {
                case EComparisons.Equals:
                    return c == 0;

                case EComparisons.GreaterThan:
                    return c > 0;

                case EComparisons.GreaterThanOrEqual:
                    return c >= 0;

                case EComparisons.LessThan:
                    return c < 0;

                case EComparisons.LessThanOrEqual:
                    return c <= 0;

                case EComparisons.NotEqual:
                    return c != 0;

                case EComparisons.OneMoreThan:
                    if (rhs is int && lhs is int)
                        return Int32.Parse(lhs.ToString()) == Int32.Parse(rhs.ToString()) + 1;
                    return false;

                case EComparisons.TwoMoreThan:
                    if (rhs is int && lhs is int)
                        return Int32.Parse(lhs.ToString()) == Int32.Parse(rhs.ToString()) + 2;
                    return false;

                case EComparisons.OneLessThan:
                    if (rhs is int && lhs is int)
                        return Int32.Parse(lhs.ToString()) == Int32.Parse(rhs.ToString()) - 1;
                    return false;

                default:
                    throw new InvalidOperationException("Undefined comparison in " + comparison.GetType().ToString());
            }
		}
	}

	public enum EOperations
	{
		Unknown = 0,
		Add,
		Remove,
        Set,
	}

    public enum EPriorityContext
    {
        Unknown = 0,
        Normal,
        Ready,
        ProcedeToCombat,
        ResolveTopOfChain,
        ProcedeToSecondMain,
        ProceedToEndTurn,
        EndPhase,
        AutoPass,
        ResolveCombat,
    }

	public enum ETurnPhases
	{
		Unknown = 0,
		[ToolName]
		NotPlaying,
		[ToolName]
		PreGame,
        [ToolName]
        PickGoesFirst,
		[ToolName]
		StartGame,
		[ToolName]
		StartTurn,
		[ToolName]
		Ready, // are there start turn FX that go here?
		[ToolName]
		Prep,
		[ToolName]
		Draw,
		[ToolName]
		FirstMainPhase, // includes declaration of intent
		[ToolName]
		DeclareCombatPriorityWindow,
		[ToolName]
		DeclareAttack,
		[ToolName]
		DeclareAttackPriorityWindow,
		[ToolName]
		DeclareDefense,
		[ToolName]
		DeclareDefensePriorityWindow,
		// attackers choose how to damage blockers, blockers choose how to damage attackers
		[ToolName("Assign Swift Strike Damage")]
		AssignFirstStrikeDamage,
		[ToolName("Swift Strike Priority Window")]
		FirstStrikePriorityWindow,
		[ToolName]
		AssignDamage,
		[ToolName]
		SecondMainPhase,
		[ToolName]
		EndPhase,
		[ToolName]
		Discard,
		[ToolName]
		EndTurn,
		[ToolName(false)]
		Checksum, // hidden in tool; this is an internal detail
		[ToolName]
		EndGame,
	}

	public enum EPlayerEliminatedReason
	{
		Unknown = 0,
		ChampionDefeated,
		DeckExhausted,
		ScriptedEvent,
		ChessTimerExpired,
		TurnPhaseTimerExpired,
		PlayerQuit,
        PlayerQuitOnReconnect,
		GMFiat,
	}

    public enum ECardUsage
    {
        None         = 0x00,
        Play         = 0x01,
        Activate     = 0x02,
        Attack       = 0x04,
        Defend       = 0x08,
        ForcedAttack = 0x10,
        PlayForFree  = 0x20,
    }


	public static partial class Extensions
	{
		public static bool IsMainPhasePriorityWindow(this ETurnPhases turnPhase)
		{
			switch (turnPhase)
            {
			    case ETurnPhases.FirstMainPhase:
			    case ETurnPhases.SecondMainPhase:
			        return true;
			}

			return false;
		}

		public static bool IsPriorityWindow(this ETurnPhases turnPhase)
		{
			switch (turnPhase)
            {
			    //case ETurnPhases.Prep:
                //case ETurnPhases.Draw:
                case ETurnPhases.Ready:
			    case ETurnPhases.FirstMainPhase:
                case ETurnPhases.DeclareCombatPriorityWindow:
			    case ETurnPhases.DeclareAttackPriorityWindow:
			    case ETurnPhases.DeclareDefensePriorityWindow:
			    case ETurnPhases.FirstStrikePriorityWindow:
			    case ETurnPhases.SecondMainPhase:
			    case ETurnPhases.EndPhase:
			        return true;
			}

			return false;
		}
	}

	[Flags]
	public enum ECardImplStatus
	{
		[ToolName]
		Unknown = 0x0000,
		[ToolName]
		DesignComplete = 0x0001,        // Set by CZE Design. Card’s text is finished.
		[ToolName]
		SupportedByMechanics = 0x0002,  // Set by gameplay engineer. Card’s text is supported by mechanics.
		[ToolName]
		SupportedByUI = 0x0004,         // Set by UI engineer. Card’s text is supported by UI.
		[ToolName]
		ReadyForGame = 0x0008,          // Set by Design. Card is functionally complete. 
		[ToolName]
		SupportedByAI = 0x0010,         // Set by AI engineer.  AI supports using the card.
		[ToolName]
		ReadyForTesting = 0x0020,       // Set automatically when Ready for Game and Supported by AI are checked. Card can be tested by QA. Ticks off Failed Test.
		[ToolName]
		FailedTesting = 0x0040,         // Set by QA. Ticks off Ready for Test.
		[ToolName]
		Done = 0x00080 | DesignComplete | SupportedByMechanics | SupportedByUI | ReadyForGame
						| SupportedByAI | ReadyForTesting,	// Set by QA 
	};

    public enum ESetFormat
    {
        //Partial Format Flags
        Invalid = 0x00000,
        PvESets = 0x00001,
        Set1    = 0x00002,
        Set2    = 0x00004,
        Set3    = 0x00008,
        Set4    = 0x00010,
        Set5    = 0x00020,
        Set6    = 0x00040,
        Set7    = 0x00080,
        Set8    = 0x00100,
        Set9    = 0x00200,
        Set10   = 0x00400,

        //Chapter and Book Formats
        Chapter1 = Set1|Set2,
        Chapter2 = Set3|Set4,
        Book1    = Chapter1|Chapter2,
        Chapter3 = Set5 | Set6,
        Chapter4 = Set7 | Set8,
        Book2    = Chapter3 | Chapter4,

        //PVP Constructed Formats
        Standard = Book1,
        WildWest = 0x10000,

        //PVE Formats
        PvE = PvESets | Book1, //Functionally the same as Standard Constructed at the moment

        Any = PvESets | Book1 | Book2 | WildWest,
    };

    public enum EPlayFormat
    {
        PvP            = 0x00000,  //If the PvE flag isn't set, this is a PvP match
        PvE            = 0x00001,

        Micro          = 0x00002,  //20 Card Decks
        Limited        = 0x00004,  //40 Card Decks
        Constructed    = 0x00008,  //60 Card Decks

        SingleGame     = 0x00010,
        BestOf3        = 0x00020,

        Tournament     = 0x00100,

		PvEConstructed = PvE | Constructed,

        Any            = PvP | PvE | Micro | Limited | Constructed | SingleGame | BestOf3,
    };

	public enum ECardImplStage
	{
		[ToolName("Needs Final Design", "Set by CZE if they feel a card is not ready to be scripted.", true)]
		NeedsFinalDesign,
		[ToolName("Needs GPG Support", " Set by anyone who feels the card needs mechanics or UI support.", true)]
		NeedsGPGSupport,
		[ToolName("Needs Scripting", "Set by CZE if they feel the card is design complete and ready to be scripted.", true)]
		NeedsScripting,
		[ToolName("Needs PvP Testing", "Set by CZE when they believe the card is scripted according to game text.", true)]
		NeedsPvPTesting,
		[ToolName("Needs AI Review", "Set by test when a card passes PvP testing or if a card failed PvE Testing. If a card fails testing, Implementation Notes include the bug.", true)]
		NeedsAIReview,
		[ToolName("Needs PvE Testing", "Set by AI engieer when he believes AI can play with the card.", true)]
		NeedsPvETesting,
		[ToolName("Done", "Set by test when a card can by played in all valid formats.", true)]
		Done,
	};

	[Flags]
	public enum EItemImplStatus
	{
		[ToolName]
		Unknown = 0x0000,
		[ToolName]
		DesignComplete = 0x0001,
		[ToolName]
		SupportedByMechanics = 0x0002,
		[ToolName]
		SupportedByUI = 0x0004,
		[ToolName]
		ReadyForGame = 0x0008,
		[ToolName]
		SupportedByAI = 0x0010,
		[ToolName]
		ReadyForTesting = 0x0020,
		[ToolName]
		FailedTesting = 0x0040,
		[ToolName]
		Done = 0x00080,
	};

	[Flags]
	public enum EDungeonImplStatus
	{
		[ToolName]
		Unknown = 0x0000,
		[ToolName]
		EncountersComplete = 0x0001, // Someone has done the basic encounter creation, including the encounter file and deck files.
		[ToolName]
		ConversationsComplete = 0x0002, // Someone has created all basic conversations for the dungeon.
		[ToolName]
		NodesComplete = 0x0004, //  Someone has created all nodes and have positioned them, created and assigned their rooms, and set up the general art for the dungeon.
		[ToolName]
		EventsComplete = 0x0008, //  Someone has gone through and created the node event queues and performed all complex event wiring for the dungeon nodes, encounters, and conversations.
		[ToolName]
		ReadyForTest = 0x0010,
		[ToolName]
		Done = 0x0020,
		[ToolName]
		FailedTest = 0x0040,
	};

	[Flags]
	public enum ECardTypes
	{
		Unknown = 0x0000,
		[ToolName]
		Champion = 0x0001,
		[ToolName]
		Troop = 0x0002,
		Gear = 0x0004,
		[ToolName("Basic Action")]
		BasicAction = 0x0008, 
		[ToolName]
		Resource = 0x0010,
		[ToolName]
		Artifact = 0x0020,
		[ToolName("Quick Action")]
		QuickAction = 0x0040, 
		[ToolName]
		Constant = 0x00800, 
		[ToolName]
		Token = 0x01000,
		[ToolName]
		Quick = 0x02000,
        [ToolName]
        Mod = 0x04000,
        [ToolName]
        Bane = 0x08000,
        AnyCard = Troop | BasicAction | Resource | Artifact | QuickAction | Constant | Quick | Mod | Bane,
	}

	[Flags]
	public enum EGemTypes : ulong // 64-bits, baby!
	{
		[ToolName]
		Unknown = 0,

		[ToolName("Wild Minor 1")]
		Wild_Minor_1 = (1 << 1),
		[ToolName("Wild Minor 2")]
		Wild_Minor_2 = (1 << 2),
		[ToolName("Wild Major 1")]
		Wild_Major_1 = (1 << 3),
		[ToolName("Wild Major 2")]
		Wild_Major_2 = (1 << 4),

		[ToolName("Blood Minor 1")]
		Blood_Minor_1 = (1 << 5),
		[ToolName("Blood Minor 2")]
		Blood_Minor_2 = (1 << 6),
		[ToolName("Blood Major 1")]
		Blood_Major_1 = (1 << 7),
		[ToolName("Blood Major 2")]
		Blood_Major_2 = (1 << 8),

		[ToolName("Ruby Minor 1")]
		Ruby_Minor_1 = (1 << 9),
		[ToolName("Ruby Minor 2")]
		Ruby_Minor_2 = (1 << 10),
		[ToolName("Ruby Major 1")]
		Ruby_Major_1 = (1 << 11),
		[ToolName("Ruby Major 2")]
		Ruby_Major_2 = (1 << 12),

		[ToolName("Diamond Minor 1")]
		Diamond_Minor_1 = (1 << 13),
		[ToolName("Diamond Minor 2")]
		Diamond_Minor_2 = (1 << 14),
		[ToolName("Diamond Major 1")]
		Diamond_Major_1 = (1 << 15),
		[ToolName("Diamond Major 2")]
		Diamond_Major_2 = (1 << 16),

		[ToolName("Sapphire Minor 1")]
		Sapphire_Minor_1 = (1 << 17),
		[ToolName("Sapphire Minor 2")]
		Sapphire_Minor_2 = (1 << 18),
		[ToolName("Sapphire Major 1")]
		Sapphire_Major_1 = (1 << 19),
		[ToolName("Sapphire Major 2")]
		Sapphire_Major_2 = (1 << 20),

		[ToolName("Warrior Minor")]
		Warrior_Minor = (1 << 21),
		[ToolName("Warrior Major")]
		Warrior_Major = (1 << 22),
	}

    [Flags]
    public enum EAICardStates
    {
        None        = 0,
        AbleToBlock = (1 << 0),
    }

	// these are separate from ECardTypes because they're not flags and I only want the spell type.
	// these go on abilities and govern the turn phase they can be used in.
	public enum EAbilityCastingBehavior
	{
		[ToolName("Quick")]
		QuickAction = ECardTypes.QuickAction,
		[ToolName("Basic")]
		BasicAction = ECardTypes.BasicAction,
	}

	[Flags]
	public enum ECardShards
	{
		Unknown = 0x0000,
		[ToolName]
		Colorless = 0x0001,
		[ToolName("Blood")]
        Blood = 0x0004,
        [ToolName("Ruby")]
        Ruby = 0x0008,
		[ToolName("Sapphire")]
        Sapphire = 0x0010,
		[ToolName("Wild")]
        Wild = 0x0020,
		[ToolName("Diamond")]
        Diamond = 0x0040,

		// the card editor tool shouldn't be able to set a card color to these.
		Any = Colorless | Diamond | Wild | Ruby | Sapphire | Blood,
		AnyColor = Diamond | Wild | Ruby | Sapphire | Blood,
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Returns true if the given color is Black, Red, White, Blue or Green, but not a combo.
		/// </summary>
		public static bool IsThresholdColor(this ECardShards color)
		{
			return
				ECardShards.Blood == color ||
				ECardShards.Ruby == color ||
				ECardShards.Sapphire == color ||
				ECardShards.Wild == color ||
				ECardShards.Diamond == color;
		}
	}

    public enum ECardValues
    {
        None = 0,
        Attack,
        Defense,
        CastingCost,
        ResourceCost,
    }

	[Flags]
	public enum ECardAttributes
	{
		Unknown = 0,

		[ToolName("Life Drain")]
		SpiritDrain = (1 << 0),
		[ToolName]
		Flight = (1 << 1),
		[ToolName]
		Speed = (1 << 2), 
		[ToolName("Sky Guard")]
		SkyGuard = (1 << 3),
		[ToolName("Crush")]
		Juggernaught = (1 << 4),
		[ToolName("Steadfast")]
		Steadfast = (1 << 5),
		[ToolName("Invincible")]
		Immortal = (1 << 6),
		[ToolName("Spell Shield")]
		SpellShield = (1 << 7),
		[ToolName]
		Unique = (1 << 8),
		[ToolName("Can't Attack")]
		CantAttack = (1 << 9), // Functionally equivalent to Defensive but isn't supposed to be a targetable keyword.
		[ToolName("Can't Block")]
		CantBlock = (1 << 10),
		[ToolName]
		Defensive = (1 << 11),
		[ToolName("Must Attack")]
		ForceAttack = (1 << 12),
		[ToolName("No Auto-Ready")]
		CantReadyAutomatically = (1 << 13),
		[ToolName("Swift Strike")]
		FirstStrike = (1 << 14),
		[ToolName("Rage")]
		Rage = (1 << 15),
		[ToolName("Must Block")]
		MustBlock = (1 << 16),
		[ToolName("Can't be Blocked")]
		CantBeBlocked = (1 << 17),
		[ToolName("Prevent Combat Damage")]
		PreventCombatDamage = (1 << 18), // Similar to Immortal but isn't supposed to be a targetable keyword.
		[ToolName("Prevent Non-Combat Damage")]
		PreventNonCombatDamage = (1 << 19), // Similar to Immortal but isn't supposed to be a targetable keyword.
		[ToolName("Prevent All Damage")]
		PreventAllDamage = PreventCombatDamage | PreventNonCombatDamage, // Similar to Immortal but isn't supposed to be a targetable keyword.
		[ToolName("Dual Strike")]
		DualStrike = (1 << 20),
		[ToolName("Can't Inflict Combat Damage")]
		CantInflictCombatDamage = (1 << 21),
		[ToolName("Can't Inflict Non-Combat Damage")]
		CantInflictNonCombatDamage = (1 << 22),
		[ToolName("Can't Inflict Any Damage")]
		CantInflictAnyDamage = CantInflictCombatDamage | CantInflictNonCombatDamage,
		[ToolName("Enters play Exhausted")]
		EntersPlayExhausted = (1 << 23),
		[ToolName("Inspire")]
		Inspire = (1 << 24),
		[ToolName("Escalation")]
		Escalation = (1 << 25),
		[ToolName("Doesn't ready next ready step.")]
		DoesntReadyNextReadyStep = (1 << 26),
		[ToolName("Troops dealt damage by this troop are voided.")]
		VoidsDamagedTroops = (1 << 27),
        [ToolName("Can be played as a Quick Action.")]
        QuickAction = (1 << 28),
        [ToolName("Allows inspire troops to work from Graveyard.")]
        AllowYardInspire = (1 << 29),
        [ToolName("Must be blocked.")]
        MustBeBlocked = (1 << 30),
    }

	[Flags]
	public enum ECardStates
	{
		None = 0,
		Tapped = (1 << 0),
		Blocking = (1 << 1),
		Attacking = (1 << 2),
		Damaged = (1 << 4),
		Healed = (1 << 5),
		Dead = (1 << 6),
		HasAttacked = (1 << 7),
		HasBlocked = (1 << 8),
		EffectExpired = (1 << 9),
		ZoneChangeReplacement = (1 << 10),
		Activated = (1 << 11),
		VoidsIfDestroyed = (1 << 12),
        CameOutThisTurn = (1 << 13),
        StartedATurnOnYourSide = (1 << 14),
	}

	[Flags]
	public enum ECardEvents
	{
		None = 0,
		AttackBuffed = (1 << 0),
		DefenseBuffed = (1 << 1),
		Damaged = (1 << 2),
		Exhausted = (1 << 3),
		Readied = (1 << 4),
		Healed = (1 << 5),
		Generic = (1 << 6),
		AttackDebuffed = (1 << 7),
		DefenseDebuffed = (1 << 8),
		EffectExpired = (1 << 9),
		CostChanged = (1 << 10),
	}


	[Obsolete("Use ECardTypes")]
	public enum ESpellTypes
	{
		None,
		[ToolName("Basic")]
		Default,
		[ToolName]
		Instant,
		[ToolName]
		Persistent, // TODO: Docs say there are 2 types of persist spells: enchantment and attachments
	}

	public enum EAbilityDurations
	{
		Unknown = 0,
		// one-shot effect
		Instant,
		WhileCardInPlay,
		EndOfTurn,
		// TODO: will there be until-the-end-of-your-turn or until-the-end-of-your-next-turn abilities?  
		// Yes.
		BeginningOfOwnersTurn,
		EndOfGame,
		Permanent,
		WhileCardTapped,
		AfterCardsReadyOnPlayersTurn,
		AfterNextTimeDamaged,
        UntilItLeavesYourHand,
	}

	// these properties are tied to the ability instance
	public enum EAbilityProperties
	{
		Unknown = 0,
		/// <summary>
		/// Required resourcesspent by the player to activate this ability.  This is defined 
		/// in the ability template
		/// </summary>
		AbilityResourceCost,
		/// <summary>
		/// Required charge points spent by the player to activate this ability.  This is defined 
		/// in the ability template
		/// </summary>
		AbilityChargePointCost,
		/// <summary>
		/// Optional resources spent by the player to activate this ability as in an "X Cost/X Damage" spell
		/// </summary>
		AbilityResourceXCost,
		/// <summary>
		/// Charge points spent by the player to activate this ability as in an "X Cost/X Damage" spell
		/// </summary>
		AbilityChargePointXCost,
		/// <summary>
		/// First available option ability.  Use for generic options.
		/// </summary>
		AbilityFirstOption,
		/// <summary>
		/// Second available option ability.  Use for generic options.
		/// </summary>
		AbilitySecondOption,
        /// <summary>
        /// Required spell points spent by the player to activate this ability.  This is defined 
        /// in the ability template
        /// </summary>
        AbilitySpellPointCost,
        /// <summary>
        /// Spell points spent by the player to activate this ability as in an "X Cost/X Damage" spell
        /// </summary>
        AbilitySpellPointXCost,
	}

    public enum EAbilityTypes
    {
        Unknown = 0,
        TriggeredAbility,
        ActivatedAbility,
        ContinuousAbility,
    }

	// these properties are tied to the effect instances on an ability instance.  Different effects might
	// do different quanitities of damage or affect different numbers of targets, etc.
	public enum EAbilityEffectProperties
	{
		Unknown = 0,
		/// <summary>
		/// Number of targets this effect hit, after all validations are taken in to account.
		/// </summary>
		NumberOfTargetsAffected,
		/// <summary>
		/// Defense value of all targets hit by the effect
		/// </summary>
		TotalTargetDefense,
		/// <summary>
		/// Attack value of all targets hit by the effect
		/// </summary>
		TotalTargetAttack,
		/// <summary>
		/// Number of targets destroyed by this effect.
		/// </summary>
		NumberOfTargetsDestroyed,
		/// <summary>
		/// Amount of damage dealt to all targets by this effect.
		/// </summary>
		TotalDamageDealt,
		/// <summary>
		/// Cost of all targets hit by the effect
		/// </summary>
		TotalTargetCost,

        NumSacrificed,
        TotalSacrificedAttack,
        TotalSacrificedDefense,

        TotalVoidedAttack,
        TotalVoidedDefense,
    }

	// these properties on a card are available to effects for calculation
	public enum ECardProperties
	{
		Unknown = 0,
		CurrentAttackValue,
		CurrentDefenseValue,
		CurrentHealthValue,
        ResourceCostTrue,
		/// <summary>
		/// Required resourcesspent by the player to activate this ability.  This is defined 
		/// in the ability template
		/// </summary>
		ResourceCost,
		/// <summary>
		/// Optional resources spent by the player to activate this ability as in an "X Cost/X Damage" spell
		/// </summary>
		ResourceXCost,
		SacrificeCost,
		DiscardCost,
		ExhaustCost,
		VoidCost,
		CounterCost,
		LifeCost,
		ChargePointCost,
		InternalMemoryValue,
		LastDamageDealt,
        CastingCost,
        PutIntoDeckCost,
		RevealCost,
		PutIntoHandCost,
        SpellPointCost,
	}


	public enum ETurnPhasePlayers
	{
		None,
		All,
		Active,
		Defending,
	}
	
	public enum EExtendedArt
	{
		Any,
		NoEA,
		OnlyEA
	}
	
	public enum EAlternateArt
	{
		Any,
		NoAA,
		OnlyAA
	}

	public enum ERarity
	{
        Unknown,
		[ToolName("Other")]
		Land,
		[ToolName]
		Common,
		[ToolName]
		Uncommon,
		[ToolName]
		Rare,
		[ToolName]
		Epic,
		[ToolName]
		Legendary,
		[ToolName]
        Promo,
        [ToolName]
        PvE,
	}

	[Flags]
	public enum ECardEffectStateType
	{
		None = 0,
		[ToolName]
		Hovered = (1 << 0),
		[ToolName]
		Playable = (1 << 1),
		[ToolName]
		SummoningSickness = (1 << 2),
		[ToolName]
		Activatable = (1 << 3),
		[ToolName]
		Discardable = (1 << 4),
		[ToolName]
		Targetable = (1 << 5),
		[ToolName]
		Resurrectable = (1 << 6),
		[ToolName]
		Referenced = (1 << 7),
		[ToolName]
		Attacking = (1 << 8),
		[ToolName]
		Blocking = (1 << 9),
		[ToolName]
		Selected = (1 << 10),
		[ToolName]
		Exhausted = (1 << 11),
		[ToolName]
		BeginMove = (1 << 12),
		[ToolName]
		Moving = (1 << 13),
		[ToolName]
		AttackBlocked = (1 << 14),
		[ToolName]
		CanReady = (1 << 15),
		[ToolName]
		CanAttack = (1 << 16),
		[ToolName]
		CanBlock = (1 << 17),
		[ToolName]
		Blockable = (1 << 18),
		[ToolName]
		Sacrificable = (1 << 19),
		[ToolName]
		Toxified = (1 << 20),
		[ToolName]
		Vulnerable = (1 << 21),
		[ToolName]
		Tunneling = (1 << 22),
        [ToolName]
        DeathSentence = (1 <<23),
        [ToolName]
        Hidden = (1 << 24),
        [ToolName]
        Dazed = (1 << 25),
        [ToolName]
        Burning = (1 << 26),
        [ToolName]
        Armored = (1 << 27),
        [ToolName]
        DamageShield = (1 << 28),
    }

	[Flags]
	public enum ECardEffectEventType
	{
		None = 0,
		[ToolName]
		GenericEffect = (1 << 0),
		[ToolName]
		Block = (1 << 1),
		[ToolName]
		AttackBuffed = (1 << 2),
		[ToolName]
		DefenseBuffed = (1 << 3),
		[ToolName]
		Exhaust = (1 << 4),
		[ToolName]
		Ready = (1 << 5),
		[ToolName]
		PlaySpell = (1 << 6),
		[ToolName]
		PlayTroop = (1 << 7),
		[ToolName]
		PlayResource = (1 << 8),
		[ToolName]
		PlayArtifact = (1 << 9),
		[ToolName]
		EndMove = (1 << 10),
		[ToolName]
		Voided = (1 << 11),
		[ToolName]
		Destroyed = (1 << 12),
		[ToolName]
		Summoned = (1 << 13),
		[ToolName]
		Transformed = (1 << 14),
		[ToolName]
		Damaged = (1 << 15),
		[ToolName]
		Zoom = (1 << 16),
		[ToolName]
		Unzoom = (1 << 17),
		[ToolName]
		BeginMove = (1 << 18),
		[ToolName]
		Attack = (1 << 19),
		[ToolName]
		Healed = (1 << 20),
		[ToolName]
		AttackDebuffed = (1 << 21),
		[ToolName]
		DefenseDebuffed = (1 << 22),
		[ToolName]
		Exhausted = (1 << 23),
		[ToolName]
		EffectExpired = (1 << 24),
		[ToolName]
		ActivatedAbility = (1 << 25),
		[ToolName]
		BaneDrawn = (1 << 26),
        [ToolName]
        VennenEggSpawned = (1 << 27),
	}


	public enum EMatchResult
	{
		None,
		Win,
		Lose,
		Tie,
	}

	public enum EMatchmakingQueue
	{
		Type1,
		Type2,
		Type3,
		Type4,
	}

	public enum ELineConnectorType
	{
		[ToolName]
		None = 0,
		[ToolName]
		Attack,
		[ToolName]
		Blocked,
		[ToolName]
		Ability,
		[ToolName]
		Chain,
	}

	public enum ECardProjectileType
	{
		[ToolName]
		None = 0,
		[ToolName]
		Attack,
		[ToolName]
		Blocked,
		[ToolName]
		Ability,
		[ToolName]
		PlayResource,
        [ToolName]
        PlayShift,
        [ToolName]
        PlayProphecy,
        [ToolName]
        BaneDamage,
	}

	public enum EDungeonEffectType
	{
		None = -1,
		Victory,
		Failure,
		Unlock,
	}

	public enum EChallengeBucket
	{
		None,
		[ToolName]
		Profile,
		[ToolName]
		Quest,
		[ToolName]
		Dungeon,
		[ToolName]
		ClientFX,
	}

	public enum EChallengeGroup
	{
		None,
		[ToolName]
		General,
		[ToolName]
		Exploration,
		[ToolName]
		Campaign,
		[ToolName]
		Profession,
		[ToolName]
		Special,
		[ToolName]
		Dungeon,
		[ToolName]
		Raid,
		[ToolName]
		Tournament,
		[ToolName]
		Races,
		[ToolName]
		Reputation,
		[ToolName]
		Collection,
		[ToolName]
		Guild,
		[ToolName]
		Card,
	}

	//  TODO:  Find out what styles of decks Crypto thinks players will be able to make (these are high level concepts)
	public enum EDeckStyle
	{
		Unknown = -1,
		Aggro,
		Control,
		_Count,
	}

    public enum EDeckLock
    {
        Not_Locked = 0,
        Tournament_Lock = 1,
        Waiting_Room_Lock = 2,
        Player_Lock = 3,
        Escrow_Lock = 4,
        Arena_Lock = 5,
        Campaign_Lock = 6
    }

	public enum EEffectNodeName
	{
		None = -1,
		Card_Abilities = 0,
		Card_Info = 1,
		Card_Set = 2,
		Card_Cost = 3,
		Card_Flavortext = 4,
		Card_Name = 5,
		Card_Power = 6,
		Card_Toughness = 7,
		Card_TypeExtended = 8,
		Card_Type = 9,
		Card_TypeStandard = 10,
		Card_TypelineRestrictions = 11,
		Card_Portrait = 12,
		Card_Center = 13,
		Card_ManaThreshold = 51,
		Card_Faction = 52,
		Card_DistanceView_Power = 53,
		Card_DistanceView_Toughness = 54,
		Card_GemBar = 55,

		Card_Attribute_Flight = 14,
		Card_Attribute_Juggernaught = 15,
		Card_Attribute_SpiritDrain = 16,
		Card_Attribute_Immortal = 17,
		Card_Attribute_CantAttack = 18,
		Card_Attribute_CantBlock = 19,
		Card_Attribute_Steadfast = 20,
		Card_Attribute_SkyGuard = 21,
		Card_Attribute_SpellShield = 22,
		Card_Attribute_ForceAttack = 23,

		Board_Center = 24,
		Board_Phase_Ready = 25,
		Board_Phase_Main = 26,
		Board_Phase_DeclareCombat = 27,
		Board_Phase_ChooseAttackers = 28,
		Board_Phase_ChooseBlockers = 29,
		Board_Phase_AssignDamage = 30,
		Board_Phase_2ndMain = 31,
		Board_Phase_EndTurn = 32,
		Board_Player_Dais = 33,
		Board_Player_Deck = 34,
		Board_Player_Graveyard = 35,
		Board_Opponent_Dais = 36,
		Board_Opponent_Deck = 37,
		Board_Opponent_Graveyard = 38,

		Champion_Portrait = 39,
		Champion_Health = 40,
		Champion_Resources = 41,
		Champion_Charges = 42,
		Champion_AbilityA = 43,
		Champion_AbilityB = 44,
		Champion_AbilityC = 45,
		Champion_Resource_Ruby = 46,
		Champion_Resource_Diamond = 47,
		Champion_Resource_Sapphire = 48,
		Champion_Resource_Wild = 49,
		Champion_Resource_Blood = 50,
	}

	public enum EBoardEffects
	{
		NONE = -1,
		Phase_Ready,
		Phase_Main,
		Phase_DeclareCombat,
		Phase_ChooseAttackers,
		Phase_ChooseBlockers,
		Phase_AssignDamage,
		Phase_2ndMain,
		Phase_EndTurn,
		Player,
		Opponent,
		CombatIndicator,
		Player_Graveyard,
		Opponent_Graveyard,
	}
	
	public enum EPhaseStops
	{
		Unknown    = 0,
		Ready      = 1 << 0,
		FirstMain  = 1 << 1,
		Combat     = 1 << 2,
		Attackers  = 1 << 3,
		Blockers   = 1 << 4,
		Damage     = 1 << 5,
		SecondMain = 1 << 6,
		EndTurn    = 1 << 7,
	}

	public enum EAICommand
	{
		Reset = 0,
		SetPassive,
		SetForceAttackAll,
		SetForceBlockOne,
		SetForceBlockEven,
	}

	/// <summary>
	/// Use with toolname attribute to specify what control the editor uses for that property
	/// </summary>
	public enum EEditorType
	{
		Default,
		/// <summary>
		/// For string properties that represent a path to a file in the data directory
		/// </summary>
		Path,
		/// <summary>
		/// For properties that have nested properties that also need to be edited
		/// If the property type has subtypes visible to the editor, then the property can also be reinstantiated as one of those subtypes
		/// </summary>
		Nested,
		/// <summary>
		/// For a list of CardThreshold objects
		/// </summary>
		CardThreshold,
	}

	/// <summary>
	/// Different types of card ability costs supported by the card cost framework.
	/// </summary>
	public enum EAbilityCostType
	{
		Default,
		ExhaustAbilityCostType,
		SacrificeAbilityCostType,
		ShuffleIntoDeckAbilityCostType,
		DiscardAbilityCostType,
		VoidAbilityCostType,
		PutIntoDeckAbilityCostType,
		RevealAbilityCostType,
		PutIntoHandAbilityCostType,
        XCostAbilityCostType,
    }

	public enum EAIEvaluationRole
	{
		None,
		Combat,
		Play,
	}

	/// <summary>
	/// Selection modes available in the battle scene.
	/// </summary>
	public enum ECardSelectionType
	{
		/// <summary>
		/// The card cannot be selected in any way.
		/// </summary>
		None,
		/// <summary>
		/// The card can be targeted by an ability (which does not discard or resurrect the card).
		/// </summary>
		Targetable,
		/// <summary>
		/// The card has an ability that can be activated right now.
		/// </summary>
		Activatable,
		/// <summary>
		/// The card can be discarded right now.
		/// </summary>
		Discardable,
		/// <summary>
		/// The card can be "resurrected" (called back from the graveyard into hand or play) right now.
		/// </summary>
		Resurrectable,
		/// <summary>
		/// The card can be sacrificed as a cost for another card right now.
		/// </summary>
		Sacrificable,
		/// <summary>
		/// The card can be blocked by the currently chosen blocker.
		/// </summary>
		Blockable,
		/// <summary>
		/// The card can be played (e.g. from hand).
		/// </summary>
		Playable,
		/// <summary>
		/// The card can enter combat as an attacker.
		/// </summary>
		CanAttack,
		/// <summary>
		/// The card can enter combat as a blocker.
		/// </summary>
		CanBlock,
		/// <summary>
		/// The card can be readied/untapped.
		/// </summary>
		CanReady,
	}

    public enum ETrueFalseUseDefault
    {
        UseDefault = -1,
        False = 0,
        True = 1,
    }

    public enum EStateOfAbilityOnChain
    {
        Error = -1,
        NeedsInput = 0,
        WaitingForClientToProcessOtherMessages,
        Completed
    }

    public enum EMessagePriority
    {
        Low = -1,
        Default = 0,
        High = 1,
    }

    public enum ESpinEntryColor
    {
        Any = -1,
        Red = 1,
        White = 0,
        Gold = 2
    }

    public enum EChestSpinStatus
    {
        [ToolName(true)]
        NoSpin,
        [ToolName(true)]
        PaidSpin,
        [ToolName(true)]
        FreeSpin
    }

    public enum EChestUpgradeStatus
    {
        Common,
        Uncommon,
        Rare,
        Legendary,
        Primal
    }

    public enum ESpinEntrySymbol
    {
        Eye = 4,
        Hand = 2,
        Mushroom = 5,
        Moon = 3,
        Skull = 6,
        Crown = 1,
        Star = 0,
        Heart = 7,
        Spider = 9
    }

    public enum EChestRarity
    {
        Common = 0,
        Uncommon,
        Rare,
        Legendary,
        Primal,
        Promo
    }
	
	public enum EArenaFightResults
	{
		NONE,
		WIN,
		LOSE
	}

    public enum ECampaignDifficulty
    {
        EASY,
        NORMAL,
        HARD
    }

    public enum EAdventureZoneType
    {
        REGION,
        DUNGEON
    }

    public enum ELocationState
    {
        NONE,
        VICTORY,
        FAILURE
    }

    public enum EModTarget
    {
        AIPlayer,
        All,
        UserPlayer
         
    }


	public enum EMailTab
	{
		Inbox,
		Delivered,
		Read,
		Compose
	}

    public struct sCostInfo
    {
        public int Min;
        public int Max;
        public List<SessionCardId> Targets;
    }

    public enum EVisualEffectType
    {
        None,
        Arcane,
        Chaos,
        Earth,
        Fire,
        Holy,
        Ice,
        Lightning,
        Mechanical,
        Poison,
        Smoke,
        Unholy,
        Void,
        Water,
        Wild
    }

	public enum ETalentCategory
    {
        [ToolName]
        Unknown = 0,
        [ToolName]
        OutOfBattle,
        [ToolName]
        PreBattle,
        [ToolName]
        InBattle,
	}

    public enum ETalentSelectionType
    {
        [ToolName]
        Normal       = 0,
        [ToolName]
        NoDeselect   = 1 << 0,
        [ToolName]
        AutoSelected = 1 << 1,
    }
}
