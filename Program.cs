using System;
using System.Collections.Generic;

public interface IState
{
    void Enter();
    IState Update();
    void Exit();

}

public class MaquinaDeEstados
{
    private IState currentState;

    public void SetState(IState state)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = state;

        if(currentState != null)
        {
            currentState.Enter();
        }
    }

    public void Update()
    {
        if(currentState != null)
        {
            var nextState = currentState.Update();
            if(nextState != null && nextState != currentState)
            {
                SetState(nextState);
            }
        }
        else
        {
            return;
        }
    }
}

public class Extase_State : IState
{
    public void Enter()
    {
        Console.Clear();
        Console.WriteLine("$$$$$$$VOCÊ$ESTÁ$EM$ÊXTASE$$$$$$$");
    }

    public IState Update()
    {
        Console.WriteLine("Estado atual: Êxtase");
        Console.WriteLine("Pressione R para ficar FELIZ");
        string resposta = Console.ReadLine().ToLower();

        if(resposta == "r")
        {
            return new Feliz_State();
        }

        return null;
    }

    public void Exit()
    {
        Console.WriteLine("Saindo do estado ÊXTASE");
    }
}

public class Feliz_State : IState
{
    public void Enter()
    {
        Console.Clear();
        Console.WriteLine("Você está FELIX");
    }

    public IState Update()
    {
        Console.WriteLine("Estado atual: Feliz");
        Console.WriteLine("Pressione T para ficar em ÊXTASE");
        string resposta = Console.ReadLine().ToLower();

        if (resposta == "t")
        {
            return new Extase_State();
        }

        return null;
    }

    public void Exit()
    {
        Console.WriteLine("Saindo do estado FELIZ");
    }
}

class Program
{
    static void Main()
    {
        var maquinaDeEstados = new MaquinaDeEstados();
        maquinaDeEstados.SetState(new Feliz_State());

        while (true)
        {
            maquinaDeEstados.Update();
        }
    }
}