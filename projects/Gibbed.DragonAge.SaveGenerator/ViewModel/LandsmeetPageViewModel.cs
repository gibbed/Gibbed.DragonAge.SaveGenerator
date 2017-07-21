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
    public class LandsmeetPageViewModel : BasePageViewModel
    {
        private ReadOnlyCollection<OptionViewModel<Game.LandsmeetKingResult>> _KingResults;
        private ReadOnlyCollection<OptionViewModel<Game.LandsmeetAlistairResult>> _AlistairResults;
        private ReadOnlyCollection<OptionViewModel<Game.LandsmeetLoghainResult>> _LoghainResults;

        public LandsmeetPageViewModel(Game.Plot plot)
            : base(plot)
        {
        }

        public ReadOnlyCollection<OptionViewModel<Game.LandsmeetKingResult>> KingResults
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
            var list = new List<OptionViewModel<Game.LandsmeetKingResult>>();
            list.Add(new OptionViewModel<Game.LandsmeetKingResult>(
                         Strings.LandsmeetKingResult_None,
                         null,
                         Game.LandsmeetKingResult.None,
                         0));
            list.Add(new OptionViewModel<Game.LandsmeetKingResult>(
                         Strings.LandsmeetKingResult_AlistairIsKing,
                         null,
                         Game.LandsmeetKingResult.AlistairIsKing,
                         1));
            list.Add(new OptionViewModel<Game.LandsmeetKingResult>(
                         Strings.LandsmeetKingResult_AlistairEngagedToAnora,
                         null,
                         Game.LandsmeetKingResult.AlistairEngagedToAnora,
                         2));
            list.Add(new OptionViewModel<Game.LandsmeetKingResult>(
                         Strings.LandsmeetKingResult_AlistairEngagedToPlayer,
                         null,
                         Game.LandsmeetKingResult.AlistairEngagedToPlayer,
                         3));
            list.Add(new OptionViewModel<Game.LandsmeetKingResult>(
                         Strings.LandsmeetKingResult_AnoraIsQueen,
                         null,
                         Game.LandsmeetKingResult.AnoraIsQueen,
                         4));
            list.Add(new OptionViewModel<Game.LandsmeetKingResult>(
                         Strings.LandsmeetKingResult_PlayerIsKing,
                         null,
                         Game.LandsmeetKingResult.PlayerIsKing,
                         5));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.LandsmeetKingResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnKingResultChanged;
            }

            list.Sort();

            this._KingResults = new ReadOnlyCollection<OptionViewModel<Game.LandsmeetKingResult>>(list);
        }

        protected void OnKingResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.LandsmeetKingResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.LandsmeetKingResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.LandsmeetAlistairResult>> AlistairResults
        {
            get
            {
                if (this._AlistairResults == null)
                {
                    this.CreateAlistairResult();
                }

                return this._AlistairResults;
            }
        }

        private void CreateAlistairResult()
        {
            var list = new List<OptionViewModel<Game.LandsmeetAlistairResult>>();
            list.Add(new OptionViewModel<Game.LandsmeetAlistairResult>(
                         Strings.LandsmeetAlistairResult_None,
                         null,
                         Game.LandsmeetAlistairResult.None,
                         0));
            list.Add(new OptionViewModel<Game.LandsmeetAlistairResult>(
                         Strings.LandsmeetAlistairResult_Executed,
                         null,
                         Game.LandsmeetAlistairResult.Executed,
                         1));
            list.Add(new OptionViewModel<Game.LandsmeetAlistairResult>(
                         Strings.LandsmeetAlistairResult_Drunkard,
                         null,
                         Game.LandsmeetAlistairResult.Drunkard,
                         2));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.LandsmeetAlistairResult)
                {
                    option.IsSelected = true;
                }

                option.PropertyChanged += this.OnAlistairResultChanged;
            }

            list.Sort();

            this._AlistairResults = new ReadOnlyCollection<OptionViewModel<Game.LandsmeetAlistairResult>>(list);
        }

        protected void OnAlistairResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.LandsmeetAlistairResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.LandsmeetAlistairResult = option.GetValue();
            }
        }

        public ReadOnlyCollection<OptionViewModel<Game.LandsmeetLoghainResult>> LoghainResults
        {
            get
            {
                if (this._LoghainResults == null)
                {
                    this.CreateLoghainResult();
                }

                return this._LoghainResults;
            }
        }

        private void CreateLoghainResult()
        {
            var list = new List<OptionViewModel<Game.LandsmeetLoghainResult>>();
            list.Add(new OptionViewModel<Game.LandsmeetLoghainResult>(
                         Strings.LandsmeetLoghainResult_None,
                         null,
                         Game.LandsmeetLoghainResult.None,
                         0));
            list.Add(new OptionViewModel<Game.LandsmeetLoghainResult>(
                         Strings.LandsmeetLoghainResult_Executed,
                         null,
                         Game.LandsmeetLoghainResult.Executed,
                         1));
            list.Add(new OptionViewModel<Game.LandsmeetLoghainResult>(
                         Strings.LandsmeetLoghainResult_Warden,
                         null,
                         Game.LandsmeetLoghainResult.Warden,
                         2));

            foreach (var option in list)
            {
                if (option.GetValue() == this.Plot.LandsmeetLoghainResult)
                {
                    option.IsSelected = true;
                }
                option.PropertyChanged += this.OnLoghainResultChanged;
            }

            list.Sort();

            this._LoghainResults = new ReadOnlyCollection<OptionViewModel<Game.LandsmeetLoghainResult>>(list);
        }

        protected void OnLoghainResultChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as OptionViewModel<Game.LandsmeetLoghainResult>;
            if (option == null)
            {
                return;
            }
            if (option.IsSelected == true)
            {
                this.Plot.LandsmeetLoghainResult = option.GetValue();
            }
        }

        public override string DisplayName
        {
            get { return Strings.PageDisplayName_Landsmeet; }
        }

        internal override bool IsValid()
        {
            return true;
        }
    }
}
