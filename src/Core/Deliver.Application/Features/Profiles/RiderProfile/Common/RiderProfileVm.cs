namespace Deliver.Application.Features.Profiles.RiderProfile.Common;

public class RiderProfileVm
{
    public int UserId { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Phone { get; set; } = string.Empty;
    public string? ProfileImage { get; set; } = null;
}