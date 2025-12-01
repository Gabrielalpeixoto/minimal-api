using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.DTOs;

namespace Test.Mocks;

public class VeiculoServicoMock : IVeiculoServico
{
    private static List<Veiculo> veiculos = new List<Veiculo>(){
        new Veiculo{
            Id = 1,
            Nome = "Baratinha Tiggo",
            Marca = "Chery",
            Ano = 2025
        },
        new Veiculo{
            Id = 2,
            Nome = "Baratona Tiggo",
            Marca = "Chery",
            Ano = 2024
        },
    };

    public Veiculo? BuscaPorId(int id)
    {
        return veiculos.Find(a => a.Id == id);
    }

    public void Incluir(Veiculo veiculo)
    {
        veiculo.Id = veiculos.Count() + 1;
        veiculos.Add(veiculo);
    }

    public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
    {
        veiculos.Where(v => !string.IsNullOrWhiteSpace(marca) && marca.Contains(v.Marca) || string.IsNullOrWhiteSpace(marca));
        veiculos.Where(v => !string.IsNullOrWhiteSpace(nome) && nome.Contains(v.Nome) || string.IsNullOrWhiteSpace(nome));
        veiculos.Skip(((int)pagina - 1) * 10).Take(10);
        return veiculos;
    }

    public void Apagar(Veiculo veiculo)    
    {
        veiculos.Remove(veiculo);
    }

     public void Atualizar(Veiculo veiculo)
    {
        var index = veiculos.FindIndex(a => a.Id == veiculo.Id);
        veiculos[index].Ano = veiculo.Ano;
        veiculos[index].Marca = veiculo.Marca;
        veiculos[index].Nome = veiculo.Nome;       
    }
}