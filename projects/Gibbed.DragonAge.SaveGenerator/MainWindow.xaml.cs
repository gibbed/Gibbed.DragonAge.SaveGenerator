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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Gibbed.Bioware.FileFormats;
using Gibbed.DragonAge.SaveGenerator.Resources;
using Gibbed.DragonAge.SaveGenerator.ViewModel;
using GFF = Gibbed.Bioware.FileFormats.GenericFileFormat;

namespace Gibbed.DragonAge.SaveGenerator
{
    public partial class MainWindow
    {
        private readonly WizardViewModel _WizardViewModel;

        public MainWindow()
        {
            this.InitializeComponent();

            this._WizardViewModel = new WizardViewModel();
            this._WizardViewModel.RequestClose += this.OnViewModelRequestClose;
            this.DataContext = this._WizardViewModel;
        }

        public Game.Plot Result
        {
            get { return this._WizardViewModel.Plot; }
        }

        private static void SetPlotFlag(List<GenericKeyValue> plotList, string guid, int flag)
        {
            GenericKeyValue plot = plotList.FirstOrDefault(
                p => p[16402].As<string>(null) == guid);
            if (plot == null)
            {
                plot = new GenericKeyValue(GFF.FieldType.Structure, null);
                plot[16402] = new GenericKeyValue(GFF.FieldType.String, guid);
                plot[16403] = new GenericKeyValue(GFF.FieldType.UInt32, 0u);
                plot[16404] = new GenericKeyValue(GFF.FieldType.UInt32, 0u);
                plot[16405] = new GenericKeyValue(GFF.FieldType.UInt32, 0u);
                plot[16406] = new GenericKeyValue(GFF.FieldType.UInt32, 0u);
                plotList.Add(plot);
            }

            if (flag < 32)
            {
                plot[16403].Value = plot[16403].As<uint>() | (1u << flag);
            }
            else if (flag < 64)
            {
                plot[16404].Value = plot[16404].As<uint>() | (1u << flag - 32);
            }
            else if (flag < 96)
            {
                plot[16405].Value = plot[16405].As<uint>() | (1u << flag - 64);
            }
            else if (flag < 128)
            {
                plot[16406].Value = plot[16406].As<uint>() | (1u << flag - 96);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private void Export()
        {
            var filtered = this.Result.PlayerName;

            var regex = String.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars())));
            var removeInvalidChars = new Regex(regex,
                                               RegexOptions.Singleline | RegexOptions.Compiled |
                                               RegexOptions.CultureInvariant);
            filtered = removeInvalidChars.Replace(filtered, "");

            var charPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            charPath = Path.Combine(charPath, "BioWare");
            charPath = Path.Combine(charPath, "Dragon Age");
            charPath = Path.Combine(charPath, "Characters");
            charPath = Path.Combine(charPath, "Generated_" + filtered);
            charPath = Path.Combine(charPath, "Saves");

            string basePath;
            int slot = 1;
            while (true)
            {
                basePath = Path.Combine(charPath,
                                        string.Format("Slot_{0}", slot));
                if (Directory.Exists(basePath) == false)
                {
                    break;
                }
                slot++;
            }

            Directory.CreateDirectory(basePath);

            using (var output = File.Create(Path.Combine(basePath, "Barkspawn.das.met")))
            {
                this.CreateInfo(slot).Serialize(output);
            }

            using (var output = File.Create(Path.Combine(basePath, "Barkspawn.das")))
            {
                this.CreateSave().Serialize(output);
            }
        }

        protected GenericDataFile CreateInfo(int slot)
        {
            var root = new GenericKeyValue(GFF.FieldType.Structure, null);
            root.StructureId = 0;

            root[16511] = new GenericKeyValue(GFF.FieldType.String,
                                              "Save created with Gibbed's Dragon Age Save Generator.\n\n" +
                                              "*** DO NOT LOAD THIS SAVE IN DRAGON AGE: ORIGINS ***");
            root[16800] = new GenericKeyValue(GFF.FieldType.String, "Dragon Age 2");
            root[16801] = new GenericKeyValue(GFF.FieldType.Int32, 4271);
            root[16802] = new GenericKeyValue(GFF.FieldType.Int32, -1);
            root[16803] = new GenericKeyValue(GFF.FieldType.Int32, (int)this.Result.PlayerClass);
            root[16804] = new GenericKeyValue(GFF.FieldType.Int32, (int)this.Result.PlayerGender);
            root[16805] = new GenericKeyValue(GFF.FieldType.Int32, (int)this.Result.PlayerRace);
            root[16806] = new GenericKeyValue(GFF.FieldType.Int32, (int)this.Result.PlayerBackground);
            root[16807] = new GenericKeyValue(GFF.FieldType.String, this.Result.PlayerName);
            root[16808] = new GenericKeyValue(GFF.FieldType.String, string.Format("Generated #{0}", slot));
            root[16809] = new GenericKeyValue(GFF.FieldType.String, "single player;");
            root[16810] = new GenericKeyValue(GFF.FieldType.String, "SP;");

            var gff = new GenericDataFile();
            gff.Deserialize(new MemoryStream(BinaryResources.InfoGFF));
            gff.Import(root);
            return gff;
        }

