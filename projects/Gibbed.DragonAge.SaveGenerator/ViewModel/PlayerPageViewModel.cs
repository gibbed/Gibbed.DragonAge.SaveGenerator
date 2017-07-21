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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Gibbed.DragonAge.SaveGenerator.Resources;

namespace Gibbed.DragonAge.SaveGenerator.ViewModel
{
    internal class PlayerPageViewModel : BasePageViewModel
    {
        private ResourceDictionary Resources;

        private ReadOnlyCollection<OptionViewModel<Game.PlayerGender>> _AvailableGenders;
        private ReadOnlyCollection<OptionViewModel<Game.PlayerRace>> _AvailableRaces;
        private ReadOnlyCollection<OptionViewModel<Game.PlayerClass>> _AvailableClasses;
        private ReadOnlyCollection<OptionViewModel<Game.PlayerBackground>> _AvailableBackgrounds;

        public PlayerPageViewModel(Game.Plot plot)
            : base(plot)
        {
            this.Resources = new ResourceDictionary();
            this.Resources.Source = new Uri("/Gibbed.DragonAge.SaveGenerator;component/ImageResources.xaml",
                                            UriKind.RelativeOrAbsolute);
        }

        public ReadOnlyCollection<OptionViewModel<Game.PlayerGender>> AvailableGenders
        {
            get
            {
                if (this._AvailableGenders == null)
                {
                    this.CreateAvailableGenders();
                }

                return this._AvailableGenders;
            }
        }

        private void CreateAvailableGenders()
        {
            var list = new List<OptionViewModel<Game.PlayerGender>>();
            list.Add(new OptionViewModel<Game.PlayerGender>(
                         Strings.Gender_Male,
                         new ImageBrush((ImageSource)this.Resources["Gender_Male"]),
                         Game.PlayerGender.Male,
                         0));
            list.Add(new OptionViewModel<Game.PlayerGender>(
                         Strings.Gender_Female,
                         new ImageBrush((ImageSource)this.Resources["Gender_Female"]),
                         Game.PlayerGender.Female,
                         1));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.PlayerGender)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnGenderChanged;
            }

            list.Sort();

