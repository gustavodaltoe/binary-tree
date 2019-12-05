using System;
using System.Collections.Generic;

namespace ExercArvore
{
	// Classe com o nó
	public class Node
	{
		private int info;
		private Node esq, dir, pai;

		// Construtor
		public Node (int x, Node p)
		{
			info = x;
			pai = p;
			esq = null;
			dir = null;
		}

		// Properties
		public int Info
		{
			get
			{
				return info;
			}
			set
			{
				info = value;
			}
		}
		public Node Esq
		{
			get
			{
				return esq;
			}
			set
			{
				esq = value;
			}
		}
		public Node Dir
		{
			get
			{
				return dir;
			}
			set
			{
				dir = value;
			}
		}
		public Node Pai
		{
			get
			{
				return pai;
			}
			set
			{
				pai = value;
			}
		}
	}
	
	// Classe com a árvore
	public class BTree
	{
		// Nó raiz
		private Node raiz;
		public Node Raiz
		{
			get
			{
				return raiz;
			}
			set
			{
				raiz = value;
			}
		}

		// Construtor
		public BTree()
		{
			raiz = null;
		}

		// Inserindo na árvore
		public void Insert(int x)
		{
            if (x == -1)
            {
                raiz = new Node(6, null);
                insert(raiz, 4);
                insert(raiz, 8);
                insert(raiz, 2);
                insert(raiz, 5);
                insert(raiz, 10);
                return;
            }
            if (raiz == null)
                raiz = new Node(x, null);
            else
                insert(raiz, x);
		}

        private void insert(Node n, int x)
        {
            if (x < n.Info)
            {
                if (n.Esq == null)
                {
                    n.Esq = new Node(x, n);
                    avl(n);
                }
                else
                    insert(n.Esq, x);
            }
            else
            {
                if (n.Dir == null)
                {
                    n.Dir = new Node(x, n);
                    avl(n);
                }
                else
                    insert(n.Dir, x);
            }
        }

		// Pesquisa na árvore
		public Node Find(int x)
		{
			return find(raiz, x);
		}

        private Node find(Node n, int x)
        {
            if (n == null || n.Info == x)
                return n;
            if (x < n.Info)
                return find(n.Esq, x);
            else
                return find(n.Dir, x);
        }

        // Função para excluir nó
        public void Remove(int x)
        {
            remove(raiz, x);
        }

        private void remove(Node n, int x)
		{
            Node f = find(n, x);
            if (f == null)
                return;
            if (f.Esq == null)
            {
                if (f.Pai == null)
                    raiz = f.Dir;
                else
                {
                    if (f.Pai.Dir == f)
                        f.Pai.Dir = f.Dir;
                    else
                        f.Pai.Esq = f.Dir;
                    if (f.Dir != null)
                        f.Dir.Pai = f.Pai;
                    avl(f.Pai);
                }
            }
            else
            {
                if (f.Dir == null)
                {
                    if (f.Pai == null)
                        raiz = f.Esq;
                    else
                    {
                        if (f.Pai.Dir == f)
                            f.Pai.Dir = f.Esq;
                        else
                            f.Pai.Esq = f.Esq;
                        if (f.Esq != null)
                            f.Esq.Pai = f.Pai;
                        avl(f.Pai);
                    }
                }
                else
                {
                    Node p = killmin(f.Dir);
                    f.Info = p.Info;
                    if (p.Pai != null)
                        avl(p.Pai);
                }
                if (raiz != null)
                    raiz.Pai = null;
            }
        }

        private Node killmin(Node n)
        {
            if (n.Esq == null)
            {
                Node t = n;
                if (n.Pai.Dir == n)
                    n.Pai.Dir = n.Dir;
                else
                    n.Pai.Esq = n.Dir;
                if (n.Dir != null)
                    n.Dir.Pai = n.Pai;
                return t;
            }
            else
                return killmin(n.Esq);
        }

        public List<Node> InOrder()
        {
            List<Node> ordered = new List<Node>();
            return inOrder(raiz, ordered);
        }

        private List<Node> inOrder(Node t, List<Node> list)
        {
            if (t.Esq != null)
                inOrder(t.Esq, list);
            list.Add(t);
            if (t.Dir != null)
                inOrder(t.Dir, list);
            return list;
        }

