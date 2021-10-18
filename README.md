# Introducción a Unity

Ángel Tornero Hernández - alu0101224084

## Descripción

La escena que he creado ha consistido en recrear Pueblo Paleta del juego Pokémon Rojo Fuego, utilizando para ello objetos 3D básicos de Unity (a excepción de los árboles que son sacados de la Asset Store). Hay una persona en el pueblo (Ethan, sacado de Standard Assets) al lado de un Pikachu hecho también con objetos 3D básicos. La luz viene de dos direcciones diferentes, de diferente intensidad. También he añadido etiquetas a cada objeto para identificarlos (edificios, suelo, entorno, etc). Además, he añadido un FPS de los prefab de Standard Assets para andar por la escena en primera persona.

![Pueblo_Paleta_RFVH](https://user-images.githubusercontent.com/72868069/137785074-99330cd8-d075-4e21-be60-90211f1b2c9a.png)


## Script

En cuanto al script, al agregarse a un GameObject, este le asigna un id (que podremos cambiar desde el Inspector) y le crea un contador, el cual se incrementa en cada frame y lo imprime. Al iniciar la escena, los scripts muestran en la consola el nombre de los GameObject y su id.
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int counter;
    public int id;

    void Start()
    {
        Debug.Log(gameObject.name);
        Debug.Log(id);
    }

    void Update()
    {   
        counter++;
        Debug.Log(counter);
    }
}

```

## Gif

![Vídeo nuevo](https://user-images.githubusercontent.com/72868069/137784817-f720a91f-5812-408c-86d1-56e8fde251df.gif)
