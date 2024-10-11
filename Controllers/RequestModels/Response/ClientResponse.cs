﻿using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Controllers.RequestModels.Response
{
	public class ClientResponse
	{
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Cnp { get; set; } = "";
        public string Address { get; set; } = "";
        public ClientType Type { get; set; } = ClientType.Individual;
    }
}
