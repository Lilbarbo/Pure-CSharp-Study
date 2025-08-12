using System;
using System.Collections.Generic;

public class Pessoa
{
    public string nomePessoal { get; set; }
    public int idade { get; set; }
}
class Program
{
    static void Main()
    {
        List<Pessoa> pessoas = new List<Pessoa>();

        /*Pessoa pessoa1 = new Pessoa();
        pessoa1.nomePessoal = "Daniel";
        pessoa1.idade = 22;
        pessoas.Add(pessoa1);*/

        int resultado = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("-------Bem vindo ao Banco de Dados Supremo 2-------");
            Console.WriteLine("\nO que você gostaria de fazer?");
            Console.WriteLine("Digite...");
            Console.WriteLine("\nOpção 1 - Registrar Pessoa");
            Console.WriteLine("Opção 2 - Ver pessoas registradas");
            Console.WriteLine("Opção 3 - Sair");

            resultado = int.Parse(Console.ReadLine());

            switch (resultado)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("------Registro de Pessoas-------");
                    Console.Write("\nDigite o nome da pessoa que gostaria de registrar: ");
                    string nome = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(nome))
                    {
                        Console.Write("Digite sua idade: ");
                        int idade = int.Parse(Console.ReadLine());
                        if (idade > 0)
                        {
                            pessoas.Add(new Pessoa { nomePessoal = nome, idade = idade });
                            Console.WriteLine("\nPessoa registrada com sucesso!");
                            Console.WriteLine("\nPressione qualquer tecla para retornar");
                            Console.ReadKey(true);
                        }
                        else
                        {
                            Console.WriteLine("\nIdade invalida");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNome invalido");
                    }
                    break;


                case 2:
                    Console.Clear();
                    Console.WriteLine("-------Pessoas Registradas-------");
                    Console.WriteLine("\n");

                    if (pessoas.Count == 0)
                    {
                        Console.WriteLine("Nenhuma pessoa registrada");
                    }
                    else
                    {
                        foreach (var pessoa in pessoas)
                        {
                            Console.WriteLine("Nome: " + pessoa.nomePessoal + ", Idade: " + pessoa.idade);
                        }
                    }

                    Console.WriteLine("\nPressione qualquer tecla para retornar");
                    Console.ReadKey(true);

                    break;
            }
        }
        while (resultado != 3);
    }
}


