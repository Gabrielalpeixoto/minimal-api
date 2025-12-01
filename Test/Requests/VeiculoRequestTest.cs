using System.Net;
using System.Text;
using System.Text.Json;
using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class VeiculoRequestTest
{
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }
    
    [TestMethod]
    public async Task TestarGetSetPropriedades()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO{
            Nome = "Baratinha Tiggo",
            Marca = "Chery",
            Ano = 2025
        };

        var content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8,  "Application/json");

        // Act
        var response = await Setup.client.PostAsync("/veiculos", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var veiculoAdicionado = JsonSerializer.Deserialize<Veiculo>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(veiculoAdicionado?.Id);
        Assert.IsNotNull(veiculoAdicionado?.Nome);
        Assert.IsNotNull(veiculoAdicionado?.Marca);
        Assert.IsNotNull(veiculoAdicionado?.Ano);
        Console.WriteLine(veiculoAdicionado);
    }
}