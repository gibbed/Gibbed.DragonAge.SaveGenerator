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
    public class AwakeningPageViewModel : BasePageViewModel
    {
        private ReadOnlyCollection<OptionViewModel<Game.AwakeningArchitectResult>> _ArchitectResults;
        private ReadOnlyCollection<OptionViewModel<Game.AwakeningDefenseResult>> _DefenseResults;
        private ReadOnlyCollection<OptionViewModel<Game.AwakeningOptions>> _Options;

        public AwakeningPageViewModel(Game.Plot plot)
            : base(plot)
        {
        }

        public ReadOnlyCollection<OptionViewModel<Game.AwakeningArchitectResult>> ArchitectResults
        {
            get
            {
                if (this._ArchitectResults == null)
                {
                    this.CreateArchitectResults();
                }

                return this._ArchitectResults;
            }
        }

        private void CreateArchitectResults()
        {
            var list = new List<OptionViewModel<Game.AwakeningArchitectResult>>();
            list.Add(new OptionViewModel<Game.AwakeningArchitectResult>(
                         Strings.AwakeningArchitectResult_None,
                         null,
                         Game.AwakeningArchitectResult.None,
                         0));
            list.Add(new OptionViewModel<Game.AwakeningArchitectResult>(
                         Strings.AwakeningArchitectResult_KilledArchitect,
                         null,
                         Game.AwakeningArchitectResult.KilledArchitect,
                         1));
            list.Add(new OptionViewModel<Game.AwakeningArchitectResult>(
                         Strings.AwakeningArchitectResult_DealWithArchitect,
                         null,
                         Game.AwakeningArchitectResult.DealWithArchitect,
                         2));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.AwakeningArchitectResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnArchitectResultChanged;
            }

            list.Sort();

            this._ArchitectResults = new ReadOnlyCollection<OptionViewModel<Game.AwakeningArchitectResult>>(list);
        }

        protected void OnArchitectResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.AwakeningArchitectResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.AwakeningArchitectResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.AwakeningDefenseResult>> DefenseResults
        {
            get
            {
                if (this._DefenseResults == null)
                {
                    this.CreateDefenseResults();
                }

                return this._DefenseResults;
            }
        }

        private void CreateDefenseResults()
        {
            var list = new List<OptionViewModel<Game.AwakeningDefenseResult>>();
            list.Add(new OptionViewModel<Game.AwakeningDefenseResult>(
                         Strings.AwakeningDefenseResult_None,
                         null,
                         Game.AwakeningDefenseResult.None,
                         0));
            list.Add(new OptionViewModel<Game.AwakeningDefenseResult>(
                         Strings.AwakeningDefenseResult_Roads,
                         null,
                         Game.AwakeningDefenseResult.Roads,
                         1));
            list.Add(new OptionViewModel<Game.AwakeningDefenseResult>(
                         Strings.AwakeningDefenseResult_Farms,
                         null,
                         Game.AwakeningDefenseResult.Farms,
                         2));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.AwakeningDefenseResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnDefenseResultChanged;
            }

            list.Sort();

            this._DefenseResults = new ReadOnlyCollection<OptionViewModel<Game.AwakeningDefenseResult>>(list);
        }

        protected void OnDefenseResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.AwakeningDefenseResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.AwakeningDefenseResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.AwakeningOptions>> Options
        {
            get
            {
                if (this._Options == null)
                {
                    this.CreateOptions();
                }

                return this._Options;
            }
        }

        private void CreateOptions()
        {
            var list = new List<OptionViewModel<Game.AwakeningOptions>>();
            list.Add(new OptionViewModel<Game.AwakeningOptions>(
                         Strings.AwakeningOptions_Orlesian,
                         null,
                         Game.AwakeningOptions.Orlesian,
                         0));
            list.Add(new OptionViewModel<Game.AwakeningOptions>(
                         Strings.AwakeningOptions_AndersRecruited,
                         null,
                         Game.AwakeningOptions.AndersRecruited,
                         1));
            list.Add(new OptionViewModel<Game.AwakeningOptions>(
                         Strings.AwakeningOptions_NathanielRecruited,
                         null,
                         Game.AwakeningOptions.NathanielRecruited,
                         2));
            list.Add(new OptionViewModel<Game.AwakeningOptions>(
                         Strings.AwakeningOptions_NathanielFriendly,
                         null,
                         Game.AwakeningOptions.NathanielFriendly,
                         3));
            list.Add(new OptionViewModel<Game.AwakeningOptions>(
                         Strings.AwakeningOptions_AmaranthineSaved,
                         null,
                         Game.AwakeningOptions.AmaranthineSaved,
                         4));
            list.Add(new OptionViewModel<Game.AwakeningOptions>(
                         Strings.AwakeningOptions_HerrenCompletedSilverite,
                         null,
                         Game.AwakeningOptions.HerrenCompletedSilverite,
                         5));
            list.Add(new OptionViewModel<Game.AwakeningOptions>(
                         Strings.AwakeningOptions_AndersDiedInSiege,
                         null,
                         Game.AwakeningOptions.AndersDiedInSiege,
                         6));
            list.Add(new OptionViewModel<Game.AwakeningOptions>(
                         Strings.AwakeningOptions_NathanielDiedInSiege,
                         null,
                         Game.AwakeningOptions.NathanielDiedInSiege,
                         7));
            list.Add(new OptionViewModel<Game.AwakeningOptions>(
                         Strings.AwakeningOptions_VigilsKeepSiegeCompleted,
                         null,
                         Game.AwakeningOptions.VigilsKeepSiegeCompleted,
                         8));

            foreach (var option in list)
            {
                if (this.Plot.AwakeningOptions.HasFlag(option.GetValue()) == true)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnOptionChanged;
            }

            list.Sort();

            this._Options = new ReadOnlyCollection<OptionViewModel<Game.AwakeningOptions>>(list);
        }

        protected void OnOptionChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.AwakeningOptions>;
            if (option.IsSelected == true)
            {
                this.Plot.AwakeningOptions |= option.GetValue();
            }
            else
            {
                this.Plot.AwakeningOptions &= ~option.GetValue();
            }
        }

        public override string DisplayName
        {
            get { return Strings.PageDisplayName_Awakening; }
        }

        internal override bool IsValid()
        {
            return true;
        }
    }
}
