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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Gibbed.DragonAge.SaveGenerator.Resources;

namespace Gibbed.DragonAge.SaveGenerator.ViewModel
{
    public class CompanionPageViewModel : BasePageViewModel
    {
        private ReadOnlyCollection<OptionViewModel<Game.FollowerOptions>> _FollowerOptions;
        private ReadOnlyCollection<OptionViewModel<Game.RomanceOptions>> _RomanceOptions;
        private ReadOnlyCollection<OptionViewModel<Game.IsabellaResult>> _IsabellaResults;
        private ReadOnlyCollection<OptionViewModel<Game.ZevranResult>> _ZevranResults;

        public CompanionPageViewModel(Game.Plot plot)
            : base(plot)
        {
        }

        public ReadOnlyCollection<OptionViewModel<Game.FollowerOptions>> FollowerOptions
        {
            get
            {
                if (this._FollowerOptions == null)
                {
                    this.CreateFollowerOptions();
                }

                return this._FollowerOptions;
            }
        }

        private void CreateFollowerOptions()
        {
            var list = new List<OptionViewModel<Game.FollowerOptions>>();
            list.Add(new OptionViewModel<Game.FollowerOptions>(
                         Strings.FollowerOptions_DogRecruited,
                         null,
                         Game.FollowerOptions.DogRecruited,
                         0));
            list.Add(new OptionViewModel<Game.FollowerOptions>(
                         Strings.FollowerOptions_LelianaRecruited,
                         null,
                         Game.FollowerOptions.LelianaRecruited,
                         1));
            list.Add(new OptionViewModel<Game.FollowerOptions>(
                         Strings.FollowerOptions_ZevranRecruited,
                         null,
                         Game.FollowerOptions.ZevranRecruited,
                         2));

            foreach (var option in list)
            {
                if (this.Plot.FollowerOptions.HasFlag(option.GetValue()) == true)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnFollowerOptionChanged;
            }

            list.Sort();

            this._FollowerOptions = new ReadOnlyCollection<OptionViewModel<Game.FollowerOptions>>(list);
        }

