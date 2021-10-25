# Ejercicio 1

## 1. Ninguno de los objetos será físico.

![2021-10-25 16-48-34](https://user-images.githubusercontent.com/72868069/138730254-df4d1090-eeba-4f7c-a7ff-a76b7d7db761.gif)

Ninguno de los objetos cae sobre la rampa porque no tienen físicas.

## 2. La esfera tiene físicas, el cubo no.

![2021-10-25 16-49-26](https://user-images.githubusercontent.com/72868069/138730572-98bb72af-a0fe-478d-9dbb-3d81922d8a66.gif)

La esfera cae y baja por la rampa ya que se le ha añadido el componente RigidBody. El cubo sigue sin tener físicas por tanto se queda arriba.

## 3. La esfera y el cubo tienen físicas.

![2021-10-25 16-49-55](https://user-images.githubusercontent.com/72868069/138730784-32714b55-5b62-488a-81f7-26b8789c2b86.gif)

Al cubo también se le añade el componente RigidBody por lo que también baja por la rampa.

## 4. La esfera y el cubo tienen físicas y la esfera tiene 10 veces la masa del cubo.

![2021-10-25 16-50-58](https://user-images.githubusercontent.com/72868069/138730925-da48fd03-545a-49e1-b4aa-eb41125f4b98.gif)

Se ha aumentado la masa de la esfera de forma que la esfera tiene masa 10 y el cubo se queda con masa 1. He interpuesto el cubo en la trayectoria de la esfera para que se pueda observar como debido a la diferencia de masa, la esfera empuja al cubo.

## 5. La esfera tiene físicas y el cubo es de tipo IsTrigger.

![2021-10-25 16-51-40](https://user-images.githubusercontent.com/72868069/138731104-87b79acb-32ae-447c-97b6-1825d2066c9b.gif)

La esfera baja por la rampa ya que tiene físicas y el cubo, aunque sea IsTrigger (lo cual se activa en la pestaña de Box Collider del Inspector), no lo vemos ya que no tiene físicas.

## 6. La esfera tiene físicas, el cubo es de tipo IsTrigger y tiene físicas.

![2021-10-25 16-52-07](https://user-images.githubusercontent.com/72868069/138731327-b70bd2a7-5e7d-4fa9-a62b-5b09d0da54b5.gif)

Ahora vemos al cubo caer ya que le hemos añadido físicas, pero al ser IsTrigger, atraviesa la rampa y el suelo.

## 7. La esfera y el cubo tienen físicas y la esfera tiene 10 veces la masa del cubo, se impide la rotación del cubo sobre el plano XZ.

![2021-10-25 16-53-14](https://user-images.githubusercontent.com/72868069/138731474-a270d066-4daf-40e3-8524-7c38de79ecc0.gif)

En esta ocasión he interpuesto nuevamente el cubo en la trayectoria de la esfera para que se pueda observar como debido a la diferencia de masa, la esfera empuja al cubo. Esta vez, el cubo no baja la rampa ya que le hemos eliminado la rotación en los ejes X y Z.
