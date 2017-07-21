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
    public class ParagonPageViewModel : BasePageViewModel
    {
        private ReadOnlyCollection<OptionViewModel<Game.ParagonAnvilResult>> _AnvilResults;
        private ReadOnlyCollection<OptionViewModel<Game.ParagonKingResult>> _KingResults;

        public ParagonPageViewModel(Game.Plot plot)
            : base(plot)
        {
        }

        public ReadOnlyCollection<OptionViewModel<Game.ParagonAnvilResult>> AnvilResults
        {
            get
            {
                if (this._AnvilResults == null)
                {
                    this.CreateAnvilResults();
                }

                return this._AnvilResults;
            }
        }

        private void CreateAnvilResults()
        {
            var list = new List<OptionViewModel<Game.ParagonAnvilResult>>();
            list.Add(new OptionViewModel<Game.ParagonAnvilResult>(
                         Strings.ParagonAnvilResult_None,
                         null,
                         Game.ParagonAnvilResult.None,
                         0));
            list.Add(new OptionViewModel<Game.ParagonAnvilResult>(
                         Strings.ParagonAnvilResult_Caridin,
                         null,
                         Game.ParagonAnvilResult.Caridin,
                         1));
            list.Add(new OptionViewModel<Game.ParagonAnvilResult>(
                         Strings.ParagonAnvilResult_BrankaAlive,
                         null,
                         Game.ParagonAnvilResult.BrankaAlive,
                         2));
            list.Add(new OptionViewModel<Game.ParagonAnvilResult>(
                         Strings.ParagonAnvilResult_BrankaSuicided,
                         null,
                         Game.ParagonAnvilResult.BrankaSuicided,
                         3));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.ParagonAnvilResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnAnvilResultChanged;
            }

            list.Sort();

            this._AnvilResults = new ReadOnlyCollection<OptionViewModel<Game.ParagonAnvilResult>>(list);
        }

        protected void OnAnvilResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.ParagonAnvilResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.ParagonAnvilResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.ParagonKingResult>> KingResults
        {
            get
            {
                if (this._KingResults == null)
                {
                    this.CreateKingResults();
                }

                return this._KingResults;
            }
        }

        private void CreateKingResults()
        {
            var list = new List<OptionViewModel<Game.ParagonKingResult>>();
            list.Add(new OptionViewModel<Game.ParagonKingResult>(
                         Strings.ParagonKingResult_None,
                         null,
                         Game.ParagonKingResult.None,
                         0));
            list.Add(new OptionViewModel<Game.ParagonKingResult>(
                         Strings.ParagonKingResult_KingIsBhelen,
                         null,
                         Game.ParagonKingResult.KingIsBhelen,
                         1));
            list.Add(new OptionViewModel<Game.ParagonKingResult>(
                         Strings.ParagonKingResult_KingIsHarrowmont,
                         null,
                         Game.ParagonKingResult.KingIsHarrowmont,
                         2));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.ParagonKingResult)
                {
                    option.IsSelected = true;
                }

                option.PropertyChanged += this.OnKingResultChanged;
            }

            list.Sort();

            this._KingResults = new ReadOnlyCollection<OptionViewModel<Game.ParagonKingResult>>(list);
        }

        protected void OnKingResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.ParagonKingResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.ParagonKingResult = option.GetValue();
            }
        }

        public override string DisplayName
        {
            get { return Strings.PageDisplayName_Paragon; }
        }

        internal override bool IsValid()
        {
            return true;
        }
    }
}