        public List<Node> PreOrder()
        {
            List<Node> ordered = new List<Node>();
            return preOrder(raiz, ordered);
        }

        private List<Node> preOrder(Node t, List<Node> list)
        {
            list.Add(t);
            if (t.Esq != null)
                inOrder(t.Esq, list);
            if (t.Dir != null)
                inOrder(t.Dir, list);
            return list;
        }

        public List<Node> PosOrder()
        {
            List<Node> ordered = new List<Node>();
            return posOrder(raiz, ordered);
        }

        private List<Node> posOrder(Node t, List<Node> list)
        {
            if (t.Esq != null)
                inOrder(t.Esq, list);
            if (t.Dir != null)
                inOrder(t.Dir, list);
            list.Add(t);
            return list;
        }

        public List<Node> InLevel()
        {
            List<Node> list = new List<Node>();
            Fila f = new Fila(100);
            f.Insert(raiz);
            while(!f.IsFilaVazia())
            {
                Node n = (Node)f.Remove();
                list.Add(n);
                if (n.Esq != null)
                    f.Insert(n.Esq);
                if (n.Dir != null)
                    f.Insert(n.Dir);
            }
            return list;
        }

        private void avl(Node n)
        {
            int fb = calcBalance(n);
            int fbEsq = 0, fbDir = 0;

            if (n.Esq != null)
                fbEsq = calcBalance(n.Esq);
            if (n.Dir != null)
                fbDir = calcBalance(n.Dir);

            if (fb == 2)
            {
                // SUBARVORE ESQUERDA
                // caso 1
                // fb com fbEsq tem o msm sinal
                if (fbEsq >= 0)
                {
                    rotacaoDireita(n);
                }
                // caso 2
                // fb com fbEsq tem sinal oposto
                else
                {
                    rotacaoEsquerda(n.Esq);
                    rotacaoDireita(n);
                }
            }
            else if (fb == -2)
            { 
                // SUBARVORE DIRETIA
                // caso 1
                // fb com fbDir tem o msm sinal
                if (fbDir < 0)
                {
                    rotacaoEsquerda(n);
                }
                // caso 2
                // fb com fbdDir tem sinal oposto
                else
                {
                    rotacaoDireita(n.Dir);
                    rotacaoEsquerda(n);
                }
            }

            if (n != raiz)
                avl(n.Pai);
        }

        private int calcBalance(Node n)
        {
            int he = 0, hd = 0;

            if (n.Esq != null)
                he = calcHeight(n.Esq);
            if (n.Dir != null)
                hd = calcHeight(n.Dir);

            return he - hd;
        }

        private int calcHeight(Node n)
        {
            int leftHeight = 0;
            int rightHeight = 0;

            if (n.Esq != null || n.Dir != null)
            {
                if (n.Esq != null)
                    leftHeight += calcHeight(n.Esq);
                if (n.Dir != null)
                    rightHeight += calcHeight(n.Dir);
            }
            else
            {
                return 1;
            }

            if (leftHeight > rightHeight)
                return leftHeight + 1;
            else
                return rightHeight + 1;
        }

        private void rotacaoDireita(Node p)
        {
            Node q, temp;
            q = p.Esq;
            if (p == raiz)
            {
                raiz = q;
                p.Pai = q;
                q.Pai = null;
            }
            else
            {
                q.Pai = p.Pai;
                p.Pai = q;
                if (q.Pai.Esq == p)
                    q.Pai.Esq = q;
                else
                    q.Pai.Dir = q;
            }
            temp = q.Dir;
            q.Dir = p;
            p.Esq = temp;
        }

        private void rotacaoEsquerda(Node p)
        {
            Node q, temp;
            q = p.Dir;
            if (p == raiz)
            {
                raiz = q;
                p.Pai = q;
                q.Pai = null;
            }
            else
            {
                q.Pai = p.Pai;
                p.Pai = q;
                if (q.Pai.Esq == p)
                    q.Pai.Esq = q;
                else
                    q.Pai.Dir = q;
            }
            temp = q.Esq;
            q.Esq = p;
            p.Dir = temp;
        }

    }
}
