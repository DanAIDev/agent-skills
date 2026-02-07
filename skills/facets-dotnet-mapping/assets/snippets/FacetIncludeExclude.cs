using System;
using System.Collections.Generic;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public decimal Salary { get; set; }
}

// Exclude pattern (most fields, hide sensitive ones)
[Facet(typeof(User), Exclude = new[] { "PasswordHash", "Salary" })]
public partial record UserPublicDto;

// Include pattern (only a small subset)
[Facet(typeof(User), Include = new[] { "FirstName", "LastName", "Email" })]
public partial record UserContactDto;

// Usage examples (pseudo-code):
// var dto = user.ToFacet<UserPublicDto>();
// var dtos = users.SelectFacets<UserContactDto>();
