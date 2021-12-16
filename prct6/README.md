# Práctica 6: Interfaces modales

La escena implementada consiste en un escenario basado en un videojuego de Pokémon en el que el jugador es un Pikachu que se puede controlar gracias a la brújula y el acelerómetro del móvil.

El funcionamiento de los Scripts no require demasiado entendimiento ya que es tal cual el código que facilita la documentación de Unity. El mayor detalle a tener en cuenta es que al estar usando en mi caso Unity Remote 5 y no apk, el código empieza a ejecutarse antes de llegar al móvil, por lo que al realizar la lectura de location, aun no tenemos los permisos para ello, por lo que en el Awake se realiza una pequeña espera para que así dé tiempo a que ambos dispositivos se comuniquen con el fin de obtener la información de la ubicación.

[Repositorio Github](https://github.com/alu0101224084/interfaces-inteligentes/tree/main/prct6)
