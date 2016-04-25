using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WSAD_App1.Models.Data;


namespace WSAD_App1.Models.ViewModels.SessionSearch
{
    [DataContract]
    public class SessionSearchViewModel
    {
        public SessionSearchViewModel(Session sessionDTO)
        {
            Id = sessionDTO.Id;
            Title = sessionDTO.Title;
            Presenter = sessionDTO.Presenter;
            Description = sessionDTO.Description;
        }
        [DataMember]
        public string Description { get; private set; }
        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        public string Presenter { get; private set; }
        [DataMember]
        public string Title { get; private set; }
    }
}