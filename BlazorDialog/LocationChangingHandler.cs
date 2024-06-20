using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public class LocationChangingHandler : ILocationChangingHandler, IDisposable
    {
        private readonly NavigationManager _navigationManager;
        private IDisposable? _currentRegistration;
        private List<Dialog> _dialogsStack = new List<Dialog>();

        public LocationChangingHandler(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void RegisterLocationChangingHandler(Dialog dialog)
        {
            if (!_dialogsStack.Contains(dialog))
            {
                _dialogsStack.Add(dialog);
            }

            if(_currentRegistration == null)
            {
                _currentRegistration = _navigationManager.RegisterLocationChangingHandler(OnLocationChanging);
            }
        }

        private ValueTask OnLocationChanging(LocationChangingContext context)
        {
            if (!context.IsNavigationIntercepted && _dialogsStack.Any(x => x.PreventNavigation))
            {
                context.PreventNavigation();
            }
            return ValueTask.CompletedTask;
        }

        public void RemoveLocationChangingHandler(Dialog dialog)
        {
            if (_dialogsStack.Contains(dialog))
            {
                _dialogsStack.Remove(dialog);
            }

            // if there are no dialogs left in the stack, we can remove the location changing handler
            if (_currentRegistration != null && _dialogsStack.Count == 0)
            {
                _currentRegistration.Dispose();
                _currentRegistration = null;
            }
        }

        public void Dispose()
        {
            if(_currentRegistration != null)
            {
               _currentRegistration.Dispose();
                _currentRegistration = null;
            }
        }
    }
}
