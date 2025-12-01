using MinimalApi.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var veiculo = new Veiculo();

        // Act
        veiculo.Id = 1;
        veiculo.Nome = "Baratinha  Tiggo";
        veiculo.Marca = "Chery";
        veiculo.Ano = 2025;

        // Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("Baratinha  Tiggo", veiculo.Nome);
        Assert.AreEqual("Chery", veiculo.Marca);
        Assert.AreEqual(2025, veiculo.Ano);
    }
}