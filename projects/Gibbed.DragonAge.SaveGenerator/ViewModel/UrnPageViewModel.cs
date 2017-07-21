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
    public class UrnPageViewModel : BasePageViewModel
    {
        private ReadOnlyCollection<OptionViewModel<Game.UrnOptions>> _UrnOptions;
        private ReadOnlyCollection<OptionViewModel<Game.LelianaResult>> _LelianaResults;

        public UrnPageViewModel(Game.Plot plot)
            : base(plot)
        {
        }

        public ReadOnlyCollection<OptionViewModel<Game.UrnOptions>> Options
        {
            get
            {
                if (this._UrnOptions == null)
                {
                    this.CreateOptions();
                }

                return this._UrnOptions;
            }
        }

        private void CreateOptions()
        {
            var list = new List<OptionViewModel<Game.UrnOptions>>();
            list.Add(new OptionViewModel<Game.UrnOptions>(
                         Strings.UrnOptions_GenitiviReturnedToDenerim,
                         null,
                         Game.UrnOptions.GenitiviReturnedToDenerim,
                         0));

            foreach (var option in list)
            {
                if (this.Plot.UrnOptions.HasFlag(option.GetValue()) == true)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnOptionChanged;
            }

            list.Sort();

            this._UrnOptions = new ReadOnlyCollection<OptionViewModel<Game.UrnOptions>>(list);
        }

        protected void OnOptionChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.UrnOptions>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.UrnOptions |= option.GetValue();
            }
            else
            {
                this.Plot.UrnOptions &= ~option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.LelianaResult>> LelianaResults
        {
            get
            {
                if (this._LelianaResults == null)
                {
                    this.CreateLelianaResults();
                }

                return this._LelianaResults;
            }
        }

        private void CreateLelianaResults()
        {
            var list = new List<OptionViewModel<Game.LelianaResult>>();
            list.Add(new OptionViewModel<Game.LelianaResult>(
                         Strings.LelianaResult_None,
                         null,
                         Game.LelianaResult.None,
                         0));
            list.Add(new OptionViewModel<Game.LelianaResult>(
                         Strings.LelianaResult_LeftForever,
                         null,
                         Game.LelianaResult.LeftForever,
                         1));
            list.Add(new OptionViewModel<Game.LelianaResult>(
                         Strings.LelianaResult_Killed,
                         null,
                         Game.LelianaResult.Killed,
                         2));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.LelianaResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnLelianaResultChanged;
            }

            list.Sort();

            this._LelianaResults = new ReadOnlyCollection<OptionViewModel<Game.LelianaResult>>(list);
        }

        protected void OnLelianaResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.LelianaResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.LelianaResult = option.GetValue();
            }
        }

        public override string DisplayName
        {
            get { return Strings.PageDisplayName_Urn; }
        }

        internal override bool IsValid()
        {
            return true;
        }
    }
}
