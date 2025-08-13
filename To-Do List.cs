using System;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.ComponentModel.Design.Serialization;

public class TodoItem
{
    public string name { get; set; }
    public string description { get; set; }
    public bool resolvida { get; set; }

}

class Program
{
    static List<TodoItem> tasks = new ();
    static string appDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TodoConsole");
    static string dataFilePath = Path.Combine(appDir, "tasks.json");

    static DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
    static void Main()
    {
        Directory.CreateDirectory(appDir);
        LoadTasks();

        int valor = 0;
        do
        {
            Console.Clear();
            Console.WriteLine("-------TO-DO LIST-------");
            Console.WriteLine("-------" + dataAtual.ToString() + "-------");
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
            bool respostaValida = false;
            Console.Clear();
            Console.WriteLine("\n-------Adicionar uma Task-------");
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

                TodoItem novaTask = new TodoItem();
                novaTask.name = nomeTask;
                novaTask.description = descricao;
                novaTask.resolvida = false; //por padrão as tasks começam não estando resolvidas

                tasks.Add(novaTask);
                SaveTasks();

                Console.WriteLine("\nTask adicionada com sucesso!!");
                Console.WriteLine("Pressione qualquer tecla para retornar");
                Console.ReadKey();

                respostaValida = true;
            }
            else
            {
                Console.WriteLine("\nNome inválido!");
                Console.WriteLine("Pressione qualquer tecla para retornar");
                Console.ReadKey();
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
                Console.WriteLine("Status: Resolvida? " + task.resolvida);
                taskIndex++;
            }

            int valor;
            Console.WriteLine("\nPressione 1 para concluir uma task");
            Console.WriteLine("Pressione 2 para apagar uma task");
            Console.WriteLine("Pressione 3 para apagar todas as tasks");
            Console.WriteLine("Pressione qualquer outra tecla para retornar");
            int.TryParse(Console.ReadLine(), out valor);
            switch (valor)
            {
                case 1:
                    FinishTask(); break;
                case 2:
                    DeleteTask(); break;
                case 3:
                    DeleteAllTasks(); break;
                default:
                    break;
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

        if (tasks.Count > indexTask && tasks[indexTask] != null)
        {
            var taskConcluida = tasks[indexTask];

            if (!taskConcluida.resolvida)
            {
                taskConcluida.resolvida = true;
                SaveTasks();
                Console.WriteLine("\nTask " + taskConcluida.name + " concluída com sucesso!");
                Console.WriteLine("Pressione qualquer tecla para retornar");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nEssa task já foi concluída");
                Console.WriteLine("Pressione qualquer tecla para retornar");
                Console.ReadKey();
                return;
            }
        }
        else
        {
            Console.WriteLine("\nNumeração de task inválida");
            Console.WriteLine("Pressione qualquer tecla para retornar");
            Console.ReadKey();
            return;
        }
    }

    static void DeleteTask()
    {
        int taskIndex;
        Console.WriteLine("\nDigite a numeração da task que você deseja deletar");
        int.TryParse(Console.ReadLine(), out taskIndex);

        if (tasks.Count > taskIndex && tasks[taskIndex] != null)
        {
            var taskDeletada = tasks[taskIndex];
            Console.WriteLine("\nTask " + taskDeletada.name + " deletada com sucesso");
            tasks.Remove(taskDeletada);
            SaveTasks();
            Console.WriteLine("Pressione qualquer tecla para retornar");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("\nNumeração de task inválida");
            Console.WriteLine("Pressione qualquer tecla para retornar");
            Console.ReadKey();
            return;
        }
    }

    static void DeleteAllTasks()
    {
        tasks.Clear();
        SaveTasks();
        Console.WriteLine("\nTodas as tasks foram deletas");
        Console.WriteLine("Pressione qualquer tecla para retornar");
        Console.ReadKey();
    }

    private static void SaveTasks()
    {
        try
        {
            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            var tempPath = dataFilePath + ".tmp";
            File.WriteAllText(tempPath, json);
            if (File.Exists(dataFilePath))
            {
                File.Delete(dataFilePath);
            }
            File.Move(tempPath, dataFilePath);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"\n[AVISO] Falha ao salvar as tasks: {ex.Message}");
        }
    }

    private static void LoadTasks()
    {
        try
        {
            if (!File.Exists(dataFilePath))
            {
                return;
            }
            var json = File.ReadAllText(dataFilePath);
            var loaded = JsonSerializer.Deserialize<List<TodoItem>>(json);
            if (loaded is { Count: > 0 })
            {
                tasks.Clear();
                tasks.AddRange(loaded);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[AVISO] Falha ao carregar as tasks: {ex.Message}\nUm novo arquivo será criado ao salvar.");
        }

    }
}
