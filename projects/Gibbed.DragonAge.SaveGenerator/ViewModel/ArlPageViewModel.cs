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
    public class ArlPageViewModel : BasePageViewModel
    {
        private ReadOnlyCollection<OptionViewModel<Game.ArlSiegeOptions>> _SiegeOptions;
        private ReadOnlyCollection<OptionViewModel<Game.ArlConnorResult>> _ConnorResults;
        private ReadOnlyCollection<OptionViewModel<Game.ArlRitualResult>> _RitualResults;
        private ReadOnlyCollection<OptionViewModel<Game.ArlDemonOptions>> _DemonOptions;

        public ArlPageViewModel(Game.Plot plot)
            : base(plot)
        {
        }

        public ReadOnlyCollection<OptionViewModel<Game.ArlSiegeOptions>> SiegeOptions
        {
            get
            {
                if (this._SiegeOptions == null)
                {
                    this.CreateSiegeOptions();
                }

                return this._SiegeOptions;
            }
        }

        private void CreateSiegeOptions()
        {
            var list = new List<OptionViewModel<Game.ArlSiegeOptions>>();

            list.Add(new OptionViewModel<Game.ArlSiegeOptions>(
                         Strings.ArlSiegeOptions_Abandoned,
                         null,
                         Game.ArlSiegeOptions.Abandoned,
                         0));
            list.Add(new OptionViewModel<Game.ArlSiegeOptions>(
                         Strings.ArlSiegeOptions_Over,
                         null,
                         Game.ArlSiegeOptions.Over,
                         1));

            foreach (var option in list)
            {
                if (this.Plot.ArlSiegeOptions.HasFlag(option.GetValue()) == true)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnSiegeOptionChanged;
            }

            list.Sort();

            this._SiegeOptions = new ReadOnlyCollection<OptionViewModel<Game.ArlSiegeOptions>>(list);
        }

        protected void OnSiegeOptionChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.ArlSiegeOptions>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.ArlSiegeOptions |= option.GetValue();
            }
            else
            {
                this.Plot.ArlSiegeOptions &= ~option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.ArlConnorResult>> ConnorResults
        {
            get
            {
                if (this._ConnorResults == null)
                {
                    this.CreateConnorResults();
                }

                return this._ConnorResults;
            }
        }

        private void CreateConnorResults()
        {
            var list = new List<OptionViewModel<Game.ArlConnorResult>>();
            list.Add(new OptionViewModel<Game.ArlConnorResult>(
                         Strings.ArlConnorResult_None,
                         null,
                         Game.ArlConnorResult.None,
                         0));
            list.Add(new OptionViewModel<Game.ArlConnorResult>(
                         Strings.ArlConnorResult_ConnorFreed,
                         null,
                         Game.ArlConnorResult.ConnorFreed,
                         1));
            list.Add(new OptionViewModel<Game.ArlConnorResult>(
                         Strings.ArlConnorResult_IsoldeKilledConnor,
                         null,
                         Game.ArlConnorResult.IsoldeKilledConnor,
                         2));
            list.Add(new OptionViewModel<Game.ArlConnorResult>(
                         Strings.ArlConnorResult_IsoldeKnockedOutPCKilledConnor,
                         null,
                         Game.ArlConnorResult.IsoldeKnockedOutPCKilledConnor,
                         3));
            list.Add(new OptionViewModel<Game.ArlConnorResult>(
                         Strings.ArlConnorResult_PCKilledConnor,
                         null,
                         Game.ArlConnorResult.PCKilledConnor,
                         4));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.ArlConnorResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnConnorResultChanged;
            }

            list.Sort();

            this._ConnorResults = new ReadOnlyCollection<OptionViewModel<Game.ArlConnorResult>>(list);
        }

        protected void OnConnorResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.ArlConnorResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.ArlConnorResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.ArlRitualResult>> RitualResults
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
            var list = new List<OptionViewModel<Game.ArlRitualResult>>();
            list.Add(new OptionViewModel<Game.ArlRitualResult>(
                         Strings.ArlRitualResult_None,
                         null,
                         Game.ArlRitualResult.None,
                         0));
            list.Add(new OptionViewModel<Game.ArlRitualResult>(
                         Strings.ArlRitualResult_JowanDoesRitual,
                         null,
                         Game.ArlRitualResult.JowanDoesRitual,
                         1));
            list.Add(new OptionViewModel<Game.ArlRitualResult>(
                         Strings.ArlRitualResult_CircleDoesRitual,
                         null,
                         Game.ArlRitualResult.CircleDoesRitual,
                         1));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.ArlRitualResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnRitualResultChanged;
            }

            list.Sort();

            this._RitualResults = new ReadOnlyCollection<OptionViewModel<Game.ArlRitualResult>>(list);
        }

        protected void OnRitualResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.ArlRitualResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.ArlRitualResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.ArlDemonOptions>> DemonOptions
        {
            get
            {
                if (this._DemonOptions == null)
                {
                    this.CreateDemonOptions();
                }

                return this._DemonOptions;
            }
        }

        private void CreateDemonOptions()
        {
            var list = new List<OptionViewModel<Game.ArlDemonOptions>>();
            list.Add(new OptionViewModel<Game.ArlDemonOptions>(
                         Strings.ArlDemonResult_Intimidated,
                         null,
                         Game.ArlDemonOptions.Intimidated,
                         0));
            list.Add(new OptionViewModel<Game.ArlDemonOptions>(
                         Strings.ArlDemonResult_AcceptedOffer,
                         null,
                         Game.ArlDemonOptions.AcceptedOffer,
                         1));

            foreach (var option in list)
            {
                if (this.Plot.ArlDemonOptions.HasFlag(option.GetValue()) == true)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnDemonOptionChanged;
            }

            list.Sort();

            this._DemonOptions = new ReadOnlyCollection<OptionViewModel<Game.ArlDemonOptions>>(list);
        }

        protected void OnDemonOptionChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.ArlDemonOptions>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.ArlDemonOptions |= option.GetValue();
            }
            else
            {
                this.Plot.ArlDemonOptions &= ~option.GetValue();
            }
        }

        public override string DisplayName
        {
            get { return Strings.PageDisplayName_Arl; }
        }

        internal override bool IsValid()
        {
            return true;
        }
    }
}