        protected GenericDataFile CreateSave()
        {
            var root = new GenericKeyValue(GFF.FieldType.Structure, null);
            root.StructureId = 0;

            var campaign = new GenericKeyValue(GFF.FieldType.Structure, null);
            campaign[23] = new GenericKeyValue(GFF.FieldType.Int32, 1);
            campaign[16014] = new GenericKeyValue(GFF.FieldType.String, "single player");
            campaign[16450] = new GenericKeyValue(GFF.FieldType.Structure, null);
            campaign[16499] = new GenericKeyValue(GFF.FieldType.Int32, 120000);

            var journal = new GenericKeyValue(GFF.FieldType.Structure, null);
            journal[16505] = new GenericKeyValue(GFF.FieldType.Structure, null);
            journal[16506] = new GenericKeyValue(GFF.FieldType.Structure, null);
            journal[16510] = new GenericKeyValue(GFF.FieldType.String, "");
            journal[16511] = new GenericKeyValue(GFF.FieldType.TalkString, new GFF.Builtins.TalkString());
            journal[16513] = new GenericKeyValue(GFF.FieldType.Structure, null);
            journal[16516] = new GenericKeyValue(GFF.FieldType.Structure, null);
            journal[16522] = new GenericKeyValue(GFF.FieldType.Structure, null);
            journal[16525] = new GenericKeyValue(GFF.FieldType.Structure, null);
            campaign[16504] = journal;

            campaign[16530] = new GenericKeyValue(GFF.FieldType.Structure, null);

            var worldmap = new GenericKeyValue(GFF.FieldType.Structure, null);
            worldmap[16781] = new GenericKeyValue(GFF.FieldType.String, "denerim");
            worldmap[16782] = new GenericKeyValue(GFF.FieldType.String, "wide_open_world");
            worldmap[16783] = new GenericKeyValue(GFF.FieldType.Structure, null);
            worldmap[16790] = new GenericKeyValue(GFF.FieldType.UInt32, 2u);
            worldmap[16791] = new GenericKeyValue(GFF.FieldType.UInt32, 5u);
            campaign[16780] = worldmap;

            var plotActions = new GenericKeyValue(GFF.FieldType.Structure, null);
            plotActions[16841] = new GenericKeyValue(GFF.FieldType.UInt16, (ushort)1);
            plotActions[16842] = new GenericKeyValue(GFF.FieldType.UInt16, (ushort)0);
            plotActions[16843] = new GenericKeyValue(GFF.FieldType.Structure, null);
            campaign[16840] = plotActions;

            campaign[17000] = new GenericKeyValue(GFF.FieldType.Structure, null);
            campaign[17100] = new GenericKeyValue(GFF.FieldType.String, "den02al_den_market");
            campaign[17101] = new GenericKeyValue(GFF.FieldType.String, "den200ar_market");
            campaign[17102] = new GenericKeyValue(GFF.FieldType.Vector3, new GFF.Builtins.Vector3(11.0f, 22.0f, 33.0f));
            campaign[17103] = new GenericKeyValue(GFF.FieldType.Vector3, new GFF.Builtins.Vector3(44.0f, 55.0f, 66.0f));

            root[16000] = campaign;
            root[16001] = new GenericKeyValue(GFF.FieldType.Structure, null);

            var playerChar = new GenericKeyValue(GFF.FieldType.Structure, null);

            var character = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[3] = new GenericKeyValue(GFF.FieldType.String, "default_player");
            character[4] = new GenericKeyValue(GFF.FieldType.Vector3, new GFF.Builtins.Vector3());
            character[5] = new GenericKeyValue(GFF.FieldType.Quaternion, new GFF.Builtins.Quaternion());
            character[23] = new GenericKeyValue(GFF.FieldType.Int32, 1234);
            character[16201] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)1);
            character[16203] = new GenericKeyValue(GFF.FieldType.Int8, (sbyte)1);

            var creatureStats = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16305] = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16306] = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16307] = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16308] = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16313] = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16314] = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16315] = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16316] = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16317] = new GenericKeyValue(GFF.FieldType.UInt32, (uint)0);
            creatureStats[16318] = new GenericKeyValue(GFF.FieldType.Int8, (sbyte)0);
            creatureStats[16329] = new GenericKeyValue(GFF.FieldType.Int8, (sbyte)0);
            creatureStats[16339] = new GenericKeyValue(GFF.FieldType.String, "single player;");
            creatureStats[16340] = new GenericKeyValue(GFF.FieldType.String, "SP;SP;SP;SP;SP;SP;SP;SP;SP;SP;SP;");
            creatureStats[16350] = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16351] = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16352] = new GenericKeyValue(GFF.FieldType.Structure, null);
            creatureStats[16468] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16209] = creatureStats;

            character[16210] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16211] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16212] = new GenericKeyValue(GFF.FieldType.Int32, -1);
            character[16218] = new GenericKeyValue(GFF.FieldType.UInt32, (uint)0);
            character[16219] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16220] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            character[16221] = new GenericKeyValue(GFF.FieldType.String, "");
            character[16222] = new GenericKeyValue(GFF.FieldType.String, "player");
            character[16226] = new GenericKeyValue(GFF.FieldType.Int32, -1);
            character[16227] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16250] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            character[16254] = new GenericKeyValue(GFF.FieldType.Int32, 0);
            character[16255] = new GenericKeyValue(GFF.FieldType.TalkString,
                                                   new GFF.Builtins.TalkString(215786, this.Result.PlayerName));
            character[16256] = new GenericKeyValue(GFF.FieldType.UInt32, (uint)1);
            character[16262] = new GenericKeyValue(GFF.FieldType.Single, 0.0f);
            character[16263] = new GenericKeyValue(GFF.FieldType.Int32, 0);
            character[16279] = new GenericKeyValue(GFF.FieldType.Int8, (sbyte)0);
            character[16280] = new GenericKeyValue(GFF.FieldType.Int32, 0);
            character[16281] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            character[16282] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);

            var appearance = new GenericKeyValue(GFF.FieldType.Structure, null);
            appearance[16321] = new GenericKeyValue(GFF.FieldType.UInt16, (ushort)this.Result.PlayerGender);
            appearance[16322] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)this.Result.PlayerGender);
            appearance[16324] = new GenericKeyValue(GFF.FieldType.Single, 0.0f);
            appearance[16325] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            appearance[16326] = new GenericKeyValue(GFF.FieldType.Structure, null);
            appearance[16327] = new GenericKeyValue(GFF.FieldType.UInt16,
                                                    (ushort)(this.Result.PlayerGender == Game.PlayerGender.Male ? 1 : 2));
            appearance[16328] = new GenericKeyValue(GFF.FieldType.String, "");
            character[16320] = appearance;

            character[16332] = new GenericKeyValue(GFF.FieldType.Single, 0.0f);
            character[16333] = new GenericKeyValue(GFF.FieldType.Single, 0.0f);
            character[16334] = new GenericKeyValue(GFF.FieldType.Single, 0.0f);
            character[16335] = new GenericKeyValue(GFF.FieldType.Single, 0.0f);
            character[16336] = new GenericKeyValue(GFF.FieldType.Single, 0.0f);
            character[16337] = new GenericKeyValue(GFF.FieldType.Single, 0.0f);
            character[16338] = new GenericKeyValue(GFF.FieldType.Single, 0.0f);
            character[16451] = new GenericKeyValue(GFF.FieldType.UInt32, (uint)0);
            character[16453] = new GenericKeyValue(GFF.FieldType.Int32, -1);
            character[16454] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            character[16455] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            character[16456] = new GenericKeyValue(GFF.FieldType.Int8, (sbyte)-1);
            character[16457] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)1);
            character[16458] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)1);
            character[16459] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)1);
            character[16460] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)this.Result.PlayerRace);
            character[16463] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)1);
            character[16464] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16467] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            character[16469] = new GenericKeyValue(GFF.FieldType.Int8, (sbyte)0);
            character[16470] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            character[16471] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16472] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16474] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            character[16475] = new GenericKeyValue(GFF.FieldType.Single, 1.0f);
            character[16600] = new GenericKeyValue(GFF.FieldType.UInt32, (uint)0xFFFFFFFF);
            character[16612] = new GenericKeyValue(GFF.FieldType.UInt32, (uint)100);
            character[16623] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16721] = new GenericKeyValue(GFF.FieldType.Structure, null);

            var actionQueue = new GenericKeyValue(GFF.FieldType.Structure, null);
            actionQueue[16720] = new GenericKeyValue(GFF.FieldType.Structure, null);
            actionQueue[16730] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16740] = actionQueue;

            character[16821] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)1);

            var tacticsTable = new GenericKeyValue(GFF.FieldType.Structure, null);
            tacticsTable[16823] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)1);
            tacticsTable[16824] = new GenericKeyValue(GFF.FieldType.Structure, null);
            tacticsTable[16832] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            tacticsTable[16833] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)2);
            tacticsTable[16834] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            tacticsTable[16835] = new GenericKeyValue(GFF.FieldType.Structure, null);
            tacticsTable[16836] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16822] = tacticsTable;

            character[16950] = new GenericKeyValue(GFF.FieldType.Structure, null);
            character[16951] = new GenericKeyValue(GFF.FieldType.String, null);
            character[16952] = new GenericKeyValue(GFF.FieldType.String, null);
            character[17000] = new GenericKeyValue(GFF.FieldType.Structure, null);

            playerChar[16208] = character;
            playerChar[16295] = new GenericKeyValue(GFF.FieldType.Single, 1.0f);
            playerChar[16296] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            playerChar[16298] = new GenericKeyValue(GFF.FieldType.Int32, 1);

            root[16002] = playerChar;

            var partyList = new GenericKeyValue(GFF.FieldType.Structure, null);
            partyList[23] = new GenericKeyValue(GFF.FieldType.Int32, 2);
            partyList[16204] = new GenericKeyValue(GFF.FieldType.Structure, null);
            partyList[16210] = new GenericKeyValue(GFF.FieldType.Structure, null);
            partyList[16211] = new GenericKeyValue(GFF.FieldType.Structure, null);
            partyList[16212] = new GenericKeyValue(GFF.FieldType.Int32, 1);
            partyList[16226] = new GenericKeyValue(GFF.FieldType.Int32, 100);
            partyList[16227] = new GenericKeyValue(GFF.FieldType.Structure, null);
            partyList[16270] = new GenericKeyValue(GFF.FieldType.Int32, 0u);
            partyList[16274] = new GenericKeyValue(GFF.FieldType.Int32, 0u);
            partyList[16275] = new GenericKeyValue(GFF.FieldType.Structure, null);
            partyList[16278] = new GenericKeyValue(GFF.FieldType.Int32, 0u);
            partyList[16288] = new GenericKeyValue(GFF.FieldType.Structure, null);
            partyList[16290] = new GenericKeyValue(GFF.FieldType.Structure, null);
            partyList[16291] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            partyList[16292] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            partyList[16293] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            partyList[16294] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            partyList[16299] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            partyList[16310] = new GenericKeyValue(GFF.FieldType.Single, 0.0f);

            var plotManager = new GenericKeyValue(GFF.FieldType.Structure, null);
            var plotList = new List<GenericKeyValue>();

            plotManager[16401] = new GenericKeyValue(GFF.FieldType.Structure, plotList);
            partyList[16400] = plotManager;
            partyList[16503] = new GenericKeyValue(GFF.FieldType.Structure, null);
            root[16003] = partyList;

            var version = new GenericKeyValue(GFF.FieldType.Structure, null);
            version[16007] = new GenericKeyValue(GFF.FieldType.UInt8, (byte)0);
            version[16770] = new GenericKeyValue(GFF.FieldType.Int32, 12393);
            version[16771] = new GenericKeyValue(GFF.FieldType.Int32, 1);
            root[16004] = version;

            var gameState = new GenericKeyValue(GFF.FieldType.Structure, null);
            gameState[16626] = new GenericKeyValue(GFF.FieldType.Int32, 12345);

            var aiMaster = new GenericKeyValue(GFF.FieldType.Structure, null);
            aiMaster[16630] = new GenericKeyValue(GFF.FieldType.Structure, null);

            gameState[16636] = aiMaster;

            var worldTimer = new GenericKeyValue(GFF.FieldType.Structure, null);
            worldTimer[16701] = new GenericKeyValue(GFF.FieldType.Int32, 12345);
            worldTimer[16702] = new GenericKeyValue(GFF.FieldType.Int32, 12345);
            gameState[16700] = worldTimer;

            root[16005] = gameState;
            root[16006] = new GenericKeyValue(GFF.FieldType.Structure, null);

            var storySoFar = new GenericKeyValue(GFF.FieldType.Structure, null);
            storySoFar[16970] = new GenericKeyValue(GFF.FieldType.Structure, null);
            storySoFar[16975] = new GenericKeyValue(GFF.FieldType.Structure, null);
            root[16008] = storySoFar;

            root[16500] = new GenericKeyValue(GFF.FieldType.Structure, null);

            // Background
            switch (this.Result.PlayerBackground)
            {
                case Game.PlayerBackground.Magi:
                {
                    SetPlotFlag(plotList, "C9736A91F42440758E570D9ECD796597", 0);
                    break;
                }

                case Game.PlayerBackground.DwarfCommoner:
                {
                    SetPlotFlag(plotList, "C9736A91F42440758E570D9ECD796597", 1);
                    break;
                }

                case Game.PlayerBackground.DwarfNoble:
                {
                    SetPlotFlag(plotList, "C9736A91F42440758E570D9ECD796597", 2);
                    break;
                }

                case Game.PlayerBackground.ElfCity:
                {
                    SetPlotFlag(plotList, "C9736A91F42440758E570D9ECD796597", 3);
                    break;
                }

                case Game.PlayerBackground.ElfDalish:
                {
                    SetPlotFlag(plotList, "C9736A91F42440758E570D9ECD796597", 4);
                    break;
                }

                case Game.PlayerBackground.HumanNoble:
                {
                    SetPlotFlag(plotList, "C9736A91F42440758E570D9ECD796597", 7);
                    break;
                }
            }

            // The Arl of Redcliffe
            switch (this.Result.ArlConnorResult)
            {
                case Game.ArlConnorResult.ConnorFreed:
                {
                    SetPlotFlag(plotList, "245482960AA04DB58C90E40C8354B6B5", 19);
                    break;
                }

                case Game.ArlConnorResult.IsoldeKilledConnor:
                {
                    SetPlotFlag(plotList, "245482960AA04DB58C90E40C8354B6B5", 20);
                    break;
                }

                case Game.ArlConnorResult.IsoldeKnockedOutPCKilledConnor:
                {
                    SetPlotFlag(plotList, "245482960AA04DB58C90E40C8354B6B5", 18);
                    break;
                }

                case Game.ArlConnorResult.PCKilledConnor:
                {
                    SetPlotFlag(plotList, "245482960AA04DB58C90E40C8354B6B5", 32);
                    break;
                }
            }

            switch (this.Result.ArlRitualResult)
            {
                case Game.ArlRitualResult.JowanDoesRitual:
                {
                    SetPlotFlag(plotList, "245482960AA04DB58C90E40C8354B6B5", 21);
                    break;
                }

                case Game.ArlRitualResult.CircleDoesRitual:
                {
                    SetPlotFlag(plotList, "245482960AA04DB58C90E40C8354B6B5", 28);
                    break;
                }
            }

            if (this.Result.ArlDemonOptions.HasFlag(Game.ArlDemonOptions.Intimidated) == true)
            {
                SetPlotFlag(plotList, "80D1FC2FA12E457896C0F1B64E51EEBC", 17);
            }

            if (this.Result.ArlDemonOptions.HasFlag(Game.ArlDemonOptions.AcceptedOffer) == true)
            {
                SetPlotFlag(plotList, "80D1FC2FA12E457896C0F1B64E51EEBC", 7);
            }

            if (this.Result.ArlSiegeOptions.HasFlag(Game.ArlSiegeOptions.Abandoned) == true)
            {
                SetPlotFlag(plotList, "C8BD51CF3BC0414192BFF6BC6BF8247C", 12);
            }

            if (this.Result.ArlSiegeOptions.HasFlag(Game.ArlSiegeOptions.Over) == true)
            {
                SetPlotFlag(plotList, "C8BD51CF3BC0414192BFF6BC6BF8247C", 0);
            }

            // Broken Circle
            switch (this.Result.CircleResult)
            {
                case Game.CircleResult.SidedWithMages:
                {
                    SetPlotFlag(plotList, "C232DA078A044178AA9FCBC6E537FA75", 5);
                    break;
                }

                case Game.CircleResult.SidedWithTemplars:
                {
                    SetPlotFlag(plotList, "C232DA078A044178AA9FCBC6E537FA75", 4);
                    break;
                }
            }

            // Nature of the Beast
            switch (this.Result.BeastResult)
            {
                case Game.BeastResult.SidedWithElves:
                {
                    SetPlotFlag(plotList, "63DD3FD0AE584D59877B55269963459D", 0);
                    break;
                }

                case Game.BeastResult.SidedWithWerewolves:
                {
                    SetPlotFlag(plotList, "63DD3FD0AE584D59877B55269963459D", 13);
                    break;
                }
            }

            if (this.Result.BeastOptions.HasFlag(Game.BeastOptions.ZathrianSacrified) == true)
            {
                SetPlotFlag(plotList, "63DD3FD0AE584D59877B55269963459D", 14);
            }

            // The Urn of Sacred Ashes
            if (this.Result.UrnOptions.HasFlag(Game.UrnOptions.GenitiviReturnedToDenerim) == true)
            {
                SetPlotFlag(plotList, "8B254175421D48E1B47FC915E8750228", 3);
            }

            switch (this.Result.LelianaResult)
            {
                case Game.LelianaResult.LeftForever:
                {
                    SetPlotFlag(plotList, "919B6591AA754F5E8B871766F25A68AB", 19);
                    break;
                }

                case Game.LelianaResult.Killed:
                {
                    SetPlotFlag(plotList, "919B6591AA754F5E8B871766F25A68AB", 41);
                    break;
                }
            }

            // A Paragon of Her Kind
            switch (this.Result.ParagonKingResult)
            {
                case Game.ParagonKingResult.KingIsBhelen:
                {
                    SetPlotFlag(plotList, "B571814CBBA44127B605740BD5483A69", 7);
                    break;
                }

                case Game.ParagonKingResult.KingIsHarrowmont:
                {
                    SetPlotFlag(plotList, "B571814CBBA44127B605740BD5483A69", 8);
                    break;
                }
            }

            switch (this.Result.ParagonAnvilResult)
            {
                case Game.ParagonAnvilResult.Caridin:
                {
                    SetPlotFlag(plotList, "86FBBD4CB45D47FF885B0B2BB5407D1E", 26);
                    break;
                }

                case Game.ParagonAnvilResult.BrankaAlive:
                {
                    SetPlotFlag(plotList, "86FBBD4CB45D47FF885B0B2BB5407D1E", 4);
                    break;
                }

                case Game.ParagonAnvilResult.BrankaSuicided:
                {
                    SetPlotFlag(plotList, "86FBBD4CB45D47FF885B0B2BB5407D1E", 10);
                    break;
                }
            }

            // The Landsmeet
            switch (this.Result.LandsmeetKingResult)
            {
                case Game.LandsmeetKingResult.AlistairEngagedToAnora:
                {
                    SetPlotFlag(plotList, "841A4E6E0CDD43D3BA3BA484D9A2771F", 3);
                    break;
                }

                case Game.LandsmeetKingResult.AlistairIsKing:
                {
                    SetPlotFlag(plotList, "841A4E6E0CDD43D3BA3BA484D9A2771F", 1);
                    break;
                }

                case Game.LandsmeetKingResult.AlistairEngagedToPlayer:
                {
                    SetPlotFlag(plotList, "841A4E6E0CDD43D3BA3BA484D9A2771F", 56);
                    break;
                }

                case Game.LandsmeetKingResult.AnoraIsQueen:
                {
                    SetPlotFlag(plotList, "841A4E6E0CDD43D3BA3BA484D9A2771F", 4);
                    break;
                }

                case Game.LandsmeetKingResult.PlayerIsKing:
                {
                    SetPlotFlag(plotList, "841A4E6E0CDD43D3BA3BA484D9A2771F", 5);
                    break;
                }
            }

            switch (this.Result.LandsmeetAlistairResult)
            {
                case Game.LandsmeetAlistairResult.Drunkard:
                {
                    SetPlotFlag(plotList, "841A4E6E0CDD43D3BA3BA484D9A2771F", 16);
                    break;
                }

                case Game.LandsmeetAlistairResult.Executed:
                {
                    SetPlotFlag(plotList, "841A4E6E0CDD43D3BA3BA484D9A2771F", 17);
                    break;
                }
            }

            switch (this.Result.LandsmeetLoghainResult)
            {
                case Game.LandsmeetLoghainResult.Warden:
                {
                    SetPlotFlag(plotList, "841A4E6E0CDD43D3BA3BA484D9A2771F", 8);
                    break;
                }

                case Game.LandsmeetLoghainResult.Executed:
                {
                    SetPlotFlag(plotList, "841A4E6E0CDD43D3BA3BA484D9A2771F", 6);
                    break;
                }
            }

            // The Battle for Denerim
            switch (this.Result.ClimaxArchdemonResult)
            {
                case Game.ClimaxArchdemonResult.AlistairKilledArchdemon:
                {
                    SetPlotFlag(plotList, "A5FA53EF3C24463693440319F1D564B2", 2);
                    break;
                }

                case Game.ClimaxArchdemonResult.LoghainKilledArchdemon:
                {
                    SetPlotFlag(plotList, "A5FA53EF3C24463693440319F1D564B2", 5);
                    break;
                }

                case Game.ClimaxArchdemonResult.PlayerKilledArchdemon:
                {
                    SetPlotFlag(plotList, "A5FA53EF3C24463693440319F1D564B2", 4);
                    break;
                }
            }

            switch (this.Result.ClimaxRitualResult)
            {
                case Game.ClimaxRitualResult.RitualWithAlistair:
                {
                    SetPlotFlag(plotList, "764C8DAFF2274DFEBC7C7B32FA2BB0CD", 2);
                    SetPlotFlag(plotList, "C354FB10309D4569A325BD82C047E812", 10);
                    break;
                }

                case Game.ClimaxRitualResult.RitualWithLoghain:
                {
                    SetPlotFlag(plotList, "764C8DAFF2274DFEBC7C7B32FA2BB0CD", 12);
                    SetPlotFlag(plotList, "C354FB10309D4569A325BD82C047E812", 11);
                    break;
                }

                case Game.ClimaxRitualResult.RitualWithPlayer:
                {
                    SetPlotFlag(plotList, "764C8DAFF2274DFEBC7C7B32FA2BB0CD", 4);
                    SetPlotFlag(plotList, "C354FB10309D4569A325BD82C047E812", 12);
                    break;
                }
            }

            // Epliogue
            switch (this.Result.EpilogueBoonResult)
            {
                case Game.EpilogueBoonResult.Chancellor:
                {
                    SetPlotFlag(plotList, "7F50C9E955D6461BB34B1A21A88CD4AD", 0);
                    break;
                }

                case Game.EpilogueBoonResult.Circle:
                {
                    SetPlotFlag(plotList, "7F50C9E955D6461BB34B1A21A88CD4AD", 13);
                    break;
                }

                case Game.EpilogueBoonResult.Dalish:
                {
                    SetPlotFlag(plotList, "7F50C9E955D6461BB34B1A21A88CD4AD", 8);
                    break;
                }
            }

            // Companions
            if (this.Result.FollowerOptions.HasFlag(Game.FollowerOptions.DogRecruited) == true)
            {
                SetPlotFlag(plotList, "25BC6F5E8DA847938245071233433332", 1);
            }

            if (this.Result.FollowerOptions.HasFlag(Game.FollowerOptions.LelianaRecruited) == true)
            {
                SetPlotFlag(plotList, "25BC6F5E8DA847938245071233433332", 4);
            }

            if (this.Result.FollowerOptions.HasFlag(Game.FollowerOptions.ZevranRecruited) == true)
            {
                SetPlotFlag(plotList, "25BC6F5E8DA847938245071233433332", 9);
            }

            // Romance

            // Alistair
            if (this.Result.RomanceOptions.HasFlag(Game.RomanceOptions.AlistairRomance) == true)
            {
                SetPlotFlag(plotList, "840C666EA1FE48CBA260AB1FE42FCFA7", 21);
            }

            if (this.Result.RomanceOptions.HasFlag(Game.RomanceOptions.AlistairLove) == true)
            {
                SetPlotFlag(plotList, "840C666EA1FE48CBA260AB1FE42FCFA7", 29);
            }

            // Leliana
            if (this.Result.RomanceOptions.HasFlag(Game.RomanceOptions.LelianaRomance) == true)
            {
                SetPlotFlag(plotList, "E8CEBFA6EB2345EBB704DF06D794C803", 21);
            }

            if (this.Result.RomanceOptions.HasFlag(Game.RomanceOptions.LelianaLove) == true)
            {
                SetPlotFlag(plotList, "E8CEBFA6EB2345EBB704DF06D794C803", 29);
            }

            // Morrigan
            if (this.Result.RomanceOptions.HasFlag(Game.RomanceOptions.MorriganRomance) == true)
            {
                SetPlotFlag(plotList, "E8E833AC06C04BF2A3261A7937542D75", 21);
            }

            if (this.Result.RomanceOptions.HasFlag(Game.RomanceOptions.MorriganLove) == true)
            {
                SetPlotFlag(plotList, "E8E833AC06C04BF2A3261A7937542D75", 29);
            }

            // Zevran
            if (this.Result.RomanceOptions.HasFlag(Game.RomanceOptions.ZevranRomance) == true)
            {
                SetPlotFlag(plotList, "68F1B23EB3EA42F5B363ABE7FEB86A50", 21);
            }

            if (this.Result.RomanceOptions.HasFlag(Game.RomanceOptions.ZevranLove) == true)
            {
                SetPlotFlag(plotList, "68F1B23EB3EA42F5B363ABE7FEB86A50", 29);
            }

            // Isabella
            switch (this.Result.IsabellaResult)
            {
                case Game.IsabellaResult.SleptWith:
                {
                    SetPlotFlag(plotList, "417FCFECF184466D83210AB7CD9B8077", 27);
                    break;
                }

                case Game.IsabellaResult.IsabelaAndAlistairThreesome:
                {
                    SetPlotFlag(plotList, "417FCFECF184466D83210AB7CD9B8077", 23);
                    break;
                }

                case Game.IsabellaResult.IsabelaAndLelianaThreesome:
                {
                    SetPlotFlag(plotList, "417FCFECF184466D83210AB7CD9B8077", 24);
                    break;
                }

                case Game.IsabellaResult.IsabelaAndZevranThreesome:
                {
                    SetPlotFlag(plotList, "417FCFECF184466D83210AB7CD9B8077", 26);
                    break;
                }

                case Game.IsabellaResult.IsabelaInFoursome:
                {
                    SetPlotFlag(plotList, "417FCFECF184466D83210AB7CD9B8077", 28);
                    break;
                }
            }

            switch (this.Result.ZevranResult)
            {
                case Game.ZevranResult.KilledBeforeIntroduction:
                {
                    SetPlotFlag(plotList, "1763DEA8045E4B1F911B44E51CA314D1", 34);
                    break;
                }

                case Game.ZevranResult.GoesHostile:
                {
                    SetPlotFlag(plotList, "1763DEA8045E4B1F911B44E51CA314D1", 14);
                    break;
                }

                case Game.ZevranResult.LeavesAfterKissingFarewell:
                {
                    SetPlotFlag(plotList, "1763DEA8045E4B1F911B44E51CA314D1", 37);
                    break;
                }

                case Game.ZevranResult.LeavesForGood:
                {
                    SetPlotFlag(plotList, "1763DEA8045E4B1F911B44E51CA314D1", 19);
                    break;
                }
            }

            // Downloadable Content

            // Return to Ostagar
            switch (this.Result.CailanResult)
            {
                case Game.CailanResult.Burned:
                {
                    SetPlotFlag(plotList, "3647F8987C154EC3879BC9D6C39F0FB9", 5);
                    break;
                }

                case Game.CailanResult.Darkspawn:
                {
                    SetPlotFlag(plotList, "3647F8987C154EC3879BC9D6C39F0FB9", 7);
                    break;
                }

                case Game.CailanResult.Wolves:
                {
                    SetPlotFlag(plotList, "3647F8987C154EC3879BC9D6C39F0FB9", 6);
                    break;
                }
            }

            // The Stone Prisoner
            switch (this.Result.ShaleResult)
            {
                case Game.ShaleResult.Left:
                {
                    SetPlotFlag(plotList, "028CD0B7B4054E8C8A1C5151DDAEAFB7", 6);
                    break;
                }

                case Game.ShaleResult.Recruited:
                {
                    SetPlotFlag(plotList, "028CD0B7B4054E8C8A1C5151DDAEAFB7", 7);
                    break;
                }

                case Game.ShaleResult.Killed:
                {
                    SetPlotFlag(plotList, "028CD0B7B4054E8C8A1C5151DDAEAFB7", 8);
                    break;
                }
            }

            // Awakening
            switch (this.Result.AwakeningArchitectResult)
            {
                case Game.AwakeningArchitectResult.DealWithArchitect:
                {
                    SetPlotFlag(plotList, "865CD1C26920459CAD2C670099CE8FBE", 2);
                    break;
                }

                case Game.AwakeningArchitectResult.KilledArchitect:
                {
                    SetPlotFlag(plotList, "865CD1C26920459CAD2C670099CE8FBE", 3);
                    break;
                }
            }

            switch (this.Result.AwakeningDefenseResult)
            {
                case Game.AwakeningDefenseResult.Roads:
                {
                    SetPlotFlag(plotList, "2B2A42D365084FA2A082449D8E397A1C", 3);
                    break;
                }

                case Game.AwakeningDefenseResult.Farms:
                {
                    SetPlotFlag(plotList, "2B2A42D365084FA2A082449D8E397A1C", 2);
                    break;
                }
            }

            if (this.Result.AwakeningOptions.HasFlag(Game.AwakeningOptions.Orlesian) == true)
            {
                SetPlotFlag(plotList, "E3F7CA9BE9C543378764CD4D5AFC00A3", 0);
            }

            if (this.Result.AwakeningOptions.HasFlag(Game.AwakeningOptions.AndersRecruited) == true)
            {
                SetPlotFlag(plotList, "E3F7CA9BE9C543378764CD4D5AFC00A3", 6);
            }

            if (this.Result.AwakeningOptions.HasFlag(Game.AwakeningOptions.NathanielRecruited) == true)
            {
                SetPlotFlag(plotList, "E3F7CA9BE9C543378764CD4D5AFC00A3", 9);
            }

            if (this.Result.AwakeningOptions.HasFlag(Game.AwakeningOptions.NathanielFriendly) == true)
            {
                SetPlotFlag(plotList, "5DABBE1A8F3A440592A9F3078D3ABE16", 22);
            }

            if (this.Result.AwakeningOptions.HasFlag(Game.AwakeningOptions.AmaranthineSaved) == true)
            {
                SetPlotFlag(plotList, "1C7395DEAAC14F889A5D41F86854F48B", 7);
            }

            if (this.Result.AwakeningOptions.HasFlag(Game.AwakeningOptions.VigilsKeepSiegeCompleted) == true)
            {
                SetPlotFlag(plotList, "923E283E44DB4F48BA557085F77D1152", 7);
            }

            if (this.Result.AwakeningOptions.HasFlag(Game.AwakeningOptions.HerrenCompletedSilverite) == true)
            {
                SetPlotFlag(plotList, "217BD5BDBB814D2DB41E32F8A05FFC05", 5);
            }

            if (this.Result.AwakeningOptions.HasFlag(Game.AwakeningOptions.AndersDiedInSiege) == true)
            {
                SetPlotFlag(plotList, "80370AD2555A4CFDB33A1288F75CFC2D", 13);
            }

            if (this.Result.AwakeningOptions.HasFlag(Game.AwakeningOptions.NathanielDiedInSiege) == true)
            {
                SetPlotFlag(plotList, "F29B4323C13049BABE1D2E3007C6AD1D", 12);
            }

            // Warden's Keep
            switch (this.Result.KeepResult)
            {
                case Game.KeepResult.AvernusDoingBadExperiments:
                {
                    SetPlotFlag(plotList, "14D501EF13CE49559111ED9DC9C44457", 5);
                    break;
                }

                case Game.KeepResult.AvernusDoingGoodExperiments:
                {
                    SetPlotFlag(plotList, "14D501EF13CE49559111ED9DC9C44457", 3);
                    break;
                }
            }

            if (this.Result.KeepOptions.HasFlag(Game.KeepOptions.Completed) == true)
            {
                SetPlotFlag(plotList, "2F247F5F5B1C41F7845E3D09F20D5144", 13);
            }

            if (this.Result.KeepOptions.HasFlag(Game.KeepOptions.AvernusKilled) == true)
            {
                SetPlotFlag(plotList, "2F247F5F5B1C41F7845E3D09F20D5144", 17);
            }

            if (this.Result.KeepOptions.HasFlag(Game.KeepOptions.SophiaKilled) == true)
            {
                SetPlotFlag(plotList, "2F247F5F5B1C41F7845E3D09F20D5144", 2);
            }

            // Witch Hunt
            switch (this.Result.WitchResult)
            {
                case Game.WitchResult.Left:
                {
                    SetPlotFlag(plotList, "731EAE9148E94CE3B80B903A24C46E4A", 0);
                    break;
                }

                case Game.WitchResult.Followed:
                {
                    SetPlotFlag(plotList, "731EAE9148E94CE3B80B903A24C46E4A", 1);
                    break;
                }

                case Game.WitchResult.Stabbed:
                {
                    SetPlotFlag(plotList, "731EAE9148E94CE3B80B903A24C46E4A", 2);
                    break;
                }
            }

            // DLC: The Golems of Amgarrak
            if (this.Result.GolemStarted == true)
            {
                SetPlotFlag(plotList, "49F71F4764424E1C813E83651B4D3734", 0);
            }

            var gff = new GenericDataFile();
            gff.Deserialize(new MemoryStream(BinaryResources.SaveGFF));
            gff.Import(root);
            return gff;
        }

        protected void OnViewModelRequestClose(object sender, EventArgs e)
        {
            if (this.Result != null)
            {
                this.Export();
            }

            this.Close();
        }
    }
}
