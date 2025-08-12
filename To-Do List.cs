using System;
using System.Collections.Generic;

public class Task
{
    public string name {  get; set; }
    public string description { get; set; }
    public bool resolvida { get; set; } 

}

class Program
{
    static void Main()
    {
        List<Task> tasks = new List<Task>();
        int valor = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("-------TO-DO LIST-------");
            Console.WriteLine("\nPressione 1 para adicionar uma nova task");
            Console.WriteLine("Pressione 2 para ver todas suas tasks para hoje");
            Console.WriteLine("Pressione 3 para sair");
            int.TryParse(Console.ReadLine(), out valor);

            switch (valor)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("-------Adicionar uma Task-------");
                    Console.Write("\nDigite o nome de sua nova task: ");
                    string nomeTask = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(nomeTask))
                    {
                        Console.Write("Descreva sua taks (opcional): ");
                        string descricao = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(descricao))
                        {
                            descricao = "";
                        }

                        Task novaTask = new Task();
                        novaTask.name = nomeTask;
                        novaTask.description = descricao;
                        novaTask.resolvida = false; //por padrão as tasks começam não estando resolvidas

                        tasks.Add(novaTask);

                        Console.WriteLine("\nTask adicionada com sucesso!!");
                        Console.WriteLine("Pressione qualquer tecla para retornar");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Nome inválido");
                    }
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("-------Tasks para Hoje-------");
                    
                    if(tasks.Count > 0)
                    {
                        int taskIndex = 1;

                        foreach (var task in tasks)
                        {
                            Console.WriteLine("\n" + taskIndex + ": " + task.name);
                            Console.WriteLine(task.description);
                            Console.WriteLine("Taks resolvida? " + task.resolvida);
                            taskIndex++;
                        }

                        Console.WriteLine("\nPressione qualquer tecla para retornar");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("\nNenhuma taks para hoje");

                        Console.WriteLine("\nPressione qualquer tecla para retornar");
                        Console.ReadKey();
                    }

                    break;
            }

        }
        while (valor != 3);
    }
}
