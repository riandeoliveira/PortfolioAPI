namespace AspNetTemplate.Domain.Messages;

public abstract class Messages_PT_BR
{
    public const string AccessTokenIsRequired = "O 'token de acesso' deve ser informado.";
    public const string AvatarUrlIsRequired = "A 'url do avatar' deve ser informada.";

    public const string DescriptionIsRequired = "A 'descrição' deve ser informada.";

    public const string EmailAlreadyExists = "Este 'e-mail' já está sendo usado.";
    public const string EmailIsRequired = "O 'e-mail' deve ser informado.";
    public const string EmailIsValid = "O 'e-mail' deve ser válido.";
    public const string EntityNotFound = "Nenhuma entidade encontrada.";
    public const string EquivalentPasswords = "As 'senhas' devem ser equivalentes.";

    public const string FullNameIsRequired = "O 'nome completo' deve ser informado.";

    public const string InvalidCredentials = "O 'e-mail' ou 'senha' são inválidos.";

    public const string MaximumAvatarUrlLength = "A 'url do avatar' deve possuir no máximo 512 caracteres.";
    public const string MaximumDescriptionLength = "A 'descrição' deve possuir no máximo 1024 caracteres.";
    public const string MaximumEmailLength = "O 'e-mail' deve possuir no máximo 64 caracteres.";
    public const string MaximumFullNameLength = "O 'nome completo' deve possuir no máximo 128 caracteres.";
    public const string MaximumNameLength = "O 'nome' deve possuir no máximo 64 caracteres.";
    public const string MaximumPageNumberLength = "O 'número da página' deve ser menor ou igual a 100.";
    public const string MaximumPageSizeLength = "O 'tamanho da página' deve ser menor ou igual a 100.";
    public const string MaximumPasswordLength = "A 'senha' deve possuir no máximo 64 caracteres.";
    public const string MaximumPositionLength = "O 'cargo' deve possuir no máximo 64 caracteres.";
    public const string MaximumSpotifyAccountNameLength = "O 'nome da conta do Spotify' deve possuir no máximo 64 caracteres.";
    public const string MinimumEmailLength = "O 'e-mail' deve possuir no mínimo 8 caracteres.";
    public const string MinimumPasswordLength = "A 'senha' deve possuir no mínimo 8 caracteres.";

    public const string NameIsRequired = "O 'nome' deve ser informado.";

    public const string PasswordIsRequired = "A 'senha' deve ser informada.";
    public const string PasswordResetRequest = "Solicitação de redefinição de senha";
    public const string PositionIsRequired = "O 'cargo' deve ser informado.";

    public const string StrongPassword = "A 'senha' deve possuir pelo menos: uma letra maiúscula e um número.";

    public const string TooManyRequests = "Muitas solicitações. Tente novamente mais tarde.";

    public const string UnauthorizedOperation = "Você não possui permissão para realizar esta ação.";
    public const string UnexpectedRequestError = "Um erro ocorreu ao processar sua solicitação.";
    public const string UserNotFound = "Nenhum 'usuário' encontrado.";
}
