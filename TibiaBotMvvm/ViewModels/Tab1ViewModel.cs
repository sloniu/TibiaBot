using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace TibiaBotMvvm.ViewModels
{
    public sealed class Tab1ViewModel : Screen, IMainScreenTabItem
    {
        public Tab1ViewModel()
        {
            DisplayName = "Tab1";
        }
    }
}
