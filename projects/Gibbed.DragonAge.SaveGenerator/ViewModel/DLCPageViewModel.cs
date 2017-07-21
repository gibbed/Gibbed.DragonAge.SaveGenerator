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
    public class DLCPageViewModel : BasePageViewModel
    {
        private ReadOnlyCollection<OptionViewModel<Game.ShaleResult>> _ShaleResults;
        private ReadOnlyCollection<OptionViewModel<Game.CailanResult>> _CailanResults;
        private ReadOnlyCollection<OptionViewModel<Game.KeepResult>> _KeepResults;
        private ReadOnlyCollection<OptionViewModel<Game.KeepOptions>> _KeepOptions;
        private ReadOnlyCollection<OptionViewModel<Game.WitchResult>> _WitchResults;

        public DLCPageViewModel(Game.Plot plot)
            : base(plot)
        {
        }

        public bool GolemStarted
        {
            get { return this.Plot.GolemStarted; }
            set { this.Plot.GolemStarted = value; }
        }

        public ReadOnlyCollection<OptionViewModel<Game.ShaleResult>> ShaleResults
        {
            get
            {
                if (this._ShaleResults == null)
                {
                    this.CreateShaleResults();
                }

                return this._ShaleResults;
            }
        }

        private void CreateShaleResults()
        {
            var list = new List<OptionViewModel<Game.ShaleResult>>();
            list.Add(new OptionViewModel<Game.ShaleResult>(
                         Strings.ShaleResult_None,
                         null,
                         Game.ShaleResult.None,
                         0));
            list.Add(new OptionViewModel<Game.ShaleResult>(
                         Strings.ShaleResult_Recruited,
                         null,
                         Game.ShaleResult.Recruited,
                         1));
            list.Add(new OptionViewModel<Game.ShaleResult>(
                         Strings.ShaleResult_Killed,
                         null,
                         Game.ShaleResult.Killed,
                         2));
            list.Add(new OptionViewModel<Game.ShaleResult>(
                         Strings.ShaleResult_Left,
                         null,
                         Game.ShaleResult.Left,
                         3));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.ShaleResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnShaleResultChanged;
            }

            list.Sort();

            this._ShaleResults = new ReadOnlyCollection<OptionViewModel<Game.ShaleResult>>(list);
        }

        protected void OnShaleResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.ShaleResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.ShaleResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.CailanResult>> CailanResults
        {
            get
            {
                if (this._CailanResults == null)
                {
                    this.CreateCailanResults();
                }

                return this._CailanResults;
            }
        }

        private void CreateCailanResults()
        {
            var list = new List<OptionViewModel<Game.CailanResult>>();
            list.Add(new OptionViewModel<Game.CailanResult>(
                         Strings.CailanResult_None,
                         null,
                         Game.CailanResult.None,
                         0));
            list.Add(new OptionViewModel<Game.CailanResult>(
                         Strings.CailanResult_Burned,
                         null,
                         Game.CailanResult.Burned,
                         1));
            list.Add(new OptionViewModel<Game.CailanResult>(
                         Strings.CailanResult_Darkspawn,
                         null,
                         Game.CailanResult.Darkspawn,
                         2));
            list.Add(new OptionViewModel<Game.CailanResult>(
                         Strings.CailanResult_Wolves,
                         null,
                         Game.CailanResult.Wolves,
                         3));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.CailanResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnCailanResultChanged;
            }

            list.Sort();

            this._CailanResults = new ReadOnlyCollection<OptionViewModel<Game.CailanResult>>(list);
        }

        protected void OnCailanResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.CailanResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.CailanResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.KeepResult>> KeepResults
        {
            get
            {
                if (this._KeepResults == null)
                {
                    this.CreateKeepResults();
                }

                return this._KeepResults;
            }
        }

        private void CreateKeepResults()
        {
            var list = new List<OptionViewModel<Game.KeepResult>>();
            list.Add(new OptionViewModel<Game.KeepResult>(
                         Strings.KeepResult_None,
                         null,
                         Game.KeepResult.None,
                         0));
            list.Add(new OptionViewModel<Game.KeepResult>(
                         Strings.KeepResult_AvernusDoingBadExperiments,
                         null,
                         Game.KeepResult.AvernusDoingBadExperiments,
                         1));
            list.Add(new OptionViewModel<Game.KeepResult>(
                         Strings.KeepResult_AvernusDoingGoodExperiments,
                         null,
                         Game.KeepResult.AvernusDoingGoodExperiments,
                         2));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.KeepResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnKeepResultChanged;
            }

            list.Sort();

            this._KeepResults = new ReadOnlyCollection<OptionViewModel<Game.KeepResult>>(list);
        }

        protected void OnKeepResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.KeepResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.KeepResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.KeepOptions>> KeepOptions
        {
            get
            {
                if (this._KeepOptions == null)
                {
                    this.CreateKeepOptions();
                }

                return this._KeepOptions;
            }
        }

        private void CreateKeepOptions()
        {
            var list = new List<OptionViewModel<Game.KeepOptions>>();
            list.Add(new OptionViewModel<Game.KeepOptions>(
                         Strings.KeepOptions_Completed,
                         null,
                         Game.KeepOptions.Completed,
                         0));
            list.Add(new OptionViewModel<Game.KeepOptions>(
                         Strings.KeepOptions_SophiaKilled,
                         null,
                         Game.KeepOptions.SophiaKilled,
                         1));
            list.Add(new OptionViewModel<Game.KeepOptions>(
                         Strings.KeepOptions_AvernusKilled,
                         null,
                         Game.KeepOptions.AvernusKilled,
                         2));

            foreach (var option in list)
            {
                if (this.Plot.KeepOptions.HasFlag(option.GetValue()) == true)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnKeepOptionChanged;
            }

            list.Sort();

            this._KeepOptions = new ReadOnlyCollection<OptionViewModel<Game.KeepOptions>>(list);
        }

        protected void OnKeepOptionChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.KeepOptions>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.KeepOptions |= option.GetValue();
            }
            else
            {
                this.Plot.KeepOptions &= ~option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.WitchResult>> WitchResults
        {
            get
            {
                if (this._WitchResults == null)
                {
                    this.CreateWitchResults();
                }

                return this._WitchResults;
            }
        }

        private void CreateWitchResults()
        {
            var list = new List<OptionViewModel<Game.WitchResult>>();
            list.Add(new OptionViewModel<Game.WitchResult>(
                Strings.WitchResult_None, null, Game.WitchResult.None, 0));
            list.Add(new OptionViewModel<Game.WitchResult>(
                Strings.WitchResult_Left, null, Game.WitchResult.Left, 1));
            list.Add(new OptionViewModel<Game.WitchResult>(
                Strings.WitchResult_Followed,
                                                           null,
                                                           Game.WitchResult.Followed,
                                                           2));
            list.Add(new OptionViewModel<Game.WitchResult>(Strings.WitchResult_Stabbed,
                                                           null,
                                                           Game.WitchResult.Stabbed,
                                                           3));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.WitchResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnWitchResultChanged;
            }

            list.Sort();

            this._WitchResults = new ReadOnlyCollection<OptionViewModel<Game.WitchResult>>(list);
        }

        protected void OnWitchResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.WitchResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.WitchResult = option.GetValue();
            }
        }

        public override string DisplayName
        {
            get { return Strings.PageDisplayName_DLC; }
        }

        internal override bool IsValid()
        {
            return true;
        }
    }
}