            this._AvailableGenders = new ReadOnlyCollection<OptionViewModel<Game.PlayerGender>>(list);
        }

        protected void OnGenderChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.PlayerGender>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.PlayerGender = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.PlayerRace>> AvailableRaces
        {
            get
            {
                if (this._AvailableRaces == null)
                {
                    this.CreateAvailableRaces();
                }

                return this._AvailableRaces;
            }
        }

        private void CreateAvailableRaces()
        {
            var list = new List<OptionViewModel<Game.PlayerRace>>();

            list.Add(new OptionViewModel<Game.PlayerRace>(
                         Strings.Race_Human,
                         new ImageBrush((ImageSource)this.Resources["Race_Human_Male"]),
                         Game.PlayerRace.Human,
                         0));
            list.Add(new OptionViewModel<Game.PlayerRace>(
                         Strings.Race_Elf,
                         new ImageBrush((ImageSource)this.Resources["Race_Elf_Male"]),
                         Game.PlayerRace.Elf,
                         1));
            list.Add(new OptionViewModel<Game.PlayerRace>(
                         Strings.Race_Dwarf,
                         new ImageBrush((ImageSource)this.Resources["Race_Dwarf_Male"]),
                         Game.PlayerRace.Dwarf,
                         2));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.PlayerRace)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnRaceChanged;
            }

            list.Sort();

            this._AvailableRaces = new ReadOnlyCollection<OptionViewModel<Game.PlayerRace>>(list);
        }

        protected void OnRaceChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.PlayerRace>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.PlayerRace = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.PlayerClass>> AvailableClasses
        {
            get
            {
                if (this._AvailableClasses == null)
                {
                    this.CreateAvailableClasses();
                }

                return this._AvailableClasses;
            }
        }

        private void CreateAvailableClasses()
        {
            var list = new List<OptionViewModel<Game.PlayerClass>>();

            list.Add(new OptionViewModel<Game.PlayerClass>(
                         Strings.Class_Warrior,
                         new ImageBrush((ImageSource)this.Resources["Class_Warrior"]),
                         Game.PlayerClass.Warrior,
                         0));
            list.Add(new OptionViewModel<Game.PlayerClass>(
                         Strings.Class_Mage,
                         new ImageBrush((ImageSource)this.Resources["Class_Mage"]),
                         Game.PlayerClass.Mage,
                         1));
            list.Add(new OptionViewModel<Game.PlayerClass>(
                         Strings.Class_Rogue,
                         new ImageBrush((ImageSource)this.Resources["Class_Rogue"]),
                         Game.PlayerClass.Rogue,
                         2));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.PlayerClass)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnClassChanged;
            }

            list.Sort();

            this._AvailableClasses = new ReadOnlyCollection<OptionViewModel<Game.PlayerClass>>(list);
        }

        protected void OnClassChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.PlayerClass>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.PlayerClass = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.PlayerBackground>> AvailableBackgrounds
        {
            get
            {
                if (this._AvailableBackgrounds == null)
                {
                    this.CreateAvailableBackgrounds();
                }

                return this._AvailableBackgrounds;
            }
        }

        private void CreateAvailableBackgrounds()
        {
            var list = new List<OptionViewModel<Game.PlayerBackground>>();

            list.Add(new OptionViewModel<Game.PlayerBackground>(
                         Strings.Background_Elf_Dalish,
                         new ImageBrush((ImageSource)this.Resources["Background_Elf_Dalish"]),
                         Game.PlayerBackground.ElfDalish,
                         0));
            list.Add(new OptionViewModel<Game.PlayerBackground>(
                         Strings.Background_Dwarf_Commoner,
                         new ImageBrush((ImageSource)this.Resources["Background_Dwarf_Commoner"]),
                         Game.PlayerBackground.DwarfCommoner,
                         1));
            list.Add(new OptionViewModel<Game.PlayerBackground>(
                         Strings.Background_Magi,
                         new ImageBrush((ImageSource)this.Resources["Background_Magi"]),
                         Game.PlayerBackground.Magi,
                         2));
            list.Add(new OptionViewModel<Game.PlayerBackground>(
                         Strings.Background_Elf_City,
                         new ImageBrush((ImageSource)this.Resources["Background_Elf_City"]),
                         Game.PlayerBackground.ElfCity,
                         3));
            list.Add(new OptionViewModel<Game.PlayerBackground>(
                         Strings.Background_Human_Noble,
                         new ImageBrush((ImageSource)this.Resources["Background_Human_Noble"]),
                         Game.PlayerBackground.HumanNoble,
                         4));
            list.Add(new OptionViewModel<Game.PlayerBackground>(
                         Strings.Background_Dwarf_Noble,
                         new ImageBrush((ImageSource)this.Resources["Background_Dwarf_Noble"]),
                         Game.PlayerBackground.DwarfNoble,
                         5));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.PlayerBackground)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnBackgroundChanged;
            }

            list.Sort();

            this._AvailableBackgrounds = new ReadOnlyCollection<OptionViewModel<Game.PlayerBackground>>(list);
        }

        protected void OnBackgroundChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.PlayerBackground>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.PlayerBackground = option.GetValue();
            }
        }

        public string PlayerName
        {
            get { return this.Plot.PlayerName; }
            set { this.Plot.PlayerName = value; }
        }

        public override string DisplayName
        {
            get { return Strings.PageDisplayName_Player; }
        }

        internal override bool IsValid()
        {
            var plot = this.Plot;

            if (string.IsNullOrEmpty(plot.PlayerName) == true)
            {
                return false;
            }

            bool isMelee = plot.PlayerClass == Game.PlayerClass.Warrior ||
                           plot.PlayerClass == Game.PlayerClass.Rogue;
            bool isMagic = plot.PlayerClass == Game.PlayerClass.Mage;

            if (plot.PlayerRace == Game.PlayerRace.Human)
            {
                if (isMagic == true && plot.PlayerBackground == Game.PlayerBackground.Magi)
                {
                    return true;
                }

                if (isMelee == true && plot.PlayerBackground == Game.PlayerBackground.HumanNoble)
                {
                    return true;
                }

                return false;
            }

            if (plot.PlayerRace == Game.PlayerRace.Elf)
            {
                if (isMagic == true && plot.PlayerBackground == Game.PlayerBackground.Magi)
                {
                    return true;
                }

                if (isMelee == true && (plot.PlayerBackground == Game.PlayerBackground.ElfDalish ||
                                        plot.PlayerBackground == Game.PlayerBackground.ElfCity))
                {
                    return true;
                }

                return false;
            }

            if (plot.PlayerRace == Game.PlayerRace.Dwarf)
            {
                if (isMelee == true && (plot.PlayerBackground == Game.PlayerBackground.DwarfCommoner ||
                                        plot.PlayerBackground == Game.PlayerBackground.DwarfNoble))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
