using System.Diagnostics;
using System;

public interface IState //"Contrato". "Todo estado tem essas funções" . Obriga que qualquer classe que queria implementar um IState que utilize aqueles métodos
{
    void Enter();
    IState Tick();
    void Exit();
}

public sealed class MaquinaDeEstados 
{
   private IState currentState;

    public void SetState(IState next) //função que faz a troca de estados; se o estado recebido for diferente do atual, a função Exit() do estado atual é chamada e logo em seguida a função Enter() do novo estado
    {
        currentState?.Exit(); //.? ~= if(currentState != null)
        currentState = next;
        currentState?.Enter();
    }

    public void Tick() //Será utilizada como um Update() da Unity, chamada a cada frame verificando se alguma ação foi feita para alterar o estado
    {
        if(currentState == null)
        {
            return;
        }

        var proximo = currentState.Tick();
        if(proximo != null && proximo != currentState)
        {
            SetState(proximo);
        }
    }
}

public class IdleState : IState
{
    public void Enter()
    {
        Console.WriteLine("Entrou no estado: Idle");
    }

    public IState Tick()
    {
        Console.WriteLine("Idle... Aguardando movimento");

        Console.WriteLine("Pressione W para andar ou Enter para continuar parado");
        var input = Console.ReadLine();

        if(input?.ToLower() == "w")
        {
            return new WalkState();
        }

        return null;
    }

    public void Exit()
    {
        Console.WriteLine("Saindo do estado: Idle");
    }
}

public class WalkState : IState
{
    public void Enter()
    {
        Console.WriteLine("Entrando no estado: Walk");
    }

    public IState Tick()
    {
        Console.WriteLine("Walk... Aguardando comando");

        Console.WriteLine("Pressione S para parar ou Enter para continuar andando");
        var input = Console.ReadLine();

        if(input?.ToLower() == "s")
        {
            return new IdleState();
        }

        return null;
    }

    public void Exit()
    {
        Console.WriteLine("Saindo do estado: Walk");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var machine = new MaquinaDeEstados();
        machine.SetState(new IdleState());

        while (true)
        {
            Console.Clear();
            machine.Tick();
        }
    }
}