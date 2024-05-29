using System;
using System.Collections.Generic;

// Definição da classe Produto
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double Preco { get; set; }
    public int Estoque { get; set; }

    public Produto(int id, string nome, string descricao, double preco, int estoque)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Estoque = estoque;
    }
}

// Definição da classe Cliente
public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public Cliente(int id, string nome, string email)
    {
        Id = id;
        Nome = nome;
        Email = email;
    }
}

// Definição da classe Venda
public class Venda
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public List<Produto> Itens { get; set; }
    public double Total { get; set; }
    public DateTime Data { get; set; }

    public Venda(int id, Cliente cliente, List<Produto> itens, double total, DateTime data)
    {
        Id = id;
        Cliente = cliente;
        Itens = itens;
        Total = total;
        Data = data;
    }
}

// Classe principal do programa
public class Program
{
    static List<Produto> estoque = new List<Produto>(); // Lista para armazenar os produtos em estoque
    static List<Cliente> clientes = new List<Cliente>(); // Lista para armazenar os clientes
    static List<Venda> historicoVendas = new List<Venda>(); // Lista para armazenar o histórico de vendas
    static int proximoIdVenda = 1; // Variável para controlar o próximo ID de venda

    // Método para adicionar um produto ao estoque
    static void AdicionarProduto()
    {
        Console.WriteLine("Informe os detalhes do produto:");
        Console.Write("ID: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Descrição: ");
        string descricao = Console.ReadLine();
        Console.Write("Preço: ");
        double preco = Convert.ToDouble(Console.ReadLine());
        Console.Write("Quantidade em estoque: ");
        int quantidade = Convert.ToInt32(Console.ReadLine());

        Produto novoProduto = new Produto(id, nome, descricao, preco, quantidade);
        estoque.Add(novoProduto);
        Console.WriteLine("Produto adicionado com sucesso!");
    }

    // Método para exibir todos os produtos em estoque
    static void ExibirEstoque()
    {
        Console.WriteLine("Produtos em estoque:");
        foreach (var produto in estoque)
        {
            Console.WriteLine($"ID: {produto.Id}, Nome: {produto.Nome}, Preço: {produto.Preco}, Estoque: {produto.Estoque}");
        }
    }

    // Método para adicionar um cliente
    static void AdicionarCliente()
    {
        Console.WriteLine("Informe os detalhes do cliente:");
        Console.Write("ID: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();

        Cliente novoCliente = new Cliente(id, nome, email);
        clientes.Add(novoCliente);
        Console.WriteLine("Cliente adicionado com sucesso!");
    }

    // Método para realizar uma venda
    static void RealizarVenda()
    {
        Console.WriteLine("Informe o ID do cliente que está realizando a compra:");
        int idCliente = Convert.ToInt32(Console.ReadLine());
        Cliente cliente = clientes.Find(c => c.Id == idCliente);
        if (cliente != null)
        {
            List<Produto> itensVenda = new List<Produto>();
            double totalVenda = 0;

            Console.WriteLine("Adicione os produtos à venda (digite 0 para finalizar):");
            while (true)
            {
                Console.Write("ID do produto: ");
                int idProduto = Convert.ToInt32(Console.ReadLine());
                if (idProduto == 0)
                    break;

                Produto produto = estoque.Find(p => p.Id == idProduto);
                if (produto != null)
                {
                    Console.Write("Quantidade: ");
                    int quantidade = Convert.ToInt32(Console.ReadLine());
                    if (quantidade <= produto.Estoque)
                    {
                        itensVenda.Add(produto);
                        totalVenda += produto.Preco * quantidade;
                        produto.Estoque -= quantidade;
                    }
                    else
                    {
                        Console.WriteLine("Quantidade insuficiente em estoque!");
                    }
                }
                else
                {
                    Console.WriteLine("Produto não encontrado!");
                }
            }

            Venda novaVenda = new Venda(proximoIdVenda, cliente, itensVenda, totalVenda, DateTime.Now);
            historicoVendas.Add(novaVenda);
            proximoIdVenda++;

            Console.WriteLine("Venda realizada com sucesso!");
        }
        else
        {
            Console.WriteLine("Cliente não encontrado!");
        }
    }

    // Método para exibir o histórico de vendas
    static void ExibirHistoricoVendas()
    {
        Console.WriteLine("Histórico de vendas:");
        foreach (var venda in historicoVendas)
        {
            Console.WriteLine($"ID da Venda: {venda.Id}, Cliente: {venda.Cliente.Nome}, Data: {venda.Data}, Total: {venda.Total}");
            Console.WriteLine("Itens:");
            foreach (var item in venda.Itens)
            {
                Console.WriteLine($" - Produto: {item.Nome}, Quantidade: 1, Preço Unitário: {item.Preco}");
            }
        }
    }

    // Método principal
    static void Main(string[] args)
    {
        bool executando = true;
        while (executando)
        {
            Console.WriteLine("\nSelecione uma opção:");
            Console.WriteLine("1. Adicionar Produto");
            Console.WriteLine("2. Exibir Estoque");
            Console.WriteLine("3. Adicionar Cliente");
            Console.WriteLine("4. Realizar Venda");
            Console.WriteLine("5. Exibir Histórico de Vendas");
            Console.WriteLine("6. Sair");
            Console.Write("Opção: ");
            int opcao = Convert.ToInt32(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    AdicionarProduto();
                    break;
                case 2:
                    ExibirEstoque();
                    break;
                case 3:
                    AdicionarCliente();
                    break;
                case 4:
                    RealizarVenda();
                    break;
                case 5:
                    ExibirHistoricoVendas();
                    break;
                case 6:
                    executando = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }
}

