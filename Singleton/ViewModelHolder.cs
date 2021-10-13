using Engine.Models;
using Engine.ViewModels;
using System;
using System.Collections.Generic;
namespace Singleton
{
    /// <summary>
    /// Holds all viewmodels
    /// </summary>
    public class ViewModelHolder
    {
        private List<Viewmodels> _viewModels = new();
        private ViewModelHolder()
        {

        }
        
        private static readonly Lazy<ViewModelHolder> lazy = new(() => new ViewModelHolder());
        /// <summary>
        /// Provides lazy instantiation for ViewModelHolder
        /// </summary>
        public static ViewModelHolder Instance => lazy.Value;
        /// <summary>
        /// Adds a viewmodel to the viewmodel holder.
        /// </summary>
        /// <param name="client"></param>
        public void AddViewModelToList(Clients client)
        {
            _viewModels.Add(new Viewmodels(client));
        }
        /// <summary>
        /// Read only list for ViewModels
        /// </summary>
        public IReadOnlyList<Viewmodels> ViewModels => _viewModels.AsReadOnly();




    }
}
