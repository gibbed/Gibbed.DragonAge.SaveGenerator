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
    public class EpiloguePageViewModel : BasePageViewModel
    {
        private ReadOnlyCollection<OptionViewModel<Game.EpilogueBoonResult>> _Results;

        public EpiloguePageViewModel(Game.Plot plot)
            : base(plot)
        {
        }

        public ReadOnlyCollection<OptionViewModel<Game.EpilogueBoonResult>> Results
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
            var list = new List<OptionViewModel<Game.EpilogueBoonResult>>();
            list.Add(new OptionViewModel<Game.EpilogueBoonResult>(
                Strings.EpilogueBoonResult_None, null, Game.EpilogueBoonResult.None, 0));
            list.Add(new OptionViewModel<Game.EpilogueBoonResult>(
                Strings.EpilogueBoonResult_Chancellor, null, Game.EpilogueBoonResult.Chancellor, 1));
            list.Add(new OptionViewModel<Game.EpilogueBoonResult>(
                Strings.EpilogueBoonResult_Circle, null, Game.EpilogueBoonResult.Circle, 1));
            list.Add(new OptionViewModel<Game.EpilogueBoonResult>(
                Strings.EpilogueBoonResult_Dalish, null, Game.EpilogueBoonResult.Dalish, 1));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.EpilogueBoonResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnResultChanged;
            }

            list.Sort();

            this._Results = new ReadOnlyCollection<OptionViewModel<Game.EpilogueBoonResult>>(list);
        }

        protected void OnResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.EpilogueBoonResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.EpilogueBoonResult = option.GetValue();
            }
        }

        public override string DisplayName
        {
            get { return Strings.PageDisplayName_Epilogue; }
        }

        internal override bool IsValid()
        {
            return true;
        }
    }
}
