using System;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Task
{
    public string name {  get; set; }
    public string description { get; set; }
    public bool resolvida { get; set; } 

}

class Program
{
    static List<Task> tasks = new List<Task>();

    static DateTime localNow = DateTime.Now;
    static void Main()
    {
        int valor = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("-------TO-DO LIST-------");
            Console.WriteLine("Current Local Time: " + localNow);
            Console.WriteLine("\nPressione 1 para adicionar uma nova task");
            Console.WriteLine("Pressione 2 para ver todas suas tasks para hoje");
            Console.WriteLine("Pressione 3 para sair");
            int.TryParse(Console.ReadLine(), out valor);

            switch (valor)
            {
                case 1:
                    AddTask();

                    break;

                case 2:
                    SeeAllTasks();

                    break;

                case 3:
                    QuitAplicattion();

                    break;
            }

        }
        while (valor != 3);
    }

    static void QuitAplicattion()
    {
        Console.Clear();
        Console.WriteLine("Encerrando...");
    }

    static void AddTask()
    {
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
    }

    static void SeeAllTasks()
    {
        Console.Clear();
        Console.WriteLine("-------Tasks para Hoje-------");

        if (tasks.Count > 0)
        {
            int taskIndex = 0;

            foreach (var task in tasks)
            {
                Console.WriteLine("\n" + taskIndex + ": " + task.name);
                Console.WriteLine(task.description);
                Console.WriteLine("Taks resolvida? " + task.resolvida);
                taskIndex++;
            }

            int valor;
            Console.WriteLine("\nPressione 1 para concluir uma task");
            Console.WriteLine("Pressione qualquer outra tecla para retornar");
            int.TryParse(Console.ReadLine(), out valor);
            if(valor == 1)
            {
               FinishTask();
            }

        }
        else
        {
            Console.WriteLine("\nNenhuma taks para hoje");

            Console.WriteLine("\nPressione qualquer tecla para retornar");
            Console.ReadKey();
        }
    }

    static void FinishTask()
    {
        int indexTask;
        Console.WriteLine("\nDigite o numeração da task que você deseja concluir");
        int.TryParse(Console.ReadLine(), out indexTask);

        var taskConcluida = tasks[indexTask];
        if (tasks.Count > indexTask && taskConcluida != null)
        {
            if (!taskConcluida.resolvida)
            {
                taskConcluida.resolvida = true;
                Console.WriteLine("\nTask " + taskConcluida.name + " concluída com sucesso!");
                Console.WriteLine("Pressione qualquer tecla para retornar");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nEssa task já foi concluída");
                Console.WriteLine("Pressione qualquer tecla para retornar");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("\nNumeração de task inválida");
            Console.WriteLine("Pressione qualquer tecla para retornar");
            Console.ReadKey();
        }
    }
}
