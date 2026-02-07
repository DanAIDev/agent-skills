using System.Collections.Generic;

public class Address
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}

public class Company
{
    public string Name { get; set; } = string.Empty;
    public Address Address { get; set; } = new();
}

public class Employee
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Address Address { get; set; } = new();
    public Company Company { get; set; } = new();
    public List<Address> PreviousAddresses { get; set; } = new();
}

[Facet(typeof(Address))]
public partial record AddressDto;

[Facet(typeof(Company), NestedFacets = new[] { typeof(AddressDto) })]
public partial record CompanyDto;

[Facet(
    typeof(Employee),
    NestedFacets = new[] { typeof(AddressDto), typeof(CompanyDto) }
)]
public partial record EmployeeDto;

// The nested facets cover single references and collections.
// Usage examples (pseudo-code):
// var dto = employee.ToFacet<EmployeeDto>();
// var dtos = employees.SelectFacets<EmployeeDto>();
