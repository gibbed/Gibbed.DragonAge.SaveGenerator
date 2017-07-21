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
    public class BeastPageViewModel : BasePageViewModel
    {
        private ReadOnlyCollection<OptionViewModel<Game.BeastResult>> _Results;
        private ReadOnlyCollection<OptionViewModel<Game.BeastOptions>> _Options;

        public BeastPageViewModel(Game.Plot plot)
            : base(plot)
        {
        }

        public ReadOnlyCollection<OptionViewModel<Game.BeastResult>> Results
        {
            get
            {
                if (this._Results == null)
                {
                    this.CreateResults();
                }

                return this._Results;
            }
        }

        private void CreateResults()
        {
            var list = new List<OptionViewModel<Game.BeastResult>>();
            list.Add(new OptionViewModel<Game.BeastResult>(Strings.BeastResult_None, null, Game.BeastResult.None, 0));
            list.Add(new OptionViewModel<Game.BeastResult>(Strings.BeastResult_SidedWithElves,
                                                           null,
                                                           Game.BeastResult.SidedWithElves,
                                                           1));
            list.Add(new OptionViewModel<Game.BeastResult>(Strings.BeastResult_SidedWithWerewolves,
                                                           null,
                                                           Game.BeastResult.SidedWithWerewolves,
                                                           2));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.BeastResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnResultChanged;
            }

            list.Sort();

            this._Results = new ReadOnlyCollection<OptionViewModel<Game.BeastResult>>(list);
        }

        protected void OnResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.BeastResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.BeastResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.BeastOptions>> Options
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
            var list = new List<OptionViewModel<Game.BeastOptions>>();
            list.Add(new OptionViewModel<Game.BeastOptions>(
                         Strings.BeastOptions_ZathrianSacrified,
                         null,
                         Game.BeastOptions.ZathrianSacrified,
                         0));

            foreach (var option in list)
            {
                if (this.Plot.BeastOptions.HasFlag(option.GetValue()) == true)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnOptionChanged;
            }

            list.Sort();

            this._Options = new ReadOnlyCollection<OptionViewModel<Game.BeastOptions>>(list);
        }

        protected void OnOptionChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.BeastOptions>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.BeastOptions |= option.GetValue();
            }
            else
            {
                this.Plot.BeastOptions &= ~option.GetValue();
            }
        }

        public override string DisplayName
        {
            get { return Strings.PageDisplayName_Beast; }
        }

        internal override bool IsValid()
        {
            return true;
        }
    }
}
