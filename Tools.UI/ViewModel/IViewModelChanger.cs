using System.Threading.Tasks;

namespace Tools.UI.ViewModel
{
    public interface IViewModelSetter
    {
        Task SetCurrentViewModelAsync(PageViewModel viewModel);
    }
}
