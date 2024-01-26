using ReactiveUI;
using Tebbet.Services;

namespace Tebbet.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public UserService UserService;
    public ViewModelBase()
    {
        UserService = UserService.GetInstance();
    }
}
