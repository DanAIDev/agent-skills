// Auto-generate CRUD DTOs for a source type
[GenerateDtos(
    typeof(User),
    Types = DtoTypes.All,
    OutputType = OutputType.Record,
    DtoSuffix = "Dto"
)]
public partial class UserDtos;
