using System.Collections.Generic;

public class Person
{
    public string Name { get; set; } = string.Empty;
    public List<Person> Reports { get; set; } = new();
    public Person? Manager { get; set; }
}

[Facet(
    typeof(Person),
    MaxDepth = 2,
    PreserveReferences = true
)]
public partial record PersonDto;

// Use MaxDepth to stop infinite loops in self-referencing graphs.
// PreserveReferences keeps track of previously mapped objects.
