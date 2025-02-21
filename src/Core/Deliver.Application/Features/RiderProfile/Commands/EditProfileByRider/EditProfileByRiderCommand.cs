using MediatR;

namespace Deliver.Application.Features.RiderProfile.Commands.EditProfileByRider;

public class EditProfileByRiderCommand : IRequest<bool>
{
    public required int UserId { get; set; }
    public required int ProfileId { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Phone { get; set; } = string.Empty;
    public string? ProfileImage { get; set; } = null;
}