using Engine.Models;
using Engine.ViewModels;
using System;
using System.Collections.Generic;
namespace Singleton
{
    public class ViewModelHolder
    {
        private List<Viewmodels> _viewModels = new();
        private ViewModelHolder()
        {

        }
        private static readonly Lazy<ViewModelHolder> lazy = new(() => new ViewModelHolder());
        public static ViewModelHolder Instance => lazy.Value;

        public void AddViewModelToList(Clients client)
        {
            _viewModels.Add(new Viewmodels(client));
        }
        public IReadOnlyList<Viewmodels> ViewModels => _viewModels.AsReadOnly();




    }
}
