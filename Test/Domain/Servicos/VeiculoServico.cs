using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.Db;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }


    [TestMethod]
    public void TestandoIncluirVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo();
        veiculo.Nome = "Baratinha Tiggo";
        veiculo.Marca = "Chery";
        veiculo.Ano = 2025;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);

        // Assert
        Assert.AreEqual(1, veiculoServico.Todos(1).Count());
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo();
        veiculo.Nome = "Baratinha Tiggo";
        veiculo.Marca = "Chery";
        veiculo.Ano = 2025;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);
        var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);

        // Assert
        Assert.AreEqual(1, veiculoDoBanco?.Id);
    }

    [TestMethod]
    public void TestandoTodos()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo();
        veiculo.Nome = "Baratinha Tiggo";
        veiculo.Marca = "Chery";
        veiculo.Ano = 2025;

        var veiculo2 = new Veiculo();
        veiculo.Nome = "Baratona Tiggo";
        veiculo.Marca = "Chery";
        veiculo.Ano = 2026;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);
        veiculoServico.Incluir(veiculo2);

        var veiculosDoBanco = veiculoServico.Todos();

        // Assert
        Assert.AreEqual(2, veiculosDoBanco.Count);
    }

    [TestMethod]
    public void TestandoAtualizar()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo();
        veiculo.Nome = "Baratinha Tiggo";
        veiculo.Marca = "Chery";
        veiculo.Ano = 2025;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);
        var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);
        veiculoDoBanco.Ano = 2020;
        veiculoServico.Atualizar(veiculoDoBanco);
        var veiculoDoBancoAtualizado = veiculoServico.BuscaPorId(veiculo.Id);
        // Assert
        Assert.AreEqual(2020, veiculoDoBancoAtualizado?.Ano);
    }

    [TestMethod]
    public void TestandoApagar()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo();
        veiculo.Nome = "Baratinha Tiggo";
        veiculo.Marca = "Chery";
        veiculo.Ano = 2025;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);
        var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);
        veiculoServico.Apagar(veiculoDoBanco);
        var VerificarveiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);

        // Assert
        Assert.AreEqual(null, VerificarveiculoDoBanco);
    }
}