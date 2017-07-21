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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Gibbed.DragonAge.SaveGenerator.ViewModel
{
    public class WizardViewModel : INotifyPropertyChanged
    {
        private RelayCommand _CancelCommand;
        private Game.Plot _Plot;
        private BasePageViewModel _CurrentPage;
        private RelayCommand _MoveNextCommand;
        private RelayCommand _MovePreviousCommand;
        private ReadOnlyCollection<BasePageViewModel> _Pages;

        public WizardViewModel()
        {
            this._Plot = new Game.Plot();
            this.CurrentPage = this.Pages[0];
        }

        public ICommand CancelCommand
        {
            get
            {
                if (this._CancelCommand == null)
                {
                    this._CancelCommand =
                        new RelayCommand(() => this.CancelGeneration());
                }

                return this._CancelCommand;
            }
        }

        protected void CancelGeneration()
        {
            this._Plot = null;
            this.OnRequestClose();
        }

        public ICommand MovePreviousCommand
        {
            get
            {
                if (this._MovePreviousCommand == null)
                {
                    this._MovePreviousCommand = new RelayCommand(
                        () => this.MoveToPreviousPage(),
                        () => this.CanMoveToPreviousPage);
                }

                return this._MovePreviousCommand;
            }
        }

        protected bool CanMoveToPreviousPage
        {
            get { return 0 < this.CurrentPageIndex; }
        }

        protected void MoveToPreviousPage()
        {
            if (this.CanMoveToPreviousPage)
            {
                this.CurrentPage = this.Pages[this.CurrentPageIndex - 1];
            }
        }

        public ICommand MoveNextCommand
        {
            get
            {
                if (this._MoveNextCommand == null)
                {
                    this._MoveNextCommand = new RelayCommand(
                        () => this.MoveToNextPage(),
                        () => this.CanMoveToNextPage);
                }

                return this._MoveNextCommand;
            }
        }

        protected bool CanMoveToNextPage
        {
            get
            {
                return
                    this.CurrentPage != null &&
                    this.CurrentPage.IsValid();
            }
        }

        protected void MoveToNextPage()
        {
            if (this.CanMoveToNextPage)
            {
                if (this.CurrentPageIndex < this.Pages.Count - 1)
                {
                    this.CurrentPage = this.Pages[this.CurrentPageIndex + 1];
                }
                else
                {
                    this.OnRequestClose();
                }
            }
        }

        public Game.Plot Plot
        {
            get { return this._Plot; }
        }

        public BasePageViewModel CurrentPage
        {
            get { return this._CurrentPage; }
            private set
            {
                if (value == this._CurrentPage)
                {
                    return;
                }

                if (this._CurrentPage != null)
                {
                    this._CurrentPage.IsCurrentPage = false;
                }

                this._CurrentPage = value;

                if (this._CurrentPage != null)
                {
                    this._CurrentPage.IsCurrentPage = true;
                }

                this.OnPropertyChanged("CurrentPage");
                this.OnPropertyChanged("IsOnLastPage");
            }
        }

        public bool IsOnLastPage
        {
            get { return this.CurrentPageIndex == this.Pages.Count - 1; }
        }

        public ReadOnlyCollection<BasePageViewModel> Pages
        {
            get
            {
                if (this._Pages == null)
                {
                    this.CreatePages();
                }

                return this._Pages;
            }
        }

        public event EventHandler RequestClose;

        protected void CreatePages()
        {
            var pages = new List<BasePageViewModel>();
            pages.Add(new PlayerPageViewModel(this.Plot));
            pages.Add(new CirclePageViewModel(this.Plot));
            pages.Add(new ArlPageViewModel(this.Plot));
            pages.Add(new BeastPageViewModel(this.Plot));
            pages.Add(new UrnPageViewModel(this.Plot));
            pages.Add(new ParagonPageViewModel(this.Plot));
            pages.Add(new LandsmeetPageViewModel(this.Plot));
            pages.Add(new ClimaxPageViewModel(this.Plot));
            pages.Add(new EpiloguePageViewModel(this.Plot));
            pages.Add(new CompanionPageViewModel(this.Plot));
            pages.Add(new DLCPageViewModel(this.Plot));
            pages.Add(new AwakeningPageViewModel(this.Plot));
            pages.Add(new SummaryPageViewModel(this.Plot));
            this._Pages = new ReadOnlyCollection<BasePageViewModel>(pages);
        }

        protected int CurrentPageIndex
        {
            get
            {
                Debug.Assert(this.CurrentPage != null);
                if (this.CurrentPage == null)
                {
                    return -1;
                }
                return this.Pages.IndexOf(this.CurrentPage);
            }
        }

        protected void OnRequestClose()
        {
            var handler = this.RequestClose;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
