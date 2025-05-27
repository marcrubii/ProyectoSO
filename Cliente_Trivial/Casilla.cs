using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial // Pedimos ayuda y vimos varios tutoriales de como construir juegos similares, es una adaptación
{
    public class Casilla
    {
		//Iniciamos los atributos
		int id;
        List<int>[] moves; 
        string categoria; 
        string color;
		int x; 
		int y;

        //Iniciamos el constructor
        public Casilla(int id, int xorigen, int yorigen)
        {
            this.id = id;
            this.moves = new List<int>[6];

            // Determinar categoria
            if (this.id == 1000)
            {
                this.categoria = "Central";
                this.color = "Central";
            }
            else
            {
                bool found = false;
                List<int> amarillas = new List<int>();
                int[] casamarillas = new int[] { 1, 10, 21, 32, 41, 100, 124, 133, 142, 151 }; //miramos cuales son las amarillas en este caso
                amarillas.AddRange(casamarillas);
                foreach (int i in amarillas)
                {
                    if (i == this.id)
                    {
                        found = true;
                        this.categoria = "Historia"; // Los colores clásicos del Trivia
                        this.color = "Amarillo";
                    }
                }
                if (found == false)
                {
                    List<int> azules = new List<int>();
                    int[] casazules = new int[] { 0, 11, 20, 22, 31, 103, 112, 121, 130, 154 }; //miramos cuales son las azules
                    azules.AddRange(casazules);
                    foreach (int i in azules)
                    {
                        if (i == this.id)
                        {
                            found = true;
                            this.categoria = "Geografia";
                            this.color = "Azul";
                        }
                    }
                    if (found == false)
                    {
                        List<int> rojas = new List<int>();
                        int[] casrojas = new int[] { 3, 14, 25, 36, 34, 114, 123, 132, 141, 150 }; //miramos cuales son las rojas
                        rojas.AddRange(casrojas);
                        foreach (int i in rojas)
                        {
                            if (i == this.id)
                            {
                                found = true;
                                this.categoria = "Tecnologia";
                                this.color = "Rojo";
                            }
                        }
                        if (found == false)
                        {
                            List<int> lilas = new List<int>();
                            int[] caslilas = new int[] { 4, 13, 15, 24, 35, 102, 111, 120, 144, 153 }; //miramos cuales son las lilas
                            lilas.AddRange(caslilas);
                            foreach (int i in lilas)
                            {
                                if (i == this.id)
                                {
                                    found = true;
                                    this.categoria = "Entretenimiento";
                                    this.color = "Lila";
                                }
                            }
                            if (found == false)
                            {
                                List<int> naranjas = new List<int>();
                                int[] casnaranjas = new int[] { 6, 8, 17, 28, 39, 101, 110, 134, 143, 152 }; //miramos cuales son las naranjas
                                naranjas.AddRange(casnaranjas);
                                foreach (int i in naranjas)
                                {
                                    if (i == this.id)
                                    {
                                        found = true;
                                        this.categoria = "Deportes";
                                        this.color = "Naranja";
                                    }
                                }
                                if (found == false)
                                {
                                    List<int> verdes = new List<int>();
                                    int[] cverde = new int[] { 7, 18, 20, 22, 29, 27, 38, 104, 113, 122, 131, 140 }; //miramos cuales son las verdes
                                    verdes.AddRange(cverde);
                                    foreach (int i in verdes)
                                    {
                                        if (i == this.id)
                                        {
                                            found = true;
                                            this.categoria = "Ciencia";
                                            this.color = "Verde";
                                        }
                                    }
                                    if (found == false) 
                                    {
                                        this.categoria = "Tira otra vez";
                                        this.color = "Gris";
                                    }
                                }
                            }
                        }
                    }
                }
            }         
            
            double x, y;
            double espesor = 45;
            double radi = 675 / 2; 
            double alpha = 8.57; // El incremento de angulo que debería haber entre casillas perimetro       
            double r04 = 2 * espesor;
            double r03 = 3 * espesor;
            double r02 = 4 * espesor;
            double r01 = 5 * espesor;
            double r00 = 6 * espesor;
            double rtotal = radi; //Radio del tablero
            if (id == 1000)
            {
                x = xorigen;
                y = yorigen;
            }
            else
            {
                double beta;
                double dist_origen;
                if ((id < 42) && (id >= 0)) // Dentro del perimetro
                {
                    dist_origen = rtotal - (rtotal - r00) / 2;
                    beta = id * alpha;
                }
                else
                {
                    int ppiso = id % 10;
                    int rriera = (id - ppiso) % 100 / 10;
                    beta = rriera * 7 * alpha;
                    if (ppiso == 0)
                        dist_origen = r00 - espesor / 2;
                    else if (ppiso == 1)
                        dist_origen = r01 - espesor / 2;
                    else if (ppiso == 2)
                        dist_origen = r02 - espesor / 2;
                    else if (ppiso == 3)
                        dist_origen = r03 - espesor / 2;
                    else
                        dist_origen = r04 - espesor / 2;
                }
                x = xorigen + dist_origen * Math.Sin(beta * Math.PI / 180);
                y = yorigen - dist_origen * Math.Cos(beta * Math.PI / 180);

            }
            this.x = Convert.ToInt32(x);
            this.y = Convert.ToInt32(y);
        }

		//Métodos
		public int GetId()
        {
			return this.id;
        }
        public string GetCategoria()
        {
            return this.categoria;
        }
        public int GetX()
        {
            return this.x;
        }
        public int GetY()
        {
            return this.y;
        }
        public List<int> GetMovimientos(int dado)
        {
			return this.moves[dado-1];
        }
		public void CalculaPosiblesMovimientos(int dado)
        {
			this.moves[dado-1] = new List<int>();
            int id = this.id;
			//Casilla forma parte del circulo exterior (ids de 0 a 41)
			if ((id>=0) && (id < 42))
            {
				
				int pos1 = id + dado;
				int pos2 = id - dado;

				//Correcciones
				if (pos1 >= 42)
					pos1 = pos1 - 42;
				if (pos2 < 0)
					pos2 = 42 + pos2;

				this.moves[dado - 1].Add(pos1);
				this.moves[dado - 1].Add(pos2);

				//Desde casilla del queso
				if ((id%7) == 0)
                {
					if (dado == 6)
						this.moves[dado - 1].Add(1000);
					else
						this.moves[dado - 1].Add(100 + (id/7)*10 + (dado-1));
                }
				//" Queso inferior"
				else if ((id%7)==1 || (id%7)==2 || (id % 7) == 3)
                {
					if (dado > (id % 7))
					{
						int n = dado - (id % 7); 
						int m = (id - (id % 7)) / 7; 

						this.moves[dado - 1].Add(100 + m * 10 + (n-1));

						if (((id%7)==2 || (id % 7) == 3) && (dado>(7-(id%7))))
                        {
							n = dado - (7 - (id % 7));
							m = (id + (7 - (id % 7))) / 7;
                            if (m >= 6)
                                m = 0;
                            this.moves[dado - 1].Add(100 + m * 10 + (n-1));
                        }
					}
                }
				//"Queso superior"
                else
                {
					if (dado > (7 - id % 7))
                    {
						int n = dado - (7-(id % 7));
						int m = (id + (7 - id % 7)) / 7;
                        if (m >= 6)
                            m = 0;

						this.moves[dado - 1].Add(100 + m * 10 + (n-1));

						if (((id%7)==4 || (id%7)==5) && (dado >((id % 7))))
                        {
							n = dado - (id % 7);
							m = (id - (7 - id % 7)) / 7;

							this.moves[dado - 1].Add(100 + m * 10 + (n-1));
                        }

                    }
                }
            }

			//Casilla central
			else if (id==1000)
            {
				int rama;
				if (dado == 6)
                {
					for(rama = 0; rama < 6; rama++)
                    {
						this.moves[dado - 1].Add(rama * 7);
                    }
                }
                else
                {
                    for (rama = 0; rama < 6; rama++)
                    {
						this.moves[dado - 1].Add(100 + rama * 10 + (5 - dado));
                    }
                }
            }

            //Casillas interiores
            else
            {
				int posRama = id % 10; 
				int rama = ((id - posRama) % 100)/10; 

				if (dado > (posRama + 1))
                {
					int pos1 = (7 * rama) + (dado - (posRama+1));
					int pos2= (7 * rama) - (dado - (posRama+1));

					//Correcciones
					if (pos1 >= 42)
						pos1 = pos1 - 42;
					if (pos2 < 0)
						pos2 = 42 + pos2;

					this.moves[dado - 1].Add(pos1);
					this.moves[dado - 1].Add(pos2);
				}
				else if (dado == (posRama + 1))
                {
					this.moves[dado - 1].Add(7 * rama);
                }
                else
                {
					this.moves[dado - 1].Add(100 + rama * 10 + (posRama - dado));
                }

				if (dado < 5-posRama)
                {
					this.moves[dado - 1].Add(100 + 10 * rama + (dado + posRama));
                }
				else if (dado == 5 - posRama)
                {
					this.moves[dado - 1].Add(1000);
                }
                else
                {
					int n = dado - (5 - posRama); 
					int m = rama + 1;
					if (m > 5)
						m = 0;
					while (m != rama)
                    {
						int a = 100 + m * 10 + (5 - n);
						this.moves[dado - 1].Add(a);
						m++;
						if (m > 5)
							m = 0;
                    }
                }
            }
        }
    }
}
