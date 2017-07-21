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

using System.ComponentModel;

namespace Gibbed.DragonAge.SaveGenerator.ViewModel
{
    public abstract class BasePageViewModel : INotifyPropertyChanged
    {
        private readonly Game.Plot _Plot;
        private bool _IsCurrentPage;

        protected BasePageViewModel(Game.Plot plot)
        {
            this._Plot = plot;
        }

        public Game.Plot Plot
        {
            get { return this._Plot; }
        }

        public abstract string DisplayName { get; }
        public bool IsCurrentPage
        {
            get { return this._IsCurrentPage; }
            set
            {
                if (value == this._IsCurrentPage)
                {
                    return;
                }

                this._IsCurrentPage = value;
                this.OnPropertyChanged("IsCurrentPage");
            }
        }

        internal abstract bool IsValid();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
