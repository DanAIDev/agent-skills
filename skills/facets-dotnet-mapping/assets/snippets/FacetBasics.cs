using System;
using System.Collections.Generic;

// Sample entities (source types)
public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Order> Orders { get; set; } = new();
}

public class Order
{
    public Guid Id { get; set; }
    public decimal Total { get; set; }
}

// Facet DTOs (generated via source generator)
// Ensure the Facet attributes are available in your project.
[Facet(Of = typeof(Order), Include = new[] { "Id", "Total" })]
public partial record OrderFacet;

[Facet(
    Of = typeof(Customer),
    Include = new[] { "Id", "Name", "Orders" },
    NestedFacets = new[] { typeof(OrderFacet) }
)]
public partial record CustomerFacet;

// Usage examples (pseudo-code):
// var dto = customer.ToFacet<CustomerFacet>();
// var dtos = customers.AsQueryable().SelectFacet<CustomerFacet>();