        protected void OnFollowerOptionChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.FollowerOptions>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.FollowerOptions |= option.GetValue();
            }
            else
            {
                this.Plot.FollowerOptions &= ~option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.RomanceOptions>> RomanceOptions
        {
            get
            {
                if (this._RomanceOptions == null)
                {
                    this.CreateRomanceOptions();
                }

                return this._RomanceOptions;
            }
        }

        private void CreateRomanceOptions()
        {
            var list = new List<OptionViewModel<Game.RomanceOptions>>();
            list.Add(new OptionViewModel<Game.RomanceOptions>(
                         Strings.RomanceOptions_AlistairRomance,
                         null,
                         Game.RomanceOptions.AlistairRomance,
                         0));
            list.Add(new OptionViewModel<Game.RomanceOptions>(
                         Strings.RomanceOptions_AlistairLove,
                         null,
                         Game.RomanceOptions.AlistairLove,
                         1));
            list.Add(new OptionViewModel<Game.RomanceOptions>(
                         Strings.RomanceOptions_LelianaRomance,
                         null,
                         Game.RomanceOptions.LelianaRomance,
                         2));
            list.Add(new OptionViewModel<Game.RomanceOptions>(
                         Strings.RomanceOptions_LelianaLove,
                         null,
                         Game.RomanceOptions.LelianaLove,
                         3));
            list.Add(new OptionViewModel<Game.RomanceOptions>(
                         Strings.RomanceOptions_MorriganRomance,
                         null,
                         Game.RomanceOptions.MorriganRomance,
                         4));
            list.Add(new OptionViewModel<Game.RomanceOptions>(
                         Strings.RomanceOptions_MorriganLove,
                         null,
                         Game.RomanceOptions.MorriganLove,
                         5));
            list.Add(new OptionViewModel<Game.RomanceOptions>(
                         Strings.RomanceOptions_ZevranRomance,
                         null,
                         Game.RomanceOptions.ZevranRomance,
                         6));
            list.Add(new OptionViewModel<Game.RomanceOptions>(
                         Strings.RomanceOptions_ZevranLove,
                         null,
                         Game.RomanceOptions.ZevranLove,
                         7));

            foreach (var option in list)
            {
                if (this.Plot.RomanceOptions.HasFlag(option.GetValue()) == true)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnRomanceOptionChanged;
            }

            list.Sort();

            this._RomanceOptions = new ReadOnlyCollection<OptionViewModel<Game.RomanceOptions>>(list);
        }

        protected void OnRomanceOptionChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.RomanceOptions>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.RomanceOptions |= option.GetValue();
            }
            else
            {
                this.Plot.RomanceOptions &= ~option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.IsabellaResult>> IsabellaResults
        {
            get
            {
                if (this._IsabellaResults == null)
                {
                    this.CreateIsabellaResults();
                }

                return this._IsabellaResults;
            }
        }

        private void CreateIsabellaResults()
        {
            var list = new List<OptionViewModel<Game.IsabellaResult>>();
            list.Add(new OptionViewModel<Game.IsabellaResult>(
                         Strings.IsabellaResult_None,
                         null,
                         Game.IsabellaResult.None,
                         0));
            list.Add(new OptionViewModel<Game.IsabellaResult>(
                         Strings.IsabellaResult_SleptWith,
                         null,
                         Game.IsabellaResult.SleptWith,
                         1));
            list.Add(new OptionViewModel<Game.IsabellaResult>(
                         Strings.IsabellaResult_IsabelaAndAlistairThreesome,
                         null,
                         Game.IsabellaResult.IsabelaAndAlistairThreesome,
                         2));
            list.Add(new OptionViewModel<Game.IsabellaResult>(
                         Strings.IsabellaResult_IsabelaAndLelianaThreesome,
                         null,
                         Game.IsabellaResult.IsabelaAndLelianaThreesome,
                         3));
            list.Add(new OptionViewModel<Game.IsabellaResult>(
                         Strings.IsabellaResult_IsabelaAndZevranThreesome,
                         null,
                         Game.IsabellaResult.IsabelaAndZevranThreesome,
                         4));
            list.Add(new OptionViewModel<Game.IsabellaResult>(
                         Strings.IsabellaResult_IsabelaInFoursome,
                         null,
                         Game.IsabellaResult.IsabelaInFoursome,
                         5));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.IsabellaResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnIsabellaResultChanged;
            }

            list.Sort();

            this._IsabellaResults = new ReadOnlyCollection<OptionViewModel<Game.IsabellaResult>>(list);
        }

        protected void OnIsabellaResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.IsabellaResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.IsabellaResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.ZevranResult>> ZevranResults
        {
            get
            {
                if (this._ZevranResults == null)
                {
                    this.CreateZevranResults();
                }

                return this._ZevranResults;
            }
        }

        private void CreateZevranResults()
        {
            var list = new List<OptionViewModel<Game.ZevranResult>>();
            list.Add(new OptionViewModel<Game.ZevranResult>(
                         Strings.ZevranResult_None,
                         null,
                         Game.ZevranResult.None,
                         0));
            list.Add(new OptionViewModel<Game.ZevranResult>(
                         Strings.ZevranResult_KilledBeforeIntroduction,
                         null,
                         Game.ZevranResult.KilledBeforeIntroduction,
                         1));
            list.Add(new OptionViewModel<Game.ZevranResult>(
                         Strings.ZevranResult_GoesHostile,
                         null,
                         Game.ZevranResult.GoesHostile,
                         2));
            list.Add(new OptionViewModel<Game.ZevranResult>(
                         Strings.ZevranResult_LeavesForGood,
                         null,
                         Game.ZevranResult.LeavesForGood,
                         3));
            list.Add(new OptionViewModel<Game.ZevranResult>(
                         Strings.ZevranResult_LeavesAfterKissingFarewell,
                         null,
                         Game.ZevranResult.LeavesAfterKissingFarewell,
                         4));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.ZevranResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnZevranResultChanged;
            }

            list.Sort();

            this._ZevranResults = new ReadOnlyCollection<OptionViewModel<Game.ZevranResult>>(list);
        }

        protected void OnZevranResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.ZevranResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.ZevranResult = option.GetValue();
            }
        }

        public override string DisplayName
        {
            get { return Strings.PageDisplayName_Companion; }
        }

        internal override bool IsValid()
        {
            return true;
        }
    }
}
