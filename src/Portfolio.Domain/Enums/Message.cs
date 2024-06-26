namespace Portfolio.Domain.Enums;

public enum Message
{
    AccessTokenIsRequired,
    AuthorNotFound,
    AvatarUrlIsRequired,
    DescriptionIsRequired,

    EmailAlreadyExists,
    EmailIsNotRegistered,
    EmailIsRequired,

    EntityNotFound,
    EquivalentPasswords,
    FullNameIsRequired,

    InvalidEmail,
    InvalidLoginCredentials,

    MaximumAvatarUrlLength,
    MaximumDescriptionLength,
    MaximumEmailLength,
    MaximumFullNameLength,
    MaximumNameLength,
    MaximumPageNumberLength,
    MaximumPageSizeLength,
    MaximumPasswordLength,
    MaximumPositionLength,
    MaximumSpotifyAccountNameLength,

    MinimumEmailLength,
    MinimumPasswordLength,

    NameIsRequired,

    PasswordIsRequired,
    PasswordResetRequest,

    PositionIsRequired,
    StrongPassword,
    UnauthorizedOperation,
    UserNotFound,
}
