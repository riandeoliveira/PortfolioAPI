using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PortfolioAPI.Entities;

namespace PortfolioAPI.Seeds;

public class UserSeed : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var user1 = new User
        (
            "Rian Oliveira",
            "Rian Dias de Oliveira",
            "Desenvolvedor Web",
            "Olá, me chamo Rian, e é um prazer ver você por aqui! Desde pequeno, sempre fui fascinado por ciência e tecnologia, e hoje tenho a oportunidade de exercer minha criatividade fazendo o que tanto amo. Nos últimos 2 anos, venho desenvolvendo e aprimorando minhas habilidades em programação. Possuo uma ótima experiência com tecnologias tanto da parte de front-end quanto de back-end, algumas delas são: NodeJS; ExpressJS; React; NextJS; TypeScript; entre outras. Apesar do meu foco estar concentrado em front-end, estou sempre aberto a novas oportunidades para continuar aprendendo cada vez mais.",
            "https://i.imgur.com/4K3bKDa.png",
            "riandias2016"
        );

        builder.HasData(user1);
    }
}
