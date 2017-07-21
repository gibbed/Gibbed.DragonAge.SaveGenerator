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
    public class ClimaxPageViewModel : BasePageViewModel
    {
        private ReadOnlyCollection<OptionViewModel<Game.ClimaxRitualResult>> _RitualResults;
        private ReadOnlyCollection<OptionViewModel<Game.ClimaxArchdemonResult>> _ArchdemonResults;

        public ClimaxPageViewModel(Game.Plot plot)
            : base(plot)
        {
        }

        public ReadOnlyCollection<OptionViewModel<Game.ClimaxRitualResult>> RitualResults
        {
            get
            {
                if (this._RitualResults == null)
                {
                    this.CreateRitualResults();
                }

                return this._RitualResults;
            }
        }

        private void CreateRitualResults()
        {
            var list = new List<OptionViewModel<Game.ClimaxRitualResult>>();
            list.Add(new OptionViewModel<Game.ClimaxRitualResult>(
                         Strings.ClimaxRitualResult_None,
                         null,
                         Game.ClimaxRitualResult.None,
                         0));
            list.Add(new OptionViewModel<Game.ClimaxRitualResult>(
                         Strings.ClimaxRitualResult_RitualWithAlistair,
                         null,
                         Game.ClimaxRitualResult.RitualWithAlistair,
                         1));
            list.Add(new OptionViewModel<Game.ClimaxRitualResult>(
                         Strings.ClimaxRitualResult_RitualWithPlayer,
                         null,
                         Game.ClimaxRitualResult.RitualWithPlayer,
                         2));
            list.Add(new OptionViewModel<Game.ClimaxRitualResult>(
                         Strings.ClimaxRitualResult_RitualWithLoghain,
                         null,
                         Game.ClimaxRitualResult.RitualWithLoghain,
                         3));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.ClimaxRitualResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnRitualResultChanged;
            }

            list.Sort();

            this._RitualResults = new ReadOnlyCollection<OptionViewModel<Game.ClimaxRitualResult>>(list);
        }

        protected void OnRitualResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.ClimaxRitualResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.ClimaxRitualResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.ClimaxArchdemonResult>> ArchdemonResults
        {
            get
            {
                if (this._ArchdemonResults == null)
                {
                    this.CreateArchdemonResults();
                }

                return this._ArchdemonResults;
            }
        }

        private void CreateArchdemonResults()
        {
            var list = new List<OptionViewModel<Game.ClimaxArchdemonResult>>();
            list.Add(new OptionViewModel<Game.ClimaxArchdemonResult>(
                         Strings.ClimaxArchdemonResult_None,
                         null,
                         Game.ClimaxArchdemonResult.None,
                         0));
            list.Add(new OptionViewModel<Game.ClimaxArchdemonResult>(
                         Strings.ClimaxArchdemonResult_AlistairKilledArchdemon,
                         null,
                         Game.ClimaxArchdemonResult.AlistairKilledArchdemon,
                         1));
            list.Add(new OptionViewModel<Game.ClimaxArchdemonResult>(
                         Strings.ClimaxArchdemonResult_PlayerKilledArchdemon,
                         null,
                         Game.ClimaxArchdemonResult.PlayerKilledArchdemon,
                         2));
            list.Add(new OptionViewModel<Game.ClimaxArchdemonResult>(
                         Strings.ClimaxArchdemonResult_LoghainKilledArchdemon,
                         null,
                         Game.ClimaxArchdemonResult.LoghainKilledArchdemon,
                         3));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.ClimaxArchdemonResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnArchdemonResultChanged;
            }

            list.Sort();

            this._ArchdemonResults = new ReadOnlyCollection<OptionViewModel<Game.ClimaxArchdemonResult>>(list);
        }

        protected void OnArchdemonResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.ClimaxArchdemonResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.ClimaxArchdemonResult = option.GetValue();
            }
        }

        public override string DisplayName
        {
            get { return Strings.PageDisplayName_Climax; }
        }

        internal override bool IsValid()
        {
            return true;
        }
    }
}
