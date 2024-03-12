using System.CommandLine;

namespace Portfolio.Scripts
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Option<string> targetOption = new(
                name: "--target",
                description: "The target to use."
            );

            Option<string> nameOption = new(
                name: "--name",
                description: "The name to use."
            );

            Option<string> entityOption = new(
                name: "--entity",
                description: "The entity to use."
            );

            RootCommand rootCommand = new("Sample app for System.CommandLine");

            rootCommand.AddOption(targetOption);
            rootCommand.AddOption(nameOption);
            rootCommand.AddOption(entityOption);

            rootCommand.SetHandler((target, name, entity) =>
                ProcessCommand(target!, name!, entity!),
                targetOption,
                nameOption,
                entityOption
            );

            return await rootCommand.InvokeAsync(args);
        }

        static void ProcessCommand(string target, string name, string entity)
        {
            // Construindo o caminho da pasta
            string folderPath = Path.Combine("..", "src", target, "Features", name);

            // Verificando se a pasta já existe, caso contrário, criando
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Construindo o caminho do primeiro arquivo
            string filePathHandler = Path.Combine(folderPath, $"{name}{entity}Handler.cs");

            // Substituindo os placeholders pelos valores fornecidos para o primeiro arquivo
            string fileContentHandler = $@"using Portfolio.Utils.Extensions;
using Portfolio.Utils.Messaging;

namespace {target}.Features.{name};

public sealed class {name}{entity}Handler(
    {name}{entity}Validator validator
) : IRequestHandler<{name}{entity}Request, {name}{entity}Response>
{{
    public async Task<{name}{entity}Response> Handle({name}{entity}Request request, CancellationToken cancellationToken = default)
    {{
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        // handler logic...

        return new {name}{entity}Response();
    }}
}}
";

            // Criando o primeiro arquivo com o conteúdo substituído
            File.WriteAllText(filePathHandler, fileContentHandler);

            Console.WriteLine($"Arquivo criado: {filePathHandler}");

            // Construindo o caminho do segundo arquivo
            string filePathRequest = Path.Combine(folderPath, $"{name}{entity}Request.cs");

            // Substituindo os placeholders pelos valores fornecidos para o segundo arquivo
            string fileContentRequest = $@"using Portfolio.Utils.Messaging;

namespace {target}.Features.{name};

public sealed record {name}{entity}Request : IRequest<{name}{entity}Response>;
";

            // Criando o segundo arquivo com o conteúdo substituído
            File.WriteAllText(filePathRequest, fileContentRequest);

            Console.WriteLine($"Arquivo criado: {filePathRequest}");

            // Construindo o caminho do terceiro arquivo
            string filePathResponse = Path.Combine(folderPath, $"{name}{entity}Response.cs");

            // Substituindo os placeholders pelos valores fornecidos para o terceiro arquivo
            string fileContentResponse = $@"namespace {target}.Features.{name};

public sealed record {name}{entity}Response;
";

            // Criando o terceiro arquivo com o conteúdo substituído
            File.WriteAllText(filePathResponse, fileContentResponse);

            Console.WriteLine($"Arquivo criado: {filePathResponse}");

            // Construindo o caminho do quarto arquivo
            string filePathValidator = Path.Combine(folderPath, $"{name}{entity}Validator.cs");

            // Substituindo os placeholders pelos valores fornecidos para o quarto arquivo
            string fileContentValidator = $@"using FluentValidation;

using Portfolio.Utils.Interfaces;

namespace {target}.Features.{name};

public sealed class {name}{entity}Validator : AbstractValidator<{name}{entity}Request>
{{
    public {name}{entity}Validator(ILocalizationService localizationService)
    {{
        // validation rules...
    }}
}}
";

            // Criando o quarto arquivo com o conteúdo substituído
            File.WriteAllText(filePathValidator, fileContentValidator);

            Console.WriteLine($"Arquivo criado: {filePathValidator}");
        }
    }
}
