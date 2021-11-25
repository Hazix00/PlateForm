namespace PlateForm.API.QueryFilters
{
    public record TicketQueryFilter
    {
        public int? Id { get; init; }
        public string TitleOrDescription { get; init; }
    }
}
