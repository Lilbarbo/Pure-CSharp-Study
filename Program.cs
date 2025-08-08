using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        List<string> pessoas = new List<string>();
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("\n-----Bem vindo ao banco de dados supremo-----");
            Console.WriteLine("Digite sua opção:");
            Console.WriteLine("\nOpção 0 - Registrar nome");
            Console.WriteLine("Opção 1 - Ver nomes registradas");
            Console.WriteLine("Opção 2 - Apagar nome registrado");
            Console.WriteLine("Opção 3 - Sair");

            int escolha = int.Parse(Console.ReadLine());
            opcao = escolha;

            switch (opcao)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("\nDigite o nome da pessoa: ");
                    string nome = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(nome))
                    {
                        pessoas.Add(nome);
                        Console.WriteLine(nome + " registrado com sucesso");
                    }
                    else
                    {
                        Console.WriteLine("Nome invalido");
                    }
                    break;

                case 1:
                    Console.Clear();
                    Console.WriteLine("\n------Nomes registradas------");
                    if (pessoas.Count == 0)
                    {
                        Console.WriteLine("Nenhuma pessoa registrada");
                    }
                    else
                    {
                        foreach (string s in pessoas)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    Console.WriteLine("\nPressione qualquer tecla para retornar");
                    Console.ReadLine();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("\n--------Nomes registrados---------");
                    if(pessoas.Count == 0)
                    {
                        Console.WriteLine("\nNenhum nome registrado");
                    }
                    else
                    {
                        foreach(string s in pessoas)
                        {
                            Console.WriteLine($"{s}");
                        }
                        Console.WriteLine("\nQual nome você gostaria de apagar?");
                        string nomeParaApagar = Console.ReadLine();
                        bool pessoaApagada = pessoas.Remove(nomeParaApagar);

                        if (pessoaApagada)
                        {
                            Console.WriteLine($"\n{nomeParaApagar} apagado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Nome não encontrado");
                        }

                        Console.WriteLine("\nPressione qualquer tecla para retornar");
                        Console.ReadLine();
                    }
                    break;


                 case 3:
                    Console.Clear();
                    Console.WriteLine("Encerrando");
                            break;


                default:
                    Console.WriteLine("Resposta invalida");
                    Console.WriteLine("\nPressione qualquer tecla para retornar");
                    Console.ReadLine();
                    break;
            }


        }
        while (opcao != 3);
    }

}


