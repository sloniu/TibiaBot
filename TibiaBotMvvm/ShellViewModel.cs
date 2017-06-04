using System.Collections.Generic;
using Caliburn.Micro;
using TibiaBotMvvm.ViewModels;

namespace TibiaBotMvvm {
    public class ShellViewModel : Conductor<IMainScreenTabItem>.Collection.OneActive, IShell
    {
        public ShellViewModel(IEnumerable<IMainScreenTabItem> tabs)
        {
            Items.AddRange(tabs);
        }
    }
}