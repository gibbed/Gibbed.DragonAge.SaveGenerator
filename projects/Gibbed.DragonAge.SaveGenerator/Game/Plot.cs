/* Copyright (c) 2017 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

namespace Gibbed.DragonAge.SaveGenerator.Game
{
    public class Plot
    {
        public string PlayerName = "Bob";
        public PlayerGender PlayerGender = PlayerGender.Male;
        public PlayerRace PlayerRace = PlayerRace.Human;
        public PlayerClass PlayerClass = PlayerClass.Warrior;
        public PlayerBackground PlayerBackground = PlayerBackground.HumanNoble;

        // Followers
        public FollowerOptions FollowerOptions = FollowerOptions.DogRecruited |
                                                 FollowerOptions.LelianaRecruited |
                                                 FollowerOptions.ZevranRecruited;

        public RomanceOptions RomanceOptions = RomanceOptions.None;

        public IsabellaResult IsabellaResult = IsabellaResult.None;

        public ZevranResult ZevranResult = ZevranResult.None;

        // Broken Circle
        public CircleResult CircleResult = CircleResult.SidedWithMages;

        // The Arl of Redcliffe
        public ArlSiegeOptions ArlSiegeOptions = ArlSiegeOptions.Over;
        public ArlConnorResult ArlConnorResult = ArlConnorResult.ConnorFreed;
        public ArlRitualResult ArlRitualResult = ArlRitualResult.CircleDoesRitual;
        public ArlDemonOptions ArlDemonOptions = ArlDemonOptions.None;

        // Nature of the Beast
        public BeastResult BeastResult = BeastResult.SidedWithElves;
        public BeastOptions BeastOptions = BeastOptions.ZathrianSacrified;

        // The Urn of Sacred Ashes
        public UrnOptions UrnOptions = UrnOptions.None;
        public LelianaResult LelianaResult = LelianaResult.None;

        // A Paragon of Her Kind
        public ParagonAnvilResult ParagonAnvilResult = ParagonAnvilResult.Caridin;
        public ParagonKingResult ParagonKingResult = ParagonKingResult.KingIsHarrowmont;

        // The Landsmeet
        public LandsmeetKingResult LandsmeetKingResult = LandsmeetKingResult.AlistairIsKing;
        public LandsmeetAlistairResult LandsmeetAlistairResult = LandsmeetAlistairResult.None;
        public LandsmeetLoghainResult LandsmeetLoghainResult = LandsmeetLoghainResult.Executed;

        // The Battle of Denerim
        public ClimaxArchdemonResult ClimaxArchdemonResult = ClimaxArchdemonResult.PlayerKilledArchdemon;
        public ClimaxRitualResult ClimaxRitualResult = ClimaxRitualResult.RitualWithPlayer;

        // Epilogue
        public EpilogueBoonResult EpilogueBoonResult = EpilogueBoonResult.None;

        // DLC: The Stone Prisoner
        public ShaleResult ShaleResult = ShaleResult.None;

        // DLC: Return to Ostagar
        public CailanResult CailanResult = CailanResult.None;

        // DLC: Awakening
        public AwakeningArchitectResult AwakeningArchitectResult = AwakeningArchitectResult.DealWithArchitect;

        public AwakeningOptions AwakeningOptions = AwakeningOptions.AndersRecruited |
                                                   AwakeningOptions.HerrenCompletedSilverite |
                                                   AwakeningOptions.VigilsKeepSiegeCompleted;

        public AwakeningDefenseResult AwakeningDefenseResult = AwakeningDefenseResult.Farms;

        // DLC: Warden's Keep
        public KeepOptions KeepOptions = KeepOptions.None;

        public KeepResult KeepResult = KeepResult.None;

        // DLC: Witch Hunt
        public WitchResult WitchResult = WitchResult.None;

        // DLC: The Golems of Amgarrak
        public bool GolemStarted = false;
    }
}
