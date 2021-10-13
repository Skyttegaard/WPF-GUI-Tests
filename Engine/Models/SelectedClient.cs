using System;
using System.Collections.Generic;

namespace Engine.Models
{
    public class SelectedClient : BaseNotificationClass
    {
        private Clients _client;
        public Clients Client
        {
            get => _client;
            set
            {
                _client = value;
                OnPropertyChanged();
            }
        }
        public List<Clients> ClientList { get; set; }
        private static readonly Lazy<SelectedClient> lazy = new(() => new SelectedClient());
        private SelectedClient()
        {

        }
        /// <summary>
        /// Provides lazy instantiation for SelectedClient.
        /// </summary>
        public static SelectedClient Instance => lazy.Value;



    }
}
