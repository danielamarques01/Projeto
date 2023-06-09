﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace ProjetoDois
{
    internal class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4 }
        static void Main(string[] args)
        {
            Carregar();
                bool escolheuSair = false;
                while (!escolheuSair)
                {
                    Console.WriteLine("Sistema de clientes - Bem vindo!");
                    Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                    int intOp = int.Parse(Console.ReadLine());

                    Menu opcao = (Menu)intOp;

                    switch (opcao)
                    {
                        case Menu.Listagem:
                            Listagem();
                            break;
                        case Menu.Adicionar:
                            Adicionar();
                            break;
                        case Menu.Remover:
                            break;
                        case Menu.Sair:
                            escolheuSair = true;
                            break;


                    }

                    Console.Clear();
                }
            }

            static void Adicionar()
            {
                Cliente cliente = new Cliente();
                Console.WriteLine("Cadastro de cliente: ");
                Console.WriteLine("Nome do cliente: ");
                cliente.nome = Console.ReadLine();
                Console.WriteLine("Email do cliente: ");
                cliente.email = Console.ReadLine();
                Console.WriteLine("CPF do cliente: ");
                cliente.cpf = Console.ReadLine();

                clientes.Add(cliente);
                Salvar();

                Console.WriteLine("Cadastro concluído, aperte enter para sair. ");
                Console.ReadLine();
            }

            static void Listagem()
            {
                if(clientes.Count > 0) //SE tem pelo menos um cliente cadastrado, mostre essa logica
            {
                Console.WriteLine("Lista de clientes: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("==========================");
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado!");
            }
            Console.WriteLine("Aperte enter para sair. ");
            Console.ReadLine();
        }

        static void Salvar()
        {
            FileStream stream = new FileStream("Clientes.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter(); 

            enconder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            try //vai tentar executar um bloco de codigo, se acontecer qualquer erro
            {
                FileStream stream = new FileStream("Clientes.dat", FileMode.OpenOrCreate);
                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<Cliente>)enconder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }

                stream.Close();
            }
            catch (Exception e) //o programa vai executar o que estiver aqui 
            {
                clientes = new List<Cliente>();
            }
           
        }
    }
}

