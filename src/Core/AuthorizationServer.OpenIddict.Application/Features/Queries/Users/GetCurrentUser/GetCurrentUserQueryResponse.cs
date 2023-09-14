﻿namespace AuthorizationServer.OpenIddict.Application.Features.Queries.Users.GetCurrentUser;

public class GetCurrentUserQueryResponse
{
    public string Sex { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public DateTime BirthDate { get; set; }
    public string IdentityNumber { get; set; }
}
