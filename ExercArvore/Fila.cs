using System;
using System.Collections.Generic;
using System.Text;

namespace ExercArvore
{
    public class Fila
    {
        private object[] elementos;
        private int front, rear, contador, tam;

        public Fila(int tam)
        {
            this.tam = tam;
            this.front = 0;
            this.rear = 0;
            this.contador = 0;
            this.elementos = new Object[tam];
        }

        public void Insert(object el)
        {
            if (IsFilaCheia())
                throw new Exception("Fila cheia!");

            elementos[rear++] = el;
            contador++;
            if (rear >= tam)
                rear = 0;
        }

        public Object Remove()
        {
            if (IsFilaVazia())
                throw new Exception("Fila vazia!");

            Object retorno = elementos[front++];
            contador--;

            if (front >= tam)
                front = 0;

            return retorno;
        }

        public Object VerProximo()
        {
            if (IsFilaVazia())
                throw new Exception("Fila vazia!");

            return elementos[front];
        }

        public bool IsFilaCheia()
        {
            return contador == tam;
        }

        public bool IsFilaVazia()
        {
            return contador == 0;
        }

        public int GetContador()
        {
            return contador;
        }
    }
}
