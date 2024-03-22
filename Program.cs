using System;
using System.Net.NetworkInformation;
using static TicTacToe.Program;

namespace TicTacToe
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "TicTacToe";
            Console.SetWindowSize(50, 25);
            Console.Clear();
            string[] opcionesMenu = { "Jugar", "Cargar Partida", "Salir" };
            Coordenadas posicionMenu = new Coordenadas { x = 0, y = 0 };
            string opcionSeleccionada = GenerarMenuDeOpciones(opcionesMenu, posicionMenu);
            switch (opcionSeleccionada)
            {
                case "Jugar":
                    PantallaJuego();
                    break;
                case "Cargar Partida":
                    PantallaCargarPartida();
                    break;
                case "Salir":
                    Environment.Exit(0);
                    break;
            }
        }
        public static void PantallaJuego()
        {
            Console.Clear();
            //Configuracion apartado de los mensajes
            Coordenadas posicionMensaje = new Coordenadas { x = 2, y = Console.WindowHeight - 4 };
            Coordenadas posicionMenu = new Coordenadas
            {
                x = posicionMensaje.x + 1,
                y = posicionMensaje.y + 1
            };
            Coordenadas posicionPuntuacionJ1 = new Coordenadas { x = 3, y = 3 };
            Coordenadas posicionPuntuacionJ2 = new Coordenadas { x = 36, y = 3 };
            string borrador = "";
            //Configuracion del tablero
            Tablero tablero = new Tablero();
            tablero.TamañoCasilla = 5;
            tablero.TamañoTablero = (tablero.TamañoCasilla * 3) + 2;
            tablero.Simbolo = '█';
            tablero.Posicion = new Coordenadas
            {
                x = (Console.WindowWidth - tablero.TamañoTablero) / 2,
                y = 1
            };
            DibujarTablero(tablero);
            //Configuracion de los jugadores
            Jugador j1, j2;
            j1.Nombre = ObtenerNombreJugador(1, posicionMensaje);
            j2.Nombre = ObtenerNombreJugador(2, posicionMensaje);
            Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
            Console.Write("Jugador 1:");
            string opcionSeleccionada = GenerarMenuDeOpciones(new string[] { "X", "O" }, posicionMenu);
            Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
            Console.Write(borrador.PadLeft("Jugador 1:".Length, ' '));
            j1.Ficha = opcionSeleccionada;
            if (j1.Ficha == "X") j2.Ficha = "O";
            else j2.Ficha = "X";
            j1.Puntos = 0;
            j2.Puntos = 0;
            //Configuracion de la partida
            Partida partida = new Partida();
            partida.Movimientos = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            partida.Jugador1 = j1;
            partida.Jugador2 = j2;
            Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
            Console.Write("¿Quien empieza primero?");
            string turnoSeleccionado = GenerarMenuDeOpciones(new string[] { $"{j1.Nombre}", $"{j2.Nombre}" }, posicionMenu);
            Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
            Console.Write("                          ");
            if (turnoSeleccionado == j1.Nombre) partida.Turno = j1;
            else partida.Turno = j2;
            partida.Tablero = tablero;
            //Iniciar el juego
            bool jugarOtraPartida = false;
            do
            {
                Console.Clear();
                DibujarPuntuacion(partida.Jugador1, posicionPuntuacionJ1);
                DibujarPuntuacion(partida.Jugador2, posicionPuntuacionJ2);
                string ganador = IniciarJuego(partida, posicionMensaje);
                if (ganador == partida.Jugador1.Nombre) partida.Jugador1.Puntos++;
                else if (ganador == partida.Jugador2.Nombre) partida.Jugador2.Puntos++;
                DibujarPuntuacion(partida.Jugador1, posicionPuntuacionJ1);
                DibujarPuntuacion(partida.Jugador2, posicionPuntuacionJ2);
                Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
                Console.Write("¿Desea jugar otra partida?");
                if (GenerarMenuDeOpciones(new string[] { "Si", "No" }, posicionMenu) == "Si")
                {
                    Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
                    Console.Write(borrador.PadLeft("* ¿Desea jugar otra partida?".Length, ' '));

                    Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
                    Console.Write("¿Con los mismos jugadores?");
                    if (GenerarMenuDeOpciones(new string[] { "Si", "No" }, posicionMenu) == "Si")
                    {
                        jugarOtraPartida = true;
                        partida.Movimientos = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                        ActualizarMovimientos(partida.Movimientos, tablero);
                        Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y - 2);
                        Console.Write("".PadLeft(Console.WindowWidth, ' '));
                    }
                    else if (GenerarMenuDeOpciones(new string[] { "Si", "No" }, posicionMenu) == "No")
                    {
                        Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y - 2);
                        Console.Write("".PadLeft(Console.WindowWidth, ' '));
                        PantallaJuego();
                    }
                    Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
                    Console.Write(borrador.PadLeft("¿Desea jugar otra partida?".Length, ' '));
                }
            } while (jugarOtraPartida);
            Main();
        }
        public static string ObtenerNombreJugador(int numeroJugador, Coordenadas posicionMensaje)
        {
            string borrador = "";
            string nombre = "";
            do
            {
                Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
                Console.Write(borrador.PadLeft(9, ' '));
                Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y + 1);
                Console.Write(borrador.PadLeft(8 + nombre.Length, ' '));
                Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
                Console.Write($"Jugador {numeroJugador}");
                Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y + 1);
                Console.Write("Nombre: ");
                nombre = Console.ReadLine();
            } while (nombre.Length > 8 || nombre.Length == 0);
            Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
            Console.Write(borrador.PadLeft(9, ' '));
            Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y + 1);
            Console.Write(borrador.PadLeft(8 + nombre.Length, ' '));
            return nombre;
        }
        public static string IniciarJuego(Partida partida, Coordenadas posicionMensajes)
        {
            DibujarTablero(partida.Tablero);
            LimpiarTablero(partida.Tablero);
            ActualizarMovimientos(partida.Movimientos, partida.Tablero);
            bool juegoTerminado = false;
            do
            {
                int movimiento = PedirMovimiento(partida.Turno, posicionMensajes);
                if (EsValidoMovimiento(partida.Movimientos, movimiento))
                {
                    partida.Movimientos[movimiento - 1] = char.Parse(partida.Turno.Ficha);
                    ActualizarMovimientos(partida.Movimientos, partida.Tablero);
                    if (VerificarGanador(partida.Movimientos, char.Parse(partida.Turno.Ficha), partida.Tablero))
                    {
                        Console.SetCursorPosition((Console.WindowWidth - $"Ganaste {partida.Turno.Nombre}!".Length) / 2,
                            posicionMensajes.y - 2);
                        Console.Write($"Ganaste {partida.Turno.Nombre}!");
                        return partida.Turno.Nombre;
                    }
                    else if (VerificarEmpate(partida.Movimientos))
                    {
                        Console.SetCursorPosition(posicionMensajes.x, posicionMensajes.y - 1);
                        Console.Write($"Empate :C");
                        juegoTerminado = true;
                        break;
                    }
                    if (partida.Turno.Ficha == "X") partida.Turno = partida.Jugador2;
                    else partida.Turno = partida.Jugador1;
                }
            } while (!juegoTerminado);
            return "Empate";
        }
        public static bool EsValidoMovimiento(char[] movimientos, int posMovimiento)
        {
            if (movimientos[posMovimiento - 1] != 'X' && movimientos[posMovimiento - 1] != 'O') return true;
            return false;
        }
        public static int PedirMovimiento(Jugador turno, Coordenadas posicionMensaje)
        {
            string borrador = "";
            int movimiento = 1;
            do
            {
                Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
                Console.Write(borrador.PadLeft($"Turno para {turno.Ficha}: {turno.Nombre}".Length, ' '));
                Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y + 1);
                Console.Write(borrador.PadLeft(Console.WindowWidth - 1, ' '));

                Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
                Console.Write($"Turno para {turno.Ficha}: {turno.Nombre}");
                Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y + 1);
                Console.Write($"Ingrese movimiento: ");
                if (int.TryParse(Console.ReadLine(), out int temp)) movimiento = temp;
            } while (movimiento < 1 || movimiento > 9);

            Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y);
            Console.Write(borrador.PadLeft($"Turno para {turno.Ficha}: {turno.Nombre}".Length, ' '));
            Console.SetCursorPosition(posicionMensaje.x, posicionMensaje.y + 1);
            Console.Write(borrador.PadLeft(Console.WindowWidth - 1, ' '));
            return movimiento;
        }
        public static void DibujarTablero(Tablero tablero)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = tablero.Posicion.x; i < tablero.Posicion.x + tablero.TamañoTablero; i++)
            {
                Console.SetCursorPosition(i, tablero.Posicion.y + tablero.TamañoCasilla);
                Console.Write(tablero.Simbolo);
                Console.SetCursorPosition(i, tablero.Posicion.y + (tablero.TamañoCasilla * 2) + 1);
                Console.Write(tablero.Simbolo);
            }
            for (int i = tablero.Posicion.y; i < tablero.Posicion.y + tablero.TamañoTablero; i++)
            {
                Console.SetCursorPosition(tablero.Posicion.x + tablero.TamañoCasilla, i);
                Console.Write(tablero.Simbolo);
                Console.SetCursorPosition(tablero.Posicion.x + (tablero.TamañoCasilla * 2) + 1, i);
                Console.Write(tablero.Simbolo);
            }
            Console.ResetColor();
        }
        public static Coordenadas ObtenerCoordenadaCasilla(string tipo, int numeroDeCasilla, Tablero tablero)
        {
            int centroCasilla = tablero.TamañoCasilla / 2;
            switch (numeroDeCasilla)
            {
                case 1:
                    if (tipo == "Numero") return new Coordenadas
                    {
                        x = tablero.Posicion.x + centroCasilla,
                        y = tablero.Posicion.y + centroCasilla
                    };
                    else return new Coordenadas
                    {
                        x = tablero.Posicion.x + centroCasilla - 1,
                        y = tablero.Posicion.y + centroCasilla - 1
                    };
                case 2:
                    if (tipo == "Numero") return new Coordenadas
                    {
                        x = tablero.Posicion.x + tablero.TamañoCasilla + centroCasilla + 1,
                        y = tablero.Posicion.y + centroCasilla
                    };
                    else return new Coordenadas
                    {
                        x = tablero.Posicion.x + tablero.TamañoCasilla + centroCasilla,
                        y = tablero.Posicion.y + centroCasilla - 1
                    };
                case 3:
                    if (tipo == "Numero") return new Coordenadas
                    {
                        x = tablero.Posicion.x + (tablero.TamañoCasilla * 2) + centroCasilla + 2,
                        y = tablero.Posicion.y + centroCasilla
                    };
                    else return new Coordenadas
                    {
                        x = tablero.Posicion.x + (tablero.TamañoCasilla * 2) + centroCasilla + 1,
                        y = tablero.Posicion.y + centroCasilla - 1
                    };
                case 4:
                    if (tipo == "Numero") return new Coordenadas
                    {
                        x = tablero.Posicion.x + centroCasilla,
                        y = tablero.Posicion.y + tablero.TamañoCasilla + centroCasilla + 1
                    };
                    else return new Coordenadas
                    {
                        x = tablero.Posicion.x + centroCasilla - 1,
                        y = tablero.Posicion.y + tablero.TamañoCasilla + centroCasilla
                    };
                case 5:
                    if (tipo == "Numero") return new Coordenadas
                    {
                        x = tablero.Posicion.x + tablero.TamañoCasilla + centroCasilla + 1,
                        y = tablero.Posicion.y + tablero.TamañoCasilla + centroCasilla + 1
                    };
                    else return new Coordenadas
                    {
                        x = tablero.Posicion.x + tablero.TamañoCasilla + centroCasilla,
                        y = tablero.Posicion.y + tablero.TamañoCasilla + centroCasilla
                    };
                case 6:
                    if (tipo == "Numero") return new Coordenadas
                    {
                        x = tablero.Posicion.x + (tablero.TamañoCasilla * 2) + centroCasilla + 2,
                        y = tablero.Posicion.y + tablero.TamañoCasilla + centroCasilla + 1
                    };
                    else return new Coordenadas
                    {
                        x = tablero.Posicion.x + (tablero.TamañoCasilla * 2) + centroCasilla + 1,
                        y = tablero.Posicion.y + tablero.TamañoCasilla + centroCasilla
                    };
                case 7:
                    if (tipo == "Numero") return new Coordenadas
                    {
                        x = tablero.Posicion.x + centroCasilla,
                        y = tablero.Posicion.y + (tablero.TamañoCasilla * 2) + centroCasilla + 2
                    };
                    else return new Coordenadas
                    {
                        x = tablero.Posicion.x + centroCasilla - 1,
                        y = tablero.Posicion.y + (tablero.TamañoCasilla * 2) + centroCasilla + 1
                    };
                case 8:
                    if (tipo == "Numero") return new Coordenadas
                    {
                        x = tablero.Posicion.x + tablero.TamañoCasilla + centroCasilla + 1,
                        y = tablero.Posicion.y + (tablero.TamañoCasilla * 2) + centroCasilla + 2
                    };
                    else return new Coordenadas
                    {
                        x = tablero.Posicion.x + tablero.TamañoCasilla + centroCasilla,
                        y = tablero.Posicion.y + (tablero.TamañoCasilla * 2) + centroCasilla + 1
                    };
                case 9:
                    if (tipo == "Numero") return new Coordenadas
                    {
                        x = tablero.Posicion.x + (tablero.TamañoCasilla * 2) + centroCasilla + 2,
                        y = tablero.Posicion.y + (tablero.TamañoCasilla * 2) + centroCasilla + 2
                    };
                    else return new Coordenadas
                    {
                        x = tablero.Posicion.x + (tablero.TamañoCasilla * 2) + centroCasilla + 1,
                        y = tablero.Posicion.y + (tablero.TamañoCasilla * 2) + centroCasilla + 1
                    };
            }
            return new Coordenadas { x = 0, y = 0 };

        }
        public static void ActualizarMovimientos(char[] movimientos, Tablero tablero)
        {
            char[,] simboloX = {
                {'#', ' ', '#'},
                {' ', '#', ' '},
                {'#', ' ', '#'}
            };
            char[,] simboloO =
            {
                {'#', '#', '#'},
                {'#', ' ', '#'},
                {'#', '#', '#'}
            };
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < movimientos.Length; i++)
            {
                if (char.IsLetter(movimientos[i]))
                {
                    if (movimientos[i] == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Coordenadas coordenadas = ObtenerCoordenadaCasilla("Simbolo", i + 1, tablero);
                        for (int j = 0; j < simboloX.GetLength(0); j++)
                        {
                            for (int k = 0; k < simboloX.GetLength(1); k++)
                            {
                                Console.SetCursorPosition(coordenadas.x + k, coordenadas.y + j);
                                Console.Write(simboloX[j, k]);
                            }
                        }
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Coordenadas coordenadas = ObtenerCoordenadaCasilla("Simbolo", i + 1, tablero);
                        for (int j = 0; j < simboloO.GetLength(0); j++)
                        {
                            for (int k = 0; k < simboloO.GetLength(1); k++)
                            {
                                Console.SetCursorPosition(coordenadas.x + k, coordenadas.y + j);
                                Console.Write(simboloO[j, k]);
                            }
                        }
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Coordenadas coordenadas = ObtenerCoordenadaCasilla("Numero", i + 1, tablero);
                    Console.SetCursorPosition(coordenadas.x, coordenadas.y);
                    Console.Write(movimientos[i]);
                    Console.ResetColor();
                }
            }
            Console.ResetColor();
        }
        public static void LimpiarTablero(Tablero tablero)
        {
            char[,] limpiador = {
                {' ',' ',' '},
                {' ',' ',' '},
                {' ',' ',' '},
            };
            for (int i = 0; i < 9; i++)
            {
                Coordenadas coordenadas = ObtenerCoordenadaCasilla("Simbolo", i + 1, tablero);
                for (int j = 0; j < limpiador.GetLength(0); j++)
                {
                    for (int k = 0; k < limpiador.GetLength(1); k++)
                    {
                        Console.SetCursorPosition(coordenadas.x + k, coordenadas.y + j);
                        Console.Write(limpiador[j, k]);
                    }
                }
            }
        }
        public static bool VerificarGanador(char[] movimientos, char ficha, Tablero tablero)
        {
            //Ganador en horizontal
            if ((movimientos[0] == ficha && movimientos[1] == ficha && movimientos[2] == ficha) ||
                (movimientos[3] == ficha && movimientos[4] == ficha && movimientos[5] == ficha) ||
                (movimientos[6] == ficha && movimientos[7] == ficha && movimientos[8] == ficha))
            {
                if (movimientos[0] == ficha && movimientos[1] == ficha && movimientos[2] == ficha)
                {
                    DibujarLineaGanadora(ObtenerCoordenadaCasilla("Numero", 1, tablero), ObtenerCoordenadaCasilla("Numero", 3, tablero));
                }
                else if (movimientos[3] == ficha && movimientos[4] == ficha && movimientos[5] == ficha)
                {
                    DibujarLineaGanadora(ObtenerCoordenadaCasilla("Numero", 4, tablero), ObtenerCoordenadaCasilla("Numero", 6, tablero));
                }
                else if (movimientos[6] == ficha && movimientos[7] == ficha && movimientos[8] == ficha)
                {
                    DibujarLineaGanadora(ObtenerCoordenadaCasilla("Numero", 7, tablero), ObtenerCoordenadaCasilla("Numero", 9, tablero));
                }
                return true;
            }
            //Ganador en vertical
            if ((movimientos[0] == ficha && movimientos[3] == ficha && movimientos[6] == ficha) ||
                (movimientos[1] == ficha && movimientos[4] == ficha && movimientos[7] == ficha) ||
                (movimientos[2] == ficha && movimientos[5] == ficha && movimientos[8] == ficha))
            {
                if (movimientos[0] == ficha && movimientos[3] == ficha && movimientos[6] == ficha)
                {
                    DibujarLineaGanadora(ObtenerCoordenadaCasilla("Simbolo", 1, tablero), ObtenerCoordenadaCasilla("Simbolo", 7, tablero));
                }
                else if (movimientos[1] == ficha && movimientos[4] == ficha && movimientos[7] == ficha)
                {
                    DibujarLineaGanadora(ObtenerCoordenadaCasilla("Simbolo", 2, tablero), ObtenerCoordenadaCasilla("Simbolo", 8, tablero));
                }
                else if (movimientos[2] == ficha && movimientos[5] == ficha && movimientos[8] == ficha)
                {
                    DibujarLineaGanadora(ObtenerCoordenadaCasilla("Simbolo", 3, tablero), ObtenerCoordenadaCasilla("Simbolo", 9, tablero));
                }
                return true;
            }
            //Ganador en diagonal
            if ((movimientos[0] == ficha && movimientos[4] == ficha && movimientos[8] == ficha) ||
                (movimientos[2] == ficha && movimientos[4] == ficha && movimientos[6] == ficha))
            {
                if (movimientos[0] == ficha && movimientos[4] == ficha && movimientos[8] == ficha)
                {
                    DibujarLineaGanadora(ObtenerCoordenadaCasilla("Simbolo", 1, tablero), ObtenerCoordenadaCasilla("Simbolo", 9, tablero));
                }
                else if (movimientos[2] == ficha && movimientos[4] == ficha && movimientos[6] == ficha)
                {
                    DibujarLineaGanadora(ObtenerCoordenadaCasilla("Simbolo", 3, tablero), ObtenerCoordenadaCasilla("Simbolo", 7, tablero));
                }
                return true;
            }
            return false;

        }
        public static void DibujarLineaGanadora(Coordenadas inicio, Coordenadas final)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (inicio.x == final.x)
            {
                for (int i = inicio.y; i < final.y + 3; i++)
                {
                    Console.SetCursorPosition(inicio.x + 1, i);
                    Console.Write("█");
                }
            }
            else if (inicio.y == final.y)
            {
                for (int i = inicio.x - 1; i < final.x + 3; i++)
                {
                    Console.SetCursorPosition(i, inicio.y);
                    Console.Write("█");
                }
            }
            else
            {
                if (inicio.x < final.x)
                {
                    while (inicio.x != final.x + 3 && inicio.y != final.y + 3)
                    {
                        Console.SetCursorPosition(inicio.x, inicio.y);
                        Console.Write("█");
                        inicio.x++;
                        inicio.y++;
                    }
                }
                else
                {
                    inicio.x += 2;
                    while (inicio.x != final.x - 3 && inicio.y != final.y + 3)
                    {
                        Console.SetCursorPosition(inicio.x, inicio.y);
                        Console.Write("█");
                        inicio.x--;
                        inicio.y++;
                    }

                }
            }
            Console.ResetColor();
        }
        public static bool VerificarEmpate(char[] movimientos)
        {
            int casilla = 0;
            foreach (char mov in movimientos)
            {
                if (!char.IsNumber(mov)) casilla++;
            }
            if (casilla >= 9) return true;
            return false;
        }
        public static void PantallaCargarPartida()
        {
            Console.Clear();
        }
        public static string GenerarMenuDeOpciones(string[] opciones, Coordenadas posicionMenu)
        {
            Console.CursorVisible = false;
            int seleccion = 0;
            string simboloDeSeleccion = "->";
            ConsoleKeyInfo tecla;
            do
            {
                for (int i = 0; i < opciones.Length; i++)
                {
                    Console.SetCursorPosition(posicionMenu.x, posicionMenu.y + i);
                    Console.Write($"    {opciones[i]}");
                }
                Console.SetCursorPosition(posicionMenu.x, posicionMenu.y + seleccion);
                Console.Write($"{simboloDeSeleccion}");
                tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.Enter)
                {
                    string borrador = "";
                    for (int i = 0; i < opciones.Length; i++)
                    {
                        Console.SetCursorPosition(posicionMenu.x, posicionMenu.y + i);
                        borrador = "    " + borrador.PadLeft(opciones[i].Length, ' ');
                        Console.Write($"    {borrador}");
                    }
                    Console.SetCursorPosition(posicionMenu.x, posicionMenu.y);
                    Console.CursorVisible = true;
                    return opciones[seleccion];
                }
                else if (tecla.Key == ConsoleKey.UpArrow)
                {
                    if (seleccion <= 0) seleccion = opciones.Length - 1;
                    else seleccion--;
                }
                else if (tecla.Key == ConsoleKey.DownArrow)
                {
                    if (seleccion >= opciones.Length - 1) seleccion = 0;
                    else seleccion++;
                }
            } while (true);
        }
        public static void DibujarPuntuacion(Jugador jugador, Coordenadas posiconPuntuacion)
        {
            Console.SetCursorPosition(posiconPuntuacion.x, posiconPuntuacion.y);
            Console.Write("".PadLeft($"{jugador.Nombre}".Length, ' '));
            Console.SetCursorPosition(posiconPuntuacion.x, posiconPuntuacion.y + 1);
            Console.Write("".PadLeft($"Puntos: {jugador.Puntos}".Length, ' '));
            Console.SetCursorPosition(posiconPuntuacion.x, posiconPuntuacion.y);
            Console.Write($"{jugador.Nombre}");
            Console.SetCursorPosition(posiconPuntuacion.x, posiconPuntuacion.y + 1);
            Console.Write($"Puntos: {jugador.Puntos}");
        }
        public struct Jugador
        {
            public string Nombre;
            public string Ficha;
            public int Puntos;
        }
        public struct Tablero
        {
            public int TamañoCasilla;
            public int TamañoTablero;
            public char Simbolo;
            public Coordenadas Posicion;
        }
        public struct Coordenadas
        {
            public int x;
            public int y;
        }
        public struct Partida
        {
            public Jugador Jugador1;
            public Jugador Jugador2;
            public Jugador Turno;
            public char[] Movimientos;
            public Tablero Tablero;
        }
    }
}